using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{

    public bool activated;
        public bool imHere;

    public float pushPullScale = 1f;

    public int click = 1;

    public LayerMask ignoreLayer;

    public FirstPersonCamera fpc;
        public Movement movement;
        public CustomGravity cg;
            public Rigidbody rb;
            public Collider playerCol;

    public List<MonoBehaviour> behaviors;
        [SerializeField] private bool behaviorActivated;
        public GrappleAction ga;
        public RotateAction ra;
        public SlideAction sa;
        public ScaleAction sca;
        public BouncepadAction ba;
        public PullAction pa;
        public LightAction la;

        public ElevatorAction ea;
    
    public Camera cam;
        public GameObject cameraPivot;

    public GameObject crosshair;
        private Image crosshairImage;
    public GameObject hand;
        private Image handImage;

    [SerializeField] public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        imHere = false;

        cam = Camera.main;

        // movement components
        fpc = cam.gameObject.GetComponent<FirstPersonCamera>();
        movement = GetComponent<Movement>();
        cg = GetComponent<CustomGravity>();
            rb = GetComponent<Rigidbody>();
            playerCol = GetComponent<Collider>();
        
        crosshairImage = crosshair.GetComponent<Image>();
        handImage = hand.GetComponent<Image>();

        // all actions possible
        ga = GetComponent<GrappleAction>();
        ra = GetComponent<RotateAction>();
        sa = GetComponent<SlideAction>();
        sca = GetComponent<ScaleAction>();
        ba = GetComponent<BouncepadAction>();
        ea = GetComponent<ElevatorAction>();
        pa = GetComponent<PullAction>();
        la = GetComponent<LightAction>();

    }

    // Update is called once per frame
    void Update()
    {

        // ==== ACTION MANAGEMENT ====

        if (!imHere) {          // first frame, not 0th frame - prevents error and i don't know why

            imHere = true;

            ga.enabled = false;
            ra.enabled = false;
            sa.enabled = false;
            sca.enabled = false;
            ba.enabled = false;
            ea.enabled = false;
            pa.enabled = false;
            la.enabled = false;

        }

        BehaviorManagement();
        

        // ==== COMPONENT DETECTION ====

        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer)) {    // if somethin hit...

            var hitObject = hit.transform.gameObject;

            var behaviorFound = FindABehavior(hitObject);   // see if hit object has a valid component

            // ==== CROSSHAIR COLORU ====
            if (behaviorFound) {

                crosshairImage.color = Color.red;
                hand.SetActive(true);

            } else {

                crosshairImage.color = Color.white;
                hand.SetActive(false);

            }

        } else {

            crosshairImage.color = Color.white;

        }

    }

    // FIND A BEHAVIOUR
    //  - check if object passed has a valid
    //      component
    bool FindABehavior(GameObject hitObject) {

        var objectFound = false;

        // if grapple point found
        if (hitObject.GetComponent<GrapplePoint>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                ga.target = target;
                ga.enabled = true;

            }

        }

        // if rotating component found
        if (hitObject.GetComponent<RotateComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                ra.target = target;
                ra.enabled = true;

            }

        }

        // if rotating component found
        if (hitObject.GetComponent<SlideComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                sa.target = target;
                sa.enabled = true;

            }

        }

        // if rotating component found
        if (hitObject.GetComponent<ScaleComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                sca.target = target;
                sca.enabled = true;

            }

        }

        // if bouncepad component found
        if (hitObject.GetComponent<BouncepadComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                ba.target = target;
                ba.enabled = true;

            }

        }

        if (hitObject.GetComponent<ElevatorComponent>()) {

            // Debug.Log("WE ARE IN THIS IF STATEMENT FOR ELEVATOR COMPONENT");
            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                Debug.Log("the button has been clicked");

                behaviorActivated = true;
                ea.target = target;
                ea.enabled = true;

            }

        }

        // if bouncepad component found
        if (hitObject.GetComponent<PullComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                pa.target = target;
                pa.enabled = true;

            }

        }

        // if bouncepad component found
        if (hitObject.GetComponent<LightComponent>()) {

            objectFound = true;
            target = hitObject;

            if (Input.GetMouseButtonDown(click)) {

                behaviorActivated = true;
                la.target = target;
                la.enabled = true;

            }

        }


        // was a behavior found?
        if (objectFound || behaviorActivated) {
            return true;
        } else {
            return false;
        }

    }

    // DISABLE CONTROL 
    //  - disable movement options for better 
    //      manipulation of objects
    public void DisableControl(bool move, bool cam, bool grav, bool col = false) {

        movement.enabled = move;
        fpc.enabled = cam;
        cg.activated = grav;
        
        if (!grav) {

            rb.velocity = Vector3.zero;

        }

    }

    // ENABLE CONTROL 
    //  - restore control/movement options
    public void EnableControl() {

        behaviorActivated = false;
        movement.enabled = true;
        fpc.enabled = true;
        cg.activated = true;
        //playerCol.enabled = true;
        //rb.isKinematic = false;

    }

    public void BehaviorManagement() {

        if (Input.GetMouseButtonUp(click) && behaviorActivated) {

            ga.enabled = false;
            ra.enabled = false;
            sa.enabled = false;
            sca.enabled = false;
            ba.enabled = false;
            ea.enabled = false;
            pa.enabled = false;
            la.enabled = false;
            EnableControl();

        }

    }

    // CAMERA LOOK AT TARGET
    //  - void -- not used anymore
    public void CameraLookAtTarget() {

        Vector3 relativePos = target.transform.position - cam.gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        cam.transform.rotation = rotation;

    }

}
