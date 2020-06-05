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
        /*
        Debug.Log("The level name is: " + theLev.levelName);
        Debug.Log("The level theme is: " + theLev.levelTheme);
        Debug.Log("The difficulty is: " + theLev.levelDifficulty);
        Debug.Log("The number of the level is: " + theLev.levelNum);
        */

        for (int i = 0; i < theLev.itemsNeeded.Count; i++)
        {
            Instantiate(theLev.itemsNeeded[i], transform.position, transform.rotation);
        }

        combine(theLev.itemsNeeded[2].gameObject.name, theLev.itemsNeeded[1].gameObject.name);
        Debug.Log("Item 1 is: " + theLev.itemsNeeded[0].gameObject.name);
        Debug.Log("Item 2 is: " + theLev.itemsNeeded[1].gameObject.name);
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

    //Combo Manager. We'll check if the combo works based on string comparison with GameObject names that are in the combo area.
    //If the combo is successful, instantiate the comboItem and de-activate the 2 materials. If not, put materials back in their
    //Initial positions.
    public void combine(string firstItemName, string secondItemName)
    {
        for (int i = 0; i < theLev.comboItemsNeeded.Count; i++)
        {
            if ((firstItemName == theLev.comboItemsNeeded[i].mat1 || firstItemName == theLev.comboItemsNeeded[i].mat2) && (secondItemName == theLev.comboItemsNeeded[i].mat1) || (secondItemName == theLev.comboItemsNeeded[i].mat2))
            {
                //Combo successful!
                Debug.Log("Combo successful!");
                Instantiate(theLev.comboItemsNeeded[i].theItem, transform.position, transform.rotation);
                break;
            }
           
            if(i == theLev.comboItemsNeeded.Count)
            {
                //Item combo failed, try again!
                Debug.Log("Combo failed!");
                break;
            }
        }
    }
}
