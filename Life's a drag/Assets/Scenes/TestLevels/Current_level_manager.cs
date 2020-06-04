using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Current_level_manager : MonoBehaviour
{
    public Level_Manager theLev;

    public Text bestTimeText;
    public Text currentTimeText;
    public Text timeForStarsText;
    public Text objectiveText;

    private TimeSpan currentTime;
    //Use this bool for when we clear the level.
    private bool timerRunning;

    private float elapsedTime;
    public void Awake()
    {
        elapsedTime = 0f;

        if(theLev.bestTime > 0)
        {
            bestTimeText.text = "Best time: " + theLev.bestTime.ToString();
        }
        else 
        {
            bestTimeText.text = "Best Time: N/A";
        }

        currentTimeText.text = "Current Time: 00:00";
        timeForStarsText.text = "***: " + theLev.timeForThreeStars.ToString();
        objectiveText.text = "Objective: " + theLev.objective;
        
    }

    public void Start()
    {
        Debug.Log("The level name is: " + theLev.levelName);
        Debug.Log("The level theme is: " + theLev.levelTheme);
        Debug.Log("The difficulty is: " + theLev.levelDifficulty);
        Debug.Log("The number of the level is: " + theLev.levelNum);
    }
    void Update()
    {
        currentTimer();
       
    }


    /*
     ******************************************
     *
     !!!!!!!!!!!!!!!FUNCTIONS!!!!!!!!!!!!!!!!!!!
     * 
     ******************************************
     */

    //Timer for each level. 
    public void currentTimer()
    {
        elapsedTime += Time.deltaTime;
        currentTime = TimeSpan.FromSeconds(elapsedTime);
        string currentTimestr = "Current Time: " + currentTime.ToString("mm':'ss");
        currentTimeText.text = currentTimestr;

        //Implement the check for num Stars available here.
    
    }
}
