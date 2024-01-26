using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleAction : MonoBehaviour
{

    public bool activated = false;

    public GameObject target;

    [SerializeField] private PushAndPull pap;

    [SerializeField] private Vector2 mousePos;
    [SerializeField] private float mousePosY_last;
    [SerializeField] private float displacement;
    public float sensitivity = 0.75f;

    public float pullForce = 10f;
    public float gravProximity = 2f;
        public float turnProximity = 20f;
    public bool inProximity;
    public float gravProxRatio = 0.5f;
        [SerializeField] private float gravBubble;

    [SerializeField] private float distanceTotal;
    [SerializeField] private float distanceCurr;
    [SerializeField] private float distanceFraction;

    [SerializeField] private Quaternion savedRotation;
        private Transform savedTransform;
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
            
            distanceCurr = Vector3.Distance(target.transform.position, transform.position) - (gravProximity + 5f);
            
            if (distanceCurr <= turnProximity) { 

                distanceFraction = 1.0f - (distanceCurr/turnProximity);
                middleRotation = Quaternion.Lerp(savedRotation, targetRotation, distanceFraction);

                transform.rotation = middleRotation;
            
            }

            var move = Input.GetAxis("Mouse Y");

            if (Mathf.Abs(move) > sensitivity) {

                if (move < 0 && Vector3.Distance(transform.position, target.transform.position) >= gravProximity) {

                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -move * pullForce);

                } else if (move >= 0) {

                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, -move * pullForce);

                }

                if (distanceFraction >= 1f) {

                    inProximity = true;

                } else {

                    inProximity = false;

                }

            }

            if (distanceCurr <= gravBubble) {

                inProximity = true;

            } else {

                inProximity = false;

            }

        }

    }

    void OnDisable()
    {
        
        if (activated) {
            
            if (!inProximity) {

                transform.rotation = savedRotation;

            } else { transform.rotation = targetRotation; }

            activated = false;

            Debug.Log(" -- BYEBYE FROM GAPPLE ACTION");
            Debug.Log("transform UP from disable! " + transform.up);

            activated = false;

        }

    }

    void OnEnable()
    {

        distanceTotal = Vector3.Distance(target.transform.position, transform.position) - (gravProximity + 5f);

        pap.DisableControl(false, false, false);
        mousePosY_last = mousePos.y;
        
        savedRotation = transform.rotation;
            savedTransform = transform;

        targetRotation = target.transform.rotation;
        activated = true;

        Debug.Log("HI FROM GRAPPLE ACTION");

    }

    float CalculateAngle(float angle) {

        if(angle <= 180f)
        {
            return angle;
        }
        else
        {
            return angle - 360f;
        }

    }

}
