using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    // assign in the inspector
    public LevelLoader levelLoader;

    public void PlayGame() {
        // load the level (assuming scene index is 1 which it is rn thankfully)
        levelLoader.LoadLevel(1);
    }

    // load the game from a save file
    public void LoadGame() {
        SaveSystem.Instance.LoadGame();
        levelLoader.LoadLevel(1);
    }

    public void QuitGame() {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
