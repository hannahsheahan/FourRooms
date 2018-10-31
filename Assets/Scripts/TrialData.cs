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

    //public float maxTrialDuration;

    // Tracking data
    public List<string> timeStepTrackingData;


}
