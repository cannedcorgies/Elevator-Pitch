using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorFloor : MonoBehaviour
{
    public Text floorText;
    private int currentFloor = 1;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFloorText();
    }

    // Update is called once per frame
    void Update()
    {

     if (Input.GetKeyDown(KeyCode.Space)) {
        MoveToNextFloor();
     }
    }

    void MoveToNextFloor() {
        currentFloor++;
        UpdateFloorText();
    }

    void UpdateFloorText() {
        floorText.text = "Floor " + currentFloor.ToString();
    }
}
