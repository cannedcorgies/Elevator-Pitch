using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    // public SettingsMenu test;
    // Start is called before the first frame update
    void Start()
    {
        SettingsMenu.test += 1;
        Debug.Log("test variable: " + SettingsMenu.test);
        Debug.Log("playerpref music: " + PlayerPrefs.GetFloat("music"));
        Debug.Log("playerpref sfx: " + PlayerPrefs.GetFloat("sfx"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
