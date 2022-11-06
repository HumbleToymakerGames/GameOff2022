using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public bool placed = false;
    public bool placeOnStart = false;

    private Tilemap groundTileMap;

    public void PlaceObject()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        placed = true;
        MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), false);
    }
    public void PlaceObject(Vector3Int gridPos)
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        transform.position = groundTileMap.CellToWorld(gridPos) - new Vector3(0, transform.localScale.y / 2, 0);
        placed = true;
        MapInformation.SetTileWalkability(groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)), false);
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
