using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using UnityEngine.UI;
//Use this script to interact with level select buttons and store the names of the difficulty + theme the user picks.
//This way we can begin forming the string to populate all level select buttons with the stars and what not on the level select screens.
public class Level_Select_Manager : MonoBehaviour
{
    string difficulty;
    string theme;
    public AudioClip menuMusic;
    public AudioClip soundEffect;
    public Button noAdsButton;
    public GameObject titleScreen;
    public GameObject mainMenuScreen;


    string numHintsKey = "remainingHints";
    int easterCount;
    private string logoEasterKey = "logoEasterKey";


    public GameObject logoEasterPanel;
    public GameObject logoEasterButton;

    public static bool firstTimeInMenu;
    private string noAdsKey = "noAdsKey";

    string totalStarsKey = "Total_Stars_Obtained";
    int totalStarsVal;

    string demoMessageDispKey = "demoMessage";
    int demoMsgVal;
    public GameObject demoPanel;

    public GameObject levelTypeSelectorInstance;
    void Awake()
    {
        levelTypeSelectorInstance = GameObject.Find("LevelTypeSelector");
        //firstTimeInMenu = true;
        
       
        if(PlayerPrefs.GetInt(noAdsKey) > 0)
        {
            noAdsButton.interactable = false;
            Debug.Log("You have purchased no ads!");
        }
        else
        {
            Debug.Log("Hello, you don't have purchased ads.");
        }

        totalStarsVal = PlayerPrefs.GetInt(totalStarsKey);

    }
    void Start()
    {
        Debug.Log("Total stars are:" + totalStarsVal);
        demoMsgVal = PlayerPrefs.GetInt(demoMessageDispKey);
        

        /*
       if(firstTimeInMenu)
       {
           titleScreen.GetComponent<PanelAnimator>().StartAnimIn();
           firstTimeInMenu = false;
       }
       else
       {
           mainMenuScreen.SetActive(true);
           mainMenuScreen.GetComponent<PanelAnimator>().startOffScreen = false;
       }
       */
        
       
        if(menuMusic != null)
        {
            EazySoundManager.PlayMusic(menuMusic, 0.5f, true, false, 0.5f, 0.5f);
        }

        Input.multiTouchEnabled = false;

        /*
        if(totalStarsVal == 27 && demoMsgVal == 0)
        {
            demoPanel.SetActive(true);
        }
        */
    }
    public void setDiff(string diff)
    {
        difficulty = diff;
        Debug.Log("Difficulty is: " + difficulty);
    }

    public void setTheme(string t)
    {
        theme = t;
        Debug.Log("Theme is: " + theme);
    }

    public string getTheme()
    {
        return theme;
    }

    public string getDiff()
    {
        return difficulty;
    }

    public string getDiffAndTheme()
    {
        string combo;

        combo = difficulty + "_" + theme + "_";
        Debug.Log("diff + theme is: " + combo);
        return combo;
    }

    public void delPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All playerprefs deleted!");
    }

    public void loadTut()
    {
        Time.timeScale = 1f;
        levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().setLevelType("Tutorial");

        GetComponent<Load_Level>().LoadLevel("Tutorial");
    }

    public void noTut()
    {
        Time.timeScale = 1f;
    }

    public void setLevelType(string typeString)
    {
        if (levelTypeSelectorInstance != null)
        {
            levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().setLevelType(typeString);
        }
        else
        {
            Debug.LogWarning("Level type selector is null!!!");
        }
        
    }

    public void easterEggCheck()
    {
        easterCount += 1;
        Debug.Log("You pressed the button: " + easterCount + " times.");

        if(easterCount == 44)
        {
            logoEasterButton.GetComponent<Button>().interactable = false;

            //Check if player already found this easter egg.
            int gotYet;
            gotYet = PlayerPrefs.GetInt(logoEasterKey);

            if(gotYet == 1)
            {
                //You already got the easter egg, nothing happens.
            }
            else
            {
                PlayerPrefs.SetInt(logoEasterKey, 1);
                int currentHints = PlayerPrefs.GetInt(numHintsKey);
                
                //Give the player 25 hints cause why not lol.
                PlayerPrefs.SetInt(numHintsKey, (currentHints + 25));
                logoEasterPanel.SetActive(true);
            }
        }

    }


}
