using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float speed;

    private float randomTileSelectTimer = 0;

    public float currentTile;

    private Tilemap tileMap;

    private IList<Vector3Int> path = new List<Vector3Int>();
    private bool pathStarted = false;

    private int step = 0;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pathStarted = false;
            //Vector3 tilePos = GetComponent<MouseTileSelect>().GetSelectedTilePosition();
            Vector3 tilePos = tileMap.CellToWorld(GetComponent<MouseTileSelect>().SelectRandomTile(transform.position));

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos);
                step = path.Count - 1;
                pathStarted = true;
            }
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
                pathStarted = false;
            }
        }
    }
}