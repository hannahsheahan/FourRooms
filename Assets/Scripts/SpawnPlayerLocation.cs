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


        // Choose a random spawn orientation to look at (degrees). Keep at 0 for now...
        int numPossibleSpawnLocations = 100 - wallXPositions.Length; // The maze is a 10 x 10 grid

        for (int i = 0; i < numPossibleSpawnLocations; i++)
        {
            rots.Add(new Vector3(0, 0, 0));
        }

    }

    void Start () 
    {
        transform.position = locs[Random.Range(0, locs.Count)];
        transform.eulerAngles= rots[Random.Range(0, locs.Count)];
    }
}