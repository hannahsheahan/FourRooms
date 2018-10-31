using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DataController : MonoBehaviour {

    public GameData gameData;    // data for the entire game, including all trials
    public ParticipantData participantData;

    public int currentTrialNumber = 0;

    private string baseFilePath = "/Users/hannahsheahan/Documents/Postdoc/Unity/Tartarus/Tartarus-Maze-2/data/";
    private string participantDataFileName = "test.json"; // later make this the participant ID (cast as int)
    public string filePath;

    // ********************************************************************** //

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // when we load new scenes DataController will persist

        // Set up the save file
        DataSetup();
    }

    // ********************************************************************** //

    void Start()
    {
        GameController.control.NextScene("StartScreen");  // Note: always try to use GameController.control.NextScene to transition scenes since this autosaves the trial data.

    }
    // ********************************************************************** //

    private void DataSetup()
    {
        filePath = baseFilePath + participantDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("Warning: writing over existing datafile.");
        }
        gameData = new GameData();
        SaveData();
    }
    // ********************************************************************** //

    public void SaveData()
    {
        // do the saving to the json file here (it should taking the data from fps.coords from the trackingScript)
        string dataAsJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, dataAsJson);
    }

    public void AddTrial()
    {
        // Load in the new trial data
        Debug.Log("Next trial starting");
        gameData.allTrialData[currentTrialNumber].trialNumber = currentTrialNumber;
        gameData.allTrialData[currentTrialNumber].mapIndex = GameController.control.GetCurrentMapIndex();
        gameData.allTrialData[currentTrialNumber].mapName = GameController.control.GetCurrentMapName();

        // Will add in the spawn locations etc here later once we've got that working


        currentTrialNumber++;

    }
    // ********************************************************************** //
    /// Get and Set Methods
    // ********************************************************************** //

    public GameData GetGameData()
    {
        // Supply the trial data to the GameController
        return gameData; // for now this is a placeholder. Will eventually return which trial we are on etc
    }
    // ********************************************************************** //

    public TrialData GetCurrentTrialData()
    {
        // Supply the trial data to the GameController
        return gameData.allTrialData[0]; // for now this is a placeholder. Will eventually return which trial we are on etc
    }

    // ********************************************************************** //

    public void SetParticipantID(string ID)
    {
        gameData.participantData.id = ID;
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
}
