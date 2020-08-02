using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class Dialouge_Level_Select_Box : MonoBehaviour
{
    //This script populates info into the dialouge box. This will comprise of:
    //Preview image of the stage, best time player has, the name of the stage,
    //option to play and option to go back to menu.
    //Potentially a hint/short description of the stage.

    public Image previewImg;
    public Text bestTimeText;
    public Text LevelNameText;
    public Level_Select_Manager levelMan;

    Populate_Level_Buttons levelInfo;
    string fullLevName;
    string levNameDisplay;
    public int levelNumber;


    void OnEnable()
    {
        StartCoroutine(waitTime());
    }

    public void levelNum(int num)
    {
        levelNumber = num;
    }

    public void initializeBox()
    {
        //Set the preview image here when we have them.
        //previewImg = ...;
        string levNameTemp;
        string bestTimeKey;
        string bestTimeStr;
        float bestTime;
        GameObject currentBatchOfLevels;
        TimeSpan bestTimeFormat;

        levNameTemp = levelMan.getDiffAndTheme();
        currentBatchOfLevels = GameObject.Find(levNameTemp + "Levels");

        levelInfo = currentBatchOfLevels.GetComponent<Populate_Level_Buttons>();


        levNameDisplay = levelInfo.numButtons[levelNumber - 1].GetComponent<Button_Level_Info>().stageInfo.levelName;
        fullLevName = levNameTemp + levelNumber;
        bestTimeKey = fullLevName + "_Best_Time";

        bestTime = PlayerPrefs.GetFloat(bestTimeKey);
        bestTimeFormat = TimeSpan.FromSeconds(bestTime);
        bestTimeStr = "Best time: " + bestTimeFormat.ToString("mm':'ss");
        bestTimeText.text = bestTimeStr;
        LevelNameText.text = levNameDisplay;
       //LevelNameText.text = "This is level: " + levelNumber;

    }

    public void loadLevel()
    {
        string levelToLoad;
        levelToLoad = fullLevName;
        SceneManager.LoadScene(levelToLoad);
    }

    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.1f);
        initializeBox();
    }
    
}
