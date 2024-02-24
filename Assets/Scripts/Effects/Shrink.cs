using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{

    public bool shrinkActivated;
        public GameObject shrinkTarget;
    public float shrinkFactor = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

        shrinkActivated = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shrinkActivated) {

            var currScale = shrinkTarget.transform.localScale.x;
            var currShrinkFactor = Mathf.Lerp(currScale, shrinkFactor, Time.deltaTime);
            Debug.Log("curr shrink: " + currShrinkFactor + " -- curr scale: " + currScale);

            shrinkTarget.transform.localScale = new Vector3 (currShrinkFactor, currShrinkFactor, currShrinkFactor);

        }
        
    }

    void OnTriggerEnter(Collider col) {

        if (col.gameObject.tag == "Player") {

            shrinkActivated = true;
            shrinkTarget = col.gameObject;

        }

    }

    void OnTriggerExit(Collider col) {

        if (col.gameObject.tag == "Player") {

            shrinkActivated = false;
            col.gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);

        }

    }

}
