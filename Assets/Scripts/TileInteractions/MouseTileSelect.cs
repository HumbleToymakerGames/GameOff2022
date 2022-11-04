using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseTileSelect : MonoBehaviour
{
    public Tilemap tileMap;

    private Vector3Int selectedTilePosition;
    private Vector3Int oldTileMapPosition;

    private void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Get tile clicked
            Vector3Int tileMapPosition = tileMap.WorldToCell(mousePos);
            if (tileMap.HasTile(tileMapPosition) && MapInformation.groundMap[tileMapPosition.x - (int)MapInformation.groundMapBounds.min.x, tileMapPosition.y - (int)MapInformation.groundMapBounds.min.y] != new Vector3Int(-99999, -99999, -99999))
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

    //TODO
    public Vector3Int SelectRandomTile(Vector3 currentPosition)
    {
        Vector3Int randomTile = new Vector3Int(-9999, -9999, -9999);
        //Really inefficient but it works
        bool pathFound = false;
        while (!pathFound)
        {
            randomTile = new Vector3Int((int)Random.Range(MapInformation.groundMapBounds.xMin, MapInformation.groundMapBounds.xMax), (int)Random.Range(MapInformation.groundMapBounds.yMin, MapInformation.groundMapBounds.yMax), 0);
            if (Pathfinder.FindPath(currentPosition - new Vector3(0, transform.localScale.y, 0), tileMap.CellToWorld(randomTile)).Count > 0)
            {
                pathFound = true;
            }
        }

        return randomTile;
    }
}
