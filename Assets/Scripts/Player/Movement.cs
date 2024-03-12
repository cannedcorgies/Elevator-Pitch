using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    
    public float moveSpeed = 300f;
    public Vector3 dir;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update() {

        dir = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, Input.GetAxis("Vertical")).normalized;

    }

    void FixedUpdate()
    {

        rb.MovePosition(transform.position 
            + (transform.forward * dir.z * moveSpeed) 
            + (transform.right * dir.x * moveSpeed));


        //rb.velocity = dir * moveSpeed;

        //rb.AddForce(dir * moveSpeed);

    }

}
