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
    public static Vector3Int GetTileUnderMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get tile clicked
        Vector3Int tileMapPosition = MapInformation.groundTileMap.WorldToCell(mousePos);
        if (MapInformation.groundTileMap.HasTile(tileMapPosition) && MapInformation.groundMap[tileMapPosition.x - (int)MapInformation.groundMapBounds.min.x, tileMapPosition.y - (int)MapInformation.groundMapBounds.min.y].walkable)
        {
            selectedTilePosition = tileMapPosition;
        }

        return selectedTilePosition;
    }

    /// <summary>
    /// Gets a random tiles position on the map that can be pathed to
    /// </summary>
    /// <returns>The tiles position on the tilemap</returns>
    public static TileInfo SelectRandomTile()
    {
        /* This needs to be refactored to pick a random tile in the cafe portion of the map

        TileInfo randomTile = new TileInfo(new Vector3Int(2, -0, 2), true);
        bool validTile = false;
        while (!validTile)
        {
            randomTile = new TileInfo(new Vector3Int((int)Random.Range(MapInformation.groundMapBounds.xMin, MapInformation.groundMapBounds.xMax), (int)Random.Range(MapInformation.groundMapBounds.yMin, MapInformation.groundMapBounds.yMax), 0), true);
            if (MapInformation.groundTileMap.HasTile(randomTile.position) && randomTile.walkable)
            {
                validTile = true;
            }    
        }
        */

        return new TileInfo(new Vector3Int(Random.Range(4, -6), Random.Range(1, 5), 0), true); ;
    }

    public static TileInfo FindTileOfType(TileType tileType)
    {
        TileInfo tile = new TileInfo(new Vector3Int(-99999, -99999, -99999), true);
        IList<TileInfo> tileList = MapInformation.GetTileTypeList(tileType);
        if(tileList.Count > 0)
            tile = tileList[Random.Range(0, tileList.Count)];

        return tile;
    }

    public static void HighlightTile(Vector3Int tileMapPosition)
    {
        if (highlightTile == null)
        {
            highlightTile = Resources.Load<TileBase>("Tilesets/Assets/Highlight");
        }

        //MapInformation.overlayTileMap.ClearAllTiles();
        MapInformation.overlayTileMap.SetTileFlags(tileMapPosition, TileFlags.None);
        MapInformation.overlayTileMap.SetTile(tileMapPosition, highlightTile);
    }

    public static void ClearHighlight()
    {
        MapInformation.overlayTileMap.ClearAllTiles();
    }
}
