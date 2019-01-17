using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;


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
    public bool[][] bridgeStates;                   // whether the 4 different bridges are ON (active) or OFF (a hole in the floor)

    // Positions and orientations
    private Vector3 mazeCentre;
    private Vector3[] possiblePlayerPositions;
    private string[] playerStartRooms;
    private string[] star1Rooms;
    private string[] star2Rooms;
    private Vector3[] playerStartPositions;
    private Vector3[] playerStartOrientations;
    private Vector3 spawnOrientation;

    private Vector3[] possibleRewardPositions;
    private bool[] presentPositionHistory;
    private Vector3[][] rewardPositions;

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
    private bool[] freeForage;               // array specifying whether each trial was free foraging or not i.e. many rewards or just 2
    private const int ONE_STAR = 0;
    private const int TWO_STARS = 1;
    private string[] possibleRewardTypes; 
    private string[] rewardTypes;             // diamond or gold? (martini or beer)
    public int numberPresentsPerRoom;

    // Timer variables (public since fewer things go wrong if these are changed externally, since this will be tracked in the data, but please don't...)
    public float[] maxMovementTime;
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
        experimentVersion = "mturk_learnwithprepost";
        //experimentVersion = "mturk_learntransferpilot";
        //experimentVersion = "micro_debug"; 
        //experimentVersion = "singleblock_labpilot";
        //experimentVersion = "singleblocktransfer_labpilot";


        // Set these variables to define your experiment:
        switch (experimentVersion)
        {
            case "mturk_learnpilot":       // ----Full 4 block learning experiment-----
                practiceTrials = 2 + getReadyTrial;
                totalTrials = 16 * 4 + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 16 + restbreakOffset;                               // Take a rest after this many normal trials
                restbreakDuration = 30.0f;                                          // how long are the imposed rest breaks?
                break;

            case "mturk_learnwithprepost":
                practiceTrials = 0 + getReadyTrial;
                totalTrials = 16 * 6 + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 16 + restbreakOffset;                               // Take a rest after this many normal trials
                restbreakDuration = 30.0f;                                          // how long are the imposed rest breaks?
                break;

            case "mturk_learntransferpilot":       // ----Full 4 block learning experiment-----
                practiceTrials = 2 + getReadyTrial;
                totalTrials = 16 * 8 + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 16 + restbreakOffset;                               // Take a rest after this many normal trials
                restbreakDuration = 30.0f;                                          // how long are the imposed rest breaks?
                break;

            case "singleblock_labpilot":   // ----Mini 1 block test experiment-----
                practiceTrials = 1 + getReadyTrial;
                totalTrials = 16  + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 20   + restbreakOffset;                          // Take a rest after this many normal trials
                restbreakDuration = 5.0f;                                        // how long are the imposed rest breaks?
                break;

            case "singleblocktransfer_labpilot":   // ----Mini 1 block transfer rewards test experiment-----
                practiceTrials = 1 + getReadyTrial;
                totalTrials = 16 + setupAndCloseTrials + practiceTrials;        // accounts for the Persistent, StartScreen and Exit 'trials'
                restFrequency = 20 + restbreakOffset;                          // Take a rest after this many normal trials
                restbreakDuration = 5.0f;                                        // how long are the imposed rest breaks?
                break;

            case "micro_debug":            // ----Mini debugging test experiment-----
                practiceTrials = 1 + getReadyTrial;
                nExecutedTrials = 2;                                         // note that this is only used for the micro_debug version
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
        //maxMovementTime        = 60.0f;   // changed to be a function of trial number. Time allowed to collect both rewards, incl. wait after hitting first one
        preDisplayCueTime = 1.5f;    // will take a TR during this period
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
        rewardPositions = new Vector3[totalTrials][];
        doubleRewardTask = new bool[totalTrials];
        freeForage = new bool[totalTrials];
        rewardTypes = new string[totalTrials];
        presentPositions = new Vector3[totalTrials][];
        maxMovementTime = new float[totalTrials];
        bridgeStates = new bool[totalTrials][];                   

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

        // Add in the practice trials in an open practice arena with no colour on floors
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
            case "mturk_learnwithprepost":    // ----Full 4 block learning experiment with pre/post free-foraging tests-----

                //---- pre-training free foraging block
                nextTrial = AddFreeForageBlock(nextTrial, "cheeseandwine");
                nextTrial = RestBreakHere(nextTrial);

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
                nextTrial = RestBreakHere(nextTrial);

                //---- post-training free foraging block 
                nextTrial = AddFreeForageBlock(nextTrial, "cheeseandwine");

                break;

            case "mturk_learntransferpilot":  // ----Full 3 block learning + 3 block transfer experiment (2hrs)-----

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
                nextTrial = RestBreakHere(nextTrial);

                //---- transfer block 1
                nextTrial = AddTransferBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);

                //---- transfer block 2
                nextTrial = AddTransferBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);

                //---- transfer block 3
                nextTrial = AddTransferBlock(nextTrial);
                nextTrial = RestBreakHere(nextTrial);

                //---- transfer block 4
                nextTrial = AddTransferBlock(nextTrial);

                break;


            case "singleblock_labpilot":   // ----Mini 1 block test experiment-----

                //---- training block 1
                nextTrial = AddTrainingBlock(nextTrial);
                break;

            case "singleblocktransfer_labpilot":   // ----Mini 1 block test experiment-----

                //---- training block 1
                nextTrial = AddTransferBlock(nextTrial);
                break;

            case "micro_debug":            // ----Mini debugging test experiment-----

                nextTrial = AddTrainingBlock_micro(nextTrial, nExecutedTrials); 
                break;

            default:
                Debug.Log("Warning: defining an untested trial sequence");
                break;
        }

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
        bool freeForageFLAG = false;
        int trialInBlock;
        int contextSide = 1;             // ...this doesn't actually matter for practice trials
        // Add in the practice/familiarisation trials in an open arena
        for (int trial = setupTrials; trial < setupTrials + practiceTrials - 1; trial++)
        {
            trialInBlock = trial - setupTrials;
            // just make the rewards on each side of the hallway/bridge
            if ( trial % 2 == 0 )
            {
                SetDoubleRewardTrial(trial, trialInBlock, "cheese", "blue", "red", "yellow", contextSide, freeForageFLAG);  
            }
            else
            {
                SetDoubleRewardTrial(trial, trialInBlock, "cheese", "red", "green", "blue", contextSide, freeForageFLAG);
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
        bool collisionInSpawnLocations;
        int iterationCounter = 0;
        // generate a random set of N present positions
        for (int i = 0; i < nPresents; i++)
        {
            collisionInSpawnLocations = true;
            iterationCounter = 0;
            // make sure the rewards dont spawn on top of each other
            while (collisionInSpawnLocations)
            {
                iterationCounter++;
                collisionInSpawnLocations = false;   // benefit of the doubt
                positionsInRoom[i] = roomPositions[UnityEngine.Random.Range(0, roomPositions.Length - 1)];

                for (int j = 0; j < i; j++)  // just compare to the present positions already generated
                {
                    if (positionsInRoom[i] == positionsInRoom[j])
                    {
                        collisionInSpawnLocations = true;   // respawn the present location
                    }
                }

                // implement a catchment check for the while loop
                if (iterationCounter > 40) 
                {
                    Debug.Log("There was a while loop error: D");
                    break;
                }
            }
        }
        return positionsInRoom;
    }

    // ********************************************************************** //

    private Vector3[] ChooseNUnoccupiedPresentPositions(int trial, int nPresents, Vector3[] roomPositions)
    {
        Vector3[] positionsInRoom = new Vector3[nPresents];
        Vector3 positionInRoom = new Vector3();
        List<Vector3> spawnableRoomPositions = new List<Vector3>();
        int index;
        int desiredPositionIndex;

        // generate a random set of N present positions in this room
        for (int k = 0; k < nPresents; k++)
        {
            // find the places in the room where we haven't spawned yet this block and turn them into a list
            spawnableRoomPositions.Clear(); 

            for (int j = 0; j < roomPositions.Length; j++)
            {
                index = Array.IndexOf(possibleRewardPositions, roomPositions[j]);

                if (!presentPositionHistory[index])
                {   
                    // add to a list of unoccupied positions that can be sampled from (avoids rejection sampling)
                    spawnableRoomPositions.Add(roomPositions[j]);
                }
            }

            // make sure the reward doesn't spawn in a place that's been occupied previously this block
            bool noValidPositions = !spawnableRoomPositions.Any();
            if (noValidPositions) 
            {   
                // spawn whereever you want
                Debug.Log("All room positions have been previously occupied this block. Present will spawn anywhere in room.");
                positionInRoom = roomPositions[UnityEngine.Random.Range(0, roomPositions.Length - 1)];
            }
            else 
            {   
                // sample an unused position
                desiredPositionIndex = rand.Next(spawnableRoomPositions.Count);
                Debug.Log("This index randomly chosen was: " + desiredPositionIndex);
                positionInRoom = spawnableRoomPositions[desiredPositionIndex];
            }

            positionsInRoom[k] = positionInRoom;

            // update the history of spawn positions
            index = Array.IndexOf(possibleRewardPositions, positionInRoom);
            presentPositionHistory[index] = true;
        }

        return positionsInRoom;
    }

    // ********************************************************************** //

    private void GeneratePresentPositions(int trial, int trialInBlock, bool freeForageFLAG)
    {
        // - If the is a 2 reward covariance trial, spawn the presents in random positions within each room.
        // - However, if this is a free foraging (all rewards) trial, we want to have had
        //   every single square within each room have a present on it within the block, so this requires at least 7 trials and some constrained randomisation.

        // presents can be at any position in the room now
        presentPositions[trial] = new Vector3[numberPresentsPerRoom * 4];
        rewardPositions[trial] = new Vector3[numberPresentsPerRoom * 4];

        if (!freeForageFLAG) 
        { 
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
        else 
        {
            // constrain the randomised locations for the presents to spawn in different places to before
            // Note: each index of presentPositionHistory specifies a different square in the maze. True means the square has had a present on it, False means it hasnt

            // refresh the presentPositionHistory tracker
            if (trialInBlock == 0) 
            {
                presentPositionHistory = new bool[possibleRewardPositions.Length];
                for (int i = 0; i < presentPositionHistory.Length; i++) 
                {
                    presentPositionHistory[i] = false;
                }

                // the first trial in the block can place presents anywhere
                greenPresentPositions = ChooseNRandomPresentPositions(numberPresentsPerRoom, greenRoomPositions);
                redPresentPositions = ChooseNRandomPresentPositions(numberPresentsPerRoom, redRoomPositions);
                yellowPresentPositions = ChooseNRandomPresentPositions(numberPresentsPerRoom, yellowRoomPositions);
                bluePresentPositions = ChooseNRandomPresentPositions(numberPresentsPerRoom, blueRoomPositions);

                // concatenate all the positions of generated presents 
                greenPresentPositions.CopyTo(presentPositions[trial], 0);
                redPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length);
                yellowPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length);
                bluePresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length + yellowPresentPositions.Length);

                // monitor where the presents spawned on this trial
                for (int i = 0; i < possibleRewardPositions.Length; i++)
                {
                    if (presentPositions[trial].Contains(possibleRewardPositions[i]))
                    {
                        presentPositionHistory[i] = true;
                    }
                }
            }
            else 
            {
                // select reward positions based on ones that have not yet been occupied
                // ...but if there isn't a space in the room that hasnt been occupied, just spawn wherever in the room
                greenPresentPositions = ChooseNUnoccupiedPresentPositions(trial, numberPresentsPerRoom, greenRoomPositions);
                redPresentPositions = ChooseNUnoccupiedPresentPositions(trial, numberPresentsPerRoom, redRoomPositions);
                yellowPresentPositions = ChooseNUnoccupiedPresentPositions(trial, numberPresentsPerRoom, yellowRoomPositions);
                bluePresentPositions = ChooseNUnoccupiedPresentPositions(trial, numberPresentsPerRoom, blueRoomPositions);

                // concatenate all the positions of generated presents 
                greenPresentPositions.CopyTo(presentPositions[trial], 0);
                redPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length);
                yellowPresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length);
                bluePresentPositions.CopyTo(presentPositions[trial], greenPresentPositions.Length + redPresentPositions.Length + yellowPresentPositions.Length);

                // monitor where the presents spawned on this trial
                for (int i = 0; i < possibleRewardPositions.Length; i++)
                {
                    if (presentPositions[trial].Contains(possibleRewardPositions[i]))
                    {
                        presentPositionHistory[i] = true;
                    }
                }
            }
        }
         

        //--- alternative version

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

        // specify present positions by coloured room 
        greenPresentPositions = new Vector3[] { new Vector3(xpositions[0], yposition, zpositions[0]), new Vector3(xpositions[1], yposition, zpositions[1]) };
        redPresentPositions = new Vector3[] { new Vector3(xpositions[2], yposition, zpositions[2]), new Vector3(xpositions[3], yposition, zpositions[3]) };
        yellowPresentPositions = new Vector3[] { new Vector3(xpositions[4], yposition, zpositions[4]), new Vector3(xpositions[5], yposition, zpositions[5]) };
        bluePresentPositions = new Vector3[] { new Vector3(xpositions[6], yposition, zpositions[6]), new Vector3(xpositions[7], yposition, zpositions[7]) };
        */
        //-----
    }

    // ********************************************************************** //

    private void GeneratePossibleSettings()
    {
        // Generate all possible spawn locations for player and stars
        possiblePlayerPositions = new Vector3[roomSize * roomSize * 4]; // we are working with 4 square rooms
        possibleRewardPositions = new Vector3[roomSize * roomSize * 4];
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
        AddPossibleLocations(possibleRewardPositions, startind, XPositionsblue, starYposition, ZPositionsblue);
        startind = startind + roomSize * roomSize;

        // Red room
        float[] XPositionsred = { 156f, 164.5f, 173f, 181.5f, 190f };
        float[] ZPositionsred = { 93.3f, 101.8f, 110.3f, 118.8f, 127.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsred, playerYposition, ZPositionsred);
        AddPossibleLocations(possibleRewardPositions, startind, XPositionsred, starYposition, ZPositionsred);
        startind = startind + roomSize * roomSize;

        // Green room
        float[] XPositionsgreen = { 156f, 164.5f, 173f, 181.5f, 190f };
        float[] ZPositionsgreen = { 144.3f, 152.8f, 161.3f, 169.8f, 178.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsgreen, playerYposition, ZPositionsgreen);
        AddPossibleLocations(possibleRewardPositions, startind, XPositionsgreen, starYposition, ZPositionsgreen);
        startind = startind + roomSize * roomSize;

        // Yellow room
        float[] XPositionsyellow = { 105.1f, 113.6f, 122.1f, 130.6f, 139.1f };
        float[] ZPositionsyellow = { 144.3f, 152.8f, 161.3f, 169.8f, 178.3f };

        AddPossibleLocations(possiblePlayerPositions, startind, XPositionsyellow, playerYposition, ZPositionsyellow);
        AddPossibleLocations(possibleRewardPositions, startind, XPositionsyellow, starYposition, ZPositionsyellow);

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
        possibleRewardTypes = new string[] { "wine", "cheese", "banana", "watermelon" };
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
        bool freeForageFLAG = false;

        if (rand.Next(2) == 0)   // randomise whether the wine or cheese sub-block happens first
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine", freeForageFLAG);
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese", freeForageFLAG);
        } else
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese", freeForageFLAG);
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine", freeForageFLAG);
        }
        return nextTrial;
    }

    // ********************************************************************** //

    private int AddTransferBlock(int nextTrial)
    {
        // Add a 16 trial training block to the trial list. Trials are randomised within each context, but not between contexts 
        bool freeForageFLAG = false;

        if (rand.Next(2) == 0)   // randomise whether the watermelon or banana sub-block happens first
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "banana", freeForageFLAG);
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "watermelon", freeForageFLAG);
        }
        else
        {
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "watermelon", freeForageFLAG);
            nextTrial = SingleContextDoubleRewardBlock(nextTrial, "banana", freeForageFLAG);
        }
        return nextTrial;
    }

    // ********************************************************************** //

    private int AddFreeForageBlock(int nextTrial, string rewardSet)
    {
        // Add a 16 trial free-foraging block in which all boxes are rewarded, to the trial list. Trials are randomised within each context, but not between contexts. 
        bool freeForageFLAG = true;

        if (rewardSet == "cheeseandwine") 
        { 
            if (rand.Next(2) == 0)   // randomise whether the wine or cheese sub-block happens first
            {
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine", freeForageFLAG);
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese", freeForageFLAG);
            }
            else
            {
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "cheese", freeForageFLAG);
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "wine", freeForageFLAG);
            }
        }
        /*  // ***HRS debugging
        else if (rewardSet == "bananaandwatermelon") 
        {
            if (rand.Next(2) == 0)   // randomise whether the banana or watermelon sub-block happens first
            {
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "banana", freeForageFLAG);
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "watermelon", freeForageFLAG);
            }
            else
            {
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "watermelon", freeForageFLAG);
                nextTrial = SingleContextDoubleRewardBlock(nextTrial, "banana", freeForageFLAG);
            }
        }
        */
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

    private int SingleContextDoubleRewardBlock(int firstTrial, string context, bool freeForageFLAG)
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

            // Note that the contextSide is important for the context training blocks, but irrelevant for the free-foraging blocks
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
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides, freeForageFLAG);

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
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides, false);

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
        ShuffleTrialOrderAndStoreBlock(firstTrial, blockLength, arrayContexts, arrayStartRooms, arrayContextSides, false);
    }


    // ********************************************************************** //

    private void SetTrialInContext(int trial, int trialInBlock, string startRoom, string context, int contextSide, bool freeForageFLAG)
    {
        // This function specifies the reward covariance

        // Note the variable 'contextSide' specifies whether the two rooms containing the reward will be located on the left or right of the environment
        // e.g. if cheese context: the y/b side, vs the g/r side. if wine context: the y/g side, vs the b/r side.
        // When the trial is a free foraging trial however, the 'contextSide' variable is used to specify which of the bridges is blocked, to control CW and CCW turns from the start room (since rewards are in all rooms).

        bool trialSetCorrectly = false;

            switch (context)
            {
                case "cheese":
                       
                    if (contextSide==1)
                    {
                        SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "yellow", "blue", contextSide, freeForageFLAG);
                        trialSetCorrectly = true;
                    } 
                    else if (contextSide==2)
                    {
                        SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "green", "red", contextSide, freeForageFLAG);
                        trialSetCorrectly = true;
                    }
                    break;

                case "wine":

                    if (contextSide == 1)
                    {
                        SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "yellow", "green", contextSide, freeForageFLAG);
                        trialSetCorrectly = true;
                    }
                    else if (contextSide == 2)
                    {
                        SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "blue", "red", contextSide, freeForageFLAG);
                        trialSetCorrectly = true;
                    }
                    break;

            case "watermelon":

                if (contextSide == 1)
                {
                    SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "yellow", "blue", contextSide, freeForageFLAG);
                    trialSetCorrectly = true;
                }
                else if (contextSide == 2)
                {
                    SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "green", "red", contextSide, freeForageFLAG);
                    trialSetCorrectly = true;
                }
                break;

            case "banana":

                if (contextSide == 1)
                {
                    SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "yellow", "green", contextSide, freeForageFLAG);
                    trialSetCorrectly = true;
                }
                else if (contextSide == 2)
                {
                    SetDoubleRewardTrial(trial, trialInBlock, context, startRoom, "blue", "red", contextSide, freeForageFLAG);
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

    private void SetDoubleRewardTrial(int trial, int trialInBlock, string context, string startRoom, string rewardRoom1, string rewardRoom2, int contextSide, bool freeForageFLAG)
    {
        // This function writes the trial number indicated by the input variable 'trial'.
        // Note: use this function within another that modulates context such that e.g. for 'cheese', the rooms for room1 and room2 reward are set

        bool collisionInSpawnLocations = true;
        int iterationCounter = 0;
        bridgeStates[trial] = new bool[4];                  // there are 4 bridges

        // Check that we've inputted a valid trial number
        if ( (trial < setupTrials - 1) || (trial == setupTrials - 1) )
        {
            Debug.Log("Trial randomisation failed: invalid trial number input writing to.");
        }
        else
        {
            // Write the trial according to context and room/start locations
            rewardTypes[trial] = context;
            doubleRewardTask[trial] = true;

            // generate the random locations for the presents in each room
            GeneratePresentPositions(trial, trialInBlock, freeForageFLAG);

            if (freeForageFLAG) 
            {
                // rewards are positioned in all boxes
                trialMazes[trial] = "PrePostForage_" + rewardTypes[trial];
                freeForage[trial] = true;
                maxMovementTime[trial] = 120.0f;       // 2 mins to collect all rewards on freeforaging trials

                // select random locations in rooms 1 and 2 for the two rewards (one in each)
                star1Rooms[trial] = "";
                star2Rooms[trial] = "";

                // Specific reward locations within each room for all rewards
                for (int i = 0; i < presentPositions[trial].Length; i++)
                {
                    rewardPositions[trial][i] = presentPositions[trial][i];
                }

                // all the bridges that are available for walking over...
                for (int i = 0; i < bridgeStates[trial].Length; i++)
                {
                    bridgeStates[trial][i] = true;
                }

                // determine which bridge to disable, to control CW vs CCW turns
                // Note: contextSide==1 means they have to turn CW, contextSide==2 means they have to turn CCW
                switch (startRoom) 
                {
                    case "blue":
                        if (contextSide==1)
                        {
                            bridgeStates[trial][2] = false; // bridge 3
                        }
                        else
                        {
                            bridgeStates[trial][3] = false; // bridge 4
                        }
                        break;

                    case "red":
                        if (contextSide == 1)
                        {
                            bridgeStates[trial][1] = false; // bridge 2
                        }
                        else
                        {
                            bridgeStates[trial][2] = false; // bridge 3
                        }
                        break;

                    case "yellow":
                        if (contextSide == 1)
                        {
                            bridgeStates[trial][3] = false; // bridge 4
                        }
                        else
                        {
                            bridgeStates[trial][0] = false; // bridge 1
                        }
                        break;

                    case "green":
                        if (contextSide == 1)
                        {
                            bridgeStates[trial][0] = false; // bridge 1
                        }
                        else
                        {
                            bridgeStates[trial][1] = false; // bridge 2
                        }
                        break;

                    default:
                        Debug.Log("Warning: invalid room specified, trial sequence will not be properly counterbalanced.");
                        break;               
                }

            }
            else
            { 
                // this is a two-reward trial
                trialMazes[trial] = "FourRooms_" + rewardTypes[trial];
                freeForage[trial] = false;
                maxMovementTime[trial] = 60.0f;        // 1 min to collect just the 2 rewards on covariance trials

                // select random locations in rooms 1 and 2 for the two rewards (one in each)
                star1Rooms[trial] = rewardRoom1;
                star2Rooms[trial] = rewardRoom2;

                // Specific reward locations within each room for all rewards
                rewardPositions[trial][0] = RandomPresentInRoom(rewardRoom1);
                rewardPositions[trial][1] = RandomPresentInRoom(rewardRoom2);

                // all the bridges are available for walking over
                for (int i = 0; i < bridgeStates[trial].Length; i++) 
                { 
                    bridgeStates[trial][i] = true;
                }
            }

            // select start location as random position in given room
            playerStartRooms[trial] = startRoom;
            playerStartPositions[trial] = RandomPositionInRoom(startRoom);
            iterationCounter = 0;

            // make sure the player doesn't spawn on one of the rewards
            while ( collisionInSpawnLocations )
            {
                iterationCounter++;
                collisionInSpawnLocations = false;   // benefit of the doubt
                playerStartPositions[trial] = RandomPositionInRoom(startRoom);
               
                // make sure player doesnt spawn on a present box
                for (int k = 0; k < presentPositions[trial].Length; k++)
                {
                    if (playerStartPositions[trial] == presentPositions[trial][k])
                    {
                        collisionInSpawnLocations = true;   // respawn the player location
                    }
                    /*// This check was to stop spawning adjacent to a present box, but for several present arrangements this is impossible and results in an infinite while loop
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
                    */
                }
                // implement a catchment check for the while loop
                if (iterationCounter > 40) 
                {
                    Debug.Log("There was a while loop error: C");
                    break;
                }

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

    public void ShuffleTrialOrderAndStoreBlock(int firstTrial, int blockLength, string[] arrayContexts, string[] arrayStartRooms, int[] arrayContextSides, bool freeForageFLAG)
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
            SetTrialInContext(i + firstTrial, i, startRoom, context, contextSide, freeForageFLAG);
        }
    }

    // ********************************************************************** //

    private void GenerateRandomTrialPositions(int trial)
    {
        int iterationCounter = 0;

        // Generate a trial that randomly positions the player and reward/s
        playerStartRooms[trial] = ChooseRandomRoom();
        playerStartPositions[trial] = RandomPositionInRoom(playerStartRooms[trial]); // random start position
        playerStartOrientations[trial] = findStartOrientation(playerStartPositions[trial]);   // orient player towards the centre of the environment

        // adapted for array of reward positions
        star1Rooms[trial] = ChooseRandomRoom();
        star2Rooms[trial] = ChooseRandomRoom();
        rewardPositions[trial][0] = RandomPositionInRoom(star1Rooms[trial]);          // random star1 position in random room

        // ensure reward doesnt spawn on the player position (later this will be pre-determined)
        while (playerStartPositions[trial] == rewardPositions[trial][0])
        {
            iterationCounter++;
            rewardPositions[trial][0] = RandomPositionInRoom(star1Rooms[trial]);

            // implement a catchment check for the while loop
            if (iterationCounter > 40)
            {
                Debug.Log("There was a while loop error:  A");
                break;
            }
        }

        // One star, or two?
        if (doubleRewardTask[trial])
        {   // generate another position for star2
            rewardPositions[trial][1] = RandomPositionInRoom(star2Rooms[trial]);      // random star2 position in random room
            iterationCounter = 0;
            // ensure rewards do not spawn on top of each other, or on top of player position
            while ((playerStartPositions[trial] == rewardPositions[trial][1]) || (rewardPositions[trial][0] == rewardPositions[trial][1]))
            {
                iterationCounter++;
                rewardPositions[trial][1] = RandomPositionInRoom(star2Rooms[trial]);

                // implement a catchment check for the while loop
                if (iterationCounter > 40) 
                {
                    Debug.Log("There was a while loop error: B");
                    break;
                }
            }
        }
        else
        {   // single star to be collected
            rewardPositions[trial][1] = rewardPositions[trial][0];
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

    public Vector3[] GetRewardStartPositions(int trial)
    {
        return rewardPositions[trial];
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

    public bool GetIsFreeForaging(int trial)
    {
        return freeForage[trial];
    }

    // ********************************************************************** //

}