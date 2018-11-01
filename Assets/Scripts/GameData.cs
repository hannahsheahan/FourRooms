using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData
{
    public ParticipantData participantData;
    private static int maxNumberOfTrials = 50;
    public TrialData[] allTrialData = new TrialData[maxNumberOfTrials];

}
