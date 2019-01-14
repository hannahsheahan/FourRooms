using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRewardLocation2 : MonoBehaviour {

    /// <summary>
    /// Choose a location to spawn the star from that is actually on the grid (not in the holes)
    /// </summary>

    void Start () 
    {
        // Load the star spawn location from the configured datafile
        transform.position = GameController.control.rewardSpawnLocations[1];
        Debug.Log("Reward2 spawned at: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

}