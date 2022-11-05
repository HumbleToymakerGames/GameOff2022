using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcMovement : MonoBehaviour
{
    public float speed = 2;

    private Tilemap tileMap;
    private IList<Vector3Int> path = new List<Vector3Int>();
    private bool pathStarted = false;

    private int step = 0;
    public float timeBetweenRandomMove = 5f;
    private float moveTimer = 0;

    public GameObject orderCounter;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        //Count down timer if not currently moving
        if(!pathStarted) 
            moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            pathStarted = false;
            //Vector3 tilePos = GetComponent<MouseTileSelect>().GetSelectedTilePosition();
            Vector3 tilePos = tileMap.CellToWorld(TileSelect.SelectRandomTile(transform.position));
            //Vector3Int tilePos = tileMap.WorldToCell(orderCounter.transform.position) + new Vector3Int(-1, 0, 0);

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos);
                step = path.Count - 1;
                pathStarted = true;
            }

            //Randomize the time by a bit
            moveTimer = Random.Range(timeBetweenRandomMove - 1f, timeBetweenRandomMove + 1f);
        }
        if (pathStarted)
        {
            if (step > 0)
            {
                //Fixes bug where sometimes they go flying back to (0,0)
                if (Vector3.Distance(transform.position, (tileMap.CellToWorld(path[step])) + new Vector3(0, transform.localScale.y, 0)) < 3f)
                    transform.position = Vector3.MoveTowards(transform.position, (tileMap.CellToWorld(path[step])) + new Vector3(0, transform.localScale.y, 0), speed * Time.deltaTime);
                else
                {
                    //If bug is encountered skip the step
                    step--;
                }
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
