using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    private static GameObject placeableObjectPrefab;

    [SerializeField] private static PlacedObjectsData mapSaveData = new PlacedObjectsData();

    private static PlaceableObjectClass[] placeableObjectSOs;

    public void Awake()
    {
        placeableObjectSOs = Resources.LoadAll<PlaceableObjectClass>("Data/PlaceableObjects");
        placeableObjectPrefab = Resources.Load<GameObject>("Prefabs/PlaceableObjects/PlaceableObjectPrefab");
        LoadMap();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadMap();
        }
    }

    public static void SaveMap()
    {
        mapSaveData.placedObjects.Clear();
        foreach (TileInfo t in MapInformation.groundMap)
        {
            if (t != null)
            {
                if (t.gameObjectOnTile != null)
                {
                    mapSaveData.placedObjects.Add(new GameObjectData(t.gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.itemName, new PositionData(t.position.x, t.position.y, t.position.z)));
                }
                if (t.deskObjectOnTile != null)
                {
                    mapSaveData.placedObjects.Add(new GameObjectData(t.deskObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.itemName, new PositionData(t.position.x, t.position.y, t.position.z)));
                }
            }
        }

        SaveIntoJason();
    }

    public static void LoadMap()
    {
        Debug.Log("Load");

        mapSaveData = LoadJson();

        if (mapSaveData.placedObjects.Count > 0)
        {
            foreach (TileInfo t in MapInformation.groundMap)
            {
                if (t != null)
                {
                    if (t.gameObjectOnTile != null)
                    {
                        Destroy(t.gameObjectOnTile);
                    }
                    if (t.deskObjectOnTile != null)
                    {
                        Destroy(t.deskObjectOnTile);
                    }
                }
            }
            MapInformation.RefreshMap();
        }

        PopulateMap();
    }

    public static void SaveIntoJason()
    {
        string objectData = JsonUtility.ToJson(mapSaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json", objectData);
    }

    public static PlacedObjectsData LoadJson()
    {
        return JsonUtility.FromJson<PlacedObjectsData>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json"));
    }

    public static void PopulateMap()
    {
        foreach (GameObjectData g in mapSaveData.placedObjects)
        {
            GameObject placeableObject = Instantiate(placeableObjectPrefab);
            placeableObject.SetActive(true);

            PlaceableObject script = placeableObject.GetComponent<PlaceableObject>();

            foreach (PlaceableObjectClass p in placeableObjectSOs)
            {
                if (g.name == p.itemName)
                {
                    script.itemClass = p;
                    script.SetComponents(p);

                    script.PlaceObjectAt(new Vector3Int(g.position.x, g.position.y, g.position.z));
                }
            }
        }
    }

    public void UpdateMapData()
    {
        ////clear previous data
        //placedItems = 0;

        //IList<GameObject> objects = new List<GameObject>();
        //IList<Vector3Int> objectPositions = new List<Vector3Int>();

        //foreach (TileInfo t in MapInformation.groundMap)
        //{
        //    if (t != null)
        //    {
        //        if (t.gameObjectOnTile != null)
        //        {
        //            objects.Add(t.gameObjectOnTile);
        //            objectPositions.Add(t.position);
        //            placedItems++;
        //        }
        //        if (t.deskObjectOnTile != null)
        //        {
        //            objects.Add(t.deskObjectOnTile);
        //            objectPositions.Add(t.position);
        //            placedItems++;
        //        }
        //    }
        //}

        //foreach (Vector3Int v in objectPositions)
        //{
        //    Debug.Log(v);
        //}

        //xPos = new int[placedItems];
        //yPos = new int[placedItems];
        //zPos = new int[placedItems];
        //names = new string[placedItems];
        //for (int i = 0; i < placedItems; i++)
        //{
        //    names[i] = objects[i].GetComponent<PlaceableObject>().placeableObjectSO.itemName;

        //    xPos[i] = objectPositions[i].x;
        //    yPos[i] = objectPositions[i].y;
        //    zPos[i] = objectPositions[i].z;
        //}
    }
}

[System.Serializable]
public class PlacedObjectsData
{
    public List<GameObjectData> placedObjects = new List<GameObjectData>();
}

[System.Serializable]
public class GameObjectData
{
    public string name;
    public PositionData position;

    public GameObjectData(string name, PositionData position)
    {
        this.name = name;
        this.position = position;
    }
}

[System.Serializable]
public class PositionData
{
    public int x;
    public int y;
    public int z;

    public PositionData(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }
}
