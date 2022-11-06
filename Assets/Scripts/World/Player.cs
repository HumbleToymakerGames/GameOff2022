using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Changes player interactions based on game state
    public static ControlState controlState = ControlState.Game;

    private Movement movement;
    private PlayerEditMode edit;

    private void Start()
    {
        movement = GetComponent<Movement>();
        edit = GetComponent<PlayerEditMode>();   
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(controlState != ControlState.Game)
                controlState = ControlState.Game;
            else
                controlState = ControlState.Edit;
        }

        switch (controlState)
        {
            case ControlState.Game:
                movement.UpdateCall();
                break;
            case ControlState.Edit:
                edit.UpdateCall();
                break;
        }
    }

}

public enum ControlState { Game, Edit };
