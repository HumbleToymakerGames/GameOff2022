using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrentAppliance : MonoBehaviour
{
    public WorldAppliance currentAppliance;
    public bool atAppliance = false;

    private void Update()
    {
        if (currentAppliance != null && 
            MapInformation.groundTileMap.WorldToCell(transform.position - new Vector3(0, transform.localScale.y / 2, 0)) == MapInformation.groundTileMap.WorldToCell(currentAppliance.gameObject.transform.position - new Vector3(0, currentAppliance.gameObject.transform.localScale.y / 2, 0)) + currentAppliance.usePositionOffset)
        {
            currentAppliance.AtAppliance();
        }
    }
}
