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

        int rnd = PickBiasedRandomState(0, 0, randomStates.Length);
        State[] extendedArray = new State[nextStates.Length + 1];


        Debug.Log("rnd " +rnd);
        extendedArray[0] = randomStates[rnd];
        for (int i = 0; i < nextStates.Length; i++)
        {
            extendedArray[i + 1] = nextStates[i];
        }

        Debug.Log("States are Merged");
        return extendedArray;
    }

    int PickBiasedRandomState(int idxPrefered, int idxMin, int idxMax)
    {
        //return getrandom.NextDouble() < 0.8 ? idxPrefered : getrandom.Next(idxMin, idxMax);
     
        if (getrandom.NextDouble() < 0.8)
        {
            Debug.Log("< 0.8");
            return idxPrefered;
        }
        Debug.Log("> 0.8");
        return getrandom.Next(idxMin, idxMax);
    }

}
