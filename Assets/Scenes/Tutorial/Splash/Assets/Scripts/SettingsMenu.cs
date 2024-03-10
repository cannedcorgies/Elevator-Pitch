using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static int test = 5;
    // [SerializeField] private Slider brightnessSlider;
    // [SerializeField] private Image overLay;

    public void Start() {
        if (PlayerPrefs.HasKey("music")) {
            LoadVolume();
        } else {
            SetMusicVolume(0.62f);
            SetSFXVolume(0.62f);
        }
        // SetMusicVolume(PlayerPrefs.GetFloat("music"));
        // SetSFXVolume(PlayerPrefs.GetFloat("sfx"));
    }

    public void SetMusicVolume (float volume) {
        // Debug.Log(volume);
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("music", volume);
    }

    public void SetSFXVolume (float volume) {
        // Debug.Log(volume);
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);
    }

    // public void SetBrightness (float alpha) {
    //     var tempColor = overLay.color;
    //     tempColor.a = brightnessSlider.value;
    //     overLay.color = tempColor; 
    // }

    private void LoadVolume() {
        SetMusicVolume(PlayerPrefs.GetFloat("music"));
        SetSFXVolume(PlayerPrefs.GetFloat("sfx"));
    }
}
