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
    private WorldCustomer worldCustomerScript;

    private Vector3 oldGoal = new Vector3(-99999, -99999, -99999);

    public MovementState movementState = MovementState.Random;

    private bool exitStarted = false;

    private float timeBeforeForceRemove = 7f;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        worldCustomerScript = GetComponent<WorldCustomer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Count down timer if not currently moving
        if(!pathStarted) 
            moveTimer -= Time.deltaTime;

        if (worldCustomerScript.exiting)
        {
            Vector3 tilePos = new Vector3(0, 0, 0);
            tilePos = GameObject.FindGameObjectWithTag("Entrance").transform.position;

            if (!exitStarted)
            {
                pathStarted = false;
            }

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos, oldGoal, false);

                oldGoal = tilePos;

                step = path.Count - 1;

                if (step > 0)
                {
                    exitStarted = true;
                }
                else
                {
                    exitStarted = true;
                }
                pathStarted = true;
            }
            else if(step <= 0)
            {
                moveTimer -= Time.deltaTime;
            }

            //Check if customer has made it to the door
            if (tileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y, 0)) == tileMap.WorldToCell(GameObject.FindGameObjectWithTag("Entrance").transform.position)
                || moveTimer <= -timeBeforeForceRemove)
            {
                Destroy(gameObject);
            }
        }
        else if (moveTimer <= 0)
        {
            pathStarted = false;

            Vector3 tilePos = new Vector3(0,0,0);

            switch (movementState)
            {
                case MovementState.Random:
                    tilePos = tileMap.CellToWorld(TileSelect.SelectRandomTile().position);
                    break;
                case MovementState.Seat:
                    tilePos = tileMap.CellToWorld(TileSelect.FindTileOfType(TileType.Seat).position);
                    break;
                case MovementState.Order:
                    //TODO
                    tilePos = tileMap.CellToWorld(TileSelect.SelectRandomTile().position);
                    break;
            }

            if (!pathStarted)
            {
                //Subtract the player y scale to offset the position to feet of player
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos, oldGoal);

                oldGoal = tilePos;

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

public enum MovementState { Random, Seat, Order}
