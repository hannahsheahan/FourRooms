using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOrDisplayReward : MonoBehaviour {

	public GameObject reward;

   
	private void Update()
    {
        // Display or hide the reward location
        reward.SetActive(GameController.control.rewardVisible);
    }
}
