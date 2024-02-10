//// CREDITS ////
//
//  - From Unity Ace on YouTube
//      - https://youtu.be/5Rq8A4H6Nzw?si=W-Y-4aBOD6gqGaj5
//
//// NOTES ////
//
//  - needs some sort of camera contraining
//      - it is possible to fully rotate the camera
//      - should ideally prevent from moving further than straight up
//      - considering 90-degree locking

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    bool lockedCursor = true;

    [SerializeField] private Vector3 savedRotation;

    [SerializeField] private Vector3 upVector;
    [SerializeField] private Vector3 rightVector;


    void Start()
    {

        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        savedRotation = transform.localEulerAngles;

        upVector = transform.up;
        rightVector = transform.right;

    }

    
    void Update()
    {
        // ==== MOUSE INPUT ====

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        // ==== CAMERA VERTICAL ROTATION ====

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = new Vector3 (savedRotation.x + cameraVerticalRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // === PLAYER AND CAMERA HORIZONTAL ROTATION ====

        player.transform.RotateAround(player.transform.position, player.transform.up, inputX);
       
    }

    // ==== reset turning axis
    void OnEnable() {

        upVector = player.transform.up;
        rightVector = player.transform.right;

    }
}