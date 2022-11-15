using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SnapToGrid : MonoBehaviour
{
    /// <summary>
    /// Snaps object to the closest grid position based on the bottom center of the object
    /// </summary>
    /// <param name="transform"></param>
    public static void Snap(Transform transform)
    {
        Tilemap groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>(); ;
        transform.position = groundTileMap.CellToWorld(groundTileMap.WorldToCell(transform.position + new Vector3(0, transform.localScale.y / 2, 0)));
    }
}
