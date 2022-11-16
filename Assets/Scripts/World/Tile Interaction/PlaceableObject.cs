using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public bool placed = false;
    public bool placeOnStart = false;

    public PlaceableObjectClass placeableObjectSO;

    private TileType tileType = TileType.Furniture;

    private Tilemap groundTileMap;

    public bool flipped = false;

    public GameObject placeableObjectPrefab;

    /// <summary>
    /// Places an object where it is at currently
    /// </summary>
    /// <returns>Whether the placement was successful or not</returns>
    public bool PlaceObject()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();

        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0));
        Vector3Int indexPosition = MapInformation.GetTileIndex(gridPosition);
        if (MapInformation.groundMap[indexPosition.x, indexPosition.y].mask == placeableObjectSO.placementMask || MapInformation.groundMap[indexPosition.x, indexPosition.y].mask == Mask.Empty)
        {
            if (placeableObjectSO.type != TileType.DeskItem ? MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null : (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null || (MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null && MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.supportsDeskItems)))
            {
                placed = true;
                if (tileType == TileType.Seat || tileType == TileType.Empty)
                {
                    MapInformation.SetTileWalkability(gridPosition, true);
                }
                else
                {
                    MapInformation.SetTileWalkability(gridPosition, false);
                }


                if (placeableObjectSO.type != TileType.DeskItem)
                {
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile = gameObject;
                }
                else if (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile != null && MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.supportsDeskItems && MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile == null)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 2);
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].deskObjectOnTile = gameObject;
                }
                else if (MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile = gameObject;
                }

                MapInformation.SetTileType(gridPosition, tileType);
            }
        }
        return placed;
    }

    /// <summary>
    /// Sets all the components of an object depending on whether it is an appliance or not
    /// </summary>
    /// <param name="placeableObjectSO"></param>
    public void SetComponents(PlaceableObjectClass placeableObjectSO)
    {
        WorldAppliance worldAppliance;
        this.placeableObjectSO = placeableObjectSO;
        tileType = placeableObjectSO.type;

        gameObject.GetComponent<SpriteRenderer>().sprite = placeableObjectSO.sprite;

        if (placeableObjectSO.type == TileType.Interactable)
        {
            //If the object is interactable add components needed to be interactable
            if (gameObject.GetComponent<Rigidbody2D>() == null)
            {
                gameObject.AddComponent<Rigidbody2D>();
                Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
                rb2d.bodyType = RigidbodyType2D.Static;
            }
            if (gameObject.GetComponent<CircleCollider2D>() == null)
            {
                gameObject.AddComponent<CircleCollider2D>();
                CircleCollider2D cc2d = gameObject.GetComponent<CircleCollider2D>();
                cc2d.radius = 0.41f;
            }
            if (gameObject.GetComponent<WorldAppliance>() == null)
            {
                gameObject.AddComponent<WorldAppliance>();
                worldAppliance = gameObject.GetComponent<WorldAppliance>();
            }

            worldAppliance = gameObject.GetComponent<WorldAppliance>();
            worldAppliance.applianceSO = placeableObjectSO.applianceSOIfApplicable;
            if (!flipped)
            {
                worldAppliance.usePositionOffset = placeableObjectSO.usePositionOffsetIfApplicable;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                worldAppliance.usePositionOffset = new Vector3Int(placeableObjectSO.usePositionOffsetIfApplicable.y, placeableObjectSO.usePositionOffsetIfApplicable.x, placeableObjectSO.usePositionOffsetIfApplicable.z);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Destroy(gameObject.GetComponent<Rigidbody2D>());
            }
            if (gameObject.GetComponent<CircleCollider2D>() != null)
            {
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
            if (gameObject.GetComponent<WorldAppliance>() != null)
            {
                Destroy(gameObject.GetComponent<WorldAppliance>());
            }
        }
    }

    /// <summary>
    /// Counterintuitively this doesn't flip an object when called but takes in a bool to say which way it is flipped to
    /// </summary>
    /// <param name="flipped"></param>
    public void FlipObject(bool flipped)
    {
        WorldAppliance worldAppliance = gameObject.GetComponent<WorldAppliance>();
        this.flipped = flipped;
        if (!flipped)
        {
            if(tileType == TileType.Interactable)
                worldAppliance.usePositionOffset = placeableObjectSO.usePositionOffsetIfApplicable;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            if (tileType == TileType.Interactable)
                worldAppliance.usePositionOffset = new Vector3Int(placeableObjectSO.usePositionOffsetIfApplicable.y, placeableObjectSO.usePositionOffsetIfApplicable.x, placeableObjectSO.usePositionOffsetIfApplicable.z);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void Update()
    {
        if (placeOnStart && !placed)
        {
            SnapToGrid.Snap(transform);
            SetComponents(placeableObjectSO);
            PlaceObject();
        }
        if (placed == false && Player.controlState != ControlState.Edit)
        {
            Destroy(gameObject);
        }
    }
}

public enum TileType { Furniture, Interactable, Seat, DeskItem, Empty }