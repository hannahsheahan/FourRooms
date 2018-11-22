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
    //public ParticipantData participantData;
    private GameObject PlayerFPS;

    public int currentTrialNumber = 0;
    public bool participantIDSet = false;
    public bool participantAgeSet = false;
    public bool participantGenderSet = false;

    // Data file saving
    private string baseFilePath = "/Users/hannahsheahan/Documents/Postdoc/Unity/Tartarus/Tartarus-Maze-2/data/";
    public DateTime dateTime = DateTime.Now;
    public string stringDateTime; 
    public string filePath;
    public string fileName;
    public string dataAsJson;   // can probably make private later
    public bool writingDataProperly = true;

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
        WWW www = new WWW("http://localhost:8888/fromunity.php", webData);

        //-----------
        // v2.1 - web server (Summerfield lab one)
        //WWW www = new WWW("http://185.47.61.11/sandbox/tasks/hannahs/martinitask/lib/php/fromunity.php", webData);
        StartCoroutine(WaitForRequest(www));
    }

    // ********************************************************************** //

    // codesource for Post(): https://qiita.com/mattak/items/d01926bc57f8ab1f569a  (received 17/11/2018)
   
    IEnumerator Post(string url, string bodyJsonString)
    {
        writingDataProperly = true;
        Debug.Log("Post coroutine started.");

        var request = new UnityWebRequest(url, "POST");
        byte[] dataRaw = System.Text.Encoding.UTF8.GetBytes(bodyJsonString);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(dataRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        //yield return request.SendWebRequest();
        yield return request.SendWebRequest();

        Debug.Log("Status Code: " + request.responseCode);   // Note: code 200 means it has succeeded

        if (request.error != null)
        {
            Debug.Log("There was a request error.");
            writingDataProperly = false;
        }
        else
        {
            writingDataProperly = true;
        }


    }

    // ********************************************************************** //

    IEnumerator WaitForRequest(WWW data)
    {
        writingDataProperly = true;

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

        // Data that is consistent across trials
        gameData.totalTrials = totalTrials;
        gameData.dataRecordFrequency = config.GetDataFrequency();
        gameData.restbreakDuration = config.restbreakDuration;
        gameData.getReadyDuration = config.getReadyDuration;

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
            gameData.allTrialData[trial].goalHitPauseTime = config.goalHitPauseTime;
            gameData.allTrialData[trial].finalGoalHitPauseTime = config.finalGoalHitPauseTime;
            gameData.allTrialData[trial].preDisplayCueTime = config.preDisplayCueTime;
            gameData.allTrialData[trial].displayCueTime = config.displayCueTime;
            gameData.allTrialData[trial].goCueDelay      = config.goCueDelay;
            gameData.allTrialData[trial].minDwellAtReward  = config.minDwellAtReward;
            gameData.allTrialData[trial].displayMessageTime = config.displayMessageTime;
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
            participantAgeSet = true;
            gameData.participantGender = gender;
        }
    }
    // ********************************************************************** //
    // Note: this is obsolete, don't need separate class for this in datafile.
    //public ParticipantData GetParticipantData()
    // {
    //    // Supply trial-invariant participant information data
    //    return gameData.participantData;
    //}

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
