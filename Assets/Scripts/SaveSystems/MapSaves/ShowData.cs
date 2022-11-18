using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowData : MonoBehaviour
{
    IList<Vector3Int> positions = new List<Vector3Int>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //MapData saveData = MapInformation.mapData;
            //SaveSystem.instance.SaveGame(saveData, "Nursery");
            //Debug.Log("Game saved");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //MapInformation.mapData = SaveSystem.instance.LoadGame("Nursery");
            //Debug.Log("Game loaded");

            //for (int i = 0; i < MapInformation.mapData.placedItems; i++)
            //{
            //    positions.Add(new Vector3Int(MapInformation.mapData.xPos[i], MapInformation.mapData.yPos[i], MapInformation.mapData.zPos[i]));
            //}

            //for (int i = 0; i < MapInformation.mapData.placedItems; i++)
            //{
            //    Debug.Log(positions[i]);
            //    Debug.Log(MapInformation.mapData.names[i]);
            //}
        }
    }
}
