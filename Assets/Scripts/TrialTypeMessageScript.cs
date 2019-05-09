using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrialTypeMessageScript : MonoBehaviour
{

    // Only use this script on the Practice scene canvas
    // Note that we are doing this here instead of in GameController because of the text positioning

    public Text trialTypeMessage;
    private string cue;

    // ********************************************************************** //

    void Update()
    {
        if (GameController.control.displayCue)
        {
            if (GameController.control.freeForage) 
            {
                trialTypeMessage.text = "Every giftbox contains this item! Collect them all";
            }
            else
            {
                trialTypeMessage.text = "Find the 2 items of this type";
            }

        }
        else
        {
            trialTypeMessage.text = "";
        }
    }

    // ********************************************************************** //

}