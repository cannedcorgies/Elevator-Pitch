//// CREDITS ////
//  - inspired by mixandjam's "Better Jumping"
//      - https://github.com/mixandjam/Celeste-Movement/blob/master/Assets/Scripts/BetterJumping.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    private Rigidbody rb;
    private GroundCheck gc;
    
    public float fallMult = 2.5f;

    public float bounciness = 0.5f;  // 1 for no change -- 1< for increased bounciness
    public Vector3 pullDir;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        gc = GetComponent<GroundCheck>();

        Debug.Log(-transform.up * (fallMult - 1));
        pullDir = -transform.up;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!gc.grounded) {

            rb.velocity += pullDir * (fallMult - 1) * Time.deltaTime;

        }
        
    }

    void OnCollisionEnter(Collision col) {
        
        rb.velocity -= Vector3.Reflect(col.relativeVelocity * bounciness, col.contacts[0].normal);

    }

    void OnEnable() {

        pullDir = -transform.up;

    }

}
