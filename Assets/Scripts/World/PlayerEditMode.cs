using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerEditMode : MonoBehaviour
{
    public GameObject placeableObjectPrefab;
    private GameObject heldObject;

    private bool loadedObjects = false;

    private int selectedObject = 0;
    private bool objectSwap = false;

    private bool flipped = false;

    private IList<PlaceableObjectSO> placeableObjects = new List<PlaceableObjectSO>();
    private PlaceableObjectSO currentPlaceableObjectSO;

    public void UpdateCall()
    {
        if (!loadedObjects)
        {
            placeableObjects = Resources.LoadAll<PlaceableObjectSO>("Data/PlaceableObjects");
            currentPlaceableObjectSO = placeableObjects[0];
            loadedObjects = true;
        }

        if (heldObject == null)
        {
            heldObject = Instantiate(placeableObjectPrefab);
            heldObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            
            heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(false) + new Vector3Int(1, 1, 0));
            Vector3Int indexPosition = MapInformation.GetTileIndex(MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(true)));
            UpdateObject();
            SetObjectColor(indexPosition);
            heldObject.SetActive(true);
        }
        else if (heldObject != null)
        {
            heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(false) + new Vector3Int(1, 1, 0));

            Vector3Int indexPosition = MapInformation.GetTileIndex(MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(currentPlaceableObjectSO.type != TileType.DeskItem)));

            //Set height
            if (currentPlaceableObjectSO.type == TileType.DeskItem)
            {
                Debug.Log(currentPlaceableObjectSO.sprite.pixelsPerUnit);
                float height = MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null ? 1f : (float)(MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.pixelHeight) / 64;
                SpriteRenderer spr = heldObject.GetComponent<SpriteRenderer>();
                spr.sprite = Sprite.Create(currentPlaceableObjectSO.sprite.texture, currentPlaceableObjectSO.sprite.rect, new Vector2(0.5f, height * ((currentPlaceableObjectSO.sprite.pixelsPerUnit/2) / (currentPlaceableObjectSO.sprite.bounds.size.y * currentPlaceableObjectSO.sprite.pixelsPerUnit))), currentPlaceableObjectSO.sprite.pixelsPerUnit);
            }
            SetObjectColor(indexPosition);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
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
                    //Change index position to mouse position instead of objects
                    indexPosition = MapInformation.GetTileIndex(TileSelect.GetTileUnderMouse(false));
                    Destroy(MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null ? MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile : MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile);
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].walkable = true;
                }

                //Flip object
                if (Input.GetKeyDown(KeyCode.R))
                {
                    flipped = !flipped;
                    UpdateObject();
                }
            }
        }
    }

    public void SetObject(GameObject buttonObject = null)
    {
        currentPlaceableObjectSO = buttonObject.GetComponent<ObjectSelectButton>().placeableObjectSO;
        UpdateObject();
    }

    private void UpdateObject()
    {
        heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
        heldObject.GetComponent<PlaceableObject>().SetComponents(currentPlaceableObjectSO);
    }

    private void SetObjectColor(Vector3Int indexPosition)
    {
        Color color = Color.white;
        if (MapInformation.groundMap[indexPosition.x, indexPosition.y].mask != Mask.Empty)
        {
            //if (currentPlaceableObjectSO.type != TileType.DeskItem && (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null || MapInformation.groundMap[indexPosition.x, indexPosition.y].mask != currentPlaceableObjectSO.placementMask))
            //    color = Color.red;
            if (currentPlaceableObjectSO.placementMask != MapInformation.groundMap[indexPosition.x, indexPosition.y].mask)
            {
                color = Color.red;
            }
            if (currentPlaceableObjectSO.type != TileType.DeskItem && MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null)
            {
                color = Color.red;
            }
            if (currentPlaceableObjectSO.type == TileType.DeskItem && ((MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null && !MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.supportsDeskItems) || MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile != null))
            {
                color = Color.red;
            }
        }
        else
        {
            if (currentPlaceableObjectSO.type != TileType.DeskItem && MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null)
            {
                color = Color.red;
            }
            if (currentPlaceableObjectSO.type == TileType.DeskItem && ((MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null && !MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.supportsDeskItems) || MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile != null))
            {
                color = Color.red;
            }
        }
        color.a = 0.5f;

        heldObject.GetComponent<SpriteRenderer>().color = color;
    }
}
