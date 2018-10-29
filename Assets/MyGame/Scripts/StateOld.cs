using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class StateOld : ScriptableObject{

    private static readonly System.Random getrandom = new System.Random();

    [TextArea(10,14)][SerializeField] string storyText;
    public State[] nextStates;
    public State[] randomStates;
    //private bool findRnd;


    public string GetStateStroy()
    {
        return storyText;
    }

    public State[] GetNextStates()
    {
        if(randomStates.Length > 0 )
        {
            MergeStates();
        }

        return nextStates;
        
    }

    int CheatToBeAlive(int idxPrefered, int idxMin, int idxMax)
    {
        return getrandom.NextDouble() < 0.8 ? idxPrefered : getrandom.Next(idxMin, idxMax);
     
        //if (getrandom.NextDouble() < 0.8)
        //{
        //    return idxPrefered;
        //}

        //return getrandom.Next(idxMin, idxMax);
    }

    private void MergeStates()
    {
        int rnd = CheatToBeAlive(0,0,1);
        State[] extendedArray = new State[nextStates.Length + 1];

        extendedArray[0] = randomStates[rnd];
        for (int i = 0; i < nextStates.Length; i++)
        {
            extendedArray[i + 1] = nextStates[i];
        }
        

        nextStates = extendedArray;
        randomStates = new State[0];
        Debug.Log("States are Merged");
    }

}
