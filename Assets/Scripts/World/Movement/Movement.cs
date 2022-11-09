using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float speed = 2;
    public bool movementLocked = false;

    protected Tilemap tileMap;
    protected IList<Vector3Int> path = new List<Vector3Int>();
    protected bool pathStarted = false;
    protected int stepsRemainingInPath = 0;
    protected Vector3Int previousGoal = new Vector3Int(-99999, -99999, -99999);
    protected Vector3 characterTileOffset;


    protected virtual void Start()
    {
        tileMap = GameObject.FindGameObjectWithTag("GroundTileMap").GetComponent<Tilemap>();
        characterTileOffset = new Vector3(0, transform.localScale.y, 0); 
    }

    protected virtual void Update()
    {
        if (!pathStarted) return;

        if (stepsRemainingInPath > 0) MoveTowardNextPathStep();
        else FinishFollowingPath();
    }

    private void FinishFollowingPath()
    {
        TileSelect.ClearHighlight();
        pathStarted = false;
    }

    protected void MoveTowardNextPathStep()
    {
        Vector3 worldSpaceTarget = tileMap.CellToWorld(path[stepsRemainingInPath]) + characterTileOffset;
        transform.position = Vector3.MoveTowards(transform.position, worldSpaceTarget, speed * Time.deltaTime);
        if (transform.position == worldSpaceTarget) stepsRemainingInPath--;
    }

    public void UpdateCall()
    {
        // Why is this here?
    }

    public void SetPathTo(Vector3Int destinationTile)
    {
        if (Player.controlState != ControlState.Game || movementLocked) return;
        // TODO  this can only be used by player, extract to get a generic SetPathTo method

        path = PathToTile(destinationTile);
        TileSelect.HighlightTile(destinationTile);
        previousGoal = destinationTile;
        if (path.Count > 0)
        {
            stepsRemainingInPath = path.Count - 1;
            pathStarted = true;
        }        
    }

    protected IList<Vector3Int> PathToTile(Vector3Int destinationTile)
    {
        // This previously took in
        // 0: World position
        // 1: Tile position
        // 2: Tile Position
        // 
        // Now I am trying to make it take in all tile positions


        IList<Vector3Int> path = Pathfinder.FindPath(tileMap.WorldToCell(transform.position - characterTileOffset), destinationTile, previousGoal);
        Debug.Log("Shortest Path is of length " + path.Count);
        foreach(Vector3Int point in path)
        {
            Debug.Log(point.ToString());
        }
        
        return path;
    }
}