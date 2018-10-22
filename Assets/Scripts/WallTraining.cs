using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WallTraining : MonoBehaviour {

    private GameObject wall1;
    private GameObject wall2;
    private GameObject wall3;
    private GameObject wall4;

    // Use this for initialization
    void Start () {
		wall1 = GameObject.Find("Wall01");
        wall2 = GameObject.Find("Wall02");
        wall3 = GameObject.Find("Wall04");
        wall4 = GameObject.Find("Wall03");

        if (TrainCount.trainCount % 4 == 0)
        {
            wall1.transform.eulerAngles = new Vector3(180, 0, 0);
            wall2.transform.eulerAngles = new Vector3(180, 90, 0);
            wall3.transform.eulerAngles = new Vector3(0, 0, 0);
            wall4.transform.eulerAngles = new Vector3(0, 90, 0);
        }else if (TrainCount.trainCount % 4 == 1)
        {
            wall1.transform.eulerAngles = new Vector3(0, 0, 0);
            wall2.transform.eulerAngles = new Vector3(180, 90, 0);
            wall3.transform.eulerAngles = new Vector3(180, 0, 0);
            wall4.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (TrainCount.trainCount % 4 == 2)
        {
            wall1.transform.eulerAngles = new Vector3(0, 0, 0);
            wall2.transform.eulerAngles = new Vector3(0, 90, 0);
            wall3.transform.eulerAngles = new Vector3(180, 0, 0);
            wall4.transform.eulerAngles = new Vector3(180, 90, 0);
        }
        else if (TrainCount.trainCount % 4 == 3)
        {
            wall1.transform.eulerAngles = new Vector3(180, 0, 0);
            wall2.transform.eulerAngles = new Vector3(0, 90, 0);
            wall3.transform.eulerAngles = new Vector3(0, 0, 0);
            wall4.transform.eulerAngles = new Vector3(180, 90, 0);
        }
    }
}
