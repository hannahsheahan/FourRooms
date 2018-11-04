using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPlayerLocation : MonoBehaviour {

    /// <summary>
    /// Choose a random location to spawn the player from that is actually on the grid (not in the holes)
    /// </summary>

    public List<Vector3> locs = new List<Vector3>();
    public List<Vector3> rots = new List<Vector3>();
    public List<Vector3> wallPositions = new List<Vector3>();

    private float yposition = 72.5f;

    // Use this for initialization
    void Awake()
    {
        // Some spawn location/orientation options to randomly choose from


        // *** HRS note that these were chosen based on the 10x10 unit grid and 
        // these have now changed (note also that it spawned off the grid sometimes even then so was buggy).

        /*
        // Where can't we spawn? all the wallPositions
        int[] wallXPositions = { 105, 115, 135 ,145, 155, 165, 175, 195, 145, 145, 145, 145, 145, 145, 145, 145 };
        int[] wallZPositions = { 155, 155, 155, 155, 155, 155, 155, 155, 105, 115, 135, 145, 155, 165, 175, 185 };


        // Traverse the grid for possible spawn locations
        for (int i = 105; i < 200; i = i+10)
        {
            for (int j = 105; j < 200; j = j+10)
            {
                for (int p = 0; p < wallXPositions.Length; p++)
                {
                    if (!((wallXPositions[p]==i) && (wallZPositions[p] == j)))
                    {
                        // add the element as a valid spawn location
                        locs.Add(new Vector3(i, yposition, j));
                    }
                }
            }
        }

        */

        // Just hard code possible spawn locations for now since we will be super sensitive to this later


        // add blue room
        int[] XPositionsblue = { 95, 105, 115, 125, 135 };
        int[] ZPositionsblue = { 95, 105, 115, 125, 135 };

        // Choose a random spawn orientation to look at (degrees). Keep at 0 for now...
        for (int i = 0; i < XPositionsblue.Length; i++)
        {
            for (int j = 0; j < ZPositionsblue.Length; j++)
            {
                locs.Add(new Vector3(XPositionsblue[i], yposition, ZPositionsblue[j]));
                rots.Add(new Vector3(0, 0, 0));
            }
        }

        // add red room
        int[]  XPositionsred = { 155, 165, 175, 185, 195 };
        int[]  ZPositionsred = { 95, 105, 115, 125, 135 };

        // Choose a random spawn orientation to look at (degrees). Keep at 0 for now...
        for (int i = 0; i< XPositionsred.Length; i++)
        {
            for (int j = 0; j< ZPositionsred.Length; j++)
            {
                locs.Add(new Vector3(XPositionsred[i], yposition, ZPositionsred[j]));
                rots.Add(new Vector3(0, 0, 0));
            }
        }

        // add green room
        int[] XPositionsgreen = { 155, 165, 175, 185, 195 };
        int[] ZPositionsgreen = { 155, 165, 175, 185, 195 };

        // Choose a random spawn orientation to look at (degrees). Keep at 0 for now...
        for (int i = 0; i< XPositionsgreen.Length; i++)
        {
            for (int j = 0; j< ZPositionsgreen.Length; j++)
            {
                locs.Add(new Vector3(XPositionsgreen[i], yposition, ZPositionsgreen[j]));
                rots.Add(new Vector3(0, 0, 0));
            }
        }

        // add yellow room
        int[] XPositionsyellow = { 95, 105, 115, 125, 135 };
        int[] ZPositionsyellow = { 155, 165, 175, 185, 195 };

        // Choose a random spawn orientation to look at (degrees). Keep at 0 for now...
        for (int i = 0; i< XPositionsyellow.Length; i++)
        {
            for (int j = 0; j< ZPositionsyellow.Length; j++)
            {
                locs.Add(new Vector3(XPositionsyellow[i], yposition, ZPositionsyellow[j]));
                rots.Add(new Vector3(0, 0, 0));
            }
        }
    }

    void Start () 
    {
        transform.position = locs[Random.Range(0, locs.Count)];
        transform.eulerAngles= rots[Random.Range(0, rots.Count)];
    }
}