using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInformation : MonoBehaviour
{
    public static Vector3Int[,] groundMap;
    public static BoundsInt groundMapBounds;

    public bool refresh = false;

    private void Start()
    {
        RefreshMap();
    }

    private void Update()
    {
        if (refresh)
        {
            RefreshMap();
            refresh = false;
        }
    }

    public void RefreshMap()
    {
        Tilemap groundTileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        Tilemap objectTileMap = GameObject.FindGameObjectWithTag("ObjectTileMap").GetComponent<Tilemap>();

        groundMapBounds = groundTileMap.cellBounds;
        groundMap = new Vector3Int[groundMapBounds.max.x - groundMapBounds.min.x + 2, groundMapBounds.max.y - groundMapBounds.min.y + 2];

        for (int y = groundMapBounds.max.y; y > groundMapBounds.min.y; y--)
        {
            for (int x = groundMapBounds.max.y; x > groundMapBounds.min.x; x--)
            {
                Vector3Int tileLocation = new Vector3Int(x - 1, y - 1, 0);

                //Create an array of all floor tiles that don't have objects on them
                Debug.Log(tileLocation.z);
                Debug.Log(groundTileMap.GetTile(tileLocation));
                if (groundTileMap.HasTile(tileLocation))
                {
                    Debug.Log(objectTileMap.GetTile(tileLocation));
                    if (!objectTileMap.HasTile(tileLocation))
                    {
                        groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))] = tileLocation;
                    }
                    else
                    {
                        groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))] = new Vector3Int(-99999, -99999, -99999);
                    }
                }
                else
                {
                    groundMap[((x - 1) + (-groundMapBounds.min.x)), ((y - 1) + (-groundMapBounds.min.y))] = new Vector3Int(-99999, -99999, -99999);
                }
            }
        }
    }
}
