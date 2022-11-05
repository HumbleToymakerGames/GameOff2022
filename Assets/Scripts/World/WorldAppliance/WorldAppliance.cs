using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAppliance : MonoBehaviour
{
    private bool markedUnWalkable = false;

    public Vector3Int usePositionOffset;

    public ApplianceSO appliance;

    private bool selected = false;

    void Update()
    {
        //Mark tile unwalkable after being created
        if (GetComponent<SnapToGrid>().snapped && !markedUnWalkable)
        {
            MapInformation.SetTileWalkability(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y/2, 0)), false);
            markedUnWalkable = true;
        }
    }

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().PathTo(MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)) + usePositionOffset);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition += new Vector3(screenPosition.x <= Screen.width/2 ? Screen.width/4 : -Screen.width / 4, 0, 0);
        screenPosition.y = Screen.height/2;
        UIManager.Instance.ShowApplianceContextPanel(new Appliance(appliance), screenPosition);
    }
}
