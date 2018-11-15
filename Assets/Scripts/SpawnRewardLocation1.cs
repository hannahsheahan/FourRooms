using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRewardLocation1 : MonoBehaviour {

    /// <summary>
    /// Choose a location to spawn the star from that is actually on the grid (not in the holes)
    /// </summary>

    void Start () 
    {
        // Load the star spawn location from the configured datafile
        transform.position = GameController.control.star1SpawnLocation;
        Debug.Log("Reward1 spawned at: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

}