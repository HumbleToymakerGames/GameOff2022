using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcMovement : Movement
{
    public GameObject orderCounter;
    public float timeBetweenRandomMove = 5f;
    public NPCMovementState movementState = NPCMovementState.Random;

    private float timeUntilRandomMovement = 0;
    private WorldCustomer worldCustomerScript;
    private Vector3Int doorTile;

    protected override void Start()
    {
        base.Start();
        worldCustomerScript = GetComponent<WorldCustomer>();
        doorTile = tileMap.WorldToCell(GameObject.FindGameObjectWithTag("Entrance").transform.position);
    }

    protected override void Update()
    {
        if (movementState == NPCMovementState.Random) RandomMovementTick();
        if (movementState == NPCMovementState.Exiting) DestroyIfAtExit();
        MoveTowardNextPathStep();
    }

    private void RandomMovementTick()
    {
        timeUntilRandomMovement -= Time.deltaTime;

        if (timeUntilRandomMovement <= 0)
        {
            path = PathForNPCMovementState(movementState);
            timeUntilRandomMovement = Random.Range(timeBetweenRandomMove - 1f, timeBetweenRandomMove + 1f);
        }
        
    }

    private IList<Vector3Int> PathForNPCMovementState(NPCMovementState movementState)
    {
        switch (movementState)
        {
            case NPCMovementState.Random:
                IList<Vector3Int> candidatePath = PathToTile(TileSelect.SelectRandomTile().position);
                while (candidatePath.Count == 0)
                {
                    // If destination is unreachable, PathToTile will return an empty path
                    candidatePath = PathToTile(TileSelect.SelectRandomTile().position);
                }
                return candidatePath;
            case NPCMovementState.Seat:
                return PathToTile(TileSelect.FindTileOfType(TileType.Seat).position);
            case NPCMovementState.Exiting:
                return PathToTile(tileMap.WorldToCell(GameObject.FindGameObjectWithTag("Entrance").transform.position));
            default:
                return PathToTile(TileSelect.SelectRandomTile().position);
        }
    }

    private void DestroyIfAtExit()
    {
        Vector3Int currentPositionTile = tileMap.WorldToCell(transform.position - characterTileOffset);
        if (currentPositionTile == doorTile) Destroy(gameObject);
    }
   
}



public enum NPCMovementState { Random, Seat, Order, Exiting }
