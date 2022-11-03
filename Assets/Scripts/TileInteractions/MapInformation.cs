using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapInformation : MonoBehaviour
{
    public static MapInformation _instance;
    public static MapInformation Instance { get { return _instance;  } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Tilemap tileMap = gameObject.GetComponentInChildren<Tilemap>();

        BoundsInt bounds = tileMap.cellBounds;

        for (int z = bounds.max.z; z > bounds.min.z; z--)
        {
            for (int y = bounds.max.y; y > bounds.min.y; y--)
            {
                for (int x = bounds.max.y; x > bounds.min.x; x--)
                {
                    Vector3Int tileLocation = new Vector3Int(x, y, z);

                    if (tileMap.HasTile(tileLocation))
                    {
                        
                    }
                }
            }
        }
    }
}
