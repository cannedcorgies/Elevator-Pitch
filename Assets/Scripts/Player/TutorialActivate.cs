using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialActivate : MonoBehaviour
{

    public bool inTrigger;

    public float activateTime;
        public float nextActivate;
        public bool mouseActivated;

    public float blinkTime;
        public float nextBlink;

    public GameObject mouseOff;
        public GameObject mouseOn;

    // Start is called before the first frame update
    void Start()
    {
    
        inTrigger = false;
        nextActivate = Time.time + activateTime; 

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActivate && inTrigger) {

            if (!mouseActivated) {
                
                nextBlink = Time.time + blinkTime;
                mouseActivated = true;

            }

        } 
        
        if (!inTrigger) {

            mouseOff.SetActive(false);
            mouseOn.SetActive(false);

        }

        if (mouseActivated) {

            mouseOff.SetActive(true);

            if (Time.time > nextBlink) {

                if (!mouseOn.activeSelf) {

                    mouseOn.SetActive(true);

                } else {

                    mouseOn.SetActive(false);

                }

                nextBlink = Time.time + blinkTime;

            }

        }
        
    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "Tutorial") {

            inTrigger = true;
            nextActivate = Time.time + activateTime; 

        }

    }

    void OnTriggerExit(Collider other) {

        if (other.tag == "Tutorial") {

            inTrigger = false;
            mouseActivated = false;

        }

    }

}
