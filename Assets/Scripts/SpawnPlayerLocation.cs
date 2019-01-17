using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayerLocation : MonoBehaviour {

    /// <summary>
    /// Choose a random location to spawn the player from that is actually on the grid (not in the holes)
    /// </summary>

    void Start () 
    {
        // Load the player spawn location from the configured datafile
        transform.position = GameController.control.playerSpawnLocation;
        transform.eulerAngles = GameController.control.playerSpawnOrientation;
        //Debug.Log("Player spawned at: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

}