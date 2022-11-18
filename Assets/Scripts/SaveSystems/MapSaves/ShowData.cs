using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowData : MonoBehaviour
{
    MapData saveData = new MapData();

    IList<Vector3Int> positions = new List<Vector3Int>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            saveData = MapInformation.mapData;
            SaveSystem.instance.SaveGame(saveData, "Nursery");
            Debug.Log("Game saved");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            saveData = SaveSystem.instance.LoadGame("Nursery");
            Debug.Log("Game loaded");

            for (int i = 0; i < saveData.placedItems; i++)
            {
                positions.Add(new Vector3Int(saveData.positions[0,i], saveData.positions[1, i], saveData.positions[2, i]));
            }

            for (int i = 0; i < saveData.placedItems; i++)
            {
                Debug.Log(positions[i]);
                Debug.Log(saveData.names[i]);
            }
        }
    }
}
