using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
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
    private GameObject PlayerFPS;

    public int currentTrialNumber = 0;
    public bool participantIDSet = false;
    public bool participantAgeSet = false;
    public bool participantGenderSet = false;
    public bool participantFeedbackGiven = false;
    public bool participantFeedbackSubmitted = false;        

    // Data file saving
    private string baseFilePath = "/Users/hannahsheahan/Documents/Postdoc/Unity/FourRooms/data/";
    public DateTime dateTime = DateTime.Now;
    public string stringDateTime; 
    public string filePath;
    public string fileName;
    public string dataAsJson;   // can probably make private later
    public bool writingDataProperly = true;
    public string confirmationCode;

    // Loading trial configuration variables
    public int totalTrials;
    public List<int> trialList = new List<int>();  // this makes dynamically changing/reinserting error trials at different locations possible
    public int trialListIndex = 0;                 // keeps track of where in the trial sequence we are (independent of currentTrialNumber for where to save the data)
    public int numberPresentsPerRoom;

    public System.Random rnd = new System.Random();

    // ********************************************************************** //

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // when we load new scenes DataController will persist

        // Create a random subject completion/confirmation code
        int code = rnd.Next(0, 1000000);          // This will specify a subject-unique (probably) confirmation code for them to enter after finishing experiment to show completion
        confirmationCode = code.ToString();

        while (confirmationCode.Length < 7)       // pad the code string with zeros until its 7 digits
        {
            confirmationCode = "0" + confirmationCode;
        }

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
        stringDateTime = dateTime.ToString("dd-MM-yy", DateTimeFormatInfo.InvariantInfo) + '_' + dateTime.ToString("t", DateTimeFormatInfo.InvariantInfo) + '_' + confirmationCode;
        stringDateTime = stringDateTime.Replace("/", "-");   // make sure you don't have conflicting characters for writing to web server
        stringDateTime = stringDateTime.Replace(":", "-");   // make sure you don't have conflicting characters for writing to web server

        fileName = "dataFile_" + stringDateTime + ".json";
        filePath = baseFilePath + fileName;  // later add a timestamp number to this so files arent overwritten
        if (File.Exists(filePath))
        {
            Debug.Log("Warning: writing over existing datafile.");
        }
    }
    // ********************************************************************** //

    public void SaveData()
    {
        // convert the data to JSON format
        Debug.Log("Saving trial.");
        dataAsJson = JsonUtility.ToJson(gameData);

        WWWForm webData = new WWWForm();
        webData.AddField("gameData", dataAsJson);
        webData.AddField("fileName", fileName);

        // v1.0 - local file saving
        //File.WriteAllText(filePath, dataAsJson);
        //-----------
        // v2.0 - local server testing (using MAMP)
        //WWW www = new WWW("http://localhost:8888/fromunity.php", webData);
        //-----------

        // v2.1 - web server (Summerfield lab one)
        WWW www = new WWW("http://185.47.61.11/sandbox/tasks/hannahs/martinitask/lib/php/fromunity.php", webData);
        StartCoroutine(WaitForRequest(www));
    }

    // ********************************************************************** //

    IEnumerator WaitForRequest(WWW data)
    {
        // writingDataProperly = true;

        yield return data;
        if (data.error != null)
        {
            writingDataProperly = false;
        }
        else
        {
            Debug.Log(data.text);
            writingDataProperly = true;
        }
    }

    // ********************************************************************** //

    public void LoadTrialSequence()
    {
        // Load in the trial sequence to the data controller and save it
        config = new ExperimentConfig();
        totalTrials = config.GetTotalTrials();

        // Create the gameData object where we will store all the data
        gameData = new GameData(totalTrials);

        // Specify the number of presents/gifts per room for tracking their state in the GameController
        numberPresentsPerRoom = config.numberPresentsPerRoom;

        // Data that is consistent across trials
        gameData.confirmationCode = confirmationCode;
        gameData.experimentVersion = config.experimentVersion;
        gameData.totalTrials = totalTrials;
        gameData.dataRecordFrequency = config.GetDataFrequency();
        gameData.restbreakDuration = config.restbreakDuration;
        gameData.getReadyDuration = config.getReadyDuration;
        gameData.transferCounterbalance = config.transferCounterbalance;
        gameData.intermingledTrials = config.intermingledTrials;

        Debug.Log("Total number of trials to load: " + totalTrials);

        // Add each required trial data to gameData in turn
        for (int trial = 0; trial < totalTrials; trial++)
        {
            trialList.Add(trial); 
            gameData.allTrialData[trial].mapName = config.GetTrialMaze(trial);

            // Positions and orientations
            gameData.allTrialData[trial].playerSpawnLocation = config.GetPlayerStartPosition(trial);
            gameData.allTrialData[trial].playerSpawnOrientation = config.GetPlayerStartOrientation(trial);

            gameData.allTrialData[trial].rewardPositions = config.GetRewardStartPositions(trial);
            gameData.allTrialData[trial].presentPositions = config.presentPositions[trial];
            gameData.allTrialData[trial].bridgeStates = config.bridgeStates[trial];

            // Rewards
            gameData.allTrialData[trial].rewardType = config.GetRewardType(trial);
            gameData.allTrialData[trial].doubleRewardTask = config.GetIsDoubleReward(trial);
            gameData.allTrialData[trial].freeForage = config.GetIsFreeForaging(trial);

            // Timer variables (can change these for each trial later e.g. with jitter)
            gameData.allTrialData[trial].maxMovementTime = config.maxMovementTime[trial];
            gameData.allTrialData[trial].goalHitPauseTime = config.goalHitPauseTime;
            gameData.allTrialData[trial].finalGoalHitPauseTime = config.finalGoalHitPauseTime;
            gameData.allTrialData[trial].preDisplayCueTime = config.preDisplayCueTime;
            gameData.allTrialData[trial].displayCueTime = config.displayCueTime;
            gameData.allTrialData[trial].goCueDelay      = config.goCueDelay;
            gameData.allTrialData[trial].minDwellAtReward  = config.minDwellAtReward;
            gameData.allTrialData[trial].displayMessageTime = config.displayMessageTime;
            gameData.allTrialData[trial].errorDwellTime  = config.errorDwellTime;
            gameData.allTrialData[trial].hallwayFreezeTime = config.hallwayFreezeTime;
        }
        SaveData();   // Note: Important to keep this here. It seems unimportant, but without it the timing of object initialisation changes somehow(?) and errors emerge. Make sure this isn't too sensitive or figure out a better way to resolve this issue
    }

    // ********************************************************************** //

    public void AddTrial()
    {
        AssembleTrialData();

        currentTrialNumber = trialList[trialListIndex + 1];  // This is incorrect I think. We want the trial List and current trial number to be essentially independnt
        trialListIndex++;
    }

    // ********************************************************************** //

    public void ReinsertErrorTrial()
    {
        AssembleTrialData();                                         // store the error data for that attempt

        // Determine where to integrate another trial attempt
        int trialInsertIndex = trialListIndex + 1;                   // default to repeating trial immediately (if code below is ever deprecated)
        int trialInsertNumber;
        int trial = currentTrialNumber;
        string[] menuScenesArray = new string[] { "Exit", "RestBreak", "GetReady" };
        bool searching = true;

        if (!gameData.intermingledTrials) 
        {
            // find the next trial in the trial list that is of a different context to the current (error trial) context
            while (gameData.allTrialData[trial].mapName == gameData.allTrialData[currentTrialNumber].mapName)
            {
                trial++;
                if (trial == totalTrials)                                // we need an out for this loop, and don't want to access trials that dont exist
                {
                    break;
                }
            }
        }
        else 
        {
            // the trials are sequenced to intermingle across reward types, so just insert the trial at the end of this block before the getready/restbreak/exit
            while (searching)
            {
                int pos = Array.IndexOf(menuScenesArray, gameData.allTrialData[trial].mapName);
                // the trial is a restbreak, exit or get ready trial, so choose the trial before it as the reinsertion point.
                if (pos > -1)
                {
                    searching = false;                                   // safety check
                    break;
                }
                else 
                { 
                    trial++;
                }
                if (trial == totalTrials)                                // this should never be struck, but its a good safety to make sure we don't access an element that doesnt exist
                {
                    Debug.Log("Something has gone a bit wrong with trial reinsertion because this shouldn't trigger.");
                    break;
                }
            }
        }

        trialInsertNumber = trial;                                           // the trial NUMBER to insert the repeat trial at
        trialInsertIndex = trialList.FindIndex(a => a == trialInsertNumber); // the corresponding trial INDEX in the trialList (will change as we perform more error trials etc)

        // Insert the repeat trial just before the context change
        trialList.Insert(trialInsertIndex, currentTrialNumber); 

        // load next trial in trial list
        currentTrialNumber = trialList[trialListIndex + 1];        
        trialListIndex++;
    }

    // ********************************************************************** //

    public void AssembleTrialData()
    {
        Debug.Log("Current trial is: " + currentTrialNumber);
        // Update the gameData with the actual executed trialList
        gameData.trialList = trialList;

        // Transfer over the just-finished trial data
        gameData.allTrialData[currentTrialNumber].trialNumber = currentTrialNumber;
        gameData.allTrialData[currentTrialNumber].mapName = GameController.control.GetCurrentMapName();

        // Treat these as list elements so that on trials in which we have multiple attempts we save all the data within that trial
        gameData.allTrialData[currentTrialNumber].FLAG_trialTimeout.Add(GameController.control.FLAG_trialTimeout);
        gameData.allTrialData[currentTrialNumber].FLAG_trialError.Add(GameController.control.FLAG_trialError);
        gameData.allTrialData[currentTrialNumber].FLAG_dataWritingError.Add(GameController.control.FLAG_dataWritingError);
        gameData.allTrialData[currentTrialNumber].FLAG_frameRateError.Add(GameController.control.FLAG_frameRateError);
        gameData.allTrialData[currentTrialNumber].FLAG_fullScreenModeError.Add(GameController.control.FLAG_fullScreenModeError);
        gameData.allTrialData[currentTrialNumber].FLAG_cliffFallError.Add(GameController.control.FLAG_cliffFallError);
        gameData.allTrialData[currentTrialNumber].firstMovementTime.Add(GameController.control.firstMovementTime);
        gameData.allTrialData[currentTrialNumber].totalMovementTime.Add(GameController.control.totalMovementTime);
        gameData.allTrialData[currentTrialNumber].trialListIndex.Add(trialListIndex);


        // This is only updated if the trial is finished correctly anyway
        gameData.allTrialData[currentTrialNumber].trialScore = GameController.control.trialScore;

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

            // Add in the gift wrapping state transition data
            List<string> trackedGiftWrapStateData = new List<string>(); // We stop collecting data here, just it case it keeps incrementing with another timestep
            trackedGiftWrapStateData = GameController.control.giftWrapStateTransitions;

            stringLength = trackedGiftWrapStateData.Count;
            for (var i = 0; i < stringLength; i++)
            {
                gameData.allTrialData[currentTrialNumber].giftWrapStateTransitions.Add(trackedGiftWrapStateData[i]);
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
            gameData.participantID = ID;
        }
    }

    // ********************************************************************** //

    public void SetParticipantFeedback(string feedback)
    {
        if (feedback != "")  // you must provide SOME feedback
        {
            participantFeedbackGiven = true;
            gameData.participantFeedback = feedback;
        }
    }

    // ********************************************************************** //

    public void SubmitParticipantFeedback() 
    {
        // Note that we are using a separate flag for this because we need the
        // feedback to save to file OnClick(), before they reveal the code and exit.
        if (participantFeedbackGiven) 
        { 
            SaveData();

            participantFeedbackSubmitted = true;
        }
    }

    // ********************************************************************** //

    public void SetParticipantAge(string age)
    {
        if (age != "")  // you're not allowed to give a fake age  ***HRS can add check for numbers
        {
            participantAgeSet = true;
            gameData.participantAge = age;
        }
    }

    // ********************************************************************** //

    public void SetParticipantGender(int gender)
    {
        if (gender != 0) // must make a selection
        {
            participantGenderSet = true;
            gameData.participantGender = gender;
        }
    }

    // ********************************************************************** //

    public string GetConfirmationCode()
    {
        return confirmationCode;
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

    // ********************************************************************** //

    public float GetGetReadyDuration()
    {
        // Supply trial-invariant participant information data
        return gameData.getReadyDuration;
    }
}
