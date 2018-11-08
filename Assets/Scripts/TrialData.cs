using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TrialData
{
    // Static per trial data
    public int trialNumber = 0;
    public int mapIndex;
    public string mapName;

    //public Vector3 playerSpawnLocation;
    //public Vector3 playerSpawnRotation;

    //public Vector3 goalLocation;

    // trial event times
    public float maxMovementTime;
    public float firstMovementTime;       // time until first star collected
    public float totalMovementTime;       // time until second star collected

    // trial error flags
    public bool FLAG_trialTimeout;        
    public bool FLAG_trialError;


    //public bool trialError;

    // Tracking data
    public List<string> stateTransitions = new List<string>();
    public List<string> timeStepTrackingData = new List<string>();


}
