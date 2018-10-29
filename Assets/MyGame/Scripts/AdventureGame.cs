using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour {

    [SerializeField] Text textComponent;
    [SerializeField] State startingState;

    State actualState;

	// Use this for initialization
	void Start () {
        actualState = startingState;
        textComponent.text = actualState.GetStateStroy();
	}

    // Update is called once per frame
    void Update () {
        ManageState();
	}

    private State doTransition(State currentState, State nextState)
    {
//        count += 1;
           

        if (currentState.name == "Collect" && nextState.name == "Collect")
        {
            Debug.Log("Wolle +1");
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
