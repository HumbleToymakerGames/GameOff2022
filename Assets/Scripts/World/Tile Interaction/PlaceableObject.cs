using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public bool placed = false;
    public bool placeOnStart = false;

    public TileType tileType = TileType.Furniture;

    private Tilemap groundTileMap;

    public void PlaceObject()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        placed = true;
        if (tileType == TileType.Seat)
            MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), true);
        else
            MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), false);

        MapInformation.SetTileType(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), tileType);
    }
    public void PlaceObject(Vector3Int gridPos)
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        transform.position = groundTileMap.CellToWorld(gridPos) - new Vector3(0, transform.localScale.y / 2, 0);
        placed = true;
        

        if(tileType == TileType.Seat)
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

public enum TileType { Furniture, Interactable, Seat }