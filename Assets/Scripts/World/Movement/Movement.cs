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
    protected int stepsRemainingInPath
    {
        get 
        {
            if (path.Count == 0) return 0;
            return path.Count - 1;
        }
    }

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

    protected void FinishFollowingPath()
    {
        TileSelect.ClearHighlight();
        pathStarted = false;
    }

    protected void ResetPath()
    {
        path.Clear();
        pathStarted = false;
    }

    protected void MoveTowardNextPathStep()
    {
        if (path.Count == 0) return;
        Vector3 worldSpaceTarget = tileMap.CellToWorld(path[stepsRemainingInPath]) + characterTileOffset;
        transform.position = Vector3.MoveTowards(transform.position, worldSpaceTarget, speed * Time.deltaTime);
        if (transform.position == worldSpaceTarget) path.RemoveAt(stepsRemainingInPath);
    }

    public void SetPlayerPathTo(Vector3Int destinationTile)
    {
        if (Player.controlState != ControlState.Game || movementLocked) return;
        SetPathTo(destinationTile, Mask.Kitchen);
        TileSelect.HighlightTile(destinationTile);
    }

    protected bool SetPathTo(Vector3Int destinationTile, Mask mask)
    {
        path = PathToTile(destinationTile, mask);
        previousGoal = destinationTile;
        if (path.Count > 0) pathStarted = true;
        return pathStarted;
    }

    protected IList<Vector3Int> PathToTile(Vector3Int destinationTile, Mask mask)
    {
        return Pathfinder.FindPath(tileMap.WorldToCell(transform.position - characterTileOffset), destinationTile, previousGoal, false, mask);
    }

}