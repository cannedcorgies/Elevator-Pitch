using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAction : MonoBehaviour
{

    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float sens = 50f;

    float minZ = -45;
    float maxZ = 45;

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

             // if mouse is being pulled down then rotate the lever accordingly
             if (move < 0) {
                // rotate based on mouse pulling
                target.transform.Rotate(0, 0, sens * Time.deltaTime);

                // clamp the rotation to a certain range so it doesnt pass a certain range
                Vector3 currentRotation = target.transform.localEulerAngles;
                currentRotation.z = ClampAngle(currentRotation.z, minZ, maxZ);
                target.transform.localEulerAngles = currentRotation;
             }
             // if mouse is being pushed move the lever up
             else if (move > 0) {
                // rotate based on mouse pulling
                // in this case invert the movement so it goes up instead of down
                target.transform.Rotate(0, 0, -sens * Time.deltaTime);

                // clamp the rotation to a certain range so it doesnt pass a certain range
                Vector3 currentRotation = target.transform.localEulerAngles;
                currentRotation.z = ClampAngle(currentRotation.z, minZ, maxZ);
                target.transform.localEulerAngles = currentRotation;
             }

        }

        // reset the lever so that its back at the top
        if (NormalizeAngle(target.transform.localEulerAngles.z) >= 40f) {
            Debug.Log("are we in here???");
            target.transform.Rotate(0, 0, -70f * Time.deltaTime);

            Vector3 currentRotation = target.transform.localEulerAngles;
            currentRotation.z = ClampAngle(currentRotation.z, minZ, maxZ);
            target.transform.localEulerAngles = currentRotation;
            // Debug.Log(target.name);
        }
        
    }

    // return an angle that is clamped between the range that is passed in
    float ClampAngle(float angle, float min, float max) {
        angle = NormalizeAngle(angle);
        return Mathf.Clamp(angle, min, max);
    }

    // normalizes the angle so that it is in the range of -180 to 180
    // instead of 0 to 360
    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
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