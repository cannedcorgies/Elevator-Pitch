using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorComponent : MonoBehaviour
{
    public Vector3 startPos;
    public float maxDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
