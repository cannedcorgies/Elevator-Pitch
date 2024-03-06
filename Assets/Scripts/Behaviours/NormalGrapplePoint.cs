using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGrapplePoint : MonoBehaviour
{

    public GameObject myLight;
    public Light lightControl;

    // Start is called before the first frame update
    void Start()
    {

        lightControl = myLight.GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
