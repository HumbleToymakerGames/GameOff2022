using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public bool placed = false;
    public bool placeOnStart = false;

    public PlaceableObjectSO placeableObjectSO;

    private TileType tileType = TileType.Furniture;

    private Tilemap groundTileMap;


    /// <summary>
    /// Places an object where it is at currently
    /// </summary>
    /// <returns>Whether the placement was successful </returns>
    public bool PlaceObject()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();

        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0));
        Vector3Int indexPosition = MapInformation.GetTileIndex(gridPosition);
        if (MapInformation.groundMap[indexPosition.x, indexPosition.y].walkable)
        {
            placed = true;
            if (tileType == TileType.Seat || tileType == TileType.Empty)
                MapInformation.SetTileWalkability(gridPosition, true);
            else
                MapInformation.SetTileWalkability(gridPosition, false);

            MapInformation.SetTileType(gridPosition, tileType);
        }

        return placed;
    }

    /// <summary>
    /// For placing an object at a specific grid position
    /// </summary>
    /// <param name="gridPos"></param>
    public void PlaceObject(Vector3Int gridPos)
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        transform.position = groundTileMap.CellToWorld(gridPos) - new Vector3(0, transform.localScale.y / 2, 0);
        placed = true;


        if (tileType == TileType.Seat || tileType == TileType.Empty)
            MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), true);
        else
            MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), false);

        MapInformation.SetTileType(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), tileType);
    }

    public void Update()
    {
        if (placeOnStart && !placed)
        {
            SnapToGrid.Snap(transform);
            PlaceObject();
        }
        if (placed == false && Player.controlState != ControlState.Edit)
        {
            Destroy(gameObject);
        }
    }
}

public enum TileType { Furniture, Interactable, Seat, Empty }