using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(menuName="State")]
public class State : ScriptableObject
{
    [TextArea(10,14)]
    [SerializeField] 
    string storyText;

    [SerializeField]
    State[] nextStates;

    [SerializeField]
    bool karelChoice = false;

    public string GetStateStory() {
        var returnValue = storyText;

        returnValue = ReplaceGenderBasedTexts(returnValue);
        returnValue = ReplaceKarelBasedTexts(returnValue);

        return returnValue;
    }

    private string ReplaceGenderBasedTexts(string text) {
        string returnValue = text;

        Regex regex = new Regex(@"(\[[a-z]*/[a-z]*\])"); 
        MatchCollection matches = regex.Matches(returnValue);

        foreach (Match match in matches) {
            GroupCollection groups = match.Groups;
            
            string combinedChoices = groups[1].ToString();

            string[] choices = combinedChoices.Replace("[", "").Replace("]", "").Split('/');
            string pickedChoice = AdventureGame.isPrince ? choices[0] : choices[1];

            returnValue = returnValue.Replace(combinedChoices, pickedChoice);
        }

        return returnValue;
    }

    private string ReplaceKarelBasedTexts(string text) {
        string returnValue = text;

        Regex regex = new Regex(@"(\[karel:\s(?:.|\n|\r)*\])"); 
        MatchCollection matches = regex.Matches(returnValue);

        foreach (Match match in matches) {
            GroupCollection groups = match.Groups;
            
            string foundMatch = groups[1].ToString();

            if (!AdventureGame.karelPickedUp) {
                returnValue = returnValue.Replace(foundMatch, "");
            } else {
                returnValue = returnValue.Replace(foundMatch, foundMatch.Replace("[karel: ", "").Replace("]", ""));
            }
        }
        return returnValue;
    }

    public bool IsKarelChoice() {
        return karelChoice;
    }

    public State[] GetNextStates() {
        return nextStates;
    }
}
