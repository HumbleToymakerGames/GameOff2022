using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float speed = 2;

    private Tilemap tileMap;

    private IList<Vector3Int> path = new List<Vector3Int>();
    private bool pathStarted = false;

    private int step = 0;

    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pathStarted = false;
            Vector3 tilePos = tileMap.CellToWorld(TileSelect.GetTileUnderMouse());

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos);
                step = path.Count - 1;
                pathStarted = true;
            }

            TileSelect.HighlightTile(TileSelect.GetTileUnderMouse());
        }
        if (pathStarted)
        {
            if (step > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, (tileMap.CellToWorld(path[step])) + new Vector3(0, transform.localScale.y, 0), speed * Time.deltaTime);
                if (transform.position == (tileMap.CellToWorld(path[step])) + new Vector3(0, transform.localScale.y, 0))
                {
                    step--;
                }
            }
            else
            {
                TileSelect.ClearHighlight();
                pathStarted = false;
            }
        }
    }
}