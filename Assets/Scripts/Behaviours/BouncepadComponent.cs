using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncepadComponent : MonoBehaviour
{

    public float bounciness = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        
        Debug.Log("COLLIDED");

        var rb = col.gameObject.GetComponent<Rigidbody>();

        if (rb) {

            Debug.Log("AHH SHOULD BOUNCE");

            rb.velocity += Vector3.Reflect(col.relativeVelocity * bounciness, col.contacts[0].normal);

        }

    }

}
