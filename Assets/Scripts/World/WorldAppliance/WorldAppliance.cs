using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldAppliance : MonoBehaviour
{
    private bool markedUnWalkable = false;

    public Vector3Int usePositionOffset;

    public ApplianceSO applianceSO;
    private Appliance appliance;

    private bool selected = false;

    private void Start()
    {
        appliance = new Appliance(applianceSO);
        appliance.worldAppliance = this;
    }

    void Update()
    {
        //Mark tile unwalkable after being created
        if (GetComponent<SnapToGrid>().snapped && !markedUnWalkable)
        {
            MapInformation.SetTileWalkability(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y/2, 0)), false);
            markedUnWalkable = true;
        }

        //Appliance working
        if(appliance.anyFunctionRunning)
        {
            appliance.UpdateFunction();
        }
    }

    private void OnMouseDown()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition += new Vector3(screenPosition.x <= Screen.width / 2 ? Screen.width / 4 : -Screen.width / 4, 0, 0);
        screenPosition.y = Screen.height / 2;
        EventHandler.CallDidClickApplianceEvent(appliance, Camera.main.WorldToScreenPoint(transform.position));
        UIManager.Instance.ShowApplianceContextPanel(appliance, screenPosition);
    }

    public void MovePlayerToAppliance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Movement>().PathTo(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)) + usePositionOffset);
        player.GetComponent<PlayerCurrentAppliance>().currentAppliance = this;
    }

    public void AtAppliance()
    {
        appliance.StartFunction();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCurrentAppliance>().currentAppliance = null;
    }
}
