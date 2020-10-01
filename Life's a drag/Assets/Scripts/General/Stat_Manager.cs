using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class Stat_Manager : MonoBehaviour
{
    string firstTimeKey = "firstTimeKey";
    // Start is called before the first frame update
    void Start()
    {
        checkFirstTime();
    }

    public void checkFirstTime()
    {
        if (PlayerPrefs.GetInt(firstTimeKey) == 0)
        {
            Debug.Log("Would you like to try the tutorial???");
            PlayerPrefs.SetFloat("Global_Volume", 1);
            PlayerPrefs.SetFloat("Music_Volume", 1);
            PlayerPrefs.SetFloat("SFX_Volume", 1);
        }
        else
            Debug.Log("This isn't your first time playing, nice!");

        EazySoundManager.GlobalVolume = PlayerPrefs.GetFloat("Global_Volume");
        EazySoundManager.GlobalMusicVolume = PlayerPrefs.GetFloat("Music_Volume");
        EazySoundManager.GlobalSoundsVolume = PlayerPrefs.GetFloat("SFX_Volume");
    }

}

