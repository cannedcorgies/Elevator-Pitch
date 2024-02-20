using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light flickerLight;
    public float minTime;
    public float maxTime;
    public float timer;

    public AudioSource audioSource;
    public AudioClip lightAudio;

    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
    }

    void Flicker() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) {
            flickerLight.enabled = !flickerLight.enabled;
            timer = Random.Range(minTime, maxTime);
            audioSource.PlayOneShot(lightAudio);
        }
    }
}
