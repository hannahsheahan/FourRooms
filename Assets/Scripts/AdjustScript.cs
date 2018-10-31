using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScript : MonoBehaviour {
    /// <summary>
    /// This script is here only as a testing script to make sure that data is persisting between scenes.
    /// </summary>


    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 140, 120, 30), "Health up") || Input.GetKeyUp(KeyCode.Q))
        {
            GameController.control.health += 10;
        }
        if (GUI.Button(new Rect(10, 180, 120, 30), "Health down") || Input.GetKeyUp(KeyCode.W))
        {
            GameController.control.health -= 10;
        }
        if (GUI.Button(new Rect(10, 220, 120, 30), "Experience up") || Input.GetKeyUp(KeyCode.E))
        {
            GameController.control.experience += 10;
        }
        if (GUI.Button(new Rect(10, 260, 120, 30), "Experience down") || Input.GetKeyUp(KeyCode.R))
        {
            GameController.control.experience -= 10;
        }

    }
}
