using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelect : MonoBehaviour
{
    private static Tilemap tileMap;
    private static TileBase highlightTile;
    private static TileBase tileSearch;

    private static Vector3Int selectedTilePosition;
    private static Vector3Int oldTileMapPosition;

    /// <summary>
    /// Gets the floor tile that is currently under the cursor
    /// </summary>
    /// <returns>The tiles tilemap position</returns>
    public static Vector3Int GetTileUnderMouse(bool requiredWalkable)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get tile clicked
        Vector3Int tileMapPosition = MapInformation.groundTileMap.WorldToCell(mousePos);
        if (MapInformation.groundTileMap.HasTile(tileMapPosition))
        {
            if (requiredWalkable && MapInformation.groundMap[tileMapPosition.x - (int)MapInformation.groundMapBounds.min.x, tileMapPosition.y - (int)MapInformation.groundMapBounds.min.y].walkable)
                selectedTilePosition = tileMapPosition;
            else if (!requiredWalkable)
                selectedTilePosition = tileMapPosition;
        }

        return selectedTilePosition;
    }

    /// <summary>
    /// Gets a random tiles position on the map that can be pathed to
    /// </summary>
    /// <returns>The tiles position on the tilemap</returns>
    public static TileInfo SelectRandomTile(Mask mask)
    {
        // Still not great or perfomant but just need it to work for now
        TileInfo randomTile = new TileInfo(new Vector3Int(2, -0, 2), true);
        bool validTile = false;

        //this is to prevent freezing the game if a mask isn't valid
        int allowedAttempts = 100;
        while (!validTile)
        {
            randomTile = new TileInfo(new Vector3Int((int)Random.Range(MapInformation.groundMapBounds.xMin, MapInformation.groundMapBounds.xMax), (int)Random.Range(MapInformation.groundMapBounds.yMin, MapInformation.groundMapBounds.yMax), 0), true);
            if (MapInformation.groundTileMap.HasTile(randomTile.position) && randomTile.mask == mask && randomTile.walkable)
            {
                validTile = true;
            }
            allowedAttempts--;
            if (allowedAttempts <= 0)
                break;
        }
        return randomTile;
    }

    /// <summary>
    /// Finds a tile that matches the passed in tileType. Untested in a while and unsure if it works properly now. TODO if we end up needing it
    /// </summary>
    /// <param name="tileType"></param>
    /// <returns></returns>
    public static TileInfo FindTileOfType(TileType tileType)
    {
        TileInfo tile = new TileInfo(new Vector3Int(-99999, -99999, -99999), true);
        IList<TileInfo> tileList = MapInformation.GetTileTypeList(tileType);
        if(tileList.Count > 0)
            tile = tileList[Random.Range(0, tileList.Count)];

        return tile;
    }

    /// <summary>
    /// Applies the highlight overlay at the tilemap position that is passed in. Clears all other highlights when called.
    /// </summary>
    /// <param name="tileMapPosition"></param>
    public static void HighlightTile(Vector3Int tileMapPosition)
    {
        if (highlightTile == null)
        {
            highlightTile = Resources.Load<TileBase>("Tilesets/Assets/Highlight");
        }

        ClearHighlight();
        MapInformation.overlayTileMap.SetTileFlags(tileMapPosition, TileFlags.None);
        MapInformation.overlayTileMap.SetTile(tileMapPosition, highlightTile);
    }

    /// <summary>
    /// Clears all highlights
    /// </summary>
    public static void ClearHighlight()
    {
        MapInformation.overlayTileMap.ClearAllTiles();
    }
}
