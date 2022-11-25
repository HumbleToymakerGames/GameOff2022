using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapData : MonoBehaviour
{
    private static GameObject placeableObjectPrefab;

    public static bool saveLoadEnabled = true;

    [SerializeField] private static PlacedObjectsData mapSaveData = new PlacedObjectsData();

    private static PlaceableObjectClass[] placeableObjectSOs;

    public void Start()
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
        if (saveLoadEnabled)
        {
            if (mapSaveData == null)
                mapSaveData = new PlacedObjectsData();

            mapSaveData.placedObjects.Clear();
            foreach (TileInfo t in MapInformation.groundMap)
            {
                if (t != null)
                {
                    if (t.gameObjectOnTile != null)
                    {
                        mapSaveData.placedObjects.Add(new GameObjectData(t.gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.itemName, new PositionData(t.position.x, t.position.y, t.position.z), t.gameObjectOnTile.GetComponent<PlaceableObject>().flipped));
                    }
                    if (t.deskObjectOnTile != null)
                    {
                        mapSaveData.placedObjects.Add(new GameObjectData(t.deskObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.itemName, new PositionData(t.position.x, t.position.y, t.position.z), t.deskObjectOnTile.GetComponent<PlaceableObject>().flipped, true));
                    }
                }
            }

            SaveIntoJason();
        }
    }

    public static void LoadMap()
    {
        if (saveLoadEnabled)
        {
            Debug.Log("Load");

            mapSaveData = LoadJson();

            if (mapSaveData == null)
                mapSaveData = new PlacedObjectsData();

            if (mapSaveData != null
                && mapSaveData.placedObjects.Count > 0)
            {
                if (MapInformation.groundMap != null)
                {
                    Debug.Log("Ground Map not null");
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
                }
                else
                {
                    Debug.Log("Ground Map null");
                }
                PopulateMap();
                SaveMap();
            }
        }
    }

    public static void SaveIntoJason()
    {
        string objectData = JsonUtility.ToJson(mapSaveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json", objectData);
    }

    public static PlacedObjectsData LoadJson()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json"))
            return JsonUtility.FromJson<PlacedObjectsData>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json"));
        else
        {
            SaveIntoJason();
            return JsonUtility.FromJson<PlacedObjectsData>(System.IO.File.ReadAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + ".json"));
        }

    }

    public static void PopulateMap()
    {
        GameObject lastPlacedObject = null;
        for (int i = 0; i < mapSaveData.placedObjects.Count; i++)
        {
            GameObject placeableObject = Instantiate(placeableObjectPrefab);
            placeableObject.SetActive(true);

           
            PlaceableObject script = placeableObject.GetComponent<PlaceableObject>();

            foreach (PlaceableObjectClass p in placeableObjectSOs)
            {
                if (mapSaveData.placedObjects[i].name == p.itemName)
                {
                    script.itemClass = p;
                    script.flipped = mapSaveData.placedObjects[i].flipped;
                    script.SetComponents(p);


                    //if (currentPlaceableObjectSO.type == TileType.DeskItem)
                    //{
                    //    float height = MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile == null ? 1f : (float)(MapInformation.groundMap[indexPosition.x, indexPosition.y].gameObjectOnTile.GetComponent<PlaceableObject>().placeableObjectSO.pixelHeight) / 64;
                    //    SpriteRenderer spr = heldObject.GetComponent<SpriteRenderer>();
                    //    spr.sprite = Sprite.Create(currentPlaceableObjectSO.sprite.texture, currentPlaceableObjectSO.sprite.rect, new Vector2(0.5f, height * ((currentPlaceableObjectSO.sprite.pixelsPerUnit / 2) / (currentPlaceableObjectSO.sprite.bounds.size.y * currentPlaceableObjectSO.sprite.pixelsPerUnit))), currentPlaceableObjectSO.sprite.pixelsPerUnit);
                    //    heldObject.GetComponent<PlaceableObject>().height = height;
                    //}


                    if (lastPlacedObject != null && mapSaveData.placedObjects[i].onDesk)
                    {
                        float height = (float)(lastPlacedObject.GetComponent<PlaceableObject>().placeableObjectSO.pixelHeight) / 64;
                        SpriteRenderer spr = placeableObject.GetComponent<SpriteRenderer>();
                        spr.sprite = Sprite.Create(p.sprite.texture, p.sprite.rect, new Vector2(0.5f, height * ((p.sprite.pixelsPerUnit / 2) / (p.sprite.bounds.size.y * p.sprite.pixelsPerUnit))), p.sprite.pixelsPerUnit);

                        script.GetComponent<PlaceableObject>().height = height;
                    }
                    else
                    {
                        float height = 1f;
                        SpriteRenderer spr = placeableObject.GetComponent<SpriteRenderer>();
                        spr.sprite = Sprite.Create(p.sprite.texture, p.sprite.rect, new Vector2(0.5f, height * ((p.sprite.pixelsPerUnit / 2) / (p.sprite.bounds.size.y * p.sprite.pixelsPerUnit))), p.sprite.pixelsPerUnit);

                        script.GetComponent<PlaceableObject>().height = height;
                    }
                        

                    script.PlaceObjectAt(new Vector3Int(mapSaveData.placedObjects[i].position.x, mapSaveData.placedObjects[i].position.y, mapSaveData.placedObjects[i].position.z));
                    break;
                }
            }

            lastPlacedObject = placeableObject;
        }

        //foreach (GameObjectData g in mapSaveData.placedObjects)
        //{
        //    //Move objects that were on top of things to a new list so they can be created after the ground objects
        //    if (g.onDesk)
        //    {
        //        deskObjects.Add(g);
        //    }
        //    else
        //    {
        //        GameObject placeableObject = Instantiate(placeableObjectPrefab);
        //        placeableObject.SetActive(true);

        //        PlaceableObject script = placeableObject.GetComponent<PlaceableObject>();

        //        Debug.Log(g.name);

        //        foreach (PlaceableObjectClass p in placeableObjectSOs)
        //        {
        //            if (g.name == p.itemName)
        //            {
        //                script.itemClass = p;
        //                script.flipped = g.flipped;
        //                script.SetComponents(p);

        //                script.PlaceObjectAt(new Vector3Int(g.position.x, g.position.y, g.position.z));
        //            }
        //        }
        //    }
        //}
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
    public bool flipped;
    public bool onDesk;

    public GameObjectData(string name, PositionData position, bool flipped, bool onDesk = false)
    {
        this.name = name;
        this.position = position;
        this.flipped = flipped;
        this.onDesk = onDesk;
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
