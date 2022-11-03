using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseTileSelect : MonoBehaviour
{
    public Tilemap tileMap;

    private Vector3Int selectedTilePosition;
    private Vector3Int oldTileMapPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Get tile clicked
            Vector3Int tileMapPosition = tileMap.WorldToCell(mousePos);
            if (tileMap.HasTile(tileMapPosition))
            {
                if (oldTileMapPosition != null)
                {
                    tileMap.SetColor(oldTileMapPosition, Color.white);
                }
                selectedTilePosition = tileMapPosition;
                tileMap.SetTileFlags(tileMap.WorldToCell(mousePos), TileFlags.None);
                tileMap.SetColor(tileMapPosition, Color.red);

                oldTileMapPosition = tileMapPosition;
            }
        }
    }

    public Vector3 GetSelectedTilePosition()
    {
        return tileMap.CellToWorld(selectedTilePosition);
    }
}
