using UnityEngine;
using System.Collections.Generic;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class LightFlickerEffect : MonoBehaviour {
    
    public new Light light;
    
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;

    void Start() {
        
        smoothQueue = new Queue<float>(smoothing);
        
        if (light == null) {
            
            light = GetComponent<Light>();
        
        }

        maxIntensity = light.intensity;
        minIntensity = light.intensity * 0.75f;
        
    }

    void Update() {
        if (light == null)
            return;

        // pop off an item if too big
        while (smoothQueue.Count >= smoothing) {
            lastSum -= smoothQueue.Dequeue();
        }

        // Generate random new item, calculate new average
        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // Calculate new smoothed average
        light.intensity = lastSum / (float)smoothQueue.Count;
    }

    public void Reset() {

        smoothQueue.Clear();
        lastSum = 0;

    }

}