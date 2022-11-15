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

    private bool flipped = false;

    private IList<PlaceableObjectSO> placeableObjects = new List<PlaceableObjectSO>();
    private PlaceableObjectSO currentPlaceableObjectSO;

    // Using this instead of update so that it can be disabled in the Player Script
    public void UpdateCall()
    {
        //Ensures that the player has an object in their hand when switching to edit mode
        if (!loadedObjects)
        {
            currentPlaceableObjectSO = Resources.LoadAll<PlaceableObjectSO>("Data/PlaceableObjects")[0];
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

            //Set the "height" of an object so it appears to be on top of something else
            if (currentPlaceableObjectSO.type == TileType.DeskItem)
            {
                float height = MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null ? 1f : (float)(MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.pixelHeight) / 64;
                SpriteRenderer spr = heldObject.GetComponent<SpriteRenderer>();
                spr.sprite = Sprite.Create(currentPlaceableObjectSO.sprite.texture, currentPlaceableObjectSO.sprite.rect, new Vector2(0.5f, height * ((currentPlaceableObjectSO.sprite.pixelsPerUnit/2) / (currentPlaceableObjectSO.sprite.bounds.size.y * currentPlaceableObjectSO.sprite.pixelsPerUnit))), currentPlaceableObjectSO.sprite.pixelsPerUnit);
            }
            SetObjectColor(indexPosition);

            //Make sure mouse isn't over any ui
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

    /// <summary>
    /// Sets the object to be placed to whatever is attached to the button pressed
    /// </summary>
    /// <param name="buttonObject"></param>
    public void SetObject(GameObject buttonObject = null)
    {
        currentPlaceableObjectSO = buttonObject.GetComponent<ObjectSelectButton>().placeableObjectSO;
        UpdateObject();
    }

    /// <summary>
    /// A quick way to refresh all object properties after a change has been made
    /// </summary>
    private void UpdateObject()
    {
        heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
        heldObject.GetComponent<PlaceableObject>().SetComponents(currentPlaceableObjectSO);
    }

    /// <summary>
    /// Contains the checks to see whether an object should be turned red to indicate it's in an unplaceable position
    /// </summary>
    /// <param name="indexPosition"></param>
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
