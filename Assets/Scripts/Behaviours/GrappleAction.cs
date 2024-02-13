using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{

    public bool activated = false;

    public GameObject target;

    private PushAndPull pap;
        private CustomGravity cg;

    private Vector3 savedPullDir;

    // variables for determining when player shifts gravity towards grapple point
    public float pullForce = 10f;
    public float gravProximity = 2f;
    public bool inProximity;

    //variables for sliding in/out
    public float drag = 5f;
    [SerializeField] private float pullVel;
    [SerializeField] private float pullRatio;

    // variables for determining closeness to gravity shift
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float distanceTotal;
        [SerializeField] private float distanceCurr;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;
        
        pap = GetComponent<PushAndPull>();
        cg = GetComponent<CustomGravity>();

    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {
            
            
            distanceCurr = Vector3.Distance(target.transform.position, transform.position);  // get current distance to point before you start turning
            

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
            if (distanceCurr <= gravProximity) {   // if within a certain range of grapple point...

                Debug.Log("IN!");

                inProximity = true;     // ..gravity change
                cg.pullDir = -target.transform.up;

            } else {

                inProximity = false;
                cg.pullDir = savedPullDir;

            }

        }

    }

    void OnDisable()
    {
        // wot
        if (activated) {
            
            activated = false;      // i'm a stupid coder so of course i set activated to false twice in a row

        }

    }

    void OnEnable()
    {

        distanceTotal = Vector3.Distance(target.transform.position, transform.position) - (gravProximity + 5f);

        savedPullDir = cg.pullDir;

        pap.DisableControl(false, false, false);
        
        pullRatio = 0f;
        pullVel = 0f;
        startPos = transform.position;

        activated = true;

    }

}
