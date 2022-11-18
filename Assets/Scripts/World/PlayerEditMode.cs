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

    private IList<PlaceableObjectClass> placeableObjects = new List<PlaceableObjectClass>();
    private PlaceableObjectClass currentPlaceableObjectSO;
    private ItemClass currentItemClass;
    private SlotClass slot;

    // Using this instead of update so that it can be disabled in the Player Script
    public void UpdateCall()
    {
        //Ensures that the player has an object in their hand when switching to edit mode
        //if (!loadedObjects)
        //{
        //    currentPlaceableObjectSO = Resources.LoadAll<PlaceableObjectClass>("Data/PlaceableObjects")[0];
        //    loadedObjects = true;
        //}

        if (currentPlaceableObjectSO != null)
        {
            if (heldObject == null)
            {
                if (slot.GetQuantity() > 0)
                {
                    heldObject = Instantiate(placeableObjectPrefab);
                    heldObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

                    heldObject.transform.position = MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(false) + new Vector3Int(1, 1, 0));
                    Vector3Int indexPosition = MapInformation.GetTileIndex(MapInformation.groundTileMap.CellToWorld(TileSelect.GetTileUnderMouse(true)));
                    UpdateObject();
                    SetObjectColor(indexPosition);
                    heldObject.SetActive(true);
                }
                else
                {
                    currentPlaceableObjectSO = null;
                    currentItemClass = null;
                    slot = null;
                }
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
                    spr.sprite = Sprite.Create(currentPlaceableObjectSO.sprite.texture, currentPlaceableObjectSO.sprite.rect, new Vector2(0.5f, height * ((currentPlaceableObjectSO.sprite.pixelsPerUnit / 2) / (currentPlaceableObjectSO.sprite.bounds.size.y * currentPlaceableObjectSO.sprite.pixelsPerUnit))), currentPlaceableObjectSO.sprite.pixelsPerUnit);
                }
                SetObjectColor(indexPosition);

                //Make sure mouse isn't over any ui
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //try place object
                        heldObject.GetComponent<PlaceableObject>().itemClass = currentItemClass;
                        if (heldObject.GetComponent<PlaceableObject>().PlaceObject())
                        {
                            heldObject.GetComponent<SpriteRenderer>().color = Color.white;
                            heldObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            heldObject = null;

                            MapData.SaveMap();
                        }
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
        if (Input.GetMouseButtonDown(1))
        {
            Vector3Int indexPosition = MapInformation.GetTileIndex(TileSelect.GetTileUnderMouse(false));
            if (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null)
            {
                GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<NurseryShopManager>().Add(MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null ? MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO : MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO, 1);
                Destroy(MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null ? MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile : MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile);
                if (MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null)
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile = null;
                else
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile = null;

                MapInformation.groundMap[indexPosition.x, indexPosition.y].walkable = true;
                MapData.SaveMap();
            }
        }
    }

    /// <summary>
    /// Sets the object to be placed to whatever is attached to the button pressed
    /// </summary>
    /// <param name="buttonObject"></param>
    public void SetObject(GameObject buttonObject, SlotClass slot)
    {
        if(buttonObject != null)
            currentPlaceableObjectSO = buttonObject.GetComponent<ObjectSelectButton>().placeableObjectSO;
        if (slot != null)
        {
            this.slot = slot;
            currentItemClass = slot.GetItem();

        }
        if(heldObject != null)
            UpdateObject();
    }

    /// <summary>
    /// A quick way to refresh all object properties after a change has been made
    /// </summary>
    private void UpdateObject()
    {
        heldObject.GetComponent<PlaceableObject>().FlipObject(flipped);
        heldObject.GetComponent<PlaceableObject>().SetComponents(currentPlaceableObjectSO);
        heldObject.GetComponent<PlaceableObject>().itemClass = currentItemClass;
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
