//// NOTES ////
//
//  - how can we make this action more predictable?
//      - is the back and forth relative to player
//          position intuitive?
//      - should we instead lock the sliding to an axis?
//
//  - also, think of more organic ways to limit the sliding
//      - e.g. stops sliding if it collides with object
//      - e.g. player stays on object

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAction : MonoBehaviour
{
    public bool activated = false;
    [SerializeField] private PushAndPull pap;

    public GameObject target;

    public float slidePower = 50f;
        public float maxDistance = 20f;
        public Vector3 maxVector;

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

            var dir = transform.forward * move * Time.deltaTime * slidePower;
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

        var slidey = target.gameObject.GetComponent<SlideComponent>();
        // basically, max distance defined as a square around origin
        maxVector = slidey.startPos;
        maxDistance = slidey.maxDistance;

    }

}
