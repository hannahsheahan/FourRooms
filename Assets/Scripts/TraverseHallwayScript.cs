using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseHallwayScript : MonoBehaviour {

    private bool doorlocked = true;

    private void OnTriggerEnter(Collider other)
    {

        if (doorlocked)  // 'unlock' the door
        {
            // run GameController function that freezes people for X seconds (can add this as a state later to make analysis easier later).
            GameController.control.HallwayFreeze();
            Debug.Log("Traversing hallway, player is frozen in place.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        doorlocked = false;  // You've opened this door once so can go back through whenever you want
    }

}
