using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelect : MonoBehaviour
{
    public static Tilemap tileMap;

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
        if (MapInformation.groundTileMap.HasTile(tileMapPosition) && MapInformation.groundMap[tileMapPosition.x - (int)MapInformation.groundMapBounds.min.x, tileMapPosition.y - (int)MapInformation.groundMapBounds.min.y] != new Vector3Int(-99999, -99999, -99999))
        {
            //if (oldTileMapPosition != null)
            //{
            //    MapInformation.groundTileMap.SetColor(oldTileMapPosition, Color.white);
            //}
            //MapInformation.groundTileMap.SetTileFlags(MapInformation.groundTileMap.WorldToCell(mousePos), TileFlags.None);
            //MapInformation.groundTileMap.SetColor(tileMapPosition, Color.red);

            //oldTileMapPosition = tileMapPosition;

            selectedTilePosition = tileMapPosition;
        }

        return selectedTilePosition;
    }

    /// <summary>
    /// Gets a random tiles position on the map that can be pathed to
    /// </summary>
    /// <param name="startPosition"></param>
    /// <returns>The tiles position on the tilemap</returns>
    public static Vector3Int SelectRandomTile(Vector3 startPosition)
    {
        Vector3Int randomTile = new Vector3Int(-99999, -99999, -99999);
        bool validTile = false;
        while (!validTile)
        {
            randomTile = new Vector3Int((int)Random.Range(MapInformation.groundMapBounds.xMin, MapInformation.groundMapBounds.xMax), (int)Random.Range(MapInformation.groundMapBounds.yMin, MapInformation.groundMapBounds.yMax), 0);
            if (MapInformation.groundTileMap.HasTile(randomTile) && MapInformation.groundTileMap.CellToWorld(randomTile) != new Vector3Int(-99999, -99999, -99999))
            {
                validTile = true;
            }    
        }

        return randomTile;
    }

    public static void HighlightTile(Vector3Int tileMapPosition)
    {
        if (oldTileMapPosition != null)
        {
            MapInformation.groundTileMap.SetColor(oldTileMapPosition, Color.white);
        }
        MapInformation.groundTileMap.SetTileFlags(tileMapPosition, TileFlags.None);
        MapInformation.groundTileMap.SetColor(tileMapPosition, Color.gray);

        oldTileMapPosition = tileMapPosition;
    }

    public static void ClearHighlight()
    {
        if (oldTileMapPosition != null)
        {
            MapInformation.groundTileMap.SetColor(oldTileMapPosition, Color.white);
        }
    }
}
