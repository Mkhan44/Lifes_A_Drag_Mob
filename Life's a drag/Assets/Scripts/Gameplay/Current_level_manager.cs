﻿using System.Collections;
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

    /* This function is called from the Required_item_collision class. The item's name is passed here.
     * De-activate the item, lower the item's left count by 1, and if it was the final item: We'll call
     * the level complete function. (To be added)
     * Also, we will need to highlight that picture when we implement the pictures of the items into the scrolling UI to indicate
     * that it has been found.
     */
    public void gotItem(string theName, string theTag, bool isMat)
    {
        string instanceName;
        instanceName = theName + "(Clone)";
        GameObject theInstance;
        theInstance = GameObject.Find(instanceName);
        if(theTag == "RequiredItem" && !isMat)
        {
            //theInstance.SetActive(false);
            Destroy(theInstance);
            itemsLeft--;
        }
        else if(theTag == "RequiredItem" && isMat)
        {
            Debug.Log("The item is not a required item for level completion! It's also a material.");
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item_Needed>().initialPos;
        }
        else
        {
            Debug.Log("The item is not a required item for level completion!");
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item_Needed>().initialPos;
        }
       

        numItemsLeftText.text = "Items left: " + itemsLeft;
            if (itemsLeft == 0)
            {
                Debug.Log("You found all the items!");
                //Call level Complete function! (TO BE ADDED)
            }
    }
}
