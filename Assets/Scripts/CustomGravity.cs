//// CREDITS ////
//
//  - inspired by mixandjam's "Better Jumping"
//      - https://github.com/mixandjam/Celeste-Movement/blob/master/Assets/Scripts/BetterJumping.cs
//
//// REQUIREMENTS ////
//
//  - rigidbody
//  - groundcheck

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{

    private Rigidbody rb;
    private GroundCheck gc;
    public GameObject camera;
    private FirstPersonCamera fpc;
    
    public float fallMult = 2.5f;

    public float bounciness = 0.5f;  // 1 for no change -- 1< for increased bounciness
    public Vector3 pullDir;
        public float correctionScalar = 1f;

    [SerializeField] private bool irregular;
        private bool adjusting;
            private bool adjustingClick;
        public float adjustingTime = 0.5f;
        private float timePassed = 0f;
        private Vector3 newUp;
        private Vector3 oldUp;
        private Vector3 newForward;
        private Vector3 oldForward;
        private Vector3 combinedAxis;
    
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        gc = GetComponent<GroundCheck>();
        fpc = camera.GetComponent<FirstPersonCamera>();

        Debug.Log(-transform.up * (fallMult - 1));
        pullDir = -transform.up;

        adjusting = false;
        adjustingClick = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!gc.grounded) {

            rb.velocity += pullDir * (fallMult - 1) * Time.deltaTime;

        }

        Quaternion upRotation = Quaternion.FromToRotation(transform.up, -pullDir);
        Quaternion newRotation = Quaternion.Slerp(rb.rotation, upRotation * rb.rotation, Time.fixedDeltaTime * correctionScalar);
        rb.MoveRotation(newRotation);
        
    }

    //  ==== BOUNCING BEHAVIOR
    void OnCollisionEnter(Collision col) {
        
        rb.velocity -= Vector3.Reflect(col.relativeVelocity * bounciness, col.contacts[0].normal);

        ContactPoint contact = col.contacts[0];
        if (!CheckIrregular(col.gameObject.transform.up.normalized) && irregular && !adjusting) {

            Debug.Log("old forward: " + transform.forward);

            var vectorDifference = contact.point - transform.position;

            var axisList = new List<float> { vectorDifference.x, vectorDifference.y, vectorDifference.z };
            var greatestUp = 0;

            greatestUp = GetGreatestAxis(axisList);

            var newAxis = new List<float> {0, 0, 0};
            newAxis[greatestUp] = 1.0f;
            
            oldUp = transform.up;
            newUp = new Vector3(newAxis[0], newAxis[1], newAxis[2]) * -1f * (vectorDifference[greatestUp]/Mathf.Abs(vectorDifference[greatestUp]));

            pullDir = -newUp;

        }

    }

    void OnEnable() {

        pullDir = -transform.up;
        irregular = CheckIrregular(transform.up);

    }

    bool CheckIrregular(Vector3 piece) {

        var checks = 0;

        var normalMe = Vector3.Normalize(piece);

        if (!Mathf.Approximately(0f, Mathf.Round(normalMe.x))) {

            checks ++;

        }

        if (!Mathf.Approximately(0f, Mathf.Round(normalMe.y))) {

            checks ++;

        }

        if (!Mathf.Approximately(0f, Mathf.Round(normalMe.z))) {

            checks ++;

        }

        if (checks > 1) {

            return true;

        } else {

            return false;

        }

    }

    int GetGreatestAxis(List<float> someVector) {

        var savedIndex = 0;

        for (int i = 0; i < someVector.Count; i++) {

            if (Mathf.Abs(someVector[i]) > Mathf.Abs(someVector[savedIndex])) {

                savedIndex = i;

            }

        }

        return savedIndex;

    }

    void AdjustAxis() {

        if (adjusting) {

            fpc.enabled = false;
            rb.velocity = Vector3.zero;

            //transform.up = Vector3.Lerp(oldUp, newUp, timePassed/adjustingTime); 
            //transform.forward = Vector3.Lerp(oldForward, newForward, timePassed/adjustingTime); 

            transform.LookAt(Vector3.Lerp(oldForward, combinedAxis, timePassed/adjustingTime));

            if (timePassed >= adjustingTime) {

                adjusting = false;
                pullDir = -transform.up;
                fpc.enabled = true;
                Debug.Log("-- new up: " + transform.up);

            }

            timePassed += Time.deltaTime;

        }

    }

}
