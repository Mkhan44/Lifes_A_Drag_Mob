using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Button_Level_Info : MonoBehaviour
{
   // public string theme;
    //public bool hasRequirement;
    //public int requiredStars;
    public Level_Manager stageInfo;

    public Level_Manager easyLevel;
    public Level_Manager normalLevel;
    public Level_Manager hardLevel;

    public GameObject levManager;
    public GameObject themePop;

    public Dialouge_Level_Select_Box dialougeBox;

    //Will change based on theme.
    public TextMeshProUGUI errorText;

    int levelNum;
    int starsRequired;
    int starsNeeded;
    string whatTheme;
    string whatDiff;

    int numChallengeStarsReq;
    int themeStarsTotal;
    string themeStarsTotalKey = "_Stars_Obtained";

    int clearCheck;

    Coroutine animateRoutine;
    public void Start()
    {
        levManager = GameObject.Find("Level_Select_Manager");

        /*
        if(stageInfo.starRequirement > 0)
        {
            starsRequired = stageInfo.starRequirement;
        }
        
        setTheme();
        setDiff();
        string findStr = whatDiff + "_" + whatTheme + "_Levels";
        themePop = GameObject.Find(findStr);
        if (themePop == null)
        {
            Debug.LogWarning("Hey, we didn't find the object for populate level button!");
        }

        themeStarsTotalKey = stageInfo.levelTheme + themeStarsTotalKey;
        numChallengeStarsReq = stageInfo.challengeStarReq;
        themeStarsTotal = PlayerPrefs.GetInt(themeStarsTotalKey);
        */
    }

    public void initializeButton()
    {
        themeStarsTotalKey = "_Stars_Obtained";

        if (stageInfo.starRequirement > 0)
        {
            starsRequired = stageInfo.starRequirement;
        }

        setTheme();
        setDiff();
     //   string findStr = whatDiff + "_" + whatTheme + "_Levels";
        string findStr = whatTheme + "_Levels";
        themePop = GameObject.Find(findStr);
        if (themePop == null)
        {
            Debug.LogWarning("Hey, we didn't find the object for populate level button!");
        }

        themeStarsTotalKey = stageInfo.levelTheme + themeStarsTotalKey;
        //Debug.Log("The key is: " + themeStarsTotalKey);
        numChallengeStarsReq = stageInfo.challengeStarReq;
        themeStarsTotal = PlayerPrefs.GetInt(themeStarsTotalKey);
    }
    /*
    public string getTheme()
    {
        return theme;
    }
    public int getStarReq()
    {
        if (!hasRequirement)
        {
            requiredStars = 0;
        }
        return requiredStars;
    }
     */
    public void setNum(int num)
    {
        levelNum = num;
    }

    public void setTheme()
    {
        whatTheme = levManager.GetComponent<Level_Select_Manager>().getTheme();
    }

    public void setDiff()
    {
        whatDiff = levManager.GetComponent<Level_Select_Manager>().getDiff();
    }

    //Called from populate buttons.
    public void calculateStarsLeft(int starNum)
    {
        starsNeeded = starNum;
    }

    //Our onClick() event.
    public void whatToCall()
    {
        string levelType;
        Level_Select_Manager tempLvlSelect;

        tempLvlSelect = levManager.GetComponent<Level_Select_Manager>();
        levelType = tempLvlSelect.levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().getLevelType();

        if(levelType == "Normal")
        {
            if (starsNeeded > 0)
            {
                lockedMessage();
            }
            else
            {
                dialougeBoxInfo();
            }
        }
        else
        {
            //If(player has enough stars in the theme) , do dialouge box. Else, print message that they don't have it.
            if (themeStarsTotal < numChallengeStarsReq || !themePop.GetComponent<Populate_Level_Buttons>().allChallengeComp)
            {
                challengeLockedMessage();
            }
            else
            {
                dialougeBoxInfo();
            }
            
        }

       
    }
    //We have enough stars, spawn in dialouge box.
    public void dialougeBoxInfo()
    {
        dialougeBox.levelNumber = (levelNum);
        dialougeBox.amIActive();
    }
    //we don't have enough stars, put up the message instead.
    public void lockedMessage()
    {
        //Call the function.
        themePop.GetComponent<Populate_Level_Buttons>().displayErrorText(starsNeeded, whatTheme);
    }

    public void challengeLockedMessage()
    {
        themeStarsTotal = PlayerPrefs.GetInt(themeStarsTotalKey);

        //Call the function.
        themePop.GetComponent<Populate_Level_Buttons>().displayChallengeErrorText(numChallengeStarsReq, whatTheme, themeStarsTotal);
    }


    public void challengeClearCheck(int theCheck)
    {
        clearCheck = theCheck;
        if(clearCheck == 0)
        {
            Debug.Log("We didn't clear " + stageInfo.levelName + ".");
        }
        else
        {
            Debug.Log("We cleared " + stageInfo.levelName + " !");
        }
    }

    //Tells the level select which difficulty to use.
    public void assignLev(string difficulty)
    {
       
        switch(difficulty)
        {
            case "Easy":
                {
                    stageInfo = easyLevel;
                    break;
                }
            case "Medium":
                {
                    stageInfo = normalLevel;
                    break;
                }
            case "Hard":
                {
                    stageInfo = hardLevel;
                    break;
                }
            default:
                {
                    stageInfo = easyLevel;
                    Debug.LogWarning("The difficulty was invalid.");
                    break;
                }
        }

        if(stageInfo == null)
        {
            Debug.LogWarning("Stage info is Null! Something was not assigned.");
            stageInfo = easyLevel;
        }

        starsRequired = stageInfo.starRequirement;
        starsNeeded = starsRequired;
        Debug.Log("Assigned difficulty is: " + stageInfo.levelDifficulty);

  
    }
    IEnumerator animateText()
    {
        float i = 0.0f;
        float rate = 0.0f;
        Color32 startColor = new Color32(255,255,255,255);
        Color32 endColor = new Color32(255,255,255,0);
      

        rate = (1.0f / 5.0f) * 1.0f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            errorText.color = Color32.Lerp(startColor, endColor, (i));
            yield return null;
        }

        /*
        while(errorText.alpha > 0)
        {
            yield return new WaitForSeconds(0.1f);
            errorText.alpha = (errorText.alpha - 1);
            yield return null;
        }
         */
    }
}
