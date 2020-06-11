using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
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
    
    //Combine box references.
    public GameObject combineBox1;
    public GameObject combineBox2;
    public GameObject resultBox;
    bool resultFilled = false;

    //Level completed variables.
    bool levelComplete = false;
    private string nextSceneName;
    public Text levelCompleteText;
    public GameObject nextLevelButton;
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
        nextSceneName = theLev.levelDifficulty + "_" + theLev.levelTheme + "_" + theLev.levelNum.ToString();

       // Debug.Log(nextSceneName);


        levelCompleteText.enabled = false;
        nextLevelButton.SetActive(false);
        for (int i = 0; i < theLev.requiredItems.Count; i++)
        {
            Instantiate(theLev.requiredItems[i].item, new Vector3(theLev.requiredItems[i].xPos, theLev.requiredItems[i].yPos,0), Quaternion.identity);
        }

        //Calculate how many items are needed to complete the level.
        //The total number of combo items + the number of regular items that are not
        //materials for a combo item.
        int numMats = 0;
        for (int j = 0; j < theLev.comboItemsNeeded.Count; j++)
        {
            for (int k = 0; k < theLev.requiredItems.Count; k++ )
            {
                if ((theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat1 || theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat2))
                {
                    numMats += 1;
                }
            }
               
        }
        Debug.Log(numMats);
        itemsLeft = (theLev.comboItemsNeeded.Count + (theLev.requiredItems.Count - numMats));
        numItemsLeftText.text = "Items left: " + itemsLeft;
    }
    void Update()
    {
        if (!levelComplete)
        {
            currentTimer();
        }
   
    }

    void FixedUpdate()
    {
        checkCombineStatus();
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

  
    public void checkCombineStatus() 
    {
        
        if ((!Input.GetMouseButton(0)) && combineBox1.GetComponent<Combine_Collision>().collided && combineBox2.GetComponent<Combine_Collision>().collided)
        {
            if(resultFilled)
            {
                Debug.Log("Can't combine items, need to clear result area first!");
                //take out the item from the result box!
                resultBox.GetComponent<Combine_Collision>().tempObject.transform.position = resultBox.GetComponent<Combine_Collision>().tempObject.GetComponent<Draggable_Item>().initialPos;
            }
            else
            {
                combine(combineBox1.GetComponent<Combine_Collision>().objectName, combineBox2.GetComponent<Combine_Collision>().objectName, combineBox1.GetComponent<Combine_Collision>().tagToCompare, combineBox2.GetComponent<Combine_Collision>().tagToCompare);
            }
            
        }

        if(resultBox.GetComponent<Combine_Collision>().collided)
        {
            resultFilled = true;
        }
        else
            resultFilled = false;
         
    }
    

    //Combo Manager. We'll check if the combo works based on string comparison with GameObject names that are in the combo area.
    //If the combo is successful, instantiate the comboItem and de-activate the 2 materials. If not, put materials back in their
    //Initial positions.
    public void combine(string firstItemName, string secondItemName, string tagToCompare1, string tagToCompare2)
    {
        string instanceName;
        if (tagToCompare1 == "RequiredItem")
        {
            instanceName = firstItemName + "(Clone)";
        }
        else
        {
            instanceName = firstItemName;
        }


        string instanceName2;
        if (tagToCompare2 == "RequiredItem")
        {
            instanceName2 = secondItemName + "(Clone)";
        }
        else
        {
            instanceName2 = secondItemName;
        }


        GameObject item1;
        GameObject item2;
        item1 = GameObject.Find(instanceName);
        item2 = GameObject.Find(instanceName2);

        string mat1;
        string mat2;

        for (int i = 0; i < theLev.comboItemsNeeded.Count; i++)
        {
            mat1 = theLev.comboItemsNeeded[i].mat1;
            mat2 = theLev.comboItemsNeeded[i].mat2;

            if ((firstItemName == mat1 || firstItemName == mat2) && (secondItemName == mat1 || secondItemName == mat2))
            {
               
                //Combo successful!
                Debug.Log("Combo successful!");
                Destroy(item1);
                Destroy(item2);
                Instantiate(theLev.comboItemsNeeded[i].theItem, new Vector3(2.4f,-3.5f,0f), transform.rotation);
                break;
            }
           
            if(i == theLev.comboItemsNeeded.Count-1)
            {
                //Item combo failed, try again!
                Debug.Log("Combo failed!");
                item1.transform.position = item1.GetComponent<Draggable_Item>().initialPos;
                item2.transform.position = item2.GetComponent<Draggable_Item>().initialPos;
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
        if(theTag == "RequiredItem")
        {
            instanceName = theName + "(Clone)";
        }
        else
        {
            instanceName = theName;
        }


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
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item>().initialPos;
        }
        else
        {
            Debug.Log("This is a background item!");
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item>().initialPos;
        }
       

        numItemsLeftText.text = "Items left: " + itemsLeft;
            if (itemsLeft == 0)
            {
                levelCompleted();
            }
    }

    void levelCompleted()
    {
        Debug.Log("You found all the items!");
        //Make the level up buttons visible. Make a pop up that shows your best time, etc.
        levelComplete = true;
        levelCompleteText.enabled = true;
        if(DoesSceneExist(nextSceneName))
        {
           nextLevelButton.SetActive(true);
        }

        //Test code for seeing if level progression works.
        nextLevelButton.SetActive(true);

        //Test to ensure there is a level after this one. If not; don't let the 'next level' button appear.
        //Have a retry button to restart the scene if the player wants to try again.
        //Have a quit to menu button for the player to quit to the menu.
        //If we implement it, have the player able to share their score to social media from this as well.
        //Save the player's best time if they beat it. 
        //Show animation of how many stars out of 3 the player received.
    }

   public void loadNextLevel()
    {
        string tempLoad = "testlevel2";

        SceneManager.LoadScene(tempLoad);

        //Use this code for the real game.
       //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
        //SceneManager.LoadScene(nextSceneName);
    }

   /// <summary>
   /// Returns true if the scene 'name' exists and is in your Build settings, false otherwise
   /// </summary>
   public static bool DoesSceneExist(string name)
   {
       if (string.IsNullOrEmpty(name))
           return false;

       for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
       {
           var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
           var lastSlash = scenePath.LastIndexOf("/");
           var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

           if (string.Compare(name, sceneName, true) == 0)
               return true;
       }

       return false;
   }
}
