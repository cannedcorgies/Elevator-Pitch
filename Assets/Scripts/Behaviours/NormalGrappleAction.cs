using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGrappleAction : MonoBehaviour
{

    public bool activated = false;
    // public GrapplePoint gp;
    public NormalGrapplePoint ngp;
    public float lightScale = 2f;
    public float savedLight;
    private AudioSource audz;
    public AudioClip clickOn;
    public AudioClip clickOff;
    public GameObject target;
    private PushAndPull pap;

    //variables for sliding in/out
    public float drag = 5f;
    public float pullForce = 10f;
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
        audz = GetComponent<AudioSource>();
        pap = GetComponent<PushAndPull>();
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

        }

    }

    void OnDisable()
    {
        // wot
        audz.PlayOneShot(clickOff);
        ngp.lightControl.intensity = savedLight;

        if (activated) {
            
            activated = false;

        }

    }

    void OnEnable()
    {

        ngp = target.GetComponent<NormalGrapplePoint>();
        savedLight = ngp.lightControl.intensity;
        ngp.lightControl.intensity *= lightScale;

        audz.PlayOneShot(clickOn);

        // distanceTotal = Vector3.Distance(target.transform.position, transform.position);

        // savedPullDir = cg.pullDir;

        pap.DisableControl(false, false, false);
        
        // pullRatio = 0f;
        // pullVel = 0f;
        // startPos = transform.position;

        activated = true;

    }

}