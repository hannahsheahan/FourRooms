using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitExperimentDurationScript : MonoBehaviour {

    private DataController dataController;
    private string code;

    private Timer dataSendingTimer;
    private float dataSendingWaitTime = 5.0f;

    public Text ExperimentDurationText;
    public Text ConfirmationCode;
    public Text YouMayNowExitText;
    private float totalExperimentTime;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // Fetch our single DataController
        dataSendingTimer = new Timer();
        dataSendingTimer.Reset();

        // Generate a random confirmation code at end of experiment that will encourage participant to complete
        // (and so we can easily see if they HAVE completed)
        code = dataController.GetConfirmationCode();

    }

    // ********************************************************************** //

    void Update()
    {
        totalExperimentTime = GameController.control.totalExperimentTime;

        if (totalExperimentTime > 0.0f)    // just make sure it has updated
        {
            ExperimentDurationText.text = "Total time: " + (totalExperimentTime/60.0f).ToString("0.0") + " min";
        }


        // reveal the completion code once the participant has given some feedback.
        if (dataController.participantFeedbackSubmitted)
        {
            if (dataSendingTimer.ElapsedSeconds() > dataSendingWaitTime) 
            { 
                ConfirmationCode.text = "Your completion code: " + code;
                YouMayNowExitText.text = "You may now exit the game.";
            }
            else // this should give the data from the experiment long enough to write properly before the window is exited
            {
                ConfirmationCode.text = "Sending data ...";
            }
        }
        else 
        {
            dataSendingTimer.Reset();
        }
    }
    // ********************************************************************** //
}
