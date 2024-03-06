// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public GameObject player;
    
    void SaveGame() {
        PlayerData dataToSave = new PlayerData {
            playerPos = player.transform.position
        };

        BinaryFormatter formatter = new BinaryFormatter(); 
        string path = Application.persistentDataPath + "/test.no";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, dataToSave);
        stream.Close();
    }

    void LoadGame() {
        string path = Application.persistentDataPath + "/test.no";

        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();


            player.transform.position = playerData.playerPos;
        }
        else {
            Debug.LogError("No save file in " + path + " dummy!!");
        }
    }
}
