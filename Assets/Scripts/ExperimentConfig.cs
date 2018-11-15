﻿using UnityEngine;
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
    private const int setupAndCloseTrials = 4;     // Note: there must be 3 extra trials in trial list to account for Persistent, StartScreen and Exit 'trials'.
    private const int restbreakOffset = 1;         // Note: makes specifying restbreaks more intuitive
    private const int getReadyTrial = 1;           // Note: this is the get ready screen after the practice
    private const int setupTrials = setupAndCloseTrials-1;
    private int totalTrials;
    private int practiceTrials;
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

    private Vector3[] blueRoomPositions;
    private Vector3[] redRoomPositions;
    private Vector3[] yellowRoomPositions;
    private Vector3[] greenRoomPositions;


    // Rewards
    private bool[] doubleRewardTask;         // if there are two stars to collect: true, else false
    private const int ONE_STAR = 0;
    private const int TWO_STARS = 1;
    private string[] possibleRewardTypes; 
    private string[] rewardTypes;             // diamond or gold? (martini or beer)

    // Timer variables (public since fewer things go wrong if these are changed externally, since this will be tracked in the data, but please don't...)
    public float maxMovementTime;
    public float preDisplayCueTime;
    public float goalHitPauseTime;
    public float displayCueTime;
    public float goCueDelay;
    public float minDwellAtReward;
    public float displayMessageTime;
    public float errorDwellTime;
    public float restbreakDuration;
    public float getReadyDuration;
    private float dataRecordFrequency;       // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data


    // ********************************************************************** //
    // Use a constructor to set this up
    public ExperimentConfig() 
    {

        // Set these variables to define your experiment:
        practiceTrials = 0   + getReadyTrial;
        totalTrials    = 10   + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
        restFrequency  = 4    + restbreakOffset;            // Take a rest after this many normal trials

        // Figure out how many rest breaks we will have and add them to the trial list
        nbreaks = Math.Max( (int)((totalTrials - setupAndCloseTrials - practiceTrials) / restFrequency), 0 );  // round down to whole integer
        totalTrials = totalTrials + nbreaks;
       


        // Timer variables (measured in seconds) - these can later be changed to be different per trial for jitter etc
        dataRecordFrequency = 0.04f;
        restbreakDuration   = 5.0f;    // how long are the imposed rest breaks?
        getReadyDuration    = 3.0f;    // how long do we have to 'get ready' after the practice, before main experiment begins?

        maxMovementTime     = 30.0f;
        preDisplayCueTime   = 1.5f;    // will take a TR during this period
        displayCueTime      = 2.0f;
        goCueDelay          = 1.5f;    // will take a TR during this period
        goalHitPauseTime    = 1.5f;    // we will take a TR during this period (this happens twice on 2 reward trials)
        minDwellAtReward    = 0.1f;      
        displayMessageTime  = 1.5f;     
        errorDwellTime      = 1.5f;    // Note: should be at least as long as displayMessageTime


        // These variables define the environment (are less likely to be played with)
        roomSize        = 5;           // rooms are each 5x5 grids. If this changes, you will need to change this code
        playerYposition = 72.5f;
        starYposition   = 73.5f;
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


        // Define the start up menu trials.   Note:  the other variables take their default values on these trials
        trialMazes[0] = "Persistent";
        trialMazes[1] = "StartScreen";
        trialMazes[2] = "InstructionsScreen";
        trialMazes[setupTrials + practiceTrials-1] = "GetReady";

        // Add in the practice/familiarisation trials in an open arena
        for (int trial = setupTrials; trial < setupTrials + practiceTrials-1; trial++)
        {
            trialMazes[trial] = "Practice";

            // Generate some random practice start positions and rewards
            doubleRewardTask[trial] = false;
            rewardTypes[trial] = "cheese";
            playerStartPositions[trial] = possiblePlayerPositions[UnityEngine.Random.Range(0, possiblePlayerPositions.Length - 1)]; // random start position
            playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]);   // orient player towards the centre of the environment
            star1Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];           // random reward position

            // ensure reward doesnt spawn on the player position
            while (playerStartPositions[trial] == star1Positions[trial])
            {
                star1Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];           // random star1 position
            }
        }

        // Define the final exit state
        trialMazes[totalTrials-1] = "Exit";


        // Generate the trial randomisation/list that we want

        RandomPlayerAndRewardPositions();

        // **HRS (later we can generate an array of things to cover and then permute from a function here) 

        //RandomPositionInRoom("blue");
        //ShuffleTrialOrder();  //***HRS have not written yet

    }

    // ********************************************************************** //

    private void GeneratePossibleSettings()
    {
        // Generate all possible spawn locations for player and stars
        possiblePlayerPositions = new Vector3[roomSize * roomSize * 4]; // we are working with 4 square rooms
        possibleStarPositions = new Vector3[roomSize * roomSize * 4];
        blueRoomPositions = new Vector3[roomSize * roomSize];
        redRoomPositions = new Vector3[roomSize * roomSize];
        yellowRoomPositions = new Vector3[roomSize * roomSize];
        greenRoomPositions = new Vector3[roomSize * roomSize];

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

        // Add position arrays for locations in particular rooms
        startind = 0;
        AddPossibleLocations(blueRoomPositions, startind, XPositionsblue, starYposition, ZPositionsblue);
        AddPossibleLocations(redRoomPositions, startind, XPositionsred, starYposition, ZPositionsred);
        AddPossibleLocations(greenRoomPositions, startind, XPositionsgreen, starYposition, ZPositionsgreen);
        AddPossibleLocations(yellowRoomPositions, startind, XPositionsyellow, starYposition, ZPositionsyellow);

        // Get all the possible mazes/scenes in the build that we can work with
        sceneCount = SceneManager.sceneCountInBuildSettings;
        possibleMazes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            possibleMazes[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }

        // Possible reward types
        possibleRewardTypes = new string[] { "wine", "cheese" };
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

    private void RandomPlayerAndRewardPositions()
    {

        // Generate trial content that randomly positions the player and reward/s in the different rooms
        for (int trial = setupTrials + practiceTrials; trial < totalTrials - 1; trial++)
        {
            // Deal with restbreaks and regular trials
            if ((trial - setupTrials - practiceTrials + 1) % restFrequency == 0)  // Time for a rest break
            {
                trialMazes[trial] = "RestBreak";
            }
            else                                    // It's a regular trial
            {
                // For now, change the reward type every second trial (if mod 2)
                if (trial % 2 == 0)          
                //if (trial < 0)  // for now just fix as 'always cheese'
                {
                    rewardTypes[trial] = "wine";    // use single reward type for now
                }
                else
                {
                    rewardTypes[trial] = "cheese";    // use single reward type for now
                }

                trialMazes[trial] = "FourRooms_" + rewardTypes[trial];   // set this to stay the same, for now
                doubleRewardTask[trial] = false;   // use only single star trials for now

                playerStartPositions[trial] = possiblePlayerPositions[UnityEngine.Random.Range(0, possiblePlayerPositions.Length - 1)]; // random start position
                playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]);   // orient player towards the centre of the environment
                star1Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];           // random star1 position

                // ensure reward doesnt spawn on the player position (later this will be pre-determined)
                while (playerStartPositions[trial] == star1Positions[trial])
                {
                    star1Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];           // random star1 position
                }


                if (doubleRewardTask[trial])
                {   // generate another position for star2
                    star2Positions[trial] = possibleStarPositions[UnityEngine.Random.Range(0, possibleStarPositions.Length - 1)];     // random star2 position
                }
                else
                {   // single star to be collected
                    star2Positions[trial] = star1Positions[trial];
                }
            }
        }
    }

    // ********************************************************************** //

    private Vector3 RandomPositionInRoom(string roomColour)
    {
        // select a random position in a room of a given colour
        switch (roomColour)
        {
            case "blue":
                return blueRoomPositions[UnityEngine.Random.Range(0, blueRoomPositions.Length - 1)];

            case "red":
                return redRoomPositions[UnityEngine.Random.Range(0, redRoomPositions.Length - 1)];

            case "green":
                return greenRoomPositions[UnityEngine.Random.Range(0, greenRoomPositions.Length - 1)];
            
            case "yellow":
                return yellowRoomPositions[UnityEngine.Random.Range(0, yellowRoomPositions.Length - 1)];
            
            default:
                return new Vector3(0.0f, 0.0f, 0.0f);  // this should never happen
        }
    }

    // ********************************************************************** //

    public void ShuffleTrialOrder( )
    {

        // ***HRS to write this another day

        string[] p_trialMazes = new string[totalTrials];
        bool[] p_doubleRewardTask = new bool[totalTrials];
        Vector3[] p_playerStartPositions = new Vector3[totalTrials];
        Vector3[] p_playerStartOrientations = new Vector3[totalTrials];
        Vector3[] p_star1Positions = new Vector3[totalTrials];
        Vector3[] p_star2Positions = new Vector3[totalTrials];

        // This function will take the existing trial sequence (excluding the menu etc trials), and return a randomly permuted ordering

        for (int trial = setupTrials + practiceTrials; trial < totalTrials - 1; trial++)
        {
            if (trialMazes[trial] == "RestBreak")
            {
                // save a version of the trial sequence for messing with, so we don't mess with the rest breaks
               

            }
        }

        // Shuffle the sequence
       /*
        System.Random random = new System.Random();
        for (int i = elements.Length - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            string tmp = elements[i];
            elements[i] = elements[swapIndex];
            elements[swapIndex] = tmp;
        }
        */
        // Put it back together with the rest breaks

    }


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