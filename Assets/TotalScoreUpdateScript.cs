using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalScoreUpdateScript : MonoBehaviour {

    public Text TotalScore;
    private int currentTotalScore;

    // ********************************************************************** //

    void Update () 
    {
        currentTotalScore = GameController.control.totalScore;
        TotalScore.text = currentTotalScore.ToString();

        // When the total score updates make it flash cyan
        if (GameController.control.flashTotalScore)
        {
            TotalScore.color = Color.cyan;
            TotalScore.fontSize = 50;
        }
        else
        {
            TotalScore.color = Color.white;
            TotalScore.fontSize = 36;
        }
    }

}
