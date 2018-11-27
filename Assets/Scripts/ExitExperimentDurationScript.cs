using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitExperimentDurationScript : MonoBehaviour {

    private DataController dataController;
    private string code;

    public Text ExperimentDurationText;
    public Text ConfirmationCode;
    private float totalExperimentTime;

    private void Start()
    {
        dataController = FindObjectOfType<DataController>(); // Fetch our single DataController

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

        ConfirmationCode.text = "Your completion code: " + code;
    }
    // ********************************************************************** //
}
