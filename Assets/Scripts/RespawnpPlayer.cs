using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{

    public GameObject respawnPoint;
    public Camera cam;
        private FirstPersonCamera fpc;
    private CustomGravity cg;

    public float respawnDepth = -100f;
        public bool fallDeath = true;
    public float respawnDistance = 1000f;
        public bool distanceDeath = false;
    
    public Vector3 direction = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;
        fpc = cam.gameObject.GetComponent<FirstPersonCamera>();
        cg = GetComponent<CustomGravity>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y < respawnDepth && fallDeath) {

            RespawnMe();

        }

        if (Vector3.Distance(respawnPoint.transform.position, transform.position) > respawnDistance && distanceDeath) {

            RespawnMe();

        }

    }

    void RespawnMe() {

        fpc.enabled = false;
        cg.enabled = false;

        if (GetComponent<Rigidbody>()) {

            GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);

        }

        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;

        fpc.enabled = true;
        cg.enabled = true;

        Debug.Log("Respawned");

    }

    void OnTriggerEnter(Collider other) {

        if (other.tag == "Respawn") {

            respawnPoint = other.transform.parent.gameObject;

        }

    }
}
