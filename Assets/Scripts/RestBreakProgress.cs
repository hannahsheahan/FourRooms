using UnityEngine;
using UnityEngine.UI;

public class RestBreakProgress : MonoBehaviour {
    // public GameObject LoadingText;
    public Text ProgressIndicator;
    public Image LoadingBar;
    float currentValue;
    public float speed;
    public int secondsLeft;

    // ********************************************************************** //

    void Start () 
    {
        currentValue = 0;
    }

    // ********************************************************************** //

    void Update () {
        currentValue = GameController.control.elapsedRestbreakTime / GameController.control.restbreakDuration;
        secondsLeft = (int)Mathf.Round((GameController.control.restbreakDuration - GameController.control.elapsedRestbreakTime));
        if (currentValue < 100) 
        {
        //   currentValue += speed * Time.deltaTime;
             ProgressIndicator.text = (secondsLeft).ToString();
        // LoadingText.SetActive (true);
        }
        // else {
        //     LoadingText.SetActive (false);
        //     ProgressIndicator.text = "Done";
        //}
        LoadingBar.fillAmount = currentValue; 
    }
    // ********************************************************************** //

}