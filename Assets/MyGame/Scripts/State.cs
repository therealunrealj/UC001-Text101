using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject{

    private static readonly System.Random getrandom = new System.Random();

    [TextArea(10,14)][SerializeField] string storyText;
    public State[] nextStates;
    public State[] randomStates;

    private string knitnotification, collectnotification, dehydrationnotificaton;

    public void SetKnitNotification(string notifiction)
    {
        knitnotification = notifiction;
    }

    public void SetCollectNotification(string notifiction)
    {
        collectnotification = notifiction;
    }

    public void SetDehydtrationNotification(string notifiction)
    {
        dehydrationnotificaton = notifiction;
    }


    public string GetStateStroy()
    {
        return storyText;
    }

    public State[] GetNextStates()
    {
        if (randomStates.Length == 0)
        {
            return nextStates; 
        }

        State[] extendedArray = new State[nextStates.Length + 1];

        int rnd = PickBiasedRandomState(0, 0, randomStates.Length, 0.7);
        extendedArray[0] = randomStates[rnd];
        for (int i = 0; i < nextStates.Length; i++)
        {
            extendedArray[i + 1] = nextStates[i];
        }

        return extendedArray;
    }

    int PickBiasedRandomState(int idxPrefered, int randomMin, int randomMax, double probabilityPrefered)
    {
        //return getrandom.NextDouble() < 0.8 ? idxPrefered : getrandom.Next(idxMin, idxMax);
     
        if (getrandom.NextDouble() < probabilityPrefered)
        {
            Debug.Log("< " + probabilityPrefered);
            return idxPrefered;
        }
        Debug.Log("> " + probabilityPrefered);
        return getrandom.Next(randomMin, randomMax);
    }

}
