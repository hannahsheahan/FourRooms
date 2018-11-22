using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityStandardAssets.Characters.FirstPerson;
using System.IO;
using System.Linq;

public class GameController : MonoBehaviour {
    /// <summary>
    /// The GameController is a singleton script will control the game flow between scenes, 
    /// and centralise everything so all main processes branch from here.
    /// Author: Hannah Sheahan, sheahan.hannah@gmail.com
    /// Date: 30 Oct 2018
    /// Notes: N/A
    /// Issues: N/A
    /// 
    /// </summary>

    // Persistent controllers for data management and gameplay
    private DataController dataController;
    public static GameController control;

    // Game data
    private GameObject PlayerFPS;
    private GameData currentGameData;
    private string filepath;

    // Start-of-trial data
    private TrialData currentTrialData;
    private string currentMapName;
    private string currentSceneName;

    public Vector3 playerSpawnLocation;
    public Vector3 playerSpawnOrientation;
    public Vector3 star1SpawnLocation;
    public Vector3 star2SpawnLocation;
    public Vector3 activeStarSpawnLocation;   // this is obsolete: only used for if we have sequential order on reward collection
    public bool doubleRewardTask;

    private string nextScene;

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
    private string displayMessage = "noMessage";
    public string textMessage = "";
    public bool displayCue;
    public string rewardType;
    public bool reward1Visible;
    public bool reward2Visible;
    public int trialScore = 0;
    public int totalScore = 0;
    public int nextScore;
    public bool flashTotalScore = false;
    public bool scoreUpdated = false;
    private float beforeScoreUpdateTime = 1.2f;  // this is just for display

    // Timer variables
    private Timer experimentTimer;
    private Timer stateTimer;
    private Timer movementTimer;
    public Timer messageTimer;
    private Timer restbreakTimer;
    private Timer getReadyTimer;
    public float firstMovementTime;
    public float totalMovementTime;
    public float totalExperimentTime;
    public float currentMovementTime;
    public bool displayTimeLeft;

    public float maxMovementTime;  
    private float preDisplayCueTime;
    private float goCueDelay;
    private float displayCueTime;
    private float goalHitPauseTime;
    private float finalGoalHitPauseTime;
    public  float minDwellAtReward; 
    public float displayMessageTime; 
    public float errorDwellTime;
    public float restbreakDuration;
    public float elapsedRestbreakTime;
    public float getReadyTime;
    public float getReadyDuration;

    public float dataRecordFrequency;           // NOTE: this frequency is referred to in TrackingScript.cs for player data and here for state data
    public float timeRemaining;

    // Error flags
    public bool FLAG_trialError;
    public bool FLAG_trialTimeout;

    // Game-play state machine states
    public const int STATE_STARTSCREEN = 0;
    public const int STATE_SETUP       = 1;
    public const int STATE_STARTTRIAL  = 2;
    public const int STATE_GOALAPPEAR  = 3;
    public const int STATE_DELAY       = 4;
    public const int STATE_GO          = 5;
    public const int STATE_MOVING1     = 6;
    public const int STATE_STAR1FOUND  = 7;
    public const int STATE_MOVING2     = 8;
    public const int STATE_STAR2FOUND  = 9;
    public const int STATE_FINISH      = 10;
    public const int STATE_NEXTTRIAL   = 11;
    public const int STATE_INTERTRIAL  = 12;
    public const int STATE_TIMEOUT     = 13;
    public const int STATE_ERROR       = 14;
    public const int STATE_REST        = 15;
    public const int STATE_GETREADY    = 16;
    public const int STATE_EXIT        = 17;
    public const int STATE_MAX         = 18;

    private string[] stateText = new string[] { "StartScreen","Setup","StartTrial","GoalAppear","Delay","Go","Moving1","FirstGoalHit", "Moving2", "FinalGoalHit", "Finish","NextTrial","InterTrial","Timeout","Error","Rest","Exit","GetReady","Max" };
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

        // Trial invariant data
        filepath = dataController.filePath;   //this works because we actually have an instance of dataController
        Debug.Log("File path: " + filepath);
        dataRecordFrequency = dataController.GetRecordFrequency();
        restbreakDuration = dataController.GetRestBreakDuration();
        getReadyDuration = dataController.GetGetReadyDuration();

        // Initialise FSM State
        State = STATE_STARTSCREEN;
        stateTimer = new Timer();
        stateTimer.Reset();

        experimentTimer = new Timer();
        movementTimer = new Timer();
        messageTimer = new Timer();
        restbreakTimer = new Timer();
        getReadyTimer = new Timer();
        stateTransitions.Clear();

        // Ensure cue images are off
        displayCue = false;
        reward1Visible = false;
        reward2Visible = false;

        StartExperiment();  

    }

    // ********************************************************************** //

    private void Update()     // Update() executes once per frame
    {
        UpdateText();
        currentMovementTime = movementTimer.ElapsedSeconds();

        switch (State)
        {

            case STATE_STARTSCREEN:
                // Note: we chill out here in this state until all the starting info pages are done
                if (gameStarted)
                {
                    StateNext(STATE_SETUP);
                }
                break;

            case STATE_SETUP:

                switch (TrialSetup())
                {
                    case "StartTrial":
                        // ensure the reward is hidden from sight
                        reward1Visible = false;
                        reward2Visible = false;
                        StateNext(STATE_STARTTRIAL);

                        break;
                    case "Menus":
                        // fix
                        break;

                    case "GetReady":
                        getReadyTimer.Reset();
                        StateNext(STATE_GETREADY);
                        break;

                    case "RestBreak":
                        restbreakTimer.Reset();
                        StateNext(STATE_REST);
                        break;

                    case "Exit":
                        totalExperimentTime = experimentTimer.ElapsedSeconds();
                        Cursor.visible = true;
                        StateNext(STATE_EXIT);
                        break;

                }
                break;

            case STATE_STARTTRIAL:


                StartRecording();    

                // Wait until the goal/target cue appears (will take a TR here)
                if (stateTimer.ElapsedSeconds() >= preDisplayCueTime)
                {
                    PlayerFPS.GetComponent<FirstPersonController>().enabled = false;
                    StateNext(STATE_GOALAPPEAR);
                }
                break;


            case STATE_GOALAPPEAR:
                // display the reward type cue
                displayCue = true;

                if (stateTimer.ElapsedSeconds() > displayCueTime)
                {
                    displayCue = false;
                    starFound = false;
                    StateNext(STATE_DELAY);
                    break;
                }
                break;

            case STATE_DELAY:
                // Wait for the go audio cue (will take a TR here)

                if (stateTimer.ElapsedSeconds() >= goCueDelay)
                {
                    source.PlayOneShot(goCueSound, 1F);
                    reward1Visible = true;     // make the reward itself appear in the environment
                    reward2Visible = true;     // make the reward itself appear in the environment
                    StateNext(STATE_GO);
                }
                break;

            case STATE_GO:

                // Enable the controller
                PlayerFPS.GetComponent<FirstPersonController>().enabled = true;

                // Make a 'beep' go sound and start the trial timer
                movementTimer.Reset();
                displayTimeLeft = true;   // make the trial time countdown visible
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

                // disable the player control and reset the starFound trigger ready to collect the next star
                starFound = false; 
                PlayerFPS.GetComponent<FirstPersonController>().enabled = false;

                // pause here so that we can take a TR
                if (stateTimer.ElapsedSeconds() > goalHitPauseTime)  // the trial should timeout
                {
                    //activeStarSpawnLocation = star2SpawnLocation;
                    PlayerFPS.GetComponent<FirstPersonController>().enabled = true; // let the player move again
                    StateNext(STATE_MOVING2);
                }
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

                if (stateTimer.ElapsedSeconds() > beforeScoreUpdateTime)
                {
                    // update the total score in a visible way
                    UpdateScore();
                }
                if (stateTimer.ElapsedSeconds() > finalGoalHitPauseTime)
                {
                    flashTotalScore = false;
                    StateNext(STATE_FINISH);
                }
                break;

            case STATE_FINISH:

                // stop recording the state transitions for this trial
                CancelInvoke("RecordFSMState");

                // end the trial, save the data
                NextScene();
                StateNext(STATE_SETUP);
                break;

            case STATE_TIMEOUT:

                FLAG_trialTimeout = true;

                displayMessage = "timeoutMessage";
                Debug.Log("Trial timed out: (after " + movementTimer.ElapsedSeconds() + " sec)");

                StateNext(STATE_ERROR);
                break;


            case STATE_ERROR:
                // Handle error trials by continuing to record data on the same trial
                if (FLAG_trialError == false)
                {
                    source.PlayOneShot(errorSound, 1F);
                    displayMessage = "restartTrialMessage";
                }
                FLAG_trialError = true;

                firstMovementTime = movementTimer.ElapsedSeconds();
                totalMovementTime = firstMovementTime;

                // Wait a little while in the error state to display the error message
                if (stateTimer.ElapsedSeconds() > errorDwellTime)
                {
                    // stop recording the state transitions for this trial
                    CancelInvoke("RecordFSMState"); 

                    // Restart the trial
                    NextAttempt();
                    StateNext(STATE_SETUP);

                }
                break;

          
            case STATE_REST:

                elapsedRestbreakTime = restbreakTimer.ElapsedSeconds();

                if (elapsedRestbreakTime > restbreakDuration)
                {
                    NextScene();
                    StateNext(STATE_SETUP);   // move on to the next trial
                    break;
                }
                break;


            case STATE_GETREADY:

                getReadyTime = getReadyTimer.ElapsedSeconds();

                if (getReadyTime > getReadyDuration)
                {
                    NextScene();
                    StateNext(STATE_SETUP);   // move on to the next trial
                    break;
                }
                break;


            case STATE_EXIT:
                // Display the total experiment time and wait for the participant to close the application

                // Note: at the moment this is just to save the correct exiting state transition in the datafile

                break;

        }
    }
    // ********************************************************************** //

    public void NextScene()
    {
        // Save the current trial data and move data storage to the next trial
        dataController.AddTrial();  
        dataController.SaveData();
    }

    // ********************************************************************** //

    public void NextAttempt()
    {
        // Save the current trial data before the participant tries the trial again
        dataController.AssembleTrialData();
        dataController.SaveData();
    }

    // ********************************************************************** //

    public string TrialSetup()
    {
        // Start the trial with a clean-slate
        FLAG_trialError = false;
        FLAG_trialTimeout = false;
        starFound = false;
        displayTimeLeft = false;
        scoreUpdated = false;
        trialScore = 0;

        // Load in the trial data
        currentTrialData = dataController.GetCurrentTrialData();
        nextScene = currentTrialData.mapName;

        // Location and orientation variables
        playerSpawnLocation     = currentTrialData.playerSpawnLocation;
        playerSpawnOrientation  = currentTrialData.playerSpawnOrientation;
        star1SpawnLocation      = currentTrialData.star1Location;
        star2SpawnLocation      = currentTrialData.star2Location;
        doubleRewardTask        = currentTrialData.doubleRewardTask;

        //activeStarSpawnLocation = star1SpawnLocation;

        // Timer variables
        maxMovementTime = currentTrialData.maxMovementTime;
        preDisplayCueTime = currentTrialData.preDisplayCueTime;
        displayCueTime = currentTrialData.displayCueTime;
        goalHitPauseTime = currentTrialData.goalHitPauseTime;
        finalGoalHitPauseTime = currentTrialData.finalGoalHitPauseTime;
        goCueDelay      = currentTrialData.goCueDelay;
        minDwellAtReward  = currentTrialData.minDwellAtReward;
        displayMessageTime = currentTrialData.displayMessageTime;
        errorDwellTime  = currentTrialData.errorDwellTime;
        rewardType      = currentTrialData.rewardType;

        // Start the next scene/trial
        Debug.Log("Upcoming scene: " + nextScene);
        SceneManager.LoadScene(nextScene);

        string[] menuScenesArray = new string[] { "Exit", "RestBreak", "GetReady"};

        if (menuScenesArray.Contains(nextScene))
        {
            return nextScene;   // we don't want to record data and do the FSM transitions during the exit and rest break scenes
        }
        else
        {
            return "StartTrial";
        }

        /*
        if ( (nextScene == "Exit") || (nextScene == "RestBreak") || (nextScene == "GetReady"))
        {
            return nextScene;   // we don't want to record data and do the FSM transitions during the exit and rest break scenes
        }
        else
        {
            return "StartTrial";
        }
        */


    }

    // ********************************************************************** //

    public void StartRecording()
    {
        // Make sure we're found the player and make sure they cant move (and start recording player and FSM data)
        if (PlayerFPS != null)
        {
            if (PlayerFPS.GetComponent<FirstPersonController>().enabled)
            {
                PlayerFPS.GetComponent<FirstPersonController>().enabled = false;
            }
        }
        else
        {   // Track the state-transitions at the same update frequency as the FPSPlayer (and putting it here should sync them too)
            PlayerFPS = GameObject.Find("FPSController");
            stateTransitions.Clear();                      // restart the state tracker ready for the new trial
            stateTransitions.Add("Game State");
            RecordFSMState();                              // catch the current state before the update
            InvokeRepeating("RecordFSMState", 0f, dataRecordFrequency);
            //Debug.Log("Found player.");
        }
    }

    // ********************************************************************** //

    public void StartExperiment()
    {
        experimentTimer.Reset();
        NextScene();
        TrialSetup();           // start the experiment participant menu etc
    }

    // ********************************************************************** //

    public void StartGame()
    {
        Debug.Log("The game has started now and into the FSM!");
        NextScene();
        gameStarted = true;     // start the game rolling!
        Cursor.visible = false; 
    }

    // ********************************************************************** //

    public void ContinueToNextMenuScreen()
    {
        NextScene();
        TrialSetup();
    }

    // ********************************************************************** //

    public void ExitGame()
    {
        Application.Quit();  // close the application
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

    private void UpdateText()
    {

        // Display any major errors that require the player to restart the experiment
        if (!dataController.writingDataProperly)
        {
            displayMessage = "dataWritingError";
        }


        // Display regular game messages to the player
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

            case "findRewardMessage":
                textMessage = "Find the item:";
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
                textMessage = "Restarting trial";
                if (messageTimer.ElapsedSeconds() > displayMessageTime)
                {
                    displayMessage = "noMessage"; // reset the message
                }
                break;

            case "dataWritingError":
                textMessage = "There was an error sending data to the web server.  Please exit.";
                break;
        }  
    }

    // ********************************************************************** //

    public void UpdateScore()
    {
        if (!scoreUpdated)  // just do this once
        {
            displayTimeLeft = false;   // freeze the visible countdown
            trialScore = (int)Mathf.Round(maxMovementTime - totalMovementTime);
            totalScore += trialScore;
            scoreUpdated = true;
            flashTotalScore = true;
        }
    }

    // ********************************************************************** //

    public void FallDeath()
    {   // Disable the player controller, give an error message, save the data and restart the trial
        Debug.Log("AAAAAAAAAAAAH! Player fell off the platform.");
        StateNext(STATE_ERROR);
    }

    // ********************************************************************** //

    public void StarFound()
    {
        starFound = true; // The player has been at the star for minDwellAtReward seconds
    }

    // ********************************************************************** //

    public void DisableRewardByIndex(int index)
    {
        // Disable whichever of the two rewards was just hit. Called from RewardHitScript.cs
        switch (index)
        {
            case 1:
                reward1Visible = false;
                break;

            case 2:
                reward2Visible = false;
                break;
            default:
                reward1Visible = false;
                reward2Visible = false;
                break;
        }
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
