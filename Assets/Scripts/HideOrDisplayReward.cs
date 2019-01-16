using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;


public class HideOrDisplayReward : MonoBehaviour {

	public GameObject reward;
    public int rewardIndex;
   
	private void Update()
    {

        //Debug.Log("rewardsVisible length is " + GameController.control.rewardsVisible.Length);
        reward.SetActive(GameController.control.rewardsVisible[rewardIndex]); // 2am Note: this actually works fine, its just a CPU issue


        //reward.SetActive(true);
        /*
        // ***HRS debugging
        if (rewardIndex < GameController.control.rewardsVisible.Length) 
        {
            reward.SetActive(GameController.control.rewardsVisible[rewardIndex]);
            //reward.SetActive(true);
        }
        else 
        {
            Debug.Log("There was an error enabling or disabling reward " + rewardIndex);
        }
        //reward.SetActive(true);
        */
    }
}
