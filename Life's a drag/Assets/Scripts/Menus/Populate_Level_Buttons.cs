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
    public TextMeshProUGUI themeStarText;

    public bool allChallengeComp;
    bool enoughCStars;
    string diff;

    Coroutine animateRoutine;

    public void Start()
    {
        errorMessageText.enabled = false;
        allChallengeComp = false;
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

        themeStarText.text = "Total theme stars obtained: " + numThemeStars + "/45";

    }

    //Cycle through each button in the list, find it's child star picture object, and then populate it with the amount of stars
    //which the player has obtained for that level.

    public void displayLevels()
    {
        GameObject starLeft;
        GameObject starMid;
        GameObject starRight;
        GameObject lockImg;
        GameObject clearImg;
       
        string diffTheme;
        string fullLevName;
        string starsKey;
        int starsObtained;
        int starReq;

        //This string is now in the format of "difficulty_theme_"
        diffTheme = levelSelectManager.getDiffAndTheme();

        for(int i = 0; i < numButtons.Count; i++)
        {
            numButtons[i].GetComponent<Button_Level_Info>().assignLev(diff);

            numButtons[i].GetComponent<Button_Level_Info>().setNum(i + 1);
            numButtons[i].GetComponent<Button_Level_Info>().initializeButton();

            //This should be equivilant to the star image.
            starRight = numButtons[i].transform.GetChild(0).gameObject;
            starRight.SetActive(true);
            starMid = numButtons[i].transform.GetChild(1).gameObject;
            starMid.SetActive(true);
            starLeft = numButtons[i].transform.GetChild(2).gameObject;
            starLeft.SetActive(true);
            lockImg = numButtons[i].transform.GetChild(4).gameObject;
            clearImg = numButtons[i].transform.GetChild(5).gameObject;


            //Not challenge mode, so don't have "Clear image" loaded in.

            clearImg.SetActive(false);
            //Get the name of the level, we'll use this + star key to get the amt of stars.
            fullLevName = diffTheme + (i + 1).ToString();

           // Debug.Log("The level name is: " + fullLevName);
            starsKey = fullLevName + "_Best_Stars";

            starsObtained = PlayerPrefs.GetInt(starsKey);
           // Debug.Log("We called the displayLevels function");

            
            //Debug.Log("The starsKey is: " + starsKey);
            //Debug.Log("The stars obtained for stage " + (i + 1) + " are: " + starsObtained);
            starReq = numButtons[i].GetComponent<Button_Level_Info>().stageInfo.starRequirement;

            Debug.Log("Star req for level " + (i+1) + " Is: " + starReq);
            //Swap the image.
            switch(starsObtained)
            {
                    //0 meaning if you have obtained 0 stars on that level so far.
                case 0:
                    {

                      
                        //Testing if the level needs to be locked.
                        if(starReq > 0)
                        {
                            if(numThemeStars < starReq)
                            {
                                //numButtons[i].GetComponent<Button>().interactable = false;
                                lockImg.SetActive(true);
                                starRight.SetActive(false);
                                starMid.SetActive(false);
                                starLeft.SetActive(false);
                                numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(starReq - numThemeStars);
                            }
                                //This is for if it's medium/hard. The star requirement is more than 0, but if you have enough theme stars and HAVEN'T played the level yet. That is what this code is for displaying.
                            else
                            {
                                //numButtons[i].GetComponent<Button>().interactable = true;
                                starRight.SetActive(true);
                                starMid.SetActive(true);
                                starLeft.SetActive(true);

                                starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeftBlack");
                                starRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRightBlack");
                                starMid.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMidBlack");
                                Debug.Log("THIS IS A TEST FOR MEDIUM OFFICE 1!");
                                //numButtons[i].GetComponent<Button>().interactable = true;

                                lockImg.SetActive(false);
                               
                            }
                        }
                        else
                        {
                            //Stars left are none because we have enough.
                            numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(0);

                            starRight.SetActive(true);
                            starMid.SetActive(true);
                            starLeft.SetActive(true);
                            //numButtons[i].GetComponent<Button>().interactable = true;
                            lockImg.SetActive(false);
                        }
                        break;
                    }
                case 1:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        lockImg.SetActive(false);
                        numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(0);
                        break;
                    }
                case 2:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        starRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                        lockImg.SetActive(false);
                        numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(0);
                        break;
                    }
                case 3:
                    {
                        starLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                        starRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                        starMid.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMid");
                        lockImg.SetActive(false);
                        numButtons[i].GetComponent<Button_Level_Info>().calculateStarsLeft(0);
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
        GameObject clearImg;

        string diffTheme;
        string theme;
        string fullLevName;
        string completeKey;
        int completeCheck;

        string medHardCompKey;
        string fullLevMedHard;
        string diffThemeMedHard;
        int compCheckMedHard;

        int checkNum = 0;

        //This string is now in the format of "difficulty_theme_"
        diffTheme = levelSelectManager.getDiffAndTheme();
        diffThemeMedHard = diffTheme;

        //Returns "theme" no underscores.
        theme = levelSelectManager.getTheme();

        if(diff == "Medium")
        {
            diffTheme = "Easy_" + theme + "_";
            Debug.Log("Difficulty is set to Medium, so we're using " + diffTheme);
        }
        else if(diff == "Hard")
        {
            diffTheme = "Medium_" + theme + "_";
            Debug.Log("Difficulty is set to Hard, so we're using " + diffTheme);
        }
        //Make a temp list to check whether or not each challenge level is completed.
         List<bool> completeListCheck = new List<bool>();

        for (int i = 0; i < numButtons.Count; i++)
        {
            numButtons[i].GetComponent<Button_Level_Info>().assignLev(diff);

            numButtons[i].GetComponent<Button_Level_Info>().setNum(i + 1);
            numButtons[i].GetComponent<Button_Level_Info>().initializeButton();

            //This should be equivilant to the star image.
            starRight = numButtons[i].transform.GetChild(0).gameObject;
            starRight.SetActive(false);
            starMid = numButtons[i].transform.GetChild(1).gameObject;
            starMid.SetActive(false);
            starLeft = numButtons[i].transform.GetChild(2).gameObject;
            starLeft.SetActive(false);
            lockImg = numButtons[i].transform.GetChild(4).gameObject;
            clearImg = numButtons[i].transform.GetChild(5).gameObject;

            clearImg.SetActive(true);
       

            //Get the name of the level, we'll use this + star key to get the amt of stars.
            fullLevName = diffTheme + (i + 1).ToString();
            fullLevMedHard = diffThemeMedHard + (i + 1).ToString();

           // Debug.Log("The level name is: " + fullLevName);

            //Complete key for previous levels if this is medium/hard.
            completeKey = fullLevName + "_Completed";
            completeCheck = PlayerPrefs.GetInt(completeKey);

            medHardCompKey = fullLevMedHard + "_Completed";
            compCheckMedHard = PlayerPrefs.GetInt(medHardCompKey);

           

            //THIS CHECK ONLY NEEDS TO HAPPEN ON MEDIUM OR HIGHER DIFFICULTIES.
            if (diff != "Easy")
            {
                bool tempBool;
                if (compCheckMedHard == 0)
                {
                    completeCheck = 0;
                    tempBool = false;
                    allChallengeComp = false;
                }
                else
                {
                    //It's cleared!
                    tempBool = true;
                    checkNum += 1;
                    //Use checkNum to see if we have cleared all levels. If we have not, then lockImg needs to be displayed.
                    if (checkNum == (numButtons.Count - 1))
                    {
                        allChallengeComp = true;
                    }

                }

               // completeListCheck.Add(tempBool);

            }
            //THIS CHECK ONLY NEEDS TO HAPPEN ON MEDIUM OR HIGHER DIFFICULTIES.

            //Easy mode, so allChallengeComp DOESN'T need to be checked.
            else
            {
                allChallengeComp = true;

                //Check the star Req and match it to our total stars.
                int numChallengeStarReq;

                numChallengeStarReq = numButtons[i].GetComponent<Button_Level_Info>().stageInfo.challengeStarReq;
                if(numThemeStars < numChallengeStarReq)
                {
                    enoughCStars = false;
                }
                else
                {
                    enoughCStars = true;
                }

            }

            //Checking if the level has been cleared...
            if (completeCheck == 0)
            {
                //Check if we can even play the level yet.
                Debug.Log("All challenge is: " + allChallengeComp + " And enoughCstars for level " + (i+1) + " is: " + enoughCStars);
               if(!allChallengeComp || !enoughCStars)
               {
                   clearImg.SetActive(false);
                   lockImg.SetActive(true);
               } 
               else
               {
                   lockImg.SetActive(false);
                   clearImg.SetActive(true);
                   clearImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/ClearEmpty");
               }
            }
            else
            {
                clearImg.SetActive(true);
                lockImg.SetActive(false);
                clearImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/Clear");
            }
        }

        /*
        //THIS CHECK ONLY NEEDS TO HAPPEN ON MEDIUM OR HIGHER DIFFICULTIES.
        if (diff != "Easy")
        {
            Debug.Log(completeListCheck.Count);
            for (int j = 0; j < completeListCheck.Count; j++)
            {
                if (!completeListCheck[j])
                {
                 //   Debug.Log("The " + j + " position in the list is not filled, breaking loop.");
                    allChallengeComp = false;
                    break;
                }
              //  Debug.Log("The " + j + " position in the list is filled, continuing loop.");

                //If it gets this far: We know all levels have been cleared.
                if (j == (completeListCheck.Count -1))
                {
                    Debug.Log("You cleared all previous difficulty challenge levels! Hopefully you have enough stars to play these.");
                    allChallengeComp = true;
                    break;
                }
            }

            
        }
        //THIS CHECK ONLY NEEDS TO HAPPEN ON MEDIUM OR HIGHER DIFFICULTIES.

        else
        {
            Debug.Log("The difficulty is Easy! No checking the clear conditions, just star conditions.");
            allChallengeComp = true;
        }

        */
     
       

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

    //2 ways this text can be displayed:
    //1. The player does not have enough theme stars to access any of the levels. (i.e. you need at least 16 office stars to unlock easy challenge)
    //2. The player has not completed the previous tier of challenge levels. (i.e. You have enough stars to UNLOCK the medium office stars, but you have not completed EASY office challenge levels yet.)
    public void displayChallengeErrorText(int ChallengeNumThemeStarsReq, string theme, int themeStarsTotal)
    {
        int neededStars = ChallengeNumThemeStarsReq - themeStarsTotal;

       // Debug.Log("You have: " + themeStarsTotal + " stars in this theme");

        if (animateRoutine != null)
        {
            StopCoroutine(animateRoutine);
        }
        //setTheme();
        if (errorMessageText != null)
        {
            //Play a different message based on what the player has not completed.
            if (!allChallengeComp)
            {
                string tempDiff;
                if(diff == "Medium")
                {
                    tempDiff = "Easy";
                }
                else
                {
                    tempDiff = "Medium";
                }

                errorMessageText.enabled = true;
                errorMessageText.text = "You need to complete all " + tempDiff + " " + theme + " challenge levels to play this stage. ";
                errorMessageText.color = new Color32(255, 255, 255, 255);
                animateRoutine = StartCoroutine(animateText());
                //Play animation to fade out errorMessageText ...
            }
            else
            {
                errorMessageText.enabled = true;
                errorMessageText.text = "You don't have enough " + theme + " stars to play these levels. You need " + neededStars + " more.";
                errorMessageText.color = new Color32(255, 255, 255, 255);
                animateRoutine = StartCoroutine(animateText());
                //Play animation to fade out errorMessageText ...
            }

           
        }
    }

    public IEnumerator waitTime()
    {
        string levType = levelSelectManager.levelTypeSelectorInstance.GetComponent<LevelTypeSelector>().getLevelType();
      
        yield return new WaitForSeconds(0.1f);
        string theTheme = levelSelectManager.getTheme();
        diff = levelSelectManager.getDiff();

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
