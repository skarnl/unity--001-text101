using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField]
    Text textComponent;

    [SerializeField]
    State startingState;

    State currentState;

    public static bool isPrince = false;
    public static bool karelPickedUp = false;

    // Start is called before the first frame update
    void Start() {
        currentState = startingState;

        UpdateTextComponent();
    }

    // Update is called once per frame
    void Update() {
        ManageState();
    }

    private void ManageState() {
        var nextStates = currentState.GetNextStates();

        for (int index = 0; index < nextStates.Length; index++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + index)) {
                if (currentState == startingState) {
                    isPrince = index == 0;
                }

                if (currentState.IsKarelChoice()) {
                    karelPickedUp = index == 0;
                }
                
                currentState = nextStates[index];
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q)) {
            ResetGame();
        }

        UpdateTextComponent();
    }

    private void UpdateTextComponent()
    {
        textComponent.text = currentState.GetStateStory();
    }

    private void ResetGame() {
        currentState = startingState;
        isPrince = false;
        karelPickedUp = false;
    }
}
