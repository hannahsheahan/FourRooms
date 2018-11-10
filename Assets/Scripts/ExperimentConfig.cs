using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class ExperimentConfig
{
    /// <summary>
    /// This script will contain all the experiment configuration details
    /// e.g. experiment type, trial numbers, ordering and randomisation, trial 
    /// start and end locations. Note that we keep variables private and use Get()
    /// methods since the /// experiment configuration should not be modified 
    /// outside of this script.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 08/11/2018
    /// </summary>


    // Scenes/mazes
    private const int setupAndCloseTrials = 3;     // Note: there must be 3 extra trials in trial list to account for Persistent, StartScreen and Exit 'trials'.
    private const int restbreakOffset = 1;         // Note: makes specifying restbreaks more intuitive
    private const int setupTrials = 2;
    private int totalTrials;
    private int restFrequency;
    private int nbreaks;
    private string[] trialMazes;
    private string[] possibleMazes;                // the existing mazes/scenes that can be selected from
    private int sceneCount;
    private int roomSize;
    private float playerYposition;
    private float starYposition;

    // Positions and orientations
    private Vector3 mazeCentre;
    private Vector3[] possiblePlayerPositions;
    private Vector3[] playerStartPositions;
    private Vector3[] playerStartOrientations;
    private Vector3 spawnOrientation;

    private Vector3[] possibleStarPositions;
    private Vector3[] star1Positions;
    private Vector3[] star2Positions;

    // Rewards
    private bool[] doubleRewardTask;         // if there are two stars to collect: true, else false
    private const int ONE_STAR = 0;
    private const int TWO_STARS = 1;
    private string[] possibleRewardTypes; 
    private string[] rewardTypes;             // diamond or gold? (martini or beer)

    // Timer variables (public since fewer things go wrong if these are changed externally, since this will be tracked in the data, but please don't...)
    public float maxMovementTime;
    public float goalAppearDelay;
    public float goCueDelay;
    public float minDwellAtStar;
    public float displayMessageTime;
    public float waitFinishTime;
    public float errorDwellTime;
    public float restbreakDuration;
    private float dataRecordFrequency;       // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data


    // ********************************************************************** //
    // Use a constructor to set this up
    public ExperimentConfig() 
    {

        // Set these variables to define your experiment:
        totalTrials   = 20   + setupAndCloseTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
        restFrequency = 1    + restbreakOffset;            // Take a rest after this many normal trials

        // Figure out how many rest breaks we will have and add them to the trial list
        nbreaks = (int)(totalTrials / restFrequency);  // round down to whole integer
        totalTrials = totalTrials + nbreaks;

        // ... ***HRS add other variables to control here


        // Timer variables (measured in seconds) - these can later be changed to be different per trial for jitter etc
        dataRecordFrequency = 0.04f;
        restbreakDuration   = 15.0f;    // how long are the imposed rest breaks?

        maxMovementTime     = 15.0f;
        goalAppearDelay     = 0.0f;     
        goCueDelay          = 1.5f;
        minDwellAtStar      = 0.5f;      
        displayMessageTime  = 1.5f;     
        waitFinishTime      = 1.5f;
        errorDwellTime      = 1.0f;


    // These variables define the environment (are less likely to be played with)
        roomSize        = 5;           // rooms are each 5x5 grids. If this changes, you will need to change this code
        playerYposition = 72.5f;
        starYposition   = 75.5f;
        mazeCentre      = new Vector3(145.0f, playerYposition, 145.0f);


        // Define a maze, start and goal positions, and reward type for each trial
        trialMazes = new string[totalTrials];
        playerStartPositions = new Vector3[totalTrials];
        playerStartOrientations = new Vector3[totalTrials];
        star1Positions = new Vector3[totalTrials];
        star2Positions = new Vector3[totalTrials];
        doubleRewardTask = new bool[totalTrials];
        rewardTypes = new string[totalTrials];

        // Generate a list of all the possible (player or star) spawn locations
        GeneratePossibleSettings();


        // Generate the trial randomisation etc that we want:
        // **HRS (later we can generate an array of things to cover and then permute from a function here) 

        // Define the first two trials (which are the Persistent and StartScreen states)
        // Note: this lets the other variables take their default values on these starting trials
        trialMazes[0] = "Persistent";
        trialMazes[1] = "StartScreen";

        // Define the final exit state
        trialMazes[totalTrials-1] = "Exit";

        // Let's make the trial content completely random for now, to see if it works
        for (int trial = setupTrials; trial < totalTrials-1; trial++)
        {

            if (  (trial - setupTrials + 1) % restFrequency == 0)  // Time for a rest break
            {  
                trialMazes[trial] = "RestBreak";
            }
            else                                    // It's a regular trial
            {
                trialMazes[trial] = "tartarus1";   // set this to stay the same, for now
                doubleRewardTask[trial] = false;   // use only single star trials for now
                rewardTypes[trial] = "diamond";    // use single reward type for now

                playerStartPositions[trial] = possiblePlayerPositions[UnityEngine.Random.Range(0, possiblePlayerPositions.Length-1)]; // random start position
                playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]);   // orient player towards the centre of the environment
                star1Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length-1)];           // random star1 position

                if(doubleRewardTask[trial])
                {   // generate another position for star2
                    star2Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];     // random star2 position
                }
                else
                {   // single star to be collected
                    star2Positions[trial] = star1Positions[trial];  
                }
            }
        }





        // ... ** HRS add any other randomisation etc you want here

    }

    // ********************************************************************** //

    private void GeneratePossibleSettings()
    {
        // Generate all possible spawn locations for player and stars
        possiblePlayerPositions = new Vector3[roomSize * roomSize * 4]; // we are working with 4 square rooms
        possibleStarPositions = new Vector3[roomSize * roomSize * 4];

        // Blue room
        int startind = 0;
        int[] XPositionsblue = { 95, 105, 115, 125, 135 };
        int[] ZPositionsblue = { 95, 105, 115, 125, 135 };
        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsblue, playerYposition, ZPositionsblue);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsblue, starYposition, ZPositionsblue);
        startind = startind + roomSize * roomSize;

        // Red room
        int[] XPositionsred = { 155, 165, 175, 185, 195 };
        int[] ZPositionsred = { 95, 105, 115, 125, 135 };
        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsred, playerYposition, ZPositionsred);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsred, starYposition, ZPositionsred);
        startind = startind + roomSize * roomSize;

        // Green room
        int[] XPositionsgreen = { 155, 165, 175, 185, 195 };
        int[] ZPositionsgreen = { 155, 165, 175, 185, 195 };
        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsgreen, playerYposition, ZPositionsgreen);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsgreen, starYposition, ZPositionsgreen);
        startind = startind + roomSize * roomSize;

        // Yellow room
        int[] XPositionsyellow = { 95, 105, 115, 125, 135 };
        int[] ZPositionsyellow = { 155, 165, 175, 185, 195 };
        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsyellow, playerYposition, ZPositionsyellow);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsyellow, starYposition, ZPositionsyellow);

        // Get all the possible mazes/scenes in the build that we can work with
        sceneCount = SceneManager.sceneCountInBuildSettings;
        possibleMazes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            possibleMazes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        // Possible reward types
        possibleRewardTypes = new string[] { "martini", "beer" };
    }

    // ********************************************************************** //

    void AddPossibleLocations(Vector3[] locationVar, int startind, int[] xpositions, float yposition, int[] zpositions)
    {
        int ind = startind;
        for (int i = 0; i < roomSize; i++)
        {
            for (int j = 0; j < roomSize; j++)
            {
                locationVar[ind] = new Vector3(xpositions[i], yposition, zpositions[j]);
                ind++;
            }
        }
    }

    // ********************************************************************** //

    private Vector3 findStartOrientation(Vector3 position)     {
        // Generate a starting orientation that always makes the player look towards the centre of the environment
        Vector3 lookVector = new Vector3();         lookVector = mazeCentre - position; 
        float angle = (float)Math.Atan2(lookVector.z, lookVector.x);   // angle of the vector connecting centre and spawn location         angle = 90 - angle * (float)(180 / Math.PI);                   // correct for where angles are measured from 
        if (angle<0)   // put the view angle in the range 0 to 360 degrees
        {
            angle = 360 + angle;
        }
        spawnOrientation = new Vector3(0.0f, angle, 0.0f);          return spawnOrientation;     } 
    // ********************************************************************** //
    // Get() and Set() Methods
    // ********************************************************************** //

    public int GetTotalTrials()
    {
        return totalTrials;
    }

    // ********************************************************************** //

    public float GetDataFrequency()
    {
        return dataRecordFrequency;
    }

    // ********************************************************************** //

    public string GetTrialMaze(int trial)
    {
        return trialMazes[trial];
    }

  // ********************************************************************** //

    public Vector3 GetPlayerStartPosition(int trial)
    {
        return playerStartPositions[trial];
    }

    // ********************************************************************** //

    public Vector3 GetPlayerStartOrientation(int trial)
    {
        return playerStartOrientations[trial];
    }

    // ********************************************************************** //

    public Vector3 GetStar1StartPosition(int trial)
    {
        return star1Positions[trial];
    }

    // ********************************************************************** //

    public Vector3 GetStar2StartPosition(int trial)
    {
        return star2Positions[trial];
    }

    // ********************************************************************** //

    public string GetRewardType(int trial)
    {
        return rewardTypes[trial];
    }

    // ********************************************************************** //

    public bool GetIsDoubleReward(int trial)
    {
        return doubleRewardTask[trial];
    }

    // ********************************************************************** //




}


