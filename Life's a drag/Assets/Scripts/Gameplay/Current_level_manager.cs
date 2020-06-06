using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Current_level_manager : MonoBehaviour
{
    public Level_Manager theLev;

    //How many items we need to complete the level.
    public int itemsLeft;

    public Text numItemsLeftText;
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

        for (int i = 0; i < theLev.requiredItems.Count; i++)
        {
            Instantiate(theLev.requiredItems[i].item, new Vector3(theLev.requiredItems[i].xPos, theLev.requiredItems[i].yPos,0), Quaternion.identity);
        }

      //  combine(theLev.requiredItems[2].item.gameObject.name, theLev.requiredItems[2].item.gameObject.name);

        //Calculate how many items are needed to complete the level.
        //The total number of combo items + the number of regular items that are not
        //materials for a combo item.
        int numRegItems = 0;
        for (int j = 0; j < theLev.comboItemsNeeded.Count; j++)
        {
            if (!(theLev.requiredItems[j].item.gameObject.name == theLev.comboItemsNeeded[j].mat1 || theLev.requiredItems[j].item.gameObject.name == theLev.comboItemsNeeded[j].mat2))
            {
                numRegItems += 1;
            }
        }
        itemsLeft = (theLev.comboItemsNeeded.Count + numRegItems);
        numItemsLeftText.text = "Items left: " + itemsLeft;
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
