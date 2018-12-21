using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class ExperimentConfig
{
    /// <summary>
    /// This script contains all the experiment configuration details
    /// e.g. experiment type, trial numbers, ordering and randomisation, trial 
    /// start and end locations. 
    /// Notes:  variables should eventually be turned private. Some currently public for ease of communication with DataController.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 08/11/2018
    /// </summary>


    // Scenes/mazes
    private const int setupAndCloseTrials = 7;     // Note: there must be 7 extra trials in trial list to account for Persistent, InformationScreen, BeforeStartingScreen, ConsentScreen, StartScreen, Instructions and Exit 'trials'.
    private const int restbreakOffset = 1;         // Note: makes specifying restbreaks more intuitive
    private const int getReadyTrial = 1;           // Note: this is the get ready screen after the practice
    private const int setupTrials = setupAndCloseTrials - 1;
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
    private float deltaSquarePosition;

    // Positions and orientations
    private Vector3 mazeCentre;
    private Vector3[] possiblePlayerPositions;
    private string[] playerStartRooms;
    private string[] star1Rooms;
    private string[] star2Rooms;
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
    private Vector3[] spawnedPresentPositions;

    private Vector3[] bluePresentPositions;
    private Vector3[] redPresentPositions;
    private Vector3[] yellowPresentPositions;
    private Vector3[] greenPresentPositions;

    public Vector3[][] presentPositions;

    // Rewards
    private bool[] doubleRewardTask;         // if there are two stars to collect: true, else false
    private const int ONE_STAR = 0;
    private const int TWO_STARS = 1;
    private string[] possibleRewardTypes; 
    private string[] rewardTypes;             // diamond or gold? (martini or beer)
    public int numberPresentsPerRoom;

    // Timer variables (public since fewer things go wrong if these are changed externally, since this will be tracked in the data, but please don't...)
    public float maxMovementTime;
    public float preDisplayCueTime;
    public float goalHitPauseTime;
    public float finalGoalHitPauseTime;
    public float displayCueTime;
    public float goCueDelay;
    public float minDwellAtReward;
    public float displayMessageTime;
    public float errorDwellTime;
    public float restbreakDuration;
    public float getReadyDuration;
    public float hallwayFreezeTime;
    private float dataRecordFrequency;       // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data

    // Randomisation of trial sequence
    public System.Random rand = new System.Random();

    // Preset experiments
    public string experimentVersion;
    private int nExecutedTrials;            // to be used in micro_debug mode only
    // ********************************************************************** //
    // Use a constructor to set this up
    public ExperimentConfig() 
    {
        //experimentVersion = "mturk_learnpilot";
        //experimentVersion = "micro_debug"; 
        experimentVersion = "singleblock_labpilot";
        

        // Set these variables to define your experiment:
        switch (experimentVersion)
        {
            case "mturk_learnpilot":       // ----Full 4 block learning experiment-----
                practiceTrials = 2 + getReadyTrial;
                totalTrials = 16 * 4 + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 16 + restbreakOffset;                               // Take a rest after this many normal trials
                restbreakDuration = 30.0f;                                          // how long are the imposed rest breaks?
                break;

            case "singleblock_labpilot":   // ----Mini 1 block test experiment-----
                practiceTrials = 1 + getReadyTrial;
                totalTrials = 16  + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 20   + restbreakOffset;                          // Take a rest after this many normal trials
                restbreakDuration = 5.0f;                                        // how long are the imposed rest breaks?
                break;

            case "micro_debug":            // ----Mini debugging test experiment-----
                practiceTrials = 0 + getReadyTrial;
                nExecutedTrials = 1;                                         // note that this is only used for the micro_debug version
                totalTrials = nExecutedTrials + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 2 + restbreakOffset;                            // Take a rest after this many normal trials
                restbreakDuration = 5.0f;                                       // how long are the imposed rest breaks?
                break;

            default:
                Debug.Log("Warning: defining an untested trial sequence");
                break;
        }

        // Figure out how many rest breaks we will have and add them to the trial list
        nbreaks = Math.Max( (int)((totalTrials - setupAndCloseTrials - practiceTrials) / restFrequency), 0 );  // round down to whole integer
        totalTrials = totalTrials + nbreaks;
       
        // Timer variables (measured in seconds) - these can later be changed to be different per trial for jitter etc
        dataRecordFrequency = 0.04f;
        getReadyDuration = 5.0f;    // how long do we have to 'get ready' after the practice, before main experiment begins?

        // Note that when used, jitters ADD to these values - hence they are minimums
        maxMovementTime        = 60.0f;   // time allowed to collect both rewards, incl. wait after hitting first one
        preDisplayCueTime      = 1.5f;    // will take a TR during this period
        displayCueTime         = 2.0f;
        goCueDelay             = 1.5f;    // will take a TR during this period
        goalHitPauseTime       = 1.0f;    // we will take a TR during this period
        finalGoalHitPauseTime  = 2.0f;    // we will take a TR during this period (but should be independent of first goal hit time in case we want to jitter)
        minDwellAtReward       = 0.1f;      
        displayMessageTime     = 1.5f;     
        errorDwellTime         = 1.5f;    // Note: should be at least as long as displayMessageTime
        hallwayFreezeTime      = 5.0f;    // amount of time player is stuck in place with each hallway traversal
        numberPresentsPerRoom  = 4;

        // These variables define the environment (are less likely to be played with)
        roomSize = 5;           // rooms are each 5x5 grids. If this changes, you will need to change this code
        playerYposition = 72.5f;
        starYposition   = 74.5f;
        mazeCentre      = new Vector3(145.0f, playerYposition, 145.0f);


        // Define a maze, start and goal positions, and reward type for each trial
        trialMazes = new string[totalTrials];
        playerStartRooms = new string[totalTrials];
        star1Rooms = new string[totalTrials];
        star2Rooms = new string[totalTrials];
        playerStartPositions = new Vector3[totalTrials];
        playerStartOrientations = new Vector3[totalTrials];
        star1Positions = new Vector3[totalTrials];
        star2Positions = new Vector3[totalTrials];
        doubleRewardTask = new bool[totalTrials];
        rewardTypes = new string[totalTrials];
        presentPositions = new Vector3[totalTrials][];

        // Generate a list of all the possible (player or star) spawn locations
        GeneratePossibleSettings();


        // Define the start up menu and exit trials.   Note:  the other variables take their default values on these trials
        trialMazes[0] = "Persistent";
        trialMazes[1] = "InformationScreen";
        trialMazes[2] = "BeforeStartingScreen";
        trialMazes[3] = "ConsentScreen";
        trialMazes[4] = "StartScreen";
        trialMazes[5] = "InstructionsScreen";
        trialMazes[setupTrials + practiceTrials-1] = "GetReady";
        trialMazes[totalTrials - 1] = "Exit";

        // Add in the practice trials in an open arena with little fog and no colour
        AddPracticeTrials();

        // Generate the trial randomisation/list that we want.   Note: Ensure this is aligned with the total number of trials
        int nextTrial = System.Array.IndexOf(trialMazes, null);

        // Define the full trial sequence
        switch (experimentVersion)
        {
            case "mturk_learnpilot":       // ----Full 4 block learning experiment-----

                //---- training block 1
                nextTrial = AddTrainingBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);                  

                //---- training block 2
                nextTrial = AddTrainingBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);                   

                //---- training block 3
                nextTrial = AddTrainingBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);                   

                //---- training block 4
                nextTrial = AddTrainingBlock(nextTrial);

                break;

            case "singleblock_labpilot":   // ----Mini 1 block test experiment-----

                //---- training block 1
                nextTrial = AddTrainingBlock(nextTrial);
                break;

            case "micro_debug":            // ----Mini debugging test experiment-----

                nextTrial = AddTrainingBlock_micro(nextTrial, nExecutedTrials); 
                break;

            default:
                Debug.Log("Warning: defining an untested trial sequence");
                break;
        }

        // Later experiment:

        //---- free foraging block
        //AddFreeForageBlock();   // ***HRS to make this later

        //---- training goes here

        //---- free foraging block
        //AddFreeForageBlock();


        // For debugging: print out the final trial sequence in readable text to check it looks ok
        //PrintTrialSequence();

    }

    // ********************************************************************** //

    private void PrintTrialSequence()
    {
        // This function is for debugging/checking the final trial sequence by printing to console
        for (int trial = 0; trial < totalTrials; trial++)
        {
            Debug.Log("Trial " + trial + ", Maze: " + trialMazes[trial] + ", Reward type: " + rewardTypes[trial]);
            Debug.Log("Start room: " + playerStartRooms[trial] + ", First reward room: " + star1Rooms[trial] + ", Second reward room: " + star2Rooms[trial]);
            Debug.Log("--------");
        }
    }

    // ********************************************************************** //

    private void AddPracticeTrials()
    {
        // Add in the practice/familiarisation trials in an open arena
        for (int trial = setupTrials; trial < setupTrials + practiceTrials - 1; trial++)
        {
            // just make the rewards on each side of the hallway/bridge
            if ( trial % 2 == 0 )
            {
                SetDoubleRewardTrial(trial, "cheese", "blue", "red", "yellow");  
            }
            else
            {
                SetDoubleRewardTrial(trial, "cheese", "red", "green", "blue"); 
            }
            trialMazes[trial] = "Practice";   // reset the maze for a practice trial
        }
    }

    // ********************************************************************** //

    private string ChooseRandomRoom()
    {
        // Choose a random room of the four rooms
        string[] fourRooms = { "blue", "yellow", "red", "green" };
        int n = fourRooms.Length;
        int ind = rand.Next(n);   // Note: for some reason c# wants this stored to do randomisation, not directly input to fourRooms[rand.Next(n)]

        return fourRooms[ind]; 
    }

    // ********************************************************************** //

    private Vector3[] ChooseNRandomPresentPositions( int nPresents, Vector3[] roomPositions )
    {
        Vector3[] positionsInRoom = new Vector3[nPresents];

        for (int i = 0; i < nPresents; i++)
        {
            positionsInRoom[i] = roomPositions[UnityEngine.Random.Range(0, roomPositions.Length - 1)];

            // make sure that we dont spawn multiple presents on top of each other
            for (int j = 0; j < i; j++)
            {
                if (positionsInRoom[i] == positionsInRoom[j])
                {
                    positionsInRoom[i] = roomPositions[UnityEngine.Random.Range(0, roomPositions.Length - 1)];
                }
            }
        }

        return positionsInRoom;
    }

    // ********************************************************************** //

    private void GeneratePresentPositions(int trial)
    {

        // Spawn the presents in the opposite corners of the room
        /*
        int nPresentsPerRoom = 2;   // two presents per room
        presentPositions = new Vector3[nPresentsPerRoom*4];  

        // ORDER:  green; red; yellow; blue
        float[] xpositions = { 156f, 190f, 156f, 190f, 105.1f, 139.1f, 105.1f, 139.1f };
        float[] zpositions = { 144.3f, 178.3f, 127.3f, 93.3f, 178.3f, 144.3f, 93.3f, 127.3f };
        float yposition = 74.3f;

        for (int i = 0; i < presentPositions.Length; i++)
        {
            presentPositions[i] = new Vector3(xpositions[i], yposition, zpositions[i]);
        }

        // specify present positions by coloured room (***HRS horrible hardcoding but fine for now - can change later)
        greenPresentPositions = new Vector3[] { new Vector3(xpositions[0], yposition, zpositions[0]), new Vector3(xpositions[1], yposition, zpositions[1]) };
        redPresentPositions = new Vector3[] { new Vector3(xpositions[2], yposition, zpositions[2]), new Vector3(xpositions[3], yposition, zpositions[3]) };
        yellowPresentPositions = new Vector3[] { new Vector3(xpositions[4], yposition, zpositions[4]), new Vector3(xpositions[5], yposition, zpositions[5]) };
        bluePresentPositions = new Vector3[] { new Vector3(xpositions[6], yposition, zpositions[6]), new Vector3(xpositions[7], yposition, zpositions[7]) };
        */

        // presents can be at any position in the room now
        presentPositions[trial] = new Vector3[numberPresentsPerRoom * 4];

        greenPresentPositions = ChooseNRandomPresentPositions( numberPresentsPerRoom, greenRoomPositions );
        redPresentPositions = ChooseNRandomPresentPositions( numberPresentsPerRoom, redRoomPositions );
        yellowPresentPositions = ChooseNRandomPresentPositions( numberPresentsPerRoom, yellowRoomPositions );
        bluePresentPositions = ChooseNRandomPresentPositions( numberPresentsPerRoom, blueRoomPositions );

        // concatenate all the positions of generated presents 
        greenPresentPositions.CopyTo(presentPositions[trial], 0);
        redPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length);
        yellowPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length);
        bluePresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length + yellowPresentPositions.Length);

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

        // Version 1.0 larger room positions:
        //int[] XPositionsblue = { 95, 105, 115, 125, 135 };
        //int[] ZPositionsblue = { 95, 105, 115, 125, 135 };
        //int[] XPositionsred = { 155, 165, 175, 185, 195 };
        //int[] ZPositionsred = { 95, 105, 115, 125, 135 };
        //int[] XPositionsgreen = { 155, 165, 175, 185, 195 };
        //int[] ZPositionsgreen = { 155, 165, 175, 185, 195 };
        //int[] XPositionsyellow = { 95, 105, 115, 125, 135 };
        //int[] ZPositionsyellow = { 155, 165, 175, 185, 195 };

        // Version 2.0 smaller room positions
        // Blue room
        int startind = 0;
        deltaSquarePosition = 8.5f; // ***HRS later should really use this to create loop for specifying positions
        float[] XPositionsblue = { 105.1f, 113.6f, 122.1f, 130.6f, 139.1f };
        float[] ZPositionsblue = { 93.3f, 101.8f, 110.3f, 118.8f, 127.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsblue, playerYposition, ZPositionsblue);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsblue, starYposition, ZPositionsblue);
        startind = startind + roomSize * roomSize;

        // Red room
        float[] XPositionsred = { 156f, 164.5f, 173f, 181.5f, 190f };
        float[] ZPositionsred = { 93.3f, 101.8f, 110.3f, 118.8f, 127.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsred, playerYposition, ZPositionsred);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsred, starYposition, ZPositionsred);
        startind = startind + roomSize * roomSize;

        // Green room
        float[] XPositionsgreen = { 156f, 164.5f, 173f, 181.5f, 190f };
        float[] ZPositionsgreen = { 144.3f, 152.8f, 161.3f, 169.8f, 178.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsgreen, playerYposition, ZPositionsgreen);
        AddPossibleLocations(possibleStarPositions, startind, XPositionsgreen, starYposition, ZPositionsgreen);
        startind = startind + roomSize * roomSize;

        // Yellow room
        float[] XPositionsyellow = { 105.1f, 113.6f, 122.1f, 130.6f, 139.1f };
        float[] ZPositionsyellow = { 144.3f, 152.8f, 161.3f, 169.8f, 178.3f };

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

    void AddPossibleLocations(Vector3[] locationVar, int startind, float[] xpositions, float yposition, float[] zpositions)
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

    private int RestBreakHere(int firstTrial)
    {
        // Insert a rest break here and move to the next trial in the sequence

        trialMazes[firstTrial] = "RestBreak";
        return firstTrial + 1;
    }

    // ********************************************************************** //

    private int AddTrainingBlock(int nextTrial)
    {
        // Add a 16 trial training block to the trial list. Trials are randomised within each context, but not between contexts 

        if (rand.Next(2) == 0)   // randomise whether the wine or cheese sub-block happens first
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine");
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese");
        } else
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese");
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine");
        }
        return nextTrial;
    }

    // ********************************************************************** //

    private int AddTrainingBlock_micro(int nextTrial, int numberOfTrials)
    {
        // Add a 16 trial training block to the trial list. Trials are randomised within each context, but not between contexts 

        nextTrial = DoubleRewardBlock_micro(nextTrial, "cheese", numberOfTrials);

        return nextTrial;
    }

    // ********************************************************************** //

    private int SingleContextDoubleRewardBlock(int firstTrial, string context)
    {
        // This function specifies the required trials in the block, and returns the next trial after this block
        // NOTE: Use this function if you want to 'block' by reward type

        string startRoom;
        int contextSide;
        int blockLength = 8; // Specify the next 8 trials

        string[] arrayContexts = new string[blockLength];
        string[] arrayStartRooms = new string[blockLength];
        int[] arrayContextSides = new int[blockLength];

        for (int i = 0; i < blockLength; i++)
        {
            // use a different start location for each trial
            switch (i % 4)
            {
                case 0:
                    startRoom = "yellow";
                    break;
                case 1:
                    startRoom = "green";
                    break;
                case 2:
                    startRoom = "red";
                    break;
                case 3:
                    startRoom = "blue";
                    break;
                default:
                    startRoom = "error";
                    Debug.Log("Start room specified incorrectly");
                    break;
            }

            // switch the side of the room the rewards are located on for each context
            if (blockLength % 2 !=0)
            {
                Debug.Log("Error: Odd number of trials specified per block. Specify even number for proper counterbalancing");
            }

            if (i < (blockLength/2)) 
            {
                contextSide = 1;
            }
            else
            {
                contextSide = 2;
            }

            // Store trial setup in array, for later randomisation
            arrayContexts[i] = context;
            arrayStartRooms[i] = startRoom;
            arrayContextSides[i] = contextSide;
        }

        // Randomise the trial order and save it
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides);

        return firstTrial + blockLength;
    }

    // ********************************************************************** //

    private int DoubleRewardBlock_micro(int firstTrial, string context, int blockLength)
    {
        // This is for use during testing and debugging only - it DOES NOT specify a full counterbalanced trial sequence
        // This function specifies the required trials in the block, and returns the next trial after this block

        string startRoom;
        int contextSide;

        string[] arrayContexts = new string[blockLength];
        string[] arrayStartRooms = new string[blockLength];
        int[] arrayContextSides = new int[blockLength];

        for (int i = 0; i < blockLength; i++)
        {
            // use a different start location for each trial
            switch (i % 4)
            {
                case 0:
                    startRoom = "yellow";
                    break;
                case 1:
                    startRoom = "green";
                    break;
                case 2:
                    startRoom = "red";
                    break;
                case 3:
                    startRoom = "blue";
                    break;
                default:
                    startRoom = "error";
                    Debug.Log("Start room specified incorrectly");
                    break;
            }

            // switch the side of the room the rewards are located on for each context
            if (blockLength % 2 != 0)
            {
                Debug.Log("Error: Odd number of trials specified per block. Specify even number for proper counterbalancing");
            }

            if (i < (blockLength / 2))
            {
                contextSide = 1;
            }
            else
            {
                contextSide = 2;
            }

            // Store trial setup in array, for later randomisation
            arrayContexts[i] = context;
            arrayStartRooms[i] = startRoom;
            arrayContextSides[i] = contextSide;
        }

        // Randomise the trial order and save it
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides);

        return firstTrial + blockLength;
    }
    // ********************************************************************** //

    private void TwoContextDoubleRewardBlock(int firstTrial)
    {
        // NOTE: Not currently used
        // ***HRS to write this more efficiently using SingleContextDoubleRewardBlock() later


        // This function specifies the required trials in the block, then randomises the trial order and sets it.
        // NOTE: Use this function if you want to randomise over cheese/wine ordering too

        // This is for a 16 trial block, consisting of 8 double-reward trials in 
        // each context, each split over 2 reward positions (L/L vs R/R), and 
        // across 4 different start locations. 

        string startRoom;
        string context;
        int contextSide;
        int blockLength = 16; // Specify the next 16 trials

        string[] arrayContexts = new string[blockLength];
        string[] arrayStartRooms = new string[blockLength];
        int[] arrayContextSides = new int[blockLength];
        
        for (int i = 0; i < blockLength; i++)
        {
            // separate the trials into two different sub-blocks
            if (i < 8)
            {
                context = "wine";
            }else
            {
                context = "cheese";
            }

            // use a different start location for each trial
            switch (i % 4)
            {
                case 0:
                    startRoom = "yellow";
                    break;
                case 1:
                    startRoom = "green";
                    break;
                case 2:
                    startRoom = "red";
                    break;
                case 3:
                    startRoom = "blue";
                    break;
                default:
                    startRoom = "error";
                    Debug.Log("Start room specified incorrectly");
                    break;
            }

            // switch the side of the room the rewards are located on for each context
            if ( (i < 4) || (i > 11))
            {
                contextSide = 1;
            } else
            {
                contextSide = 2;
            }

            // Store trial setup in array, for later randomisation
            arrayContexts[i] = context;
            arrayStartRooms[i] = startRoom;
            arrayContextSides[i] = contextSide;
        }

        // Randomise the trial order and save it
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides);
    }


    // ********************************************************************** //

    private void SetTrialInContext(int trial, string startRoom, string context, int contextSide)
    {
        // This function specifies the reward covariance

        // Note the variable 'contextSide' specifies whether the two rooms containing the reward will be located on the left or right of the environment
        // e.g. if cheese context: the y/b side, vs the g/r side. if wine context: the y/g side, vs the b/r side.

        bool trialSetCorrectly = false;

            switch (context)
            {
                case "cheese":
                       
                    if (contextSide==1)
                    {
                        SetDoubleRewardTrial(trial, context, startRoom, "yellow", "blue");
                        trialSetCorrectly = true;
                    } 
                    else if (contextSide==2)
                    {
                        SetDoubleRewardTrial(trial, context, startRoom, "green", "red");
                        trialSetCorrectly = true;
                    }
                    break;

                case "wine":

                    if (contextSide == 1)
                    {
                        SetDoubleRewardTrial(trial, context, startRoom, "yellow", "green");
                        trialSetCorrectly = true;
                    }
                    else if (contextSide == 2)
                    {
                        SetDoubleRewardTrial(trial, context, startRoom, "blue", "red");
                        trialSetCorrectly = true;
                    }
                    break;

                default:
                    break;
            }
    
        if (!trialSetCorrectly)
        {
            Debug.Log("Something went wrong specifying the rooms affiliated with each context!");
        }
    }

    // ********************************************************************** //

    private void SetDoubleRewardTrial(int trial, string context, string startRoom, string rewardRoom1, string rewardRoom2)
    {
        // This function writes the trial number indicated by the input variable 'trial'.
        // Note: use this function within another that modulates context such that e.g. for 'cheese', the rooms for room1 and room2 reward are set

        bool collisionInSpawnLocations = true;
        Vector3 adjacentRewardPosition;
        Vector3 rewardLoc;

        // Check that we've inputted a valid trial number
        if ( (trial < setupTrials - 1) || (trial == setupTrials - 1) )
        {
            Debug.Log("Trial randomisation failed: invalid trial number input writing to.");
        }
        else
        {
            // Write the trial according to context and room/start locations
            rewardTypes[trial] = context;

            // this is a double reward trial
            trialMazes[trial] = "FourRooms_" + rewardTypes[trial]; 
            doubleRewardTask[trial] = true;

            // generate the random locations for the presents in each room
            GeneratePresentPositions(trial);

            // select random locations in rooms 1 and 2 for the two rewards (one in each)
            star1Rooms[trial] = rewardRoom1;
            star2Rooms[trial] = rewardRoom2;

            // For a randomly selected reward location within each room
            //star1Positions[trial] = RandomPositionInRoom(rewardRoom1);  
            //star2Positions[trial] = RandomPositionInRoom(rewardRoom2);

            // For specific reward locations (at present/gift locations) within each room
            star1Positions[trial] = RandomPresentInRoom(rewardRoom1);
            star2Positions[trial] = RandomPresentInRoom(rewardRoom2);


            // select start location as random position in given room
            playerStartRooms[trial] = startRoom;
            playerStartPositions[trial] = RandomPositionInRoom(startRoom);

            // make sure the player doesn't spawn on one of the rewards
            while ( collisionInSpawnLocations )
            {
                collisionInSpawnLocations = false;   // benefit of the doubt
                playerStartPositions[trial] = RandomPositionInRoom(startRoom);
               
                // Check player doesn't spawn on, or adjacent to, a reward
                for (int rewardInd = 0; rewardInd < 2; rewardInd++)
                {
                    if (rewardInd == 0)    // check first reward position
                    {
                        rewardLoc = star1Positions[trial];
                    }
                    else                   // check second reward position
                    {
                        rewardLoc = star2Positions[trial];
                    }
                    float[] deltaXPositions = { rewardLoc.x - deltaSquarePosition, rewardLoc.x, rewardLoc.x + deltaSquarePosition };
                    float[] deltaZPositions = { rewardLoc.z - deltaSquarePosition, rewardLoc.z, rewardLoc.z + deltaSquarePosition };

                    // check all 8 positions adjacent to the reward, and the reward position itself
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            adjacentRewardPosition = new Vector3(deltaXPositions[i], star1Positions[trial].y, deltaZPositions[j]);

                            if (playerStartPositions[trial] == adjacentRewardPosition) 
                            {
                                collisionInSpawnLocations = true;   // respawn the player location
                            }
                        }
                    }
                }

                // make sure player doesnt spawn on or adjacent to a present box (makes above obsolete)
                for (int k = 0; k < presentPositions[trial].Length; k++)
                {
                    rewardLoc = presentPositions[trial][k];
                    float[] deltaXPositions = { rewardLoc.x - deltaSquarePosition, rewardLoc.x, rewardLoc.x + deltaSquarePosition };
                    float[] deltaZPositions = { rewardLoc.z - deltaSquarePosition, rewardLoc.z, rewardLoc.z + deltaSquarePosition };

                    // check all 8 positions adjacent to the box, and the box position itself
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            adjacentRewardPosition = new Vector3(deltaXPositions[i], rewardLoc.y, deltaZPositions[j]);

                            if (playerStartPositions[trial] == adjacentRewardPosition)
                            {
                                collisionInSpawnLocations = true;   // respawn the player location
                            }
                        }
                    }
                }

                /*
                // If we decide to have loads of presents, just make sure player doesnt spawn on top of them
                for (int k = 0; k < presentPositions[trial].Length; k++)
                {
                    rewardLoc = presentPositions[trial][k];
                    if (playerStartPositions[trial] == rewardLoc)
                    {
                        collisionInSpawnLocations = true;   // respawn the player location
                    }
                }
                */

            }
            // orient player towards the centre of the environment (will be maximally informative of location in environment)
            playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]); 

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

    private Vector3 RandomPresentInRoom( string roomColour)
    {
        // select a random present in a room of a given colour to put the reward in
        switch (roomColour)
        {
            case "blue":
                return bluePresentPositions[UnityEngine.Random.Range(0, bluePresentPositions.Length - 1)];

            case "red":
                return redPresentPositions[UnityEngine.Random.Range(0, redPresentPositions.Length - 1)];

            case "green":
                return greenPresentPositions[UnityEngine.Random.Range(0, greenPresentPositions.Length - 1)];

            case "yellow":
                return yellowPresentPositions[UnityEngine.Random.Range(0, yellowPresentPositions.Length - 1)];

            default:
                return new Vector3(0.0f, 0.0f, 0.0f);  // this should never happen
        }
    }

    // ********************************************************************** //

    public void ShuffleTrialOrderAndStoreBlock(int firstTrial, int blockLength, string[] arrayContexts, string[] arrayStartRooms, int[] arrayContextSides)
    {
        // This function shuffles the prospective trials from firstTrial to firstTrial+blockLength and stores them.
        // This has been checked and works correctly :)

        string startRoom;
        string context;
        int contextSide;
        bool randomiseOrder = true;
        int n = arrayContexts.Length;

        if (randomiseOrder)
        {
            // Perform the Fisher-Yates algorithm for shuffling array elements in place 
            // (use same sample for each of the 3 arrays to keep order aligned across arrays)
            for (int i = 0; i < n; i++)
            {
                int k = i + rand.Next(n - i); // select random index in array, less than n-i

                // shuffle contexts
                string tempContext = arrayContexts[k];
                arrayContexts[k] = arrayContexts[i];
                arrayContexts[i] = tempContext;

                // shuffle start room
                string tempRoom = arrayStartRooms[k];
                arrayStartRooms[k] = arrayStartRooms[i];
                arrayStartRooms[i] = tempRoom;

                // shuffle context side
                int tempContextSide = arrayContextSides[k];
                arrayContextSides[k] = arrayContextSides[i];
                arrayContextSides[i] = tempContextSide;
            }
        }
        // Store the randomised trial order
        for (int i = 0; i < n; i++)
        {
            startRoom = arrayStartRooms[i];
            context = arrayContexts[i];
            contextSide = arrayContextSides[i];
            SetTrialInContext(i + firstTrial, startRoom, context, contextSide);
        }
    }

    // ********************************************************************** //

    private void GenerateRandomTrialPositions(int trial)
    {
        // Generate a trial that randomly positions the player and reward/s
        playerStartRooms[trial] = ChooseRandomRoom();
        playerStartPositions[trial] = RandomPositionInRoom(playerStartRooms[trial]); // random start position
        playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]);   // orient player towards the centre of the environment

        star1Rooms[trial] = ChooseRandomRoom();
        star2Rooms[trial] = ChooseRandomRoom();
        star1Positions[trial] = RandomPositionInRoom(star1Rooms[trial]);          // random star1 position in random room

        // ensure reward doesnt spawn on the player position (later this will be pre-determined)
        while (playerStartPositions[trial] == star1Positions[trial])
        {
            star1Positions[trial] = RandomPositionInRoom(star1Rooms[trial]);
        }

        // One star, or two?
        if (doubleRewardTask[trial])
        {   // generate another position for star2
            star2Positions[trial] = RandomPositionInRoom(star2Rooms[trial]);      // random star2 position in random room

            // ensure rewards do not spawn on top of each other, or on top of player position
            while ((playerStartPositions[trial] == star2Positions[trial]) || (star1Positions[trial] == star2Positions[trial]))
            {
                star2Positions[trial] = RandomPositionInRoom(star2Rooms[trial]);
            }
        }
        else
        {   // single star to be collected
            star2Positions[trial] = star1Positions[trial];
        }

    }

    // ********************************************************************** //

    private void RandomPlayerAndRewardPositions()
    {
        // This script is used for debugging purposes, to run the experiment without imposing a particular training scheme

        // This function generates trial content that randomly positions the player and reward/s in the different rooms
        int n = possibleRewardTypes.Length;
        int rewardInd;
        for (int trial = setupTrials + practiceTrials; trial < totalTrials - 1; trial++)
        {
            // Deal with restbreaks and regular trials
            if ((trial - setupTrials - practiceTrials + 1) % restFrequency == 0)  // Time for a rest break
            {
                trialMazes[trial] = "RestBreak";
            }
            else                                    // It's a regular trial
            {
                rewardInd = rand.Next(n);           // select a random reward type
                rewardTypes[trial] = possibleRewardTypes[rewardInd];
                trialMazes[trial] = "FourRooms_" + rewardTypes[trial];
                doubleRewardTask[trial] = true;
                GenerateRandomTrialPositions(trial);   // randomly position player start and reward/s locations
            }
        }
    }

    // ********************************************************************** //

    public float JitterTime(float time)
    {
        // jitter uniform-randomly from the min value, to 50% higher than the min value
        return time + (0.5f*time)* (float)rand.NextDouble();
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