using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class DataController : MonoBehaviour {

    /// <summary>
    /// The DataController script is a persistent object which controls all the 
    /// data I/O (e.g. trial loading/sequencing and saving) for the experiment.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 30/11/2018
    /// </summary>

    public GameData gameData;          // data for the entire game, including all trials
    public ExperimentConfig config;   // experiment details, trial sequence, randomisation etc
    public ParticipantData participantData;
    private GameObject PlayerFPS;

    public int currentTrialNumber = 0;
    public bool participantIDSet = false;

    // Data file saving
    private string baseFilePath = "/Users/hannahsheahan/Documents/Postdoc/Unity/Tartarus/Tartarus-Maze-2/data/";
    public DateTime dateTime = DateTime.Now;
    public string stringDateTime; 
    public string filePath;

    // Loading trial configuration variables
    public int totalTrials;


    // ********************************************************************** //

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // when we load new scenes DataController will persist

        // Set up the save file and load in the pre-determined trial sequence. (Note: doing this upfront helps for testing randomisation)
        DataSetup();
        LoadTrialSequence();
    }

    // ********************************************************************** //

    void Start()
    {
        PlayerFPS = GameObject.Find("FPSController");     // This will yield null but its on purpose :)
    }

    // ********************************************************************** //

    private void Update()
    {
        // If we've moved past the StartScreen, we should have generated a FPS Player
        if (GameController.control.GetCurrentMapIndex() > (SceneManager.GetSceneByName("StartScreen").buildIndex-2))
        {
            if (PlayerFPS == null)
            {
                PlayerFPS = GameObject.Find("FPSController");
            }
        }
    }
    // ********************************************************************** //

    private void DataSetup()
    {
        stringDateTime = dateTime.ToString("dd-MM-yy", DateTimeFormatInfo.InvariantInfo) + '_' + dateTime.ToString("t", DateTimeFormatInfo.InvariantInfo);
        filePath = baseFilePath + "dataFile_" + stringDateTime + ".json";  // later add a timestamp number to this so files arent overwritten
        if (File.Exists(filePath))
        {
            Debug.Log("Warning: writing over existing datafile.");
        }
    }
    // ********************************************************************** //

    public void SaveData()
    {
        // do the saving to the json file here (it should taking the data from fps.coords from the trackingScript)
        Debug.Log("Saving trial.");
        string dataAsJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, dataAsJson);
    }

    // ********************************************************************** //

    public void LoadTrialSequence()
    {
        // Load in the trial sequence to the data controller and save it
        config = new ExperimentConfig();
        totalTrials = config.GetTotalTrials();

        // Create the gameData object where we will store all the data
        gameData = new GameData(totalTrials);

        // Data that is consistent across trials
        gameData.totalTrials = totalTrials;
        gameData.dataRecordFrequency = config.GetDataFrequency();
        gameData.restbreakDuration = config.restbreakDuration;

        Debug.Log("Total number of trials to load: " + totalTrials);

        // Add each required trial data to gameData in turn
        for (int trial = 0; trial < totalTrials; trial++)
        {
            gameData.allTrialData[trial].mapName = config.GetTrialMaze(trial);

            // Positions and orientations
            gameData.allTrialData[trial].playerSpawnLocation = config.GetPlayerStartPosition(trial);
            gameData.allTrialData[trial].playerSpawnOrientation = config.GetPlayerStartOrientation(trial);

            gameData.allTrialData[trial].star1Location = config.GetStar1StartPosition(trial);
            gameData.allTrialData[trial].star2Location = config.GetStar2StartPosition(trial);

            // Rewards
            gameData.allTrialData[trial].rewardType = config.GetRewardType(trial);
            gameData.allTrialData[trial].doubleRewardTask = config.GetIsDoubleReward(trial);

            // Timer variables (can change these for each trial later e.g. with jitter)
            gameData.allTrialData[trial].maxMovementTime = config.maxMovementTime;
            gameData.allTrialData[trial].preDisplayCueTime = config.preDisplayCueTime;
            gameData.allTrialData[trial].displayCueTime = config.displayCueTime;
            gameData.allTrialData[trial].goCueDelay      = config.goCueDelay;
            gameData.allTrialData[trial].minDwellAtReward  = config.minDwellAtReward;
            gameData.allTrialData[trial].displayMessageTime = config.displayMessageTime;
            gameData.allTrialData[trial].waitFinishTime  = config.waitFinishTime;
            gameData.allTrialData[trial].errorDwellTime  = config.errorDwellTime;

        }
        SaveData();   // Note: Important to keep this here. It seems unimportant, but without it the timing of object initialisation changes somehow(?) and errors emerge. Make sure this isn't too sensitive or figure out a better way to resolve this issue
    }

    // ********************************************************************** //

    public void AddTrial()
    {
        AssembleTrialData();
        currentTrialNumber++; 
    }

    // ********************************************************************** //

    public void AssembleTrialData()
    {
        // Transfer over the just-finished trial data
        gameData.allTrialData[currentTrialNumber].trialNumber = currentTrialNumber;
        gameData.allTrialData[currentTrialNumber].mapName = GameController.control.GetCurrentMapName();

        // Treat these as list elements so that on trials in which we have multiple attempts we save all the data within that trial
        gameData.allTrialData[currentTrialNumber].FLAG_trialTimeout.Add(GameController.control.FLAG_trialTimeout);
        gameData.allTrialData[currentTrialNumber].FLAG_trialError.Add(GameController.control.FLAG_trialError);
        gameData.allTrialData[currentTrialNumber].firstMovementTime.Add(GameController.control.firstMovementTime);
        gameData.allTrialData[currentTrialNumber].totalMovementTime.Add(GameController.control.totalMovementTime);


        // Add in the frame-by-frame data (these should be synchronized)
        if (PlayerFPS != null)
        {
            // Add in the state transition data
            List<string> trackedStateData = new List<string>(); // We stop collecting data here, just it case it keeps incrementing with another timestep
            trackedStateData = GameController.control.stateTransitions;

            int stringLength = trackedStateData.Count;
            Debug.Log("There were this many tracked state transition timesteps: " + stringLength);

            for (var i = 0; i < stringLength; i++)
            {
                gameData.allTrialData[currentTrialNumber].stateTransitions.Add(trackedStateData[i]);
            }

            // Add in the player tracking data
            List<string> trackedTrialData = new List<string>(); // We stop collecting data here, just it case it keeps incrementing with another timestep
            trackedTrialData = PlayerFPS.GetComponent<TrackingScript>().getCoords();

            stringLength = trackedTrialData.Count;
            Debug.Log("There were this many tracked navigation timesteps: " + stringLength);

            for (var i = 0; i < stringLength; i++)
            {
                gameData.allTrialData[currentTrialNumber].timeStepTrackingData.Add(trackedTrialData[i]);
            }
        }
    }

    // ********************************************************************** //
    /// Get() and Set() Methods
    // ********************************************************************** //

    public GameData GetGameData()
    {
        // Supply the trial data to the GameController
        return gameData; // for now this is a placeholder. Will eventually return which trial we are on etc
    }
    // ********************************************************************** //

    public float GetRecordFrequency()
    {
        return gameData.dataRecordFrequency;
    }
    // ********************************************************************** //

    public TrialData GetCurrentTrialData()
    {
        // Supply the trial data to the GameController
        return gameData.allTrialData[currentTrialNumber]; // for now this is a placeholder. Will eventually return which trial we are on etc
    }

    // ********************************************************************** //

    public void SetParticipantID(string ID)
    {
        if (ID != "")  // you're not allowed to give a fake ID
        {
            participantIDSet = true;
            gameData.participantData.id = ID;
        }
    }

    // ********************************************************************** //

    public ParticipantData GetParticipantData()
    {
        // Supply trial-invariant participant information data
        return gameData.participantData;
    }

    // ********************************************************************** //
    // This is obsolete since filePath is public
    public string GetFilePath()
    {
        return filePath;
    }

    // ********************************************************************** //

    public float GetRestBreakDuration()
    {
        // Supply trial-invariant participant information data
        return gameData.restbreakDuration;
    }
}
