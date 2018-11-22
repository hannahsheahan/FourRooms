using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData
{
    public string participantID;
    public string participantAge;
    public int participantGender;
    public int totalTrials;
    public float dataRecordFrequency;
    public float restbreakDuration;
    public float getReadyDuration;

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
