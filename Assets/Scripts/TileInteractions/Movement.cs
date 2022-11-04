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
            Vector3 tilePos = GetComponent<MouseTileSelect>().GetSelectedTilePosition();

            //transform.position = tileMap.CellToWorld(MapInformation.groundMap[MapInformation.GetTileIndex(tilePos).x, MapInformation.GetTileIndex(tilePos).y]);

            if (!pathStarted)
            {
                path = Pathfinder.FindPath(transform.position - new Vector3(0, transform.localScale.y, 0), tilePos);
                Debug.Log(path.Count);
                step = path.Count - 1;
                pathStarted = true;
            }
            //transform.position = Vector3.MoveTowards(transform.position, ((tileMap.CellToWorld(s)) + new Vector3(0, transform.localScale.y, 0)), speed * Time.deltaTime);
        }

        if (pathStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Debug.Log(step);
                transform.position = ((tileMap.CellToWorld(path[step])) + new Vector3(0, transform.localScale.y, 0));
                step--;
                timer = 0.25f;
            }
            if (step <= 0)
            {
                pathStarted = false;
            }
        }
    }
}