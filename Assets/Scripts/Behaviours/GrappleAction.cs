using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{

    public bool activated = false;

    public GameObject target;

    private PushAndPull pap;

    public float sensitivity = 0.75f;

    // variables for determining when player shifts gravity towards grapple point
    public float pullForce = 10f;
    public float gravProximity = 2f;
        public float turnProximity = 20f;
    public bool inProximity;
    public float gravProxRatio = 0.5f;
        [SerializeField] private float gravBubble;

    // variables for determining closeness to gravity shift
    [SerializeField] private float distanceTotal;
    [SerializeField] private float distanceCurr;
    [SerializeField] private float distanceFraction;

    // interpolation of rotation in direction of gravity shift
    [SerializeField] private Quaternion savedRotation;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Quaternion middleRotation;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;
        
        pap = GetComponent<PushAndPull>();

        gravBubble = turnProximity * gravProxRatio;

    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {
            
            // ==== ROTATION INTERPOLATION
            distanceCurr = Vector3.Distance(target.transform.position, transform.position) - (gravProximity + 5f);  // get current distance to point before you start turning
            
            if (distanceCurr <= turnProximity) {    // if you go past the turning threshold... 

                distanceFraction = 1.0f - (distanceCurr/turnProximity);
                middleRotation = Quaternion.Lerp(savedRotation, targetRotation, distanceFraction);

                transform.rotation = middleRotation;    // ..start turning
            
            }

            // ==== PULLING ACTION
            var move = Input.GetAxis("Mouse Y");

            if (Mathf.Abs(move) > sensitivity) {

                // imma be real with you: i see that the conditionals here lead to the exact same line of code, but
                //  i don't remember why i separated them; i just remember i had a reason
                //  but also
                //  i'm a little bit quirky
                if (move < 0 && Vector3.Distance(transform.position, target.transform.position) >= gravProximity) {

                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -move * pullForce);

                } else if (move >= 0) {

                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -move * pullForce);

                }

            }

            // ==== GRAVITY SHIFTING
            if (distanceCurr <= gravBubble) {   // if within a certain range of grapple point...

                inProximity = true;     // ..gravity change

            } else {

                inProximity = false;

            }

        }

    }

    void OnDisable()
    {
        
        if (activated) {
            
            if (!inProximity) {     // if found to be sufficiently close to grapple point...

                transform.rotation = savedRotation;     // ..snap to object's direction

            } else { transform.rotation = targetRotation; } // else, revert to original

            activated = false;

            activated = false;      // i'm a stupid coder so of course i set activated to false twice in a row

        }

    }

    void OnEnable()
    {

        distanceTotal = Vector3.Distance(target.transform.position, transform.position) - (gravProximity + 5f);

        pap.DisableControl(false, false, false);
        
        // first to return to if let go prematurely, second to interpolate to
        savedRotation = transform.rotation;
        targetRotation = target.transform.rotation;

        activated = true;

    }

}
