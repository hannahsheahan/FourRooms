using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TrialData
{
    /// <summary>
    /// This script contains all the data to be saved per trial
    /// Author: Hannah Sheahan
    /// Date: 31/10/2018
    /// </summary>

    // Static per trial data
    public int trialNumber = 0;

    //public int mapIndex;   // this causes some complications (because SceneManager only manages active/loaded scenes, so .buildIndex throws -1)
    public string mapName;
    public bool doubleRewardTask;
    public string rewardType;

    public Vector3 playerSpawnLocation;
    public Vector3 playerSpawnOrientation;
    public Vector3 star1Location;
    public Vector3 star2Location;

    // trial event times
    public float maxMovementTime;
    public float firstMovementTime;       // time until first star collected
    public float totalMovementTime;       // time until second star collected

    // trial configuration times
    public float goalAppearDelay;
    public float AppearDelay;
    public float goCueDelay;
    public float minDwellAtStar;      
    public float displayMessageTime;    
    public float waitFinishTime;
    public float errorDwellTime;


    // trial error flags
    public bool FLAG_trialTimeout;        
    public bool FLAG_trialError;

    // Tracking data
    public List<string> stateTransitions = new List<string>();
    public List<string> timeStepTrackingData = new List<string>();

}
