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
    Coroutine animateRoutine;
    public void Start()
    {
        levManager = GameObject.Find("Level_Select_Manager");
        if(stageInfo.starRequirement > 0)
        {
            starsRequired = stageInfo.starRequirement;
            setTheme();
            setDiff();
            string findStr = whatDiff + "_" + whatTheme + "_Levels";
            themePop = GameObject.Find(findStr);
            if(themePop == null)
            {
                Debug.LogWarning("Hey, we didn't find the object for populate level button!");
            }
        }

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

        levelType = levManager.GetComponent<LevelTypeSelector>().getLevelType();

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
            //If(player has enough stars in the theme) , give do dialouge box. Else, print message that they don't have it.

            dialougeBoxInfo();
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

        /*
        if (animateRoutine != null)
        {
            StopCoroutine(animateRoutine);
        }
        setTheme();
        if(errorText != null)
        {
            errorText.enabled = true;
            errorText.text = "You don't have enough " + whatTheme + " stars to play this level. You need " + starsNeeded + " more.";
            errorText.color = new Color32(255, 255, 255, 255);
            animateRoutine = StartCoroutine(animateText());
            //Play animation to fade out errortext ...
        }
         */
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
