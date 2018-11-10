using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData
{
    public ParticipantData participantData;
    public int totalTrials;
    public float dataRecordFrequency;
    public float restbreakDuration;

    public TrialData[] allTrialData;


    // ********************************************************************** //
    // Use a constructor
    public GameData(int trials)
    {
        //  initialize array of trials, and instantiate each trial in the array
        allTrialData = new TrialData[trials];
        for (int i = 0; i < allTrialData.Length; i++)
        {
            allTrialData[i] = new TrialData();
        }
    }
}
