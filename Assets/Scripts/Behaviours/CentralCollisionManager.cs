using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CentralCollisionManager : MonoBehaviour
{


    public Transform door1;
    public Transform door2;
    public float moveSpeed = 1f;

    public float maxOpenDistance = 4f;

    private Vector3 door1StartPosition;
    private Vector3 door2StartPosition;


    private Vector3 door1EndPosition;
    private Vector3 door2EndPosition;

    private bool doorsFullyOpened = false;

    private bool shouldOpenDoors = false;
    private bool shouldCloseDoors = false;

    void Start() {
        door1StartPosition = door1.position;
        door2StartPosition = door2.position;
    }
    void Update() {
        // check if doors should be moving
        if (shouldOpenDoors) {
            MoveDoors(true);
        }
        else if (shouldCloseDoors) {
            MoveDoors(false);
        }
    }

    private void MoveDoors(bool openOrClose) {
        // how much to move the door each frame
        float step = moveSpeed * Time.deltaTime;


        if (openOrClose) {
            if (Vector3.Distance(door1.position, door1StartPosition) < maxOpenDistance) {
                door1.position += new Vector3(step, 0, 0);
                door2.position -= new Vector3(step, 0, 0);
            } else {
                doorsFullyOpened = true;
                shouldOpenDoors = false;
                shouldCloseDoors = true;
            }
        } else if (!openOrClose && doorsFullyOpened) {
            if (door1.position != door1StartPosition) {
                door1.position = Vector3.MoveTowards(door1.position, door1StartPosition, step);
            }
            if (door2.position != door2StartPosition) {
                door2.position = Vector3.MoveTowards(door2.position, door2StartPosition, step);
            }

            // reset variables
            if (door1.position == door1StartPosition && door2.position == door2StartPosition) {
                doorsFullyOpened = false; 
                shouldCloseDoors = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("somethin entered");
        if (gameObject.name == "TriggerCube" && other.gameObject.name == "Lever") {
            Debug.Log("In here");
            shouldOpenDoors = true;
            shouldCloseDoors = false;
        }
        else if (gameObject.name == "InvisibleWall" && other.gameObject.name == "Player" ) {

            Debug.Log("DOORS CLOSING");
            shouldCloseDoors = true;
            shouldOpenDoors = false;
        }

    }
 
}

