using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldAppliance : MonoBehaviour
{
    private bool initialized = false;

    public Vector3Int usePositionOffset;

    public ApplianceSO applianceSO;
    private Appliance appliance;

    //Pre placed objects should have this value changed in the inspector

    void Update()
    {
        if (GetComponent<PlaceableObject>().placed)
        {
            //Mark tile unwalkable after being created
            if (!initialized)
            {
                appliance = new Appliance(applianceSO);
                appliance.worldAppliance = this;
                initialized = true;
            }

            //Appliance working
            if (appliance.anyFunctionRunning)
            {
                appliance.UpdateFunction();
            }
        }
    }

    private void OnMouseDown()
    {
        if (Player.controlState == ControlState.Game)
        {
            //Check if over ui when clicking if so skip selection
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition += new Vector3(screenPosition.x <= Screen.width / 2 ? Screen.width / 4 : -Screen.width / 4, 0, 0);
            screenPosition.y = Screen.height / 2;
            EventHandler.CallDidClickApplianceEvent(appliance, Camera.main.WorldToScreenPoint(transform.position));
            UIManager.Instance.ShowApplianceContextPanel(appliance, screenPosition);
        }
    }

    public void MovePlayerToAppliance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().SetPathTo(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)) + usePositionOffset);
        player.GetComponent<PlayerCurrentAppliance>().currentAppliance = this;
    }

    public void AtAppliance()
    {
        appliance.StartFunction();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCurrentAppliance>().currentAppliance = null;
    }
}
