using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PushAndPull : MonoBehaviour
{

    public bool activated;
        public bool imHere;

    public int click = 1;

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
    
    public Camera cam;
        public GameObject cameraPivot;

    public GameObject crosshair;
        public Image crosshairImage;

    [SerializeField] public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        imHere = false;

        cam = Camera.main;

        fpc = cam.gameObject.GetComponent<FirstPersonCamera>();
        movement = GetComponent<Movement>();
        cg = GetComponent<CustomGravity>();
            rb = GetComponent<Rigidbody>();
            playerCol = GetComponent<Collider>();
        
        crosshairImage = crosshair.GetComponent<Image>();

        ga = GetComponent<GrappleAction>();
        ra = GetComponent<RotateAction>();
        sa = GetComponent<SlideAction>();
        sca = GetComponent<ScaleAction>();

    }

    // Update is called once per frame
    void Update()
    {

        if (!imHere) {

            imHere = true;

            ga.enabled = false;
            ra.enabled = false;
            sa.enabled = false;
            sca.enabled = false;

        }

        BehaviorManagement();
        
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            var hitObject = hit.transform.gameObject;

            var behaviorFound = FindABehavior(hitObject);

            // change crosshair color based on whether or not object has behavior
            if (behaviorFound) {

                crosshairImage.color = Color.red;

            } else {

                crosshairImage.color = Color.white;

            }

        } else {

            crosshairImage.color = Color.white;

        }

    }

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


        // was a behavior found?
        if (objectFound || behaviorActivated) {
            return true;
        } else {
            return false;
        }

    }

    public void DisableControl(bool move, bool cam, bool grav, bool col = false) {

        movement.enabled = move;
        fpc.enabled = cam;
        cg.enabled = grav;
        playerCol.enabled = col;
        
        if (!grav) {

            rb.velocity = Vector3.zero;

        }

    }

    public void EnableControl() {

        behaviorActivated = false;
        movement.enabled = true;
        fpc.enabled = true;
        cg.enabled = true;
        playerCol.enabled = true;

    }

    public void BehaviorManagement() {

        if (Input.GetMouseButtonUp(click) && behaviorActivated) {

            ga.enabled = false;
            ra.enabled = false;
            sa.enabled = false;
            sca.enabled = false;
            EnableControl();

        }

    }

    public void CameraLookAtTarget() {

        Vector3 relativePos = target.transform.position - cam.gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        cam.transform.rotation = rotation;

    }

}
