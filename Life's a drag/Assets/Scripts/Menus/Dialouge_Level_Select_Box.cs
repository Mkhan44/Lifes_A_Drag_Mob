//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
public class Dialouge_Level_Select_Box : MonoBehaviour
{
    //This script populates info into the dialouge box. This will comprise of:
    //Preview image of the stage, best time player has, the name of the stage,
    //option to play and option to go back to menu.
    //Potentially a hint/short description of the stage.

    public Image previewImg;
    public Text bestTimeText;
    public TextMeshProUGUI bestTimeTMPRO;
    public Text LevelNameText;
    public TextMeshProUGUI levelNameTMPRO;
    public Level_Select_Manager levelMan;

    Populate_Level_Buttons levelInfo;
    string fullLevName;
    string levNameDisplay;
    public int levelNumber;

    GameObject adsManager;
    string levelsTillAdKey = "levelsTillAdPlays";
    int levelsTillAdNum;
    
    void Start()
    {
        levelsTillAdNum = PlayerPrefs.GetInt(levelsTillAdKey);
        adsManager = GameObject.Find("AdsManager");
    }
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
       
        string levelDiff = levelMan.getDiff();
        string levelTheme = levelMan.getTheme();
        string levNameTemp;
        string bestTimeKey;
        string bestTimeStr;
        float bestTime;
        GameObject currentBatchOfLevels;
        TimeSpan bestTimeFormat;

        levNameTemp = levelMan.getTheme();
        currentBatchOfLevels = GameObject.Find(levNameTemp + "_Levels");
        levNameTemp = levelMan.getDiffAndTheme();

        levelInfo = currentBatchOfLevels.GetComponent<Populate_Level_Buttons>();


        levNameDisplay = levelInfo.numButtons[levelNumber - 1].GetComponent<Button_Level_Info>().stageInfo.levelName;
        fullLevName = levNameTemp + levelNumber;
        bestTimeKey = fullLevName + "_Best_Time";

        bestTime = PlayerPrefs.GetFloat(bestTimeKey);
        bestTimeFormat = TimeSpan.FromSeconds(bestTime);
        bestTimeStr = "Best time: " + bestTimeFormat.ToString("mm':'ss");
        bestTimeTMPRO.text = bestTimeStr;
        levelNameTMPRO.text = levNameDisplay;

        if(Resources.Load<Sprite>("Level_ScreenShots/" + levelDiff + "/" + levelTheme + "/" + levelNumber) != null)
        {
            previewImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Level_ScreenShots/" + levelDiff + "/" + levelTheme + "/" + levelNumber);
        }
        
        
        //bestTimeText.text = bestTimeStr;
        //LevelNameText.text = levNameDisplay;
       //LevelNameText.text = "This is level: " + levelNumber;

    }

    public void initializeBoxChallenge()
    {

        string levelDiff = levelMan.getDiff();
        string levelTheme = levelMan.getTheme();
        string levNameTemp;
        string bestFinishTimeKey;
        string bestTimeStr;
        float bestTime;
        GameObject currentBatchOfLevels;
        TimeSpan bestTimeFormat;

        levNameTemp = levelMan.getTheme();
        currentBatchOfLevels = GameObject.Find(levNameTemp + "_Levels");
        levNameTemp = levelMan.getDiffAndTheme();

        levelInfo = currentBatchOfLevels.GetComponent<Populate_Level_Buttons>();


        levNameDisplay = levelInfo.numButtons[levelNumber - 1].GetComponent<Button_Level_Info>().stageInfo.levelName + "\n" + "(Challenge)";
        fullLevName = levNameTemp + levelNumber;
        bestFinishTimeKey = fullLevName + "_Best_Finish_Time_Left";
        //Debug.Log(bestFinishTimeKey);

        bestTime = PlayerPrefs.GetFloat(bestFinishTimeKey);
        bestTimeFormat = TimeSpan.FromSeconds(bestTime);
        bestTimeStr = "Record: " + bestTimeFormat.ToString("mm':'ss") + " left";
        bestTimeTMPRO.text = bestTimeStr;
        levelNameTMPRO.text = levNameDisplay;

        if (Resources.Load<Sprite>("Level_ScreenShots/" + levelDiff + "/" + levelTheme + "/" + levelNumber) != null)
        {
            previewImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Level_ScreenShots/" + levelDiff + "/" + levelTheme + "/" + levelNumber);
        }


        //bestTimeText.text = bestTimeStr;
        //LevelNameText.text = levNameDisplay;
        //LevelNameText.text = "This is level: " + levelNumber;

    }


    public void loadLevel()
    {
        if(adsManager != null && adsManager.GetComponent<AdsManager>().adsPurchasedCheck == 0)
        {
            if (levelsTillAdNum >= 3)
            {
                adsManager.GetComponent<AdsManager>().playInterstitialAd();
                PlayerPrefs.SetInt(levelsTillAdKey, 0);
                levelsTillAdNum = 0;
            }
            else
            {
                PlayerPrefs.SetInt(levelsTillAdKey, (levelsTillAdNum + 1));
            }

        }
        string levelToLoad;
        fullLevName = levelMan.getDiffAndTheme() + levelNumber;
        levelToLoad = fullLevName;
        levelMan.GetComponent<Load_Level>().LoadLevel(levelToLoad);
       // SceneManager.LoadScene(levelToLoad);
    }

    public void amIActive()
    {
        if (gameObject.activeSelf == true)
        {
            StartCoroutine(waitTime());
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public IEnumerator waitTime()
    {
        string levType = levelMan.levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().getLevelType();
      
        yield return new WaitForSeconds(0.1f);

        string theTheme = levelMan.getTheme();

        if (levType == "Normal")
        {
            initializeBox();
        }
        else
        {
            initializeBoxChallenge();
        }

        //initializeBox();
        this.GetComponent<PanelAnimator>().StartAnimIn();
     
    }

    
}
