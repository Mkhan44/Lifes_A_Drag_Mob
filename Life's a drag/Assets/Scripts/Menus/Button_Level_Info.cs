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

    public Dialouge_Level_Select_Box dialougeBox;

    //Will change based on theme.
    public TextMeshProUGUI errorText;

    int levelNum;
    int starsRequired;
    int starsNeeded;
    string whatTheme;
    public void Start()
    {
        levManager = GameObject.Find("Level_Select_Manager");
        if(stageInfo.starRequirement > 0)
        {
            starsRequired = stageInfo.starRequirement;
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

    //Called from populate buttons.
    public void calculateStarsLeft(int starNum)
    {
        starsNeeded = starNum;
    }

    //Our onClick() event.
    public void whatToCall()
    {
        if(starsNeeded > 0)
        {
            lockedMessage();
        }
        else
        {
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
        setTheme();
        if(errorText != null)
        {
            errorText.text = "You don't have enough " + whatTheme + " stars to play this level. You need " + starsNeeded + " more.";
            //Play animation to fade out errortext ...
        }
    }
}
