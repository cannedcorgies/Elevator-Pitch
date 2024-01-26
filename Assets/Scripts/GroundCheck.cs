using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public LayerMask groundLayer;

    public bool grounded;
    public float collisionRadius = 0.25f;
    public Vector3 bottomOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.OverlapSphere(transform.position + bottomOffset, collisionRadius, groundLayer).Length > 0) {

            grounded = true;

        } else {

            grounded = false;

        }

    }
}
