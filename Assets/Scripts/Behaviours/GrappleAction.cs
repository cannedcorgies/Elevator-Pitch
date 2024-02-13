using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{

    public bool activated = false;

    public GameObject target;

    private PushAndPull pap;

    // variables for determining when player shifts gravity towards grapple point
    public float pullForce = 10f;
    public float gravProximity = 2f;
        public float turnProximity = 20f;
    public bool inProximity;
    public float gravProxRatio = 0.5f;
        [SerializeField] private float gravBubble;
    public float drag = 5f;
    [SerializeField] private float pullVel;
    [SerializeField] private float pullRatio;

    // variables for determining closeness to gravity shift
    [SerializeField] private Vector3 startPos;
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

            pullVel += (-move * pullForce);     // you're moving!

            pullRatio += pullVel;       // displace according to velocity

            if (pullRatio < 0f) {       // for now, go backwards from starting point

                pullRatio = 0f;

            }

            transform.position = Vector3.MoveTowards(startPos, target.transform.position, pullRatio);   // actual movement

            pullVel = Mathf.Lerp(pullVel, 0f, Time.deltaTime * drag);   // pullVel drag
            

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

        pullRatio = 0f;
        pullVel = 0f;
        startPos = transform.position;

        activated = true;

    }

}
