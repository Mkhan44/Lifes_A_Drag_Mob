using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Populate_Level_Buttons : MonoBehaviour
{
    //Populate this list with the amount of buttons that we will be using
    //This way we can manually control the number of levels in a theme at any given point.
    public List<GameObject> numButtons = new List<GameObject>();
    public Level_Select_Manager levelSelectManager;
    public int numThemeStars;
    public TextMeshProUGUI errorMessageText;
    public TextMeshProUGUI titleText;

    Coroutine animateRoutine;

    public void Start()
    {
        errorMessageText.enabled = false;
    }

    void OnEnable()
    {
        StartCoroutine(waitTime());
        //Debug.Log("Called coroutine!");
    }
    

    //Function to display the number of total stars the player has for this theme.
    void checkNumThemeStars()
    {
        string theme;
        theme = levelSelectManager.getTheme();
        string themeStarsKey;

        themeStarsKey = theme + "_Stars_Obtained";

        numThemeStars = PlayerPrefs.GetInt(themeStarsKey);

    }

    //Cycle through each button in the list, find it's child star picture object, and then populate it with the amount of stars
    //which the player has obtained for that level.

    public void displayLevels()
    {
        GameObject starLeft;
        GameObject starMid;
        GameObject starRight;
        GameObject lockImg;
       
        string diffTheme;
        string fullLevName;
        string starsKey;
        int starsObtained;
        int starReq;
       

        //This string is now in the format of "difficulty_theme_"
        diffTheme = levelSelectManager.getDiffAndTheme();

        for(int i = 0; i < numButtons.Count; i++)
        {
            numButtons[i].GetComponent<Button_Level_Info>().setNum(i + 1);

            //This should be equivilant to the star image.
            starRight = numButtons[i].transform.GetChild(0).gameObject;
            starRight.SetActive(true);
            starMid = numButtons[i].transform.GetChild(1).gameObject;
            starMid.SetActive(true);
            starLeft = numButtons[i].transform.GetChild(2).gameObject;
            starLeft.SetActive(true);
            lockImg = numButtons[i].transform.GetChild(4).gameObject;

            //Get the name of the level, we'll use this + star key to get the amt of stars.
            fullLevName = diffTheme + (i + 1).ToString();

           // Debug.Log("The level name is: " + fullLevName);
            starsKey = fullLevName + "_Best_Stars";

            starsObtained = PlayerPrefs.GetInt(starsKey);

            //Debug.Log("The starsKey is: " + starsKey);
            //Debug.Log("The stars obtained for stage " + (i + 1) + " are: " + starsObtained);
            starReq = numButtons[i].GetComponent<Button_Level_Info>().stageInfo.starRequirement;
            //Swap the image.
            switch(starsObtained)
            {
                case 0:
                    {
                        //Testing if the level needs to be locked.
                        if(starReq > 0)
                        {
                            if(numThemeStars < starReq)
                            {
                                //numButtons[i].GetComponent<Button>().interactable = false;
                                lockImg.SetActive(true);
                                numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(starReq - numThemeStars);
                            }
                            else
                            {
                                //numButtons[i].GetComponent<Button>().interactable = true;
                                lockImg.SetActive(false);
                            }
                        }
                        else
                        {
                            //numButtons[i].GetComponent<Button>().interactable = true;
                            lockImg.SetActive(false);
                        }
                        break;
                    }
                case 1:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        break;
                    }
                case 2:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        starRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                        break;
                    }
                case 3:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        starRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                        starMid.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMid");
                        break;
                    }
            }

        }

    }

    public void displayLevelsChallenge()
    {
        GameObject starLeft;
        GameObject starMid;
        GameObject starRight;
        GameObject lockImg;

        string diffTheme;
        string fullLevName;
        string starsKey;
        int starsObtained;
        int starReq;


        //This string is now in the format of "difficulty_theme_"
        diffTheme = levelSelectManager.getDiffAndTheme();

        for (int i = 0; i < numButtons.Count; i++)
        {
            numButtons[i].GetComponent<Button_Level_Info>().setNum(i + 1);

            //This should be equivilant to the star image.
            starRight = numButtons[i].transform.GetChild(0).gameObject;
            starRight.SetActive(false);
            starMid = numButtons[i].transform.GetChild(1).gameObject;
            starMid.SetActive(false);
            starLeft = numButtons[i].transform.GetChild(2).gameObject;
            starLeft.SetActive(false);
            lockImg = numButtons[i].transform.GetChild(4).gameObject;

            //Get the name of the level, we'll use this + star key to get the amt of stars.
            fullLevName = diffTheme + (i + 1).ToString();

            // Debug.Log("The level name is: " + fullLevName);
            starsKey = fullLevName + "_Best_Stars";

            starsObtained = PlayerPrefs.GetInt(starsKey);

            //Debug.Log("The starsKey is: " + starsKey);
            //Debug.Log("The stars obtained for stage " + (i + 1) + " are: " + starsObtained);
            starReq = numButtons[i].GetComponent<Button_Level_Info>().stageInfo.starRequirement;

        }
    }

    //When a button is clicked, this function is called.
    public void displayErrorText(int numStarsNeeded, string theme)
    {
        if (animateRoutine != null)
        {
            StopCoroutine(animateRoutine);
        }
        //setTheme();
        if (errorMessageText != null)
        {
            errorMessageText.enabled = true;
            errorMessageText.text = "You don't have enough " + theme + " stars to play this level. You need " + numStarsNeeded + " more.";
            errorMessageText.color = new Color32(255, 255, 255, 255);
            animateRoutine = StartCoroutine(animateText());
            //Play animation to fade out errorMessageText ...
        }
    }

    public IEnumerator waitTime()
    {
        string levType = levelSelectManager.levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().getLevelType();
      
        yield return new WaitForSeconds(0.1f);
        string theTheme = levelSelectManager.getTheme();
      //  Debug.Log("Theme in coroutine is: " + theTheme);

        checkNumThemeStars();

        if(levType == "Normal")
        {
            titleText.text = theTheme + ": " + "\n" + "Level select";
            displayLevels();
        }
        else
        {
            titleText.text = theTheme + ": " + "\n" + "Level select" + "\n" + "(Challenge)";
            displayLevelsChallenge();
            //Do a function that makes stars invisible.
        }
        
    }

    IEnumerator animateText()
    {
        float i = 0.0f;
        float rate = 0.0f;
        Color32 startColor = new Color32(255, 255, 255, 255);
        Color32 endColor = new Color32(255, 255, 255, 0);


        rate = (1.0f / 4.5f) * 1.0f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            errorMessageText.color = Color32.Lerp(startColor, endColor, (i));
            yield return null;
        }
    }
    
}
