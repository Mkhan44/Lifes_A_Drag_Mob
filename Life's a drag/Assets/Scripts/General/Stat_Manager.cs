using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_Manager : MonoBehaviour
{
    string firstTimeKey = "firstTimeKey";
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void checkFirstTime()
    {
        if (PlayerPrefs.GetInt(firstTimeKey) == 0)
        {
            Debug.Log("Would you like to try the tutorial???");
            PlayerPrefs.SetInt(firstTimeKey, 1);
        }
        else
            Debug.Log("This isn't your first time playing, nice!");
    }

}
