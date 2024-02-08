using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : MonoBehaviour
{

    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float rotatePower = 5f;
        public float rotateThresh = 50f;

    private float pos;
        private float savedPos;

    [SerializeField] private Vector3 rotateAxis;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;
        pap = GetComponent<PushAndPull>();

        ResetMouse();

        
    }

    // Update is called once per frame
    void Update()
    {

        var move = Input.GetAxis("Mouse Y");

        pos += (move * rotatePower);
        

        if (activated) {

            if (pos >= savedPos + rotateThresh) {

                target.transform.RotateAround(target.transform.position, rotateAxis, 45f);
                ResetMouse();

            } else if (pos <= savedPos - rotateThresh) {

                target.transform.RotateAround(target.transform.position, rotateAxis, -45f);
                ResetMouse();

            }

        }
        
    }

    void ResetMouse() {

        pos = 0f;
        savedPos = 0f;

    }

    void OnDisable()
    {
        
        if (activated) {
            
            activated = false;

        }

    }

    void OnEnable()
    {

        pap.DisableControl(false, false, false);
        activated = true;

        ResetMouse();

        Debug.Log(target.transform.position - transform.position);

        var xAxis = Mathf.Round(transform.right.x);
        var yAxis = Mathf.Round(transform.right.y);
        var zAxis = Mathf.Round(transform.right.z);

        rotateAxis = new Vector3(xAxis, yAxis, zAxis);

    }

}
