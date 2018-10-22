using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScriptTest : MonoBehaviour {

    private float begin;
    
	// Use this for initialization
	void Start () {
        begin = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= begin + 1.5f)
        {
             SceneManager.LoadScene("tartarus" + (LevelManager.level + 1));
        }
    }
}
