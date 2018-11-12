using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitExperimentDurationScript : MonoBehaviour {

    public Text ExperimentDurationText;
    private float totalExperimentTime;

    // ********************************************************************** //

    void Update()
    {
        totalExperimentTime = GameController.control.totalExperimentTime;

        if (totalExperimentTime > 0.0f)    // just make sure it has updated
        {
            ExperimentDurationText.text = "Total time: " + (totalExperimentTime/60.0f).ToString("0.0") + " min";
        }

    }
    // ********************************************************************** //
}
