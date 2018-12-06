using UnityEngine;
using UnityEngine.UI;


public class FrozenCountdownMessageScript : MonoBehaviour
{
    public Text FrozenCountdownTime;
    private float timeLeft;
    private int secondsLeft;
    private float subtractTime;   // we are doing this horrible way just so that the time updates are in sync
    private float totalTimeLeft;

    // ********************************************************************** //

    void Update()
    {
        // Note: tried below to show a countdown for the freeze time that synced with the global countdown,
        // but was still 1 timeframe out in the update I think
        /*
        totalTimeLeft = GameController.control.maxMovementTime - GameController.control.currentMovementTime;

        if (GameController.control.State == GameController.STATE_HALLFREEZE)
        {
            secondsLeft = (int)Mathf.Round(totalTimeLeft - subtractTime);

            if (secondsLeft >= 0)
            {
                FrozenCountdownTime.text = (secondsLeft).ToString();
            }
        }
        else
        {
            FrozenCountdownTime.text = "";
            subtractTime = totalTimeLeft - GameController.control.hallwayFreezeTime;
        }
        */

        if (GameController.control.State == GameController.STATE_HALLFREEZE)
        {
            FrozenCountdownTime.text = "you must wait for 5 seconds";
            FrozenCountdownTime.color = Color.cyan;  // flash cyan since +ve update
            FrozenCountdownTime.fontSize = 36;
        }
        else
        {
            FrozenCountdownTime.text = "";
        }
    }
    // ********************************************************************** //
}