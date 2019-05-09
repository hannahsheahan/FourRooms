using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRewardLocation : MonoBehaviour {

    /// <summary>
    /// Choose a location to spawn the reward from that is actually on the grid (not in the holes).
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: Dec 2018
    /// </summary>

    public int rewardIndex=0;


    void Start () 
    {
        // Load the star spawn location from the configured datafile
        transform.position = GameController.control.rewardSpawnLocations[rewardIndex];
        //Debug.Log("Reward spawned at: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

}