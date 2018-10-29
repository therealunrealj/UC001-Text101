using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AdventureGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    [SerializeField] State startingState;
    private int passedStatesCount;
    private int collectedWoolCount;
    private double dehydrationCount;
    
    private int statesUntilRescue;

    State actualState;

	// Use this for initialization
	void Start () {
        actualState = startingState;
        textComponent.text = actualState.GetStateStroy();
        passedStatesCount = 0;
        collectedWoolCount = 0;
        statesUntilRescue = 100;
	}

    // Update is called once per frame
    void Update () {
        ManageState();
	}

    private State doTransition(State currentState, State nextState)
    {
        passedStatesCount += 1;
        dehydrationCount = (dehydrationCount < 20) ? dehydrationCount += 0.5 : dehydrationCount = 20;

        if (dehydrationCount == 20)
        {
            Debug.Log("Exit Dehydration " + passedStatesCount);
            return (State)AssetDatabase.LoadAssetAtPath("Assets/MyGame/States/Dead.Dehydration.asset", typeof(State));
        }
           
        if(currentState.name == "Collect" && nextState.name == "Rescue")
        {
            Debug.Log("Rescue in Sicht " + passedStatesCount);
            return passedStatesCount <= statesUntilRescue ? currentState : nextState;
        }
        if (currentState.name == "Collect" && nextState.name == "Collect")
        {
            Debug.Log("Wolle +1");
            collectedWoolCount += collectedWoolCount;
            return nextState;
        }
        if (currentState.name == "Knit" && nextState.name == "Knit")
        {
            Debug.Log("Wolle -2, Wasser +1");
            return nextState;

        }

        return nextState;
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
            Debug.Log("bin am leben");
        }
        textComponent.text = actualState.GetStateStroy();
    }
}
