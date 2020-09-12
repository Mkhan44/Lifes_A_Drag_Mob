using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

//Use this script to interact with level select buttons and store the names of the difficulty + theme the user picks.
//This way we can begin forming the string to populate all level select buttons with the stars and what not on the level select screens.
public class Level_Select_Manager : MonoBehaviour
{
    string difficulty;
    string theme;
    public AudioClip menuMusic;
    public AudioClip soundEffect;

    void Start()
    {
        if(menuMusic != null)
        {
            EazySoundManager.PlayMusic(menuMusic, 0.7f, true, false, 0.5f, 0.5f);
        }
    }
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

    public void delPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All playerprefs deleted!");
    }

    public void loadTut()
    {
        GetComponent<Load_Level>().LoadLevel("Tutorial");
    }



}
