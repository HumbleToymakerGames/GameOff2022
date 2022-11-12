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

    private void OnEnable()
    {
        timeUntilRandomMovement = 0;
        ResetPath();
    }

    protected override void Start()
    {
        base.Start();
        worldCustomerScript = GetComponent<WorldCustomer>();
    }

    protected override void Update()
    {
        if (movementState == NPCMovementState.Random) RandomMovementTick();
        if (movementState == NPCMovementState.Exiting) DestroyIfAtExit();
        if (stepsRemainingInPath > 0) MoveTowardNextPathStep();
        else FinishFollowingPath();
    }

    private void RandomMovementTick()
    {
        timeUntilRandomMovement -= Time.deltaTime;

        if (timeUntilRandomMovement <= 0)
        {
            Debug.Log("Find new path");
            //Continue to try and find path until valid one is found
            bool pathFound = false;
            while(!pathFound)
            {
                pathFound = SetPathTo(DestinationForNPCMovementState(movementState), Mask.Customer);
            }
            timeUntilRandomMovement = Random.Range(timeBetweenRandomMove - 1f, timeBetweenRandomMove + 1f);
        }
        
    }

    private Vector3Int DestinationForNPCMovementState(NPCMovementState movementState)
    {
        switch (movementState)
        {
            case NPCMovementState.Random:
                return TileSelect.SelectRandomTile(Mask.Customer).position;
            case NPCMovementState.Seat:
                return TileSelect.FindTileOfType(TileType.Seat).position;
            case NPCMovementState.Exiting:
                return doorTile;
            default:
                return TileSelect.SelectRandomTile(Mask.Customer).position;
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
        SetPathTo(DestinationForNPCMovementState(movementState), Mask.Customer);
    }
}



public enum NPCMovementState { Random, Seat, Order, Exiting }
