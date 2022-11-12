using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentAppliance : MonoBehaviour
{
    public WorldAppliance currentAppliance;
    public bool atAppliance = false;

    private void Update()
    {
        if (currentAppliance == null) return;

        Vector3Int currentPosition = MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0));
        Vector3Int currentAppliancePosition = MapInformation.groundTileMap.WorldToCell(currentAppliance.gameObject.transform.position - new Vector3(0, currentAppliance.gameObject.transform.localScale.y / 2, 0));
        Vector3Int currentApplianceUsePosition = currentAppliancePosition + currentAppliance.usePositionOffset;

        if (currentPosition == currentApplianceUsePosition)
        {
            Debug.Log("At appliance");
            currentAppliance.AtAppliance();
        }
    }
}
