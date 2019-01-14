using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRewardLocation1 : MonoBehaviour {

    /// <summary>
    /// Choose a location to spawn the star from that is actually on the grid (not in the holes)
    /// </summary>

    public int rewardIndex;


    void Start () 
    {
        // Determine which reward position to give each reward. Yes this is a horribly ugly way of coding this.
        // ***HRS to tidy this method later. Place these in an array of starSpawnLocations at the very least and access based on index.
       /*
         switch (rewardIndex)
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;

            case 6:

                break;

            default:
                break;

             }
             */


        // Load the star spawn location from the configured datafile
        transform.position = GameController.control.rewardSpawnLocations[0];
        Debug.Log("Reward spawned at: " + transform.position.x + ", " + transform.position.y + ", " + transform.position.z);
    }

}