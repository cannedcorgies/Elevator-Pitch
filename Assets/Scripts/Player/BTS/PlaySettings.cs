using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlaySettings : MonoBehaviour
{

    public bool grayscale = false;
        public Volume vol;
        public ColorAdjustments ca;
        public float savedSaturation;

    public float camSensitivity = 1f;
        public FirstPersonCamera fpc;
        public float savedCamSensitivity;

    public float pushPullScale = 1f;
        public PushAndPull pap;

    // Start is called before the first frame update
    void Start()
    {
        
        vol.profile.TryGet<ColorAdjustments>(out ca);
            savedSaturation = ca.saturation.value;

        savedCamSensitivity = fpc.mouseSensitivity;
        grayscale = PlayerPrefs.GetInt("grayscale") == 1 ? true : false;
        pushPullScale = PlayerPrefs.GetFloat("controls");
        camSensitivity = PlayerPrefs.GetFloat("camera");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("grayscale1: " + grayscale);
        if (grayscale) {

            ca.saturation.value = -100f;

        } else {

            ca.saturation.value = savedSaturation;

        }

        fpc.mouseSensitivity = savedCamSensitivity * camSensitivity;

        pap.pushPullScale = pushPullScale;

    }
}
