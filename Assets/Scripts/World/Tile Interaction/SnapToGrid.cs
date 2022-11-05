using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnapToGrid : MonoBehaviour
{
    public bool snapped = false;
    void Start()
    {
        Snap();
    }

    public void Snap()
    {
        Tilemap groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>(); ;
        transform.position = groundTileMap.CellToWorld(groundTileMap.WorldToCell(transform.position + new Vector3(0, transform.localScale.y / 2, 0)));
        snapped = true;
    }
}
