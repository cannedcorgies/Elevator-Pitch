using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    public bool shrinkActivated;
        public GameObject shrinkTarget;
    public float shrinkFactor = 0.3f;

    public float savedSpeed;

    // Start is called before the first frame update
    void Start()
    {

        shrinkActivated = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shrinkTarget) {
            
            if (shrinkActivated) {

                var currScale = shrinkTarget.transform.localScale.x;
                var currShrinkFactor = Mathf.Lerp(currScale, shrinkFactor, Time.deltaTime);
                //Debug.Log("curr shrink: " + currShrinkFactor + " -- curr scale: " + currScale);

                shrinkTarget.transform.localScale = new Vector3 (currShrinkFactor, currShrinkFactor, currShrinkFactor);

            } else {

                var currScale = shrinkTarget.transform.localScale.x;
                var currShrinkFactor = Mathf.Lerp(currScale, 1f, Time.deltaTime);
                //Debug.Log("curr shrink: " + currShrinkFactor + " -- curr scale: " + currScale);

                shrinkTarget.transform.localScale = new Vector3 (currShrinkFactor, currShrinkFactor, currShrinkFactor);

                if (1f - Mathf.Abs(currScale) < 0.01) {

                    shrinkTarget.transform.localScale = new Vector3 (1f, 1f, 1f);
                    shrinkTarget = null;

                }

            }

        }
        
    }

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Player") {

            shrinkActivated = true;
            shrinkTarget = col.gameObject;
            savedSpeed = shrinkTarget.GetComponent<Movement>().moveSpeed;
            shrinkTarget.GetComponent<Movement>().moveSpeed *= shrinkFactor;

        }

    }

    void OnTriggerExit(Collider col) {

        if (col.gameObject.tag == "Player") {

            shrinkActivated = false;
            shrinkTarget.GetComponent<Movement>().moveSpeed = savedSpeed;
            //col.gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);

        }

    }

}
