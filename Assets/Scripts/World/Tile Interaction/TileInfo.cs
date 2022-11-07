using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores information about each grid tile
/// </summary>
public class TileInfo
{
    public Vector3Int position;
    public bool walkable = true;
    public TileType tileType = TileType.Furniture;

    public TileInfo(Vector3Int position, bool walkable)
    {
        this.position = position;
        this.walkable = walkable;
    }

    public override string ToString()
    {
        return position + ":" + walkable;
    }
}