using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcMovement : Movement
{
    public GameObject orderCounter;
    public float timeBetweenRandomMove = 5f;
    public NPCMovementState movementState = NPCMovementState.Random;

    private float timeUntilRandomMovement = 0;
    private WorldCustomer worldCustomerScript;
    private Vector3Int doorTile = new Vector3Int(-3, 5, 0); // hardcoded to the tile in front of the door

    private Vector3 oldGoal = new Vector3(-99999, -99999, -99999);

    private bool exitStarted = false;

    protected override void Start()
    {
        base.Start();
        worldCustomerScript = GetComponent<WorldCustomer>();
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
            SetPathTo(DestinationForNPCMovementState(movementState));
            timeUntilRandomMovement = Random.Range(timeBetweenRandomMove - 1f, timeBetweenRandomMove + 1f);
        }
        
    }

    private Vector3Int DestinationForNPCMovementState(NPCMovementState movementState)
    {
        switch (movementState)
        {
            case NPCMovementState.Random:
                return TileSelect.SelectRandomTile().position;
            case NPCMovementState.Seat:
                return TileSelect.FindTileOfType(TileType.Seat).position;
            case NPCMovementState.Exiting:
                return doorTile;
            default:
                return TileSelect.SelectRandomTile().position;
        }
    }

    private void DestroyIfAtExit()
    {
        Vector3Int currentPositionTile = tileMap.WorldToCell(transform.position - characterTileOffset);
        if (currentPositionTile == doorTile) gameObject.SetActive(false);
    }

    public void StartNPCMoveToExit()
    {
        movementState = NPCMovementState.Exiting;
        SetPathTo(DestinationForNPCMovementState(movementState));
    }


}



public enum NPCMovementState { Random, Seat, Order, Exiting }
