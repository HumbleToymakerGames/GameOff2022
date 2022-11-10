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
    public TileType tileType = TileType.Empty;
    public bool taken = false;

    public GameObject gameObjectOnTile;

    public Mask mask = Mask.Empty;

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

public enum Mask { Customer, Kitchen, Empty }
