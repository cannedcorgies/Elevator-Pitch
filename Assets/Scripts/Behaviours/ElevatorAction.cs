using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAction : MonoBehaviour
{

    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float slidePower = 10f;
    public float maxDistance = 5f;
    public Vector3 maxVector;

    void Start()
    {
        // Debug.Log("CHECKKKKKKKK BELOW");
        // Debug.Log( target.name);

        activated = false;
        pap = GetComponent<PushAndPull>();
        
    }
  
    // Update is called once per frame
    void Update()
    {
        var move = Input.GetAxis("Mouse Y");

        if (activated) {

            var dir = transform.up * move * Time.deltaTime * slidePower;
            target.transform.position += dir;

            // ==== LIMIT DISPLACEMENT
            target.transform.position = new Vector3 (Mathf.Clamp(target.transform.position.x, maxVector.x - maxDistance, maxVector.x + maxDistance),
                                                        Mathf.Clamp(target.transform.position.y, maxVector.y - maxDistance, maxVector.y + maxDistance),
                                                        Mathf.Clamp(target.transform.position.z, maxVector.z - maxDistance, maxVector.z + maxDistance));

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

        var slidey = target.gameObject.GetComponent<ElevatorComponent>();
        Debug.Log("this is slidey", slidey);
        // basically, max distance defined as a square around origin
        maxVector = slidey.startPos;
        maxDistance = slidey.maxDistance;

        

    }

 
}
