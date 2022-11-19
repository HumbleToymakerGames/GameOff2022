using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Changes player interactions based on game state
    public static ControlState controlState = ControlState.Game;

    private Movement movement;
    private PlayerEditMode edit;

    public ControlState lockedState = ControlState.None;

    public GameObject placementPanel;
    private bool placementPanelShown = false;
    

    private void Start()
    {
        movement = GetComponent<Movement>();
        edit = GetComponent<PlayerEditMode>();
    }

    public void Update()
    {
        // Game state swapping
        if (lockedState == ControlState.None)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (controlState != ControlState.Game)
                    controlState = ControlState.Game;
                else
                    controlState = ControlState.Edit;
            }
        }
        else
            controlState = lockedState;

        switch (controlState)
        {
            case ControlState.Edit:
                if (!placementPanelShown)
                {
                    PlacementPanel.ShowPlacementMenu(true, GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<NurseryShopManager>().GetItems());
                    placementPanelShown = true;
                }
                edit.UpdateCall();
                break;
            case ControlState.Game:
                placementPanelShown = false;
                PlacementPanel.ShowPlacementMenu(false);
                Game();
                break;
        }
    }

    private void Game()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            GameObject lowestYValueObject = null;
            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (lowestYValueObject == null)
                        lowestYValueObject = hit.collider.gameObject;
                    else if (lowestYValueObject.transform.position.y > hit.collider.gameObject.transform.position.y)
                        lowestYValueObject = hit.collider.gameObject;
                }

                lowestYValueObject.GetComponent<WorldAppliance>().Clicked();
            }
        }
    }
}

public enum ControlState { Game, Edit, None };
