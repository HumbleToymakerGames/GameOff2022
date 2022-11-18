using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    static public SaveSystem instance;
    string filePath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SaveGame(MapData saveData, string fileName)
    {
        filePath = Application.persistentDataPath + "/" + fileName + ".data";
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public MapData LoadGame(string fileName)
    {
        filePath = Application.persistentDataPath + "/" + fileName + ".data";
        if (File.Exists(filePath))
        {
            //File exists
            FileStream dataSteam = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            MapData saveData = converter.Deserialize(dataSteam) as MapData;

            dataSteam.Close();
            return saveData;
        }
        else
        {
            //File doesn't exist
            Debug.LogError("No save file found in " + filePath);
            return null;
        }
    }
}
