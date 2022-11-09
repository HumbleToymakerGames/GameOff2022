using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEditMode : MonoBehaviour
{
    public GameObject placeableObjectPrefab;
    private GameObject heldObject;

    private bool loadedObjects = false;

    private int selectedObject = 0;
    private bool objectSwap = false;

    private IList<PlaceableObjectSO> placeableObjects = new List<PlaceableObjectSO>();
    public void UpdateCall()
    {
        if (!loadedObjects)
        {
            placeableObjects = Resources.LoadAll<PlaceableObjectSO>("Data/PlaceableObjects");
        }

        //Object selection
        // Controls will most likely be changed later on
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedObject++;
            objectSwap = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedObject--;
            objectSwap = true;
        }

        if (selectedObject >= placeableObjects.Count)
            selectedObject = 0;
        else if (selectedObject < 0)
            selectedObject = placeableObjects.Count - 1;

        if (objectSwap)
        {
            SetComponents();
            objectSwap = false;
        }
            

        if (heldObject == null)
        {
            heldObject = Instantiate(placeableObjectPrefab);
            SetComponents();
        }
        else if (heldObject != null)
        {
            heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(true) + new Vector3Int(1, 1, 0));
            Vector3Int indexPosition = MapInformation.GetTileIndex(MapInformation.groundTileMap.WorldToCell(heldObject.transform.position));
            if (Input.GetMouseButtonDown(0))
            {
                //try place object
                if (heldObject.GetComponent<PlaceableObject>().PlaceObject())
                {
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile = heldObject;
                    MapInformation.RefreshMap();
                    heldObject = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //TODO: Placed Object Deletion
                //Change index position to mouse position instead of objects
                indexPosition = MapInformation.GetTileIndex(TileSelect.GetTileUnderMouse(false) + new Vector3Int(1, 1, 0));
                Destroy(MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile);
                MapInformation.groundMap[indexPosition.x, indexPosition.y].walkable = true;
                MapInformation.RefreshMap();
            }
        }
    }

    private void SetComponents()
    {
        WorldAppliance worldAppliance;
        heldObject.GetComponent<PlaceableObject>().placeableObjectSO = placeableObjects[selectedObject];
        heldObject.GetComponent<SpriteRenderer>().sprite = placeableObjects[selectedObject].sprite;

        if (placeableObjects[selectedObject].type == TileType.Interactable)
        {
            //If the object is interactable add components needed to be interactable
            if (heldObject.GetComponent<Rigidbody2D>() == null)
            {
                heldObject.AddComponent<Rigidbody2D>();
                Rigidbody2D rb2d = heldObject.GetComponent<Rigidbody2D>();
                rb2d.bodyType = RigidbodyType2D.Static;
            }
            if (heldObject.GetComponent<CircleCollider2D>() == null)
            {
                heldObject.AddComponent<CircleCollider2D>();
                CircleCollider2D cc2d = heldObject.GetComponent<CircleCollider2D>();
                cc2d.radius = 0.41f;
            }
            if (heldObject.GetComponent<WorldAppliance>() == null)
            {
                heldObject.AddComponent<WorldAppliance>();
                worldAppliance = heldObject.GetComponent<WorldAppliance>();
            }

            worldAppliance = heldObject.GetComponent<WorldAppliance>();
            worldAppliance.applianceSO = placeableObjects[selectedObject].applianceSOIfApplicable;
            worldAppliance.usePositionOffset = placeableObjects[selectedObject].usePositionOffsetIfApplicable;
        }
        else
        {
            if (heldObject.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(heldObject.GetComponent<Rigidbody2D>());
            }
            if (heldObject.GetComponent<CircleCollider2D>() != null)
            {
                Destroy(heldObject.GetComponent<CircleCollider2D>());
            }
            if (heldObject.GetComponent<WorldAppliance>() != null)
            {
                Destroy(heldObject.GetComponent<WorldAppliance>());
            }
        }
    }
}
