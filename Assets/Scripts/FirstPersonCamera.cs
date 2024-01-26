//// CREDITS ////
//
//  - From Unity Ace on YouTube
//  - https://youtu.be/5Rq8A4H6Nzw?si=W-Y-4aBOD6gqGaj5

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    // Variables
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
        // Collect Mouse Input

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.RotateAround(transform.position, player.transform.right, -inputY);


        // Rotate the Player Object and the Camera around its Y axis

        player.transform.RotateAround(player.transform.position, upVector, inputX);
       
    }

    void OnEnable() {

        upVector = player.transform.up;
        rightVector = player.transform.right;

    }
}