using UnityEngine;
using UnityEngine.UI;

public class TrialCountdownScript : MonoBehaviour {
   
    public Text CountdownTime;
    public Text FrozenCountdownTime;
    private float timeLeft;
    private int secondsLeft;
    private int frozenSecondsLeft;
    private int subtractTime;

    // ********************************************************************** //

    void Update () {

        timeLeft = GameController.control.maxMovementTime - GameController.control.currentMovementTime;
        secondsLeft = (int)Mathf.Round(timeLeft);
        if (GameController.control.displayTimeLeft) 
        {
            if (secondsLeft >= 0)
            {
                CountdownTime.text = (secondsLeft).ToString();
            }
        }

        // display the frozen countdown
        if (GameController.control.State == GameController.STATE_HALLFREEZE)
        {
            frozenSecondsLeft = secondsLeft - subtractTime;
            // make sure it freezes for AT LEAST hallwayFreezeTime
            if ((frozenSecondsLeft >= 0) && (frozenSecondsLeft <= GameController.control.hallwayFreezeTime-1))
            {
                FrozenCountdownTime.text = (frozenSecondsLeft).ToString();
                //FrozenCountdownTime.color = Color.white;
            }
            else
            {
                FrozenCountdownTime.text = ((int)Mathf.Round(GameController.control.hallwayFreezeTime)).ToString();
                //FrozenCountdownTime.color = Color.white;
            }

        }
        else
        {
            FrozenCountdownTime.text = "";
            subtractTime = (int)Mathf.Round(timeLeft - GameController.control.hallwayFreezeTime); 
        }

    }
    // ********************************************************************** //

}