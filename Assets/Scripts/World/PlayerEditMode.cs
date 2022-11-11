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

    private bool flipped = false;

    private IList<PlaceableObjectSO> placeableObjects = new List<PlaceableObjectSO>();
    public void UpdateCall()
    {
        if (!loadedObjects)
        {
            placeableObjects = Resources.LoadAll<PlaceableObjectSO>("Data/PlaceableObjects");
        }
        Vector3Int debug = MapInformation.GetTileIndex(TileSelect.GetTileUnderMouse(false));

        //Object selection
        // Controls should be changed later on
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
            heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
            heldObject.GetComponent<PlaceableObject>().SetComponents(placeableObjects[selectedObject]);
            objectSwap = false;
        }
            

        if (heldObject == null)
        {
            heldObject = Instantiate(placeableObjectPrefab);
            heldObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
            heldObject.GetComponent<PlaceableObject>().SetComponents(placeableObjects[selectedObject]);
        }
        else if (heldObject != null)
        {
            heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(true) + new Vector3Int(1, 1, 0));
            Vector3Int indexPosition = MapInformation.GetTileIndex(MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(true)));


            Color heldObjectColor = Color.white;
            if (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null || MapInformation.groundMap[indexPosition.x, indexPosition.y].mask != placeableObjects[selectedObject].placementMask)
                heldObjectColor = Color.red;
            else
                heldObjectColor = Color.white;
            heldObjectColor.a = 0.5f;

            heldObject.GetComponent<SpriteRenderer>().color = heldObjectColor;

            if (Input.GetMouseButtonDown(0))
            {
                //try place object
                if (heldObject.GetComponent<PlaceableObject>().PlaceObject())
                {
                    heldObject.GetComponent<SpriteRenderer>().color = Color.white;
                    heldObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    heldObject = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //TODO: Placed Object Deletion, getting there

                //Change index position to mouse position instead of objects
                indexPosition = MapInformation.GetTileIndex(TileSelect.GetTileUnderMouse(false));
                Destroy(MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile);
                MapInformation.groundMap[indexPosition.x, indexPosition.y].walkable = true;
            }
            
            //Flip object
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                flipped = !flipped;
                heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
                heldObject.GetComponent<PlaceableObject>().SetComponents(placeableObjects[selectedObject]);
            }
        }
    }
}
