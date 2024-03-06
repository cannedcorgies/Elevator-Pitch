using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullAction : MonoBehaviour
{
    public bool activated = false;

    public GrapplePoint gp;
        public float lightScale = 2f;
        public float savedLight;

    private AudioSource audz;
        public AudioClip clickOn;
        public AudioClip clickOff;

    public GameObject target;

    private PushAndPull pap;
        private CustomGravity cg;

    private Vector3 savedPullDir;

    //variables for sliding in/out
    public float drag = 5f;
    public float pullForce = 10f;
    [SerializeField] private float pullVel;
    [SerializeField] private float pullRatio;

    // variables for determining closeness to gravity shift
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;
        public float offsetScale;
    [SerializeField] private float distanceTotal;
        [SerializeField] private float distanceCurr;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;

        pap = GetComponent<PushAndPull>();

    }

    // Update is called once per frame
    void Update()
    {

        if (activated) {
            
            
            // ==== PULLING ACTION
            var move = Input.GetAxis("Mouse Y");

            pullVel += (-move * pullForce);     // you're moving!

            pullRatio += pullVel;       // displace according to velocity

            if (pullRatio < 0f) {       // for now, go backwards from starting point

                pullRatio = 0f;

            }

            pap.target.transform.position = Vector3.MoveTowards(startPos, targetPos, pullRatio);   // actual movement

            pullVel = Mathf.Lerp(pullVel, 0f, Time.deltaTime * drag);   // pullVel drag
            

        }

    }

    void OnDisable()
    {
        // wot
        if (activated) {

            var targetCg = pap.target.GetComponent<CustomGravity>();

            if (targetCg) {

                targetCg.activated = true;

            }

            var targetRb = pap.target.GetComponent<Rigidbody>();
            if (targetRb) {

                targetRb.isKinematic = false;

            }
            
            activated = false;      // i'm a stupid coder so of course i set activated to false twice in a row

        }

    }

    void OnEnable()
    {

        pap.DisableControl(false, false, false);
        
        pullRatio = 0f;
        pullVel = 0f;
        startPos = pap.target.transform.position;
        var dir = startPos - transform.position;
            dir = dir.normalized;
        targetPos = transform.position - (dir * offsetScale);

        var targetCg = pap.target.GetComponent<CustomGravity>();

        if (targetCg) {

            targetCg.activated = false;

        }

        var targetRb = pap.target.GetComponent<Rigidbody>();
        if (targetRb) {

            targetRb.isKinematic = true;

        }

        activated = true;

    }
}
