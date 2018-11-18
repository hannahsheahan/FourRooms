using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOrDisplayReward : MonoBehaviour {

	public GameObject reward;
    public int rewardIndex;
   
	private void Update()
    {
        // Display or hide the reward location
        switch (rewardIndex)
        {
            case 1:
                reward.SetActive(GameController.control.reward1Visible);
                break;

            case 2:
                reward.SetActive(GameController.control.reward2Visible);
                break;

            default:
                reward.SetActive(GameController.control.reward1Visible);
                reward.SetActive(GameController.control.reward2Visible);
                break;
        }
    }
}
