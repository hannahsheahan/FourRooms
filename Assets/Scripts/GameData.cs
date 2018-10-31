using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData
{
    public ParticipantData participantData;
    private static int maxNumberOfTrials = 20;
    public TrialData[] allTrialData = new TrialData[maxNumberOfTrials];

}
