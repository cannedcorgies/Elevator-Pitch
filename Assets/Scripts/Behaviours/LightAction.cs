using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAction : MonoBehaviour
{
    public bool activated = false;

    public GameObject target;
        public LightFlickerEffect light;

    private PushAndPull pap;
    
    //variables for sliding in/out
    public float drag = 5f;
    public float pullForce = 0.005f;
    [SerializeField] private float pullVel;

    public float gap;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;

        pap = GetComponent<PushAndPull>();

    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {
            
            
            // ==== PULLING ACTION
            var move = Input.GetAxis("Mouse Y");

            pullVel += (-move * pullForce);     // you're moving!

            light.maxIntensity += pullVel;
            light.minIntensity += pullVel;

            if (light.maxIntensity < gap) { light.maxIntensity = gap; }
            if (light.minIntensity < 0) { light.minIntensity = 0; }

            pullVel = Mathf.Lerp(pullVel, 0f, Time.deltaTime * drag);   // pullVel drag
            

        }

    }

    void OnDisable()
    {
        // wot
        if (activated) {

            activated = false;      // i'm a stupid coder so of course i set activated to false twice in a row

        }

    }

    void OnEnable()
    {

        pap.DisableControl(false, false, false);
        
        pullVel = 0f;

        light = pap.target.GetComponent<LightComponent>().myLight.GetComponent<LightFlickerEffect>();

        var max = light.maxIntensity;
        var min = light.minIntensity;

        gap = max - min;

        activated = true;

    }

}
