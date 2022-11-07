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

    public bool movementLocked = false;

    private Vector3Int oldGoal = new Vector3Int(-99999, -99999, -99999);

    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
    }

    public void Update()
    {
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

    public void UpdateCall()
    {
        
    }

    public void PathTo(Vector3Int tilePos)
    {
        if (Player.controlState == ControlState.Game && !movementLocked)
        {
            pathStarted = false;

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos, oldGoal);

                oldGoal = tilePos;

                step = path.Count - 1;
                pathStarted = true;
            }

            TileSelect.HighlightTile(tilePos);
        }
    }
}