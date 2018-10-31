using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {
    /// <summary>
    /// This is a singleton script will control the game flow between scenes, 
    /// and centralise everything so all main processes branch from here.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 30 Oct 2018
    /// </summary>

    private DataController dataController;

    private GameData currentGameData;
    private TrialData currentTrialData;
    private ParticipantData currentParticipantData;

    private string participantID;
    private int currentTrialNumber;
    private float timeRemaining;
    private string currentMapName;
    private int currentMapIndex;

    private string filepath;

    public static GameController control;

    // ********************************************************************** //

    void Awake () 
    {
        // Make GameController a singleton
        if (control == null)   // if control doesn't exist, make it
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this) // if control does exist, destroy it
        {
            Destroy(gameObject);
        }
	}

    // ********************************************************************** //

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); //usually be careful with 'Find' but in this case should be ok. Ok this fetches the instance of DataController, dataController.

        // Trial invariant data
        filepath = dataController.filePath;   //this works because we actually have an instance of dataController
        Debug.Log("File path: " + filepath);

        currentGameData = dataController.GetGameData();  // doesn't mean anything yet. Is instantiated but contains nothing.
        currentParticipantData = dataController.GetParticipantData();


        participantID = currentParticipantData.id;   // *** HRS this is not working because object ref not set to an instance of object
        Debug.Log("Participant data: " + participantID);
        //participantID = dataController.GetParticipantData().id;
        /*

        // Data for the current trial
        currentTrialData = dataController.GetCurrentTrialData();
        currentTrialNumber = currentTrialData.trialNumber;
        currentMapName = currentTrialData.mapName;
        currentMapIndex = currentTrialData.mapIndex;

        // Data for current frame in current trial
        timeRemaining = currentTrialData.maxTrialDuration;
        */

    }

    // ********************************************************************** //

    public void NextScene(string scene)
    {
        // Save the current trial data and move to the next scene
        dataController.AddTrial();  // Create a new trial to store data to
        dataController.SaveData();
        Debug.Log("Upcoming scene: " + scene);
        SceneManager.LoadScene(scene);
    }
    // ********************************************************************** //

    public void SceneContinue()
    {
        // Continue by playing the current scene again
        currentMapName = currentTrialData.mapName;
        SceneManager.LoadScene(currentMapName);
    }
    // ********************************************************************** //

    public string GetCurrentMapName()
    {
        // Return the name of the currently active scene/map (for saving as part of TrialData)
        return SceneManager.GetActiveScene().name;
    }

    // ********************************************************************** //

    public int GetCurrentMapIndex()
    {
        // Return the index of the currently active scene/map (for saving as part of TrialData)
        return SceneManager.GetActiveScene().buildIndex;
    }

    // ********************************************************************** //
    // This happens within DataController now
    /*
    public void Save()
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filepath);

        PlayerData data = new PlayerData();
        data.health = health;
        data.experience = experience;

        bf.Serialize(file, data);  // Write participant data to the file
        file.Close();
    }
    */
    // ********************************************************************** //
    /*
    public void Load()
    {
        if(File.Exists(filepath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filepath, FileMode.Open);

            // cast the open binary file as the object type PlayerData to read it properly, since binary files are written object-dependent
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = data.health;
            experience = data.experience;
        }
    }
    */
    // ********************************************************************** //
    // This was a test method that is no longer needed
    /*
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Health: " + health);
        GUI.Label(new Rect(10, 30, 100, 20), "Experience: " + experience);
    }

    [Serializable]
    class PlayerData
    {
        public float health;
        public float experience;
    }
    */

}
