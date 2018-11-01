using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;

public class GameController : MonoBehaviour {
    /// <summary>
    /// This is a singleton script will control the game flow between scenes, 
    /// and centralise everything so all main processes branch from here.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 30 Oct 2018
    /// </summary>

    // Persistent controllers for data management and gameplay
    private DataController dataController;
    public static GameController control;

    // Game data
    private GameObject PlayerFPS;
    private GameData currentGameData;
    private string filepath;

    // End-of-trial data
    private TrialData currentTrialData;
    private ParticipantData currentParticipantData;
    private string currentMapName;
    private int currentMapIndex;
    private string participantID;
    private int currentTrialNumber;
    private int currentSceneIndex;
    private string currentSceneName;

    // Within-trial data that changes with timeframe
    private bool playerControlActive;
    private bool starFound = false;
    public GameObject star;
    public string screenMessage;
    public string screenMessageColor;
    //private Text wellDoneText;
    //private Text screenMessage;

    // Timer variables
    private Timer stateTimer;
    private Timer movementTimer;
    public float movementTime; 
    private float goalAppearDelay = 0.0f;   // *** HRS Figure out how to package these up into dataController savefile later
    private float goCueDelay = 0.0f;
    public  float minDwellAtStar = 0.5f;  // 500 ms
    public float displayMessageTime = 1.5f; // 1.5 sec 
    public float timeRemaining;


    // Game-play state machine states
    public const int STATE_STARTSCREEN = 0;
    public const int STATE_STARTTRIAL  = 1;
    public const int STATE_GOALAPPEAR  = 2;
    public const int STATE_DELAY       = 3;
    public const int STATE_GO          = 4;
    public const int STATE_MOVING      = 5;
    public const int STATE_STARFOUND   = 6;
    public const int STATE_FINISH      = 7;
    public const int STATE_NEXTTRIAL   = 8;
    public const int STATE_INTERTRIAL  = 9;
    public const int STATE_TIMEOUT     = 10;
    public const int STATE_ERROR       = 11;
    public const int STATE_REST        = 12;
    public const int STATE_MAX         = 13;

    private string[] stateText = new string[] { "StartScreen","StartTrial","GoalAppear","Delay","Go","Moving","GoalHit","Finish","NextTrial","InterTrial","Timeout","Error","Rest", "Max" };
    public int State;
    private bool gameStarted = false;

    // ********************************************************************** //

    void Awake ()           // Awake() executes once before anything else
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

    private void Start()     // Start() executes once when object is created
    {
        dataController = FindObjectOfType<DataController>(); //usually be careful with 'Find' but in this case should be ok. Ok this fetches the instance of DataController, dataController.

        // Trial invariant data
        filepath = dataController.filePath;   //this works because we actually have an instance of dataController
        Debug.Log("File path: " + filepath);

        currentGameData = dataController.GetGameData();  // doesn't mean anything yet. Is instantiated but contains nothing.
        currentParticipantData = dataController.GetParticipantData();
        participantID = currentParticipantData.id;

        // Initialize variables with data for the current trial
        currentTrialData = dataController.GetCurrentTrialData();
        currentTrialNumber = currentTrialData.trialNumber;
        currentMapName = currentTrialData.mapName;
        currentMapIndex = currentTrialData.mapIndex;

        // Initialise FSM State
        State = STATE_STARTSCREEN;
        stateTimer = new Timer();
        stateTimer.Reset();

        movementTimer = new Timer();

    }
    // ********************************************************************** //

    private void Update()     // Update() executes once per frame
    {
        // ***HRS to implement finite state machine in here to control game play
        // Will take basically what HideStar.cs was doing and do it here

        // HideStar should just trigger an event that updates a GameController variable



        // Data for current frame in current trial (will go in FSM)
       // timeRemaining = currentTrialData.maxTrialDuration;
       // currentMapIndex = dataController.GetCurrentTrialData().mapIndex;
        // ***HRS ^ this currentMapIndex is doing weird not updating things, so...
        // I think perhaps they have not been instantiated?

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 2; // ***HRS this is a hack but for now it's fine.
        currentSceneName = "tartarus" + currentSceneIndex;

        switch (State)
        {

            case STATE_STARTSCREEN:

                if (gameStarted)
                {
                    PlayerFPS = GameObject.Find("FPSController"); // Create a local reference to the player object that has just been created
                    StateNext(STATE_STARTTRIAL);
                }
                break;

            case STATE_STARTTRIAL:
                if (stateTimer.ElapsedSeconds() > displayMessageTime)
                {
                    screenMessage = "";
                }
                // Make sure we're found the player and enabled it to move (this is actually redundant)
                if (PlayerFPS != null)  
                {
                    if (!PlayerFPS.GetComponent<FirstPersonController>().enabled)
                    {
                        PlayerFPS.GetComponent<FirstPersonController>().enabled = true;  
                    }
                }
                else
                {
                    PlayerFPS = GameObject.Find("FPSController");
                }

                // Wait until the goal/target can appear
                if (stateTimer.ElapsedSeconds() >= goalAppearDelay)
                {
                    StateNext(STATE_GOALAPPEAR);
                }
                break;


            case STATE_GOALAPPEAR:
                // display the star (so far its already visible so can add this later)
                starFound = false;
                StateNext(STATE_DELAY);
                break;

            case STATE_DELAY:
                // Wait for the go cue
                if (stateTimer.ElapsedSeconds() >= goCueDelay)
                {
                    StateNext(STATE_GO);
                }
                break;

            case STATE_GO:

                screenMessage = "Find the star!";

                // Make a 'beep' go sound and start the trial timer
                movementTimer.Reset();

                StateNext(STATE_MOVING);
                break;

            case STATE_MOVING:

                if (stateTimer.ElapsedSeconds() > displayMessageTime)
                {
                    screenMessage = "";
                }

                if (starFound)
                {
                    movementTime = movementTimer.ElapsedSeconds();
                    StateNext(STATE_STARFOUND);
                }
                break;

            case STATE_STARFOUND:
                // display a congratulatory message

                StateNext(STATE_FINISH);
                break;

            case STATE_FINISH:

                // end the trial, save the data
                Debug.Log("The current map is: " + currentSceneIndex + " so the next map will be: " + (currentSceneIndex + 1));
                NextScene("tartarus" + (currentSceneIndex + 1));
                StateNext(STATE_STARTTRIAL);
                break;

            case STATE_ERROR:

                Debug.Log("ERROR STATE");

                // ***HRS show a message on the screen because something went wrong


                // ***HRS  Later differentiate between the different errors e.g. timeout

                //DisplayMessage("Restarting trial");
                //GUI.Label(new Rect(10, 10, 100, 20), "Restarting trial");

                screenMessageColor = "red";
                screenMessage = "Restarting trial";

                // save the data and restart the trial
                NextScene(currentSceneName);
                StateNext(STATE_STARTTRIAL);
                break;


        }




    }
    // ********************************************************************** //

    public void NextScene(string scene)
    {
        // Save the current trial data and move to the next scene
        dataController.AddTrial();  // Create a new trial to store data to
        dataController.SaveData();

        // reset the message on the screen
        screenMessage = "";

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

    public void StartGame()
    {
        // start game from the desired initial maze/trial 
        currentMapIndex = dataController.GetCurrentTrialData().mapIndex;

        // start the first trial
        NextScene("tartarus" + (currentMapIndex + 1));
        gameStarted = true;
    }

    // ********************************************************************** //

    public void StateNext(int state)
    {
        // Transition the FSM to the next state
        if (State != state)
        {
            Debug.Log("STATE TRANSITION: " + stateText[State] + " -> " + stateText[state] + ": (" + stateTimer.ElapsedSeconds().ToString("F2") + " sec)");
            State = state;
            stateTimer.Reset();   // start counting how much time we're in this new state
        }
    }

    // ********************************************************************** //
    /*
    public void DisplayMessage(string message)
    {
        screenMessage.text = message;
    }
    */
    // ********************************************************************** //

    public void LavaDeath()
    {
        Debug.Log("AAAAAAAAAAAAH! You fell and hit the lava!");
        // You've fallen into the lava, so disable the player controller, give an error message, save the data and restart the trial
        StateNext(STATE_ERROR);
    }

    // ********************************************************************** //

    public void StarFound()
    {
        starFound = true; // The player has been at the star for minDwellAtStar seconds
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


}
