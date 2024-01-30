using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAction : MonoBehaviour
{

    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float rotatePower = 1f;

    // Start is called before the first frame update
    void Start()
    {

        activated = false;
        pap = GetComponent<PushAndPull>();
        
    }

    // Update is called once per frame
    void Update()
    {

        var move = Input.GetAxis("Mouse Y");

        if (activated) {

            target.transform.RotateAround(target.transform.position, transform.right, move * rotatePower * Time.deltaTime);     // rotate up and down

        }
        
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

    }

}
