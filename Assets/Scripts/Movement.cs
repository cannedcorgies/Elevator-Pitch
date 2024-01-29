using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb;
    
    public float moveSpeed = 300f;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")).normalized;

        rb.MovePosition(transform.position 
            + (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) 
            + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime));

    }

}
