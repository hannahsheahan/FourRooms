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
    private bool doubleRewardTask = false;       // later set this in the config file. If twoRewardTask == false, there is just one star to collect on each trial

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

    // Audio clips
    public AudioClip starFoundSound;
    public AudioClip goCueSound;
    public AudioClip errorSound; 
    public AudioClip fallSound;
    private AudioSource source;

    // Messages to the screen
    public bool FLAG_trialError;
    public bool FLAG_trialTimeout;
    private string displayMessage = "noMessage";
    private string textMessage = "";
    public string screenMessageColor;

    // Timer variables
    private Timer stateTimer;
    private Timer movementTimer;
    public Timer messageTimer;
    public float firstMovementTime;
    public float totalMovementTime;
    public float maxMovementTime     = 15.0f;  
    private float goalAppearDelay    = 0.0f;   // *** HRS Figure out how to package these up into dataController savefile later
    private float goCueDelay         = 1.5f;
    public  float minDwellAtStar     = 0.5f;  // 500 ms
    public float displayMessageTime  = 1.5f; // 1.5 sec 
    public float waitFinishTime      = 1.5f;
    public float errorDwellTime      = 1.0f;
    public float timeRemaining;
    //    public float dataRecordFrequency = 0.02f;  // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data
    public float dataRecordFrequency = 0.04f;  // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data


    // Game-play state machine states
    public const int STATE_STARTSCREEN = 0;
    public const int STATE_STARTTRIAL  = 1;
    public const int STATE_GOALAPPEAR  = 2;
    public const int STATE_DELAY       = 3;
    public const int STATE_GO          = 4;
    public const int STATE_MOVING1     = 5;
    public const int STATE_STAR1FOUND  = 6;
    public const int STATE_MOVING2     = 7;
    public const int STATE_STAR2FOUND  = 8;
    public const int STATE_FINISH      = 9;
    public const int STATE_NEXTTRIAL   = 10;
    public const int STATE_INTERTRIAL  = 11;
    public const int STATE_TIMEOUT     = 12;
    public const int STATE_ERROR       = 13;
    public const int STATE_REST        = 14;
    public const int STATE_MAX         = 15;

    private string[] stateText = new string[] { "StartScreen","StartTrial","GoalAppear","Delay","Go","Moving1","FirstGoalHit", "Moving2", "FinalGoalHit", "Finish","NextTrial","InterTrial","Timeout","Error","Rest", "Max" };
    public int State;
    public List<string> stateTransitions = new List<string>();   // recorded state transitions (in sync with the player data)

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
        source = GetComponent<AudioSource>();

        FLAG_trialError = false;

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
        messageTimer = new Timer();

        stateTransitions.Clear();

    }
    // ********************************************************************** //

    private void Update()     // Update() executes once per frame
    {

        // timeRemaining = currentTrialData.maxTrialDuration;
        // currentMapIndex = dataController.GetCurrentTrialData().mapIndex;
        // ***HRS ^ this currentMapIndex is doing weird not updating things, so... I think perhaps they have not been instantiated?

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 1; // ***HRS this is a hack but for now it's fine.
        currentSceneName = "tartarus" + currentSceneIndex;

        switch (State)
        {

            case STATE_STARTSCREEN:

                if (gameStarted)
                {
                    //PlayerFPS = GameObject.Find("FPSController"); // Create a local reference to the player object that has just been created
                    StateNext(STATE_STARTTRIAL);
                }
                break;

            case STATE_STARTTRIAL:

                FLAG_trialError = false; // we start the trial with a clean-slate
                FLAG_trialTimeout = false;
                if (stateTimer.ElapsedSeconds() > displayMessageTime)
                {
                    // can put a message up about waiting until the go cue
                }

                // Make sure we're found the player and make sure they cant move
                if (PlayerFPS != null)  
                {
                    if (PlayerFPS.GetComponent<FirstPersonController>().enabled)
                    {
                        PlayerFPS.GetComponent<FirstPersonController>().enabled = false;  
                    }
                }
                else
                {
                    PlayerFPS = GameObject.Find("FPSController");

                    // Track the state-transitions at the same update frequency as the FPSPlayer (and putting it here should sync them too)
                    stateTransitions.Clear();                      // restart the state tracker ready for the new trial
                    stateTransitions.Add("Game State");
                    RecordFSMState();                              // catch the current state before the update
                    InvokeRepeating("RecordFSMState", 0f, dataRecordFrequency);  
                }

                // Wait until the goal/target can appear
                if (stateTimer.ElapsedSeconds() >= goalAppearDelay)
                {
                    PlayerFPS.GetComponent<FirstPersonController>().enabled = false;
                    StateNext(STATE_GOALAPPEAR);
                }
                break;


            case STATE_GOALAPPEAR:
                // display the star (so far its already visible so can add this later)

                // Make an image of the star appear in front of the camera (lock
                // it to the camera so its 2D or make as part of canvas), showing
                // what type of reward to collect on that trial


                starFound = false;
                StateNext(STATE_DELAY);
                break;

            case STATE_DELAY:
                // Wait for the go cue
                if (stateTimer.ElapsedSeconds() >= goCueDelay)
                {
                    source.PlayOneShot(goCueSound, 1F);
                    StateNext(STATE_GO);
                }
                break;

            case STATE_GO:

                // Enable the controller
                PlayerFPS.GetComponent<FirstPersonController>().enabled = true;

                // Make a 'beep' go sound and start the trial timer
                movementTimer.Reset();
                StateNext(STATE_MOVING1);
                break;

            case STATE_MOVING1:

                if (movementTimer.ElapsedSeconds() > maxMovementTime)  // the trial should timeout
                {
                    StateNext(STATE_TIMEOUT);
                }

                if (starFound)
                {
                    source.PlayOneShot(starFoundSound, 1F);
                    firstMovementTime = movementTimer.ElapsedSeconds();

                    if (doubleRewardTask)  // we are collecting two stars on this trial
                    {
                        StateNext(STATE_STAR1FOUND);
                    }
                    else              // there's only one star to collect
                    {
                        totalMovementTime = firstMovementTime;
                        StateNext(STATE_STAR2FOUND);
                    }
                }
                break;


            case STATE_STAR1FOUND:

                starFound = false;  // reset the starFound trigger ready to collect the next star
                StateNext(STATE_MOVING2);
                break;


            case STATE_MOVING2:

                if (movementTimer.ElapsedSeconds() > maxMovementTime)  // the trial should timeout
                {
                    StateNext(STATE_TIMEOUT);
                }

                if (starFound)
                {
                    source.PlayOneShot(starFoundSound, 1F);
                    totalMovementTime = movementTimer.ElapsedSeconds();
                    StateNext(STATE_STAR2FOUND);
                }
                break;

            case STATE_STAR2FOUND:

                displayMessage = "wellDoneMessage";      // display a congratulatory message
                PlayerFPS.GetComponent<FirstPersonController>().enabled = false; // disable controller

                if (stateTimer.ElapsedSeconds() > waitFinishTime)
                {
                    StateNext(STATE_FINISH);
                }
                break;

            case STATE_FINISH:

                // stop recording the state transitions for this trial
                CancelInvoke("RecordFSMState");

                // end the trial, save the data
                Debug.Log("The current map is: " + currentSceneIndex + " so the next map will be: " + (currentSceneIndex + 1));
                NextScene("tartarus" +  1);   // Just loop this map for now for demo
                //NextScene("tartarus" + (currentSceneIndex + 1));

                StateNext(STATE_STARTTRIAL);
                break;

            case STATE_TIMEOUT:

                displayMessage = "timeoutMessage";
                Debug.Log("Trial timed out: (after " + movementTimer.ElapsedSeconds() + " sec)");
                FLAG_trialTimeout = true;

                StateNext(STATE_ERROR);
                break;


            case STATE_ERROR:


                // ***HRS  Later differentiate between the different errors in save file e.g. timeout

                FLAG_trialError = true;
                firstMovementTime = movementTimer.ElapsedSeconds();
                totalMovementTime = firstMovementTime;


                // Wait a little while in the error state
                if (stateTimer.ElapsedSeconds() > errorDwellTime)
                {
                    // stop recording the state transitions for this trial
                    CancelInvoke("RecordFSMState");

                    // save the data and restart the trial
                    source.PlayOneShot(errorSound, 1F); 
                    Debug.Log("ERROR STATE");
                    NextScene(currentSceneName);
                    StateNext(STATE_STARTTRIAL);
                }

                break;
        
        }
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

    private void RecordFSMState()
    {
        stateTransitions.Add(State.ToString());
    }

    // ********************************************************************** //

    private void OnGUI()
    {
        switch (displayMessage)
        {

            case "noMessage":
                textMessage = "";
                messageTimer.Reset();
                break;

            case "wellDoneMessage":
                textMessage = "Well done!";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;

            case "findStarMessage":
                textMessage = "Find the star!";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;


            case "timeoutMessage":
                textMessage = "Trial timed out!";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;

            case "lavaDeathMessage":
                textMessage = "Aaaaaaaaaaaaaaaahhhh!";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;

            case "restartTrialMessage":
                textMessage = "Restarting the trial";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;

        }
       
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 200, 100), textMessage);
    }

    // ********************************************************************** //

    public void LavaDeath()
    {
        //source.PlayOneShot(fallSound, 1F);
        displayMessage = "lavaDeathMessage";
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
