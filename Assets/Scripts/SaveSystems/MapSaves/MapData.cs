using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public int placedItems = 0;
    public int[,] positions;
    public string[] names;


    public void UpdateMapData()
    {
        //clear previous data
        positions = new int[0, 0];
        names = new string[0];
        placedItems = 0;

        IList<GameObject> objects = new List<GameObject>();
        IList<Vector3Int> objectPositions = new List<Vector3Int>();

        objects.Clear();

        objectPositions.Clear();

        foreach (TileInfo t in MapInformation.groundMap)
        {
            if (t != null)
            {
                if (t.gameObjectOnTile != null)
                {
                    objects.Add(t.gameObjectOnTile);
                    objectPositions.Add(t.position);
                    placedItems++;
                }
                if (t.deskObjectOnTile != null)
                {
                    objects.Add(t.deskObjectOnTile);
                    objectPositions.Add(t.position);
                    placedItems++;
                }
            }
        }

        positions = new int[3, placedItems];
        names = new string[placedItems];
        for (int i = 0; i < placedItems; i++)
        {
            names[i] = objects[i].GetComponent<PlaceableObject>().placeableObjectSO.itemName;

            for (int x = 0; x < 3; x++)
            {
                //This feels dumb but it's alright, it happens
                switch (x) 
                {
                    case 0:
                        positions[x, i] = objectPositions[i].x;
                        break;
                    case 1:
                        positions[x, i] = objectPositions[i].y;
                        break;
                    case 2:
                        positions[x, i] = objectPositions[i].z;
                        break;
                }
            }
        }
    }
}
