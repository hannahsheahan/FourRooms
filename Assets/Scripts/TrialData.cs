﻿using UnityEngine;
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
    public List<int> trialListIndex = new List<int>();      // which trial attempt

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
    public float trialScore; 
    public List<float> firstMovementTime = new List<float>();       // time until first star collected (kept as list to account for error trials)
    public List<float> totalMovementTime = new List<float>();       // time until second star collected


    // trial configuration times
    public float preDisplayCueTime;
    public float displayCueTime;
    public float goCueDelay;
    public float minDwellAtReward;      
    public float displayMessageTime;    
    public float errorDwellTime;
    public float goalHitPauseTime;
    public float finalGoalHitPauseTime;
    public float hallwayFreezeTime;


    // trial error flags
    public List<bool> FLAG_trialTimeout = new List<bool>();        
    public List<bool> FLAG_trialError = new List<bool>();

    // Tracking data
    public List<string> stateTransitions = new List<string>();
    public List<string> timeStepTrackingData = new List<string>();

}
