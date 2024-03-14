using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public PlayerData loadedGameData;
    public bool isGameLoaded = false;

    // awake runs before Start() and this is important to not cause errors
    // with variables initializing in the wrong order
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    
    public void SaveGame() {

        // Debug.Log("WE ARE SAVING I THINK");

        PlayerData dataToSave = new PlayerData();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            // convert vector3 to something that is serializable
            dataToSave.playerPos = SerializableVector3.ToSerializeable(player.transform.position);
        }

        // create a path for the save file to go to
        string path = Application.persistentDataPath + "/test.no";
        BinaryFormatter formatter = new BinaryFormatter(); // using a binary formatter

        // serialize the data
        using (FileStream stream = new FileStream(path, FileMode.Create)) {
            formatter.Serialize(stream, dataToSave);
        }
        Debug.Log("Game saved");
    }

    public void LoadGame() {
        string path = Application.persistentDataPath + "/test.no";

        // using a binary formatter
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();

            // deserialize the save data from the path
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                loadedGameData = data;
            }

            isGameLoaded = true;
            Debug.Log("Game loaded");
        } 
        else {
            Debug.LogError("No save file found in " + path + " dummy!!!");
        }

        
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (isGameLoaded) {
            ApplyLoadedGameData();
        }
    }

    void ApplyLoadedGameData() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        // make sure player and game data exist
        if (player != null && loadedGameData != null) {
            // player.transform.position = loadedGameData.playerPos;
            player.transform.position = loadedGameData.playerPos.ToVector3();
            isGameLoaded = false;
        }
    }
    
    // for now just save the game when pressing K
    void Update() {
        if (Input.GetKeyDown("k")) { 
            SaveGame();
        }
    }
        
}
