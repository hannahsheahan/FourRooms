using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneSwitchingByButton : MonoBehaviour {

    private int currentScene;
    private bool pressed = false;

    // ********************************************************************** //

    void Update()
    {
        // use Update() to stop multiple button presses per frame doing weird things to the state transitions
        if (pressed)  
        {
            Debug.Log("Current scene: " + currentScene);
            Debug.Log("Next scene: tartarus" + (currentScene + 1));
            //GameController.control.NextScene("tartarus" + (currentScene + 1));  // load the next scene
            GameController.control.NextScene();  // load the next scene
            pressed = false;
        }
    }

    // ********************************************************************** //

    void OnGUI()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex -2; // Correct labeling for the Persistent and Start Screens

        GUI.Label(new Rect(10, 60, 120, 30), "Curent scene: " + currentScene);
        if (GUI.Button(new Rect(10, 90, 120, 30), "Load next scene") || Input.GetKeyDown("return"))
        {
            pressed = true;
        }
    }
}
