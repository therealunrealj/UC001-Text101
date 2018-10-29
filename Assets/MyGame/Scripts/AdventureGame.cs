using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AdventureGame : MonoBehaviour {

    private static readonly System.Random getrandom = new System.Random();
    [SerializeField] Text textComponent;
    [SerializeField] State startingState;
    private int passedStatesCount;
    private int collectedWoolCount;
    private double dehydrationCount;
    private bool exit;
    
    private int statesUntilRescue;

    State actualState;

	// Use this for initialization
	void Start () {
        actualState = startingState;
        textComponent.text = actualState.GetStateStroy();
        passedStatesCount = 0;
        collectedWoolCount = 0;
        dehydrationCount = 0;
        statesUntilRescue = 100;
        exit = false;
	}

    // Update is called once per frame
    void Update () {
        ManageState();
	}

    private void ResetCounters()
    {
        passedStatesCount  = 0;
        collectedWoolCount = 0;
        dehydrationCount   = 0;
    }

    private State doTransition(State currentState, State nextState)
    {

        passedStatesCount += 1;
        dehydrationCount = (dehydrationCount < 20) ? dehydrationCount += 0.5 : dehydrationCount = 20;

        if (nextState.name == "Info.Alarm")
        {
            ResetCounters();
            Debug.Log("Counters Reseted + " + passedStatesCount + " " + collectedWoolCount + " " + dehydrationCount);
        }

        if (dehydrationCount == 20)
        {
            Debug.Log("Exit Dehydration " + passedStatesCount);
            return (State)AssetDatabase.LoadAssetAtPath("Assets/MyGame/States/Dead.Dehydration.asset", typeof(State));
        }
           
        if(currentState.name == "Collect" && nextState.name == "Rescue")
        {
            Debug.Log("Rescue in Sicht " + passedStatesCount);
            if(passedStatesCount <= statesUntilRescue)
            {
                return currentState;
            }
            else
            {
                Debug.Log("Exit Rescue " + passedStatesCount);
                return nextState;
            }
        }

        if (currentState.name == "Collect" && nextState.name == "Collect")
        {   
            int nbrWool = getrandom.Next(1, 3);
            collectedWoolCount += nbrWool;
            collectedWoolCount = Clamp(collectedWoolCount, 0, 5);
            Debug.Log("Collected " + nbrWool + "kg wool: current wool count: " + collectedWoolCount);
            return nextState;
        }


        if (currentState.name == "Knit" && nextState.name == "Knit")
        {
            if(collectedWoolCount >= 2){
                collectedWoolCount -= 2;
                dehydrationCount -= 1;
                Debug.Log("Wool Knitted -2kg + 1L water for magda, current dehydration" + dehydrationCount );

            }
            else
            {
                nextState.SetKnitNotification("not enough wool for knitting. collect wool");
            }
            
            Debug.Log("Wolle -2, Wasser +1");
            return nextState;
        }

        if(currentState.name == "Fight" && (nextState.name == "Collect" || nextState.name == "Fight")){

            Debug.Log("wool before Fight in kg: " + collectedWoolCount);
            collectedWoolCount += getrandom.Next(0, 3);
            collectedWoolCount = Clamp(collectedWoolCount, 0, 5);
            Debug.Log("wool after Fight in kg: " + collectedWoolCount);

        }

        return nextState;
    }

    private int Clamp(int value, int cmin, int cmax)
    {

        return Math.Max(Math.Min(value, cmax), cmin);
    }


    private void ManageState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            State[] nextStates = actualState.GetNextStates();
            Debug.Log("States size 1");
            if(nextStates.Length < 1)
            {
                return;
            }
            State nextState = nextStates[0];
            actualState = doTransition(actualState, nextState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            State[] nextStates = actualState.GetNextStates();
            Debug.Log("States size 2");
            if (nextStates.Length < 2)
            {
                return;
            }
            State nextState = nextStates[1];
            actualState = doTransition(actualState, nextState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            State[] nextStates = actualState.GetNextStates();
            Debug.Log("States size 3");
            if (nextStates.Length < 3)
            {
                return;
            }
            State nextState = nextStates[2];
            actualState = doTransition(actualState, nextState); ;
        }
        else
        {
            //Debug.Log("bin am leben");
        }
        textComponent.text = actualState.GetStateStroy();
    }
}
