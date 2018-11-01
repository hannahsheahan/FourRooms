using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage : MonoBehaviour {

    private string message = " ";
    private string messageColor = "white";

    private void Update()
    {
        message = GameController.control.screenMessage;
        messageColor = GameController.control.screenMessageColor;
    }
    // ********************************************************************** //

    public void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        // This seems to make global changes to all uses of GUI.Label that persists across sessions...
        /*
        if (messageColor == "green")
        {
            centeredStyle.textColor = Color.green;
        }
        else if(messageColor == "red")
        {
            centeredStyle.textColor = Color.red;
        }
        else
        {
            centeredStyle.textColor = Color.white;
        }
        */
        //centeredStyle.normal.textColor = Color.white;

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 200, 100), message, centeredStyle);

    }

    // ********************************************************************** //

}
