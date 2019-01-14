using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOrDisplayReward : MonoBehaviour {

	public GameObject reward;
    public int rewardIndex;
   
	private void Update()
    {
        reward.SetActive(GameController.control.rewardsVisible[rewardIndex]);
    }
}
