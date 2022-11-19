using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInformation : MonoBehaviour
{
    public static TileInfo[,] groundMap;
    public static BoundsInt groundMapBounds;

    public static Tilemap groundTileMap;
    public static Tilemap objectTileMap;
    public static Tilemap overlayTileMap;
    public static Tilemap maskTileMap;

    public static IList<TileInfo> chairs = new List<TileInfo>();
    public static IList<TileInfo> furniture = new List<TileInfo>();
    public static IList<TileInfo> interactables = new List<TileInfo>();

    //public static MapData mapData = new MapData();

    private void Awake()
    {
        RefreshMap();
    }

    private void Update()
    {

    }

    /// <summary>
    /// Updates the array of game tiles used by other scripts
    /// </summary>
    public static void RefreshMap()
    {
        groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        objectTileMap = GameObject.FindGameObjectWithTag("ObjectTileMap").GetComponent<Tilemap>();
        overlayTileMap = GameObject.FindGameObjectWithTag("OverlayTileMap").GetComponent<Tilemap>();
        maskTileMap = GameObject.FindGameObjectWithTag("MaskTileMap").GetComponent<Tilemap>();

        groundMapBounds = groundTileMap.cellBounds;
        groundMap = new TileInfo[groundMapBounds.max.x - groundMapBounds.min.x * 2, groundMapBounds.max.y - groundMapBounds.min.y * 2];

        for (int y = groundMapBounds.max.y; y > groundMapBounds.min.y; y--)
        {
            for (int x = groundMapBounds.max.y; x > groundMapBounds.min.x; x--)
            {
                Vector3Int tileLocation = new Vector3Int(x - 1, y - 1, 0);
                groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))] = new TileInfo(tileLocation, true);
                if (groundTileMap.HasTile(tileLocation))
                {
                    if(objectTileMap.HasTile(tileLocation))
                    {
                        groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))].walkable = false;
                    }
                    if (maskTileMap.HasTile(tileLocation))
                    {
                        string tileName = maskTileMap.GetTile(tileLocation).name;
                        switch (tileName)
                        {
                            case "CustomerMask":
                                groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))].mask = Mask.Customer;
                                break;
                            case "KitchenMask":
                                groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))].mask = Mask.Kitchen;
                                break;
                        }
                    }
                }
                else
                {
                    groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))].walkable = false;
                }
            }
        }
    }

    /// <summary>
    /// Converts the world position to the appropriate x and y index for use in the tilemap arrays
    /// </summary>
    /// <param name="worldPosition">Normal world position</param>
    /// <returns>A Vector3Int with the x and y index positions</returns>
    public static Vector3Int GetTileIndex(Vector3 worldPosition)
    {
        Vector3Int tilePos = groundTileMap.WorldToCell(worldPosition);

        return new Vector3Int(((tilePos.x) + (-groundMapBounds.min.x)), ((tilePos.y) + (-groundMapBounds.min.y)), 0);
    }

    /// <summary>
    /// Converts the cell position to the appropriate x and y index for use in the tilemap arrays
    /// </summary>
    /// <param name="cellPosition">Grid cell position</param>
    /// <returns>A Vector3Int with the x and y index positions</returns>
    public static Vector3Int GetTileIndex(Vector3Int cellPosition)
    {
        Vector3Int tilePos = cellPosition;

        return new Vector3Int(((tilePos.x) + (-groundMapBounds.min.x)), ((tilePos.y) + (-groundMapBounds.min.y)), 0);
    }

    /// <summary>
    /// Sets the passed in cell position's walkability
    /// </summary>
    /// <param name="tilePos"></param>
    /// <param name="walkable"></param>
    public static void SetTileWalkability(Vector3Int tilePos, bool walkable)
    {
        Vector3Int tileIndex = GetTileIndex(tilePos);

        if (tileIndex.x > 0 && tileIndex.x < groundMapBounds.max.x - groundMapBounds.min.x * 2
            && tileIndex.y > 0 && tileIndex.y < groundMapBounds.max.y - groundMapBounds.min.y * 2)
        {
            groundMap[tileIndex.x, tileIndex.y].walkable = walkable;
        }
    }

    /// <summary>
    /// Sets a tile's type to the passed in type and adds it to the appropriate list
    /// </summary>
    /// <param name="tilePos"></param>
    /// <param name="tileType"></param>
    public static void SetTileType(Vector3Int tilePos, TileType tileType)
    {
        Vector3Int tileIndex = GetTileIndex(tilePos);

        if (tileIndex.x > 0 && tileIndex.x < groundMapBounds.max.x - groundMapBounds.min.x * 2
            && tileIndex.y > 0 && tileIndex.y < groundMapBounds.max.y - groundMapBounds.min.y * 2)
        {
            if (tileType != groundMap[tileIndex.x, tileIndex.y].tileType)
            {
                RemoveTileFromInfoList(groundMap[tileIndex.x, tileIndex.y]);
                groundMap[tileIndex.x, tileIndex.y].tileType = tileType;
                AddTileToInfoList(groundMap[tileIndex.x, tileIndex.y]);
            }
        }
    }

    /// <summary>
    /// Grabs the list of objects matching the passed in TileType
    /// </summary>
    /// <param name="tileType"></param>
    /// <returns>A List of objects matching the passed in TileType</returns>
    public static IList<TileInfo> GetTileTypeList(TileType tileType)
    { 
        IList<TileInfo> tL = chairs;
        switch (tileType)
        {
            case TileType.Furniture:
                tL = furniture;
                break;
            case TileType.Interactable:
                tL = interactables;
                break;
        }
        return tL;
    }

    private static void AddTileToInfoList(TileInfo tile)
    {
        switch (tile.tileType)
        {
            case TileType.Seat:
                chairs.Add(tile);
                break;
            case TileType.Furniture:
                furniture.Add(tile);
                break;
            case TileType.Interactable:
                interactables.Add(tile);
                break;
        }
    }

    private static void RemoveTileFromInfoList(TileInfo tile)
    {
        switch (tile.tileType)
        {
            case TileType.Seat:
                chairs.Remove(tile);
                break;
            case TileType.Furniture:
                furniture.Remove(tile);
                break;
            case TileType.Interactable:
                interactables.Remove(tile);
                break;
        }
    }

    public static void UpdateMapData()
    {
        //mapData.UpdateMapData();
    }
}
