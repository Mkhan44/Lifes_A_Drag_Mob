using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Use this script to interact with level select buttons and store the names of the difficulty + theme the user picks.
//This way we can begin forming the string to populate all level select buttons with the stars and what not on the level select screens.
public class Level_Select_Manager : MonoBehaviour
{
    string difficulty;
    string theme;

    public void setDiff(string diff)
    {
        difficulty = diff;
        Debug.Log("Difficulty is: " + difficulty);
    }

    public void setTheme(string t)
    {
        theme = t;
        Debug.Log("Theme is: " + theme);
    }

    public string getTheme()
    {
        return theme;
    }

    public string getDiff()
    {
        return difficulty;
    }

    public string getDiffAndTheme()
    {
        string combo;

        combo = difficulty + "_" + theme + "_";
        Debug.Log("diff + theme is: " + combo);
        return combo;
    }

}
