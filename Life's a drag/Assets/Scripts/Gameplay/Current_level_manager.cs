using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Hellmade.Sound;
using UnityEngine.Advertisements;
using Random = UnityEngine.Random;


public class Current_level_manager : MonoBehaviour
{
   
    public enum stageType
    {
        normalStage,
        tutorial,
        challengeStage
    }

      [Header("Stage Type related stuff")]
    public stageType currentState;
    public Level_Manager theLev;
    public Level_Manager nextLev;


    List<int> numInvis = new List<int>();
    List<GameObject> invisObjects = new List<GameObject>();
    Scene thisScene;

    //How many items we need to complete the level.
    public int itemsLeft;

    public Text numItemsLeftText;
    public Text bestTimeText;
    public Text currentTimeText;
    public Text timeForStarsText;
    public Text objectiveText;

    private TimeSpan currentTime;

     [Header("TMPro UI Stuff")]
    //New TMPRO  Ui stuff.
    public TextMeshProUGUI objectiveTextMesh;
    public TextMeshProUGUI currentTimeTextMesh;
    public TextMeshProUGUI bestTimeTextMesh;
    public TextMeshProUGUI timeForStarsTextMesh;
    public TextMeshProUGUI numItemsLeftTextMesh;

    //Top UI stars
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    //Regular UI holder.
    public GameObject regularUIHolder;

     [Header("Tutorial")]
    //For tutorial
    public bool control;

     [Header("Challenge Mode")]
    //For challenge mode.
    public GameObject challengeUIHolder;
    public TextMeshProUGUI timeLeftText;
    float timeLimit;
    float timeLeft;
    int rndSeed;
    public GameObject challengeCompleteHolder;
    public GameObject gameOverScreen;
    public TextMeshProUGUI challengeTimeLeftText;
    bool gameOverActive;

     [Header("Pause Menu")]
    //Pause menu
    public GameObject pauseMenuUI;
    public bool isPaused;
    public Button pauseButton;
    public Button retryButton;

     [Header("Bottom UI stuff")]
    //Bottom UI stuff.
    //public GameObject testImg;
    Transform bottomUIParent;
    Transform zoomedUIParent;
    public ScrollRect botScrollArea;
    public ScrollRect ZoomedScrollArea;
    public GameObject shrinkButton;
    public GameObject expandButton;
    public GameObject itemRequiredArea;
    public bool isZoomed;

     [Header("PlayerPrefs related")]
    //PlayerPrefs keys for each level.
    string bestTimeKey;
    string starsKey;
    int bestStars;
    float bestTime;
    string totalStarsObtainedKey;
    int totalStars;
    string totalThemeStarsKey;
    int totalThemeStars;

     [Header("Time & stars needed")]
    //References to scriptableObject stuff so we don't hafta keep referencing the object itself.
    float timeFor3Stars;
    float timeFor2Stars;
    TimeSpan timeForStarsFormat;
    string timeForStarsStr;
    int numStarsPassed;

    //Use this bool for when we clear the level.
    private bool timerRunning;
    private float elapsedTime;

     [Header("Combine Box related")]
    //Combine box references.
    public GameObject combineBox1;
    public GameObject combineBox2;
    public GameObject resultBox;
    bool resultFilled = false;
    public GameObject itemGotParticlePrefab;
    public GameObject itemSpawnParticlePrefab;

   
    //Level completed variables.
    bool levelComplete = false;
    private string nextSceneName;
    [Header("Level completed related")]
    public GameObject levelCompleteHolder;
    public TextMeshProUGUI finishTimeEndText;
    public GameObject starParticlePrefab;

     [Header("Audio Related")]
    //Music/Sfx related stuff.
    public AudioClip levelCompleteTheme;
    public AudioClip levelCompleteIntro;
    public AudioClip star1Sound;
    public AudioClip star2Sound;
    public AudioClip star3Sound;
    public AudioClip pauseSound;
    public AudioClip itemGetSound;
    public AudioClip combineSound;
    private float themeVolume;

    
    //Ad related stuff.
    private string noAdsKey = "noAdsKey";
     [Header("Ad related")]
    public int noAdsNum;
    string levelsTillAdKey = "levelsTillAdPlays";
    int levelsTillAdNum;
    public GameObject adsManager;
    public Button shareButton;

     [Header("Hint related")]
    //Hint related stuff.
    public GameObject hintCursorPrefab;
    List<GameObject> currentObsInPlay = new List<GameObject>();
    int currentObListNum;
    string numHintsKey = "remainingHints";
    int numHintsRemaining;
    public Button hintButton;
    public TextMeshProUGUI hintsLeftText;
    public TextMeshProUGUI hintPanelText;
    public GameObject hintPanel;

    //For scaling camera.
    public GameObject cameraView;
    public GameObject backgroundImage;

    public void Awake()
    {
        GameObject levelSelector =  GameObject.Find("LevelTypeSelector");
        if (levelSelector != null)
        {
            setLevelType(levelSelector.GetComponent<LevelTypeSelector>().getLevelType());
        }
        else
        {
            //Just do nothing and go with what's there if it's not specified.
        }
      

        noAdsNum = PlayerPrefs.GetInt(noAdsKey);
        thisScene = SceneManager.GetActiveScene();
        elapsedTime = 0f;

        //Throw everything below this into a function later to make it easier to read.
        levelsTillAdNum = PlayerPrefs.GetInt(levelsTillAdKey);
        adsManager = GameObject.Find("AdsManager");

        GameObject tempComb = GameObject.Find("Combo_Area_BG");
        GameObject tempCombChild = tempComb.transform.GetChild(1).gameObject;
        combineBox1 = tempCombChild.transform.GetChild(0).gameObject;

        tempCombChild = tempComb.transform.GetChild(2).gameObject;
        combineBox2 = tempCombChild.transform.GetChild(0).gameObject;

        backgroundImage = GameObject.Find("BG-01");
        if(currentState == stageType.tutorial)
        {
           
        }
        else
        {
            GameObject tempItemBank = Instantiate(itemRequiredArea, itemRequiredArea.transform.position, itemRequiredArea.transform.rotation);
            tempItemBank.transform.SetParent(backgroundImage.transform, false);

        }
        
        //Make this into a function
        //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

        cameraView = GameObject.Find("Main Camera");
        float aspectRatio = cameraView.GetComponent<Camera>().aspect;

        combineBox1.GetComponent<Combine_Collision>().setCenter();
        combineBox2.GetComponent<Combine_Collision>().setCenter();
      //  Debug.Log("The aspect ratio is: " + aspectRatio);
       

        
    }

    public void Start()
    {
        


        if (theLev.musicVolume == 0f || theLev.musicVolume > 1f)
        {
            themeVolume = 0.5f;
        }
        else
        {
            themeVolume = theLev.musicVolume;
        }

        //Debug.Log("Theme volume is: " + themeVolume);

        if (currentState == stageType.normalStage)
        {
            initializeLevel();
            challengeUIHolder.SetActive(false);
        }
        else if(currentState == stageType.challengeStage)
        {
            initializeChallenge();
            regularUIHolder.SetActive(false);
          
        }
        else
        {
            initializeTutorial();
            challengeUIHolder.SetActive(false);
        }

       // Time.timeScale = 100;

    }
    void Update()
    {
        if (!levelComplete && !isPaused)
        {
            if(currentState != stageType.challengeStage)
            {
                currentTimer();
            }
            else
            {
                currentTimerChallenge();
            }
        }

        if(currentState == stageType.challengeStage)
        {

        }
        else
        {
            //BAD CODE, RE-REFACTOR THIS...
            numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);
            // Debug.Log(numHintsRemaining);
            hintsLeftText.text = "X " + numHintsRemaining.ToString();
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



    public void setLevelType(string levelType)
    {
        //If it's normal, normal mode. Challenge, challenge mode, Tut, tutorial level...
        //Call this in awake.

        if (levelType == "Normal")
        {
            currentState = stageType.normalStage;
        }
        else if (levelType == "Challenge")
        {
            currentState = stageType.challengeStage;
        }
        else if (levelType == "Tutorial") 
        {
            currentState = stageType.tutorial;
        }
        else
        {
            currentState = stageType.normalStage;
        }
    }
    //Spawn in items, get the scene name, etc.
    public void initializeLevel()
    {
        //Don't check NoAdsNum here because no matter what we want to hide it.
        if (adsManager != null)
        {
            adsManager.GetComponent<Banner_Ads>().hideBanner();
        }
        if(theLev.levelMusic != null)
        {
            EazySoundManager.StopAllMusic();
            EazySoundManager.PlayMusic(theLev.levelMusic, themeVolume, true, false, 0.5f, 0.5f);
        }

        //Debug statement for testing hints.
         //PlayerPrefs.SetInt(numHintsKey,99);


         numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);

        control = true;

        //Displaying the initial top UI timers and objective.
        timeFor2Stars = theLev.timeForTwoStars;
        timeFor3Stars = theLev.timeForThreeStars;

        currentTimeText.text = "Current Time: 00:00";
        currentTimeTextMesh.text = "Current Time: 00:00";
      //  objectiveText.text = "Objective: " + theLev.objective;
        objectiveTextMesh.text = "Objective: " + theLev.objective;

        timeForStarsFormat = TimeSpan.FromSeconds(timeFor3Stars);
        timeForStarsStr = ": " + timeForStarsFormat.ToString("mm':'ss");
        //timeForStarsText.text = timeForStarsStr;
        timeForStarsTextMesh.text = timeForStarsStr;

        pauseMenuUI.SetActive(true);
        isPaused = false;
        isZoomed = false;

        //Might be plus 1...? Since we need the NEXT scene name. 
        //IF YOU HAVE A PROBLEM GETTING TO THE NEXT SCENE DOUBLE CHECK THIS!!!!!
        nextSceneName = theLev.levelDifficulty + "_" + theLev.levelTheme + "_" + (theLev.levelNum + 1).ToString();

      //  Debug.Log(nextSceneName);

        //
        // **********************************************************************************
        // **********************************************************************************
        // **********************************************************************************
        // PLAYERPREFS STUFF FOR LOADING UP BEST SCORES AND WHAT NOT.

        TimeSpan bestTimeFormat;
        string bestTimeStr;


        //Don't change this variable once we have solidifed it. It will break all playerPrefs if we do.
        bestTimeKey = theLev.name + "_Best_Time";
        //Debug.Log(bestTimeKey);
        starsKey = theLev.name + "_Best_Stars";
        //Debug.Log(starsKey);
        bestStars = PlayerPrefs.GetInt(starsKey);
      //  bestStarsText.text = "Best stars this level: " + bestStars;
       // Debug.Log("Best stars for this level is: " + bestStars);

        //For the current theme amount of stars. (office, house, etc.)
        totalThemeStarsKey = theLev.levelTheme + "_Stars_Obtained";
        totalThemeStars = PlayerPrefs.GetInt(totalThemeStarsKey);

        //This key is universal for all levels. Meaning that it should hold a total throughout all levels in the game.
        totalStarsObtainedKey = "Total_Stars_Obtained";
        totalStars = PlayerPrefs.GetInt(totalStarsObtainedKey);
      //  totalStarsText.text = "Total stars in game: " + totalStars; 
      //  Debug.Log("Total stars the player has is: " + totalStars);
        

        if(PlayerPrefs.GetFloat(bestTimeKey) != 0)
        {
         //   Debug.Log("We found something! it is: " + PlayerPrefs.GetFloat(bestTimeKey).ToString());
            bestTime = PlayerPrefs.GetFloat(bestTimeKey);
            bestTimeFormat = TimeSpan.FromSeconds(bestTime);
            bestTimeStr = "Best time: " + bestTimeFormat.ToString("mm':'ss");
           // bestTimeText.text = bestTimeStr;
            bestTimeTextMesh.text = bestTimeStr;
        }
        else
        {
           // bestTimeText.text = "Best time: N/A";
            bestTimeTextMesh.text = "Best time: N/A";
            bestTime = 999f;
        }

        // PLAYERPREFS STUFF FOR LOADING UP BEST SCORES AND WHAT NOT.
        //
        // **********************************************************************************
        // **********************************************************************************
        // **********************************************************************************
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^



        GameObject tempAddToListObject;
        //Spawn in the items for the level.
        for (int i = 0; i < theLev.requiredItems.Count; i++)
        {
            if (theLev.requiredItems[i].zRot != 0 || theLev.requiredItems[i].yRot !=0)
            {
               tempAddToListObject = Instantiate(theLev.requiredItems[i].item, new Vector3(theLev.requiredItems[i].xPos, theLev.requiredItems[i].yPos, 0), Quaternion.Euler(0f, theLev.requiredItems[i].yRot, theLev.requiredItems[i].zRot));
            }
            else
            {
               tempAddToListObject = Instantiate(theLev.requiredItems[i].item, new Vector3(theLev.requiredItems[i].xPos, theLev.requiredItems[i].yPos, 0), Quaternion.identity);
            }

            //Add the object we just spawned in into the list of current objects that are in play.
            currentObsInPlay.Add(tempAddToListObject);


            tempAddToListObject.transform.SetParent(backgroundImage.transform, false);
            tempAddToListObject.GetComponent<Draggable_Item>().initialPos = tempAddToListObject.transform.position;

            if (theLev.requiredItems[i].isHidden)
            {
              //  Debug.Log(theLev.requiredItems[i].item.name + " is invisible!");
                numInvis.Add(i);
            }
        }
        //Initialize this variable here since we just finished populating the list.
        currentObListNum = 0;
        numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);
        hintsLeftText.text = "X " + numHintsRemaining.ToString();
       // Debug.Log("The player has: " + numHintsRemaining + " hints left!");

        /*
        for (int sk = 0; sk < currentObsInPlay.Count; sk++)
        {
            Debug.Log(currentObsInPlay[sk].name);
        }
        */

        //Calculate how many items are needed to complete the level.
        //The total number of combo items + the number of regular items that are not
        //materials for a combo item.
        int numMats = 0;
        for (int j = 0; j < theLev.comboItemsNeeded.Count; j++)
        {
            if(theLev.comboItemsNeeded[j].isAlsoMat)
            {
                numMats += 1;
            }
            for (int k = 0; k < theLev.requiredItems.Count; k++)
            {
                
                if ((theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat1 || theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat2))
                {
                    numMats += 1;
                }
            }

        }


        //Make all items that need to be invisible not show up.
        string tempName;
        GameObject tempObject;
        for (int k = 0; k < numInvis.Count; k++)
        {
            tempName = theLev.requiredItems[numInvis[k]].item.gameObject.name + "(Clone)";
            tempObject = GameObject.Find(tempName);
            invisObjects.Add(tempObject);
            invisObjects[k].SetActive(false);
        }


        // Debug.Log(numMats);
        itemsLeft = (theLev.comboItemsNeeded.Count + (theLev.requiredItems.Count - numMats));
       // numItemsLeftText.text = "Items left: " + itemsLeft;
        numItemsLeftTextMesh.text = "Items remaining: " + itemsLeft;

        //Fill in the bottom UI with items that are needed to complete the level by using Images.
        //These images will be transparent until the corresponding item is found, then they
        //will be filled in.
        bottomUIParent = GameObject.Find("Sprite_Holder").transform;
        zoomedUIParent = GameObject.Find("Sprite_Holder2").transform;

        //Bottom UI initialization to hide it. Will have to actually hide it instead of just making it transparent.
        shrinkButton.SetActive(false);
        GameObject child = ZoomedScrollArea.transform.GetChild(0).gameObject;
        Image theBG;
        theBG = child.GetComponent<Image>();
        Color tempZoomColor;
        tempZoomColor = theBG.color;
        tempZoomColor.a = 0.0f;
        theBG.color = tempZoomColor;


        GameObject tempIcon;
        Image tempImg;
        Color tempColor;

        if (theLev.icons.Count != 0)
        {
            for (int l = 0; l < theLev.icons.Count; l++)
            {
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.transform.SetParent(bottomUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                //tempColor.a = 0.65f;

                //Red color
                tempColor = new Color32(243, 40, 0, 125);

                tempImg.color = tempColor;


                //Update the zoomed in version as well.
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.name = tempIcon.name + "2";
                tempIcon.transform.SetParent(zoomedUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                //tempColor.a = 0.65f;

                //Red color
                tempColor = new Color32(243, 40, 0, 125);

                tempImg.color = tempColor;
             
               // Debug.Log(theLev.icons[l] + " " + tempIcon.name);
            }
        }

        ScrollToTop();
          
    }

    //Timer for each level. 
    public void currentTimer()
    {
        if(elapsedTime < 3599f)
        {
            elapsedTime += Time.deltaTime;
            currentTime = TimeSpan.FromSeconds(elapsedTime);
            string currentTimestr = "Current Time: " + currentTime.ToString("mm':'ss");
          //  currentTimeText.text = currentTimestr;
            currentTimeTextMesh.text = currentTimestr;
        }
        else
        {
          //  currentTimeText.text = "Current Time: 59:99";
            currentTimeTextMesh.text = "Current Time: 59:99";
        }
      


        //Implement the check for num Stars available here.
        if (elapsedTime > (timeFor3Stars + 1) && numStarsPassed == 0)
        {
            timeForStarsFormat = TimeSpan.FromSeconds(timeFor2Stars);
            timeForStarsStr = ": " + timeForStarsFormat.ToString("mm':'ss");
           // timeForStarsText.text = timeForStarsStr;
            timeForStarsTextMesh.text = timeForStarsStr;
            numStarsPassed = 1;
        }

        if(elapsedTime > (timeFor2Stars + 1) && numStarsPassed == 1)
        {
          //  timeForStarsText.text = ": 59:99";
            timeForStarsTextMesh.text = ": 59:99";
            numStarsPassed++;
        }

        StartCoroutine(shrinkStar());
    }

    //Coroutine to shrink the stars.
    IEnumerator shrinkStar()
    {
        float i = 0.0f;
        float rate = 0.0f;

        if(elapsedTime < timeFor3Stars)
        {
            rate = (1.0f / timeFor3Stars) * 1.0f;
            while (i < 1.0f && !levelComplete)
            {
                if(!isPaused)
                {
                    i += Time.deltaTime * rate;
                    star3.transform.localScale = Vector3.Lerp(star1.transform.localScale, new Vector3(0f, 0f, 1f), (i));
                }
              
                yield return null;
            }
        }
        else
        {
            rate = (1.0f / (timeFor2Stars-timeFor3Stars)) * 1.0f;
            while (i < 1.0f && !levelComplete)
            {
                if (!isPaused)
                {
                    i += Time.deltaTime * rate;
                    star2.transform.localScale = Vector3.Lerp(star1.transform.localScale, new Vector3(0f, 0f, 1f), (i));
                }

             
                yield return null;
            }
        }
        
     
    }

    //Bottom UI zoom and small area functions. 
    //Gotta make it so that when zoom is in active, player can't affect anything.
    //Also, make sure that when you zoom out again, the zoomed in version is still being updated in the background when new items are found.
    public void zoomInActivate()
    {
        GameObject child = ZoomedScrollArea.transform.GetChild(0).gameObject;
        Image tempImg;
        tempImg = child.GetComponent<Image>();
        Color tempZoomColor;
        tempZoomColor = tempImg.color;
        tempZoomColor.a = 1.0f;
        tempImg.color = tempZoomColor;
        //Zoom in is active.
        isZoomed = true;
    }

    public void zoomInDeactivate()
    {
        /*
        GameObject child = ZoomedScrollArea.transform.GetChild(0).gameObject;
        Image tempImg;
        tempImg = child.GetComponent<Image>();
        Color tempZoomColor;
        tempZoomColor = tempImg.color;
        tempZoomColor.a = 0.0f;
        tempImg.color = tempZoomColor;
        //Zoom in is not active.
         */
        isZoomed = false;
    }

  
    public void checkCombineStatus() 
    {
        
        if ((!Input.GetMouseButton(0)) && combineBox1.GetComponent<Combine_Collision>().collided && combineBox2.GetComponent<Combine_Collision>().collided)
        {
            //Checking for if 2 of the same item (basically overlapping cause of size) happens.
            if (combineBox1.GetComponent<Combine_Collision>().objectName == combineBox2.GetComponent<Combine_Collision>().objectName)
            {
                Debug.Log("Yo, double collision of the same item!");
                string tempName = combineBox1.GetComponent<Combine_Collision>().tempObject.gameObject.name.Replace("(Clone)", "");
                for (int i = 0; i < theLev.comboItemsNeeded.Count; i++)
                {
                    if (tempName == theLev.comboItemsNeeded[i].name)
                    {
                        combineBox1.GetComponent<Combine_Collision>().tempObject.transform.position = theLev.comboItemsNeeded[i].initialPos;
                        break;
                    }
                    //If we get passed the last item, then it has to be either a required item or a draggable item, so just throw it to the initial position....Nevermind, forgot it could be a freakin' combo item >.>
                    if (i == theLev.comboItemsNeeded.Count - 1)
                    {
                        combineBox1.GetComponent<Combine_Collision>().tempObject.transform.position = combineBox1.GetComponent<Combine_Collision>().tempObject.GetComponent<Draggable_Item>().initialPos;
                    }
                }
            }
            else
            {
                combine(combineBox1.GetComponent<Combine_Collision>().objectName, combineBox2.GetComponent<Combine_Collision>().objectName, combineBox1.GetComponent<Combine_Collision>().tagToCompare, combineBox2.GetComponent<Combine_Collision>().tagToCompare);
            }
               
            
            
        }

      
         
    }
    
    //Turns on invisible items that are needed.
    public void turnOn()
    {
        if(currentState != stageType.tutorial)
        {
            for (int k = 0; k < numInvis.Count; k++)
            {
                invisObjects[k].SetActive(true);
            }  
        }
        else
        {
            invisObjects[0].SetActive(true);
        }
     
       
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
        GameObject newItem;
        //Temp just to please the system.
        newItem = GameObject.Find(instanceName);

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
              
                //Play animation to signify to player where the new item appeared!

                if(currentState == stageType.challengeStage)
                {
                    switch(rndSeed)
                    {
                        case 1:
                            {
                                Instantiate(itemSpawnParticlePrefab, theLev.comboItemsNeeded[i].spawnPoint1, Quaternion.Euler(0f, 0f, 0f));
                                newItem = Instantiate(theLev.comboItemsNeeded[i].theItem, theLev.comboItemsNeeded[i].spawnPoint1, Quaternion.Euler(0f, 0f, 0f));
                                newItem.GetComponent<Draggable_Item>().initialPos = theLev.comboItemsNeeded[i].spawnPoint1;
                                break;
                            }
                        case 2:
                            {
                                Instantiate(itemSpawnParticlePrefab, theLev.comboItemsNeeded[i].spawnPoint2, Quaternion.Euler(0f, 0f, 0f));
                                newItem = Instantiate(theLev.comboItemsNeeded[i].theItem, theLev.comboItemsNeeded[i].spawnPoint2, Quaternion.Euler(0f, 0f, 0f));
                                newItem.GetComponent<Draggable_Item>().initialPos = theLev.comboItemsNeeded[i].spawnPoint2;
                                break;
                            }
                        case 3:
                            {
                                Instantiate(itemSpawnParticlePrefab, theLev.comboItemsNeeded[i].spawnPoint3, Quaternion.Euler(0f, 0f, 0f));
                                newItem = Instantiate(theLev.comboItemsNeeded[i].theItem, theLev.comboItemsNeeded[i].spawnPoint3, Quaternion.Euler(0f, 0f, 0f));
                                newItem.GetComponent<Draggable_Item>().initialPos = theLev.comboItemsNeeded[i].spawnPoint3;
                                break;
                            }
                           
                    }
                   
                }
                else
                {
                    Instantiate(itemSpawnParticlePrefab, theLev.comboItemsNeeded[i].initialPos, Quaternion.Euler(0f, 0f, 0f));
                    newItem = Instantiate(theLev.comboItemsNeeded[i].theItem, theLev.comboItemsNeeded[i].initialPos, Quaternion.Euler(0f, 0f, 0f));
                }
                
                /*
                if(currentState != stageType.tutorial)
                {
                    newItem.transform.SetParent(backgroundImage.transform, false);
                    newItem.GetComponent<Draggable_Item>().initialPos = newItem.transform.position;
                }
               */
                //Removing the combo items from the list.

                /*
                 * Condense this into 1 for loop that's nested maybe.
                 */
                for (int j = 0; j < currentObsInPlay.Count; j++)
                {
                   
                    if (j > currentObsInPlay.Count)
                    {
                        Debug.LogWarning("trying to access an item that isn't in the list!");
                        break;
                    }
                    if (currentObsInPlay[j].gameObject.name == instanceName)
                    {
                        Debug.Log("We removed " + currentObsInPlay[j].gameObject.name + " from the list!");
                        currentObsInPlay.Remove(currentObsInPlay[j]);
                        break;

                    }
                }

                for (int k = 0; k < currentObsInPlay.Count; k++)
                {

                    if (k > currentObsInPlay.Count)
                    {
                        Debug.LogWarning("trying to access an item that isn't in the list!");
                        break;
                    }
                    if (currentObsInPlay[k].gameObject.name == instanceName2)
                    {
                        Debug.Log("We removed " + currentObsInPlay[k].gameObject.name + " from the list!");
                        currentObsInPlay.Remove(currentObsInPlay[k]);
                        break;
                    }
                }

                /*
               * Condense this into 1 for loop that's nested maybe.
               */

                //Add the combo item to the list after removing the other 2.
                currentObsInPlay.Add(newItem);


                Destroy(item1);
                Destroy(item2);

                if(combineSound != null)
                {
                    EazySoundManager.PlaySound(combineSound, 0.7f);
                }
                pauseButton.interactable = true;
                //Instantiate(theLev.comboItemsNeeded[i].theItem, new Vector3(2.4f,-3.5f,0f), Quaternion.Euler(0f, 0f, 0f));
                break;
            }
           
            if(i == theLev.comboItemsNeeded.Count-1)
            {
                //Item combo failed, try again!
                Debug.Log("Combo failed!");
                if(item1.GetComponent<Draggable_Item>().initialPos != new Vector3(0,0,0))
                {
                    item1.transform.position = item1.GetComponent<Draggable_Item>().initialPos;
                }
                else
                {
                    item1.transform.position = theLev.comboItemsNeeded[i].initialPos;
                }

                if (item2.GetComponent<Draggable_Item>().initialPos != new Vector3(0, 0, 0))
                {
                    item2.transform.position = item2.GetComponent<Draggable_Item>().initialPos;
                }
                else
                {
                    item2.transform.position = theLev.comboItemsNeeded[i].initialPos;
                }
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
    public void gotItem(string theName, string theTag, bool isMat, bool isCombo)
    {
        pauseButton.interactable = true;

        string instanceName;
        string iconInstanceName = "";
        GameObject iconInstance;
        Image tempImg;
        Color tempColor;

        if(theTag == "RequiredItem")
        {
            instanceName = theName + "(Clone)";
            iconInstanceName = theName + "_Icon" + "(Clone)";
            if (theLev.icons.Count != 0)
            {
                iconInstance = GameObject.Find(iconInstanceName);
            }
        }
        else
        {
            instanceName = theName;
        }


        GameObject theInstance;
        theInstance = GameObject.Find(instanceName);
        if(theTag == "RequiredItem" && !isMat)
        {
            if(theLev.icons.Count != 0)
            {
                if (itemGetSound != null)
                {
                    EazySoundManager.PlaySound(itemGetSound);
                }
                iconInstance = GameObject.Find(iconInstanceName);
                tempImg = iconInstance.GetComponent<Image>();
                tempColor = tempImg.color;


                //tempColor.a = 1.0f;
                
                //Green
                tempColor = new Color32(91, 231, 84, 255);
                tempImg.color = tempColor;
                Instantiate(itemGotParticlePrefab, iconInstance.transform);


                //For zoomed in version.
                iconInstance = GameObject.Find(iconInstanceName + "2");
                tempImg = iconInstance.GetComponent<Image>();
                tempColor = tempImg.color;

                //tempColor.a = 1.0f;

                //Green
                tempColor = new Color32(91, 231, 84, 255);
                tempImg.color = tempColor;
              //  Instantiate(itemGotParticlePrefab, iconInstance.transform);

                //Remove item from the list of gameobjects for hints.
                
                for(int j = 0; j< currentObsInPlay.Count; j++)
                {
                    if(j > currentObsInPlay.Count)
                    {
                        Debug.LogWarning("trying to access an item that isn't in the list!");
                        break;
                    }
                    if(currentObsInPlay[j].gameObject.name == instanceName)
                    {
                        Debug.Log("We removed " + currentObsInPlay[j].gameObject.name + " from the list!");
                        currentObsInPlay.Remove(currentObsInPlay[j]);
                        break;  
                    }
                    
                    
                }
                 
            }
            
            if(currentState == stageType.tutorial)
            {
                control = false;
                gameObject.GetComponent<First_Tutorial>().requirementCompleted();
            }
           // iconInstance.GetComponent<Image>().color.a = 1f;
            //theInstance.SetActive(false);
            Destroy(theInstance);
            itemsLeft--;
        }
        else if(theTag == "RequiredItem" && isMat && !isCombo)
        {
            Debug.Log("The item is not a required item for level completion! It's also a material.");
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item>().initialPos;
            theInstance.transform.localScale = theInstance.GetComponent<Draggable_Item>().initialScale;
        }

        else if(isCombo && isMat)
        {
            Debug.Log("It's a combo item and a material.");
            for(int i = 0; i < theLev.comboItemsNeeded.Count; i++)
            {
                if(theLev.comboItemsNeeded[i].name == theName)
                {
                    theInstance.transform.position = theLev.comboItemsNeeded[i].initialPos;
                    theInstance.transform.localScale = theInstance.GetComponent<Draggable_Item>().initialScale;
                    break;
                }
            }
        }

            //Check if item is combo item AND if item is mat.
        else
        {
            Debug.Log("This is a background item!");
            theInstance.transform.position = theInstance.GetComponent<Draggable_Item>().initialPos;
            theInstance.transform.localScale = theInstance.GetComponent<Draggable_Item>().initialScale;
        }
       

       // numItemsLeftText.text = "Items left: " + itemsLeft;
        numItemsLeftTextMesh.text = "Items remaining: " + itemsLeft;
            if (itemsLeft == 0)
            {
                if(currentState == stageType.normalStage)
                {
                    levelCompleted();
                }
                else if(currentState == stageType.challengeStage && !isPaused)
                {
                    levelCompletedChallenge();
                }
                else
                {
                    //Do nothing if it's tutorial.
                }
               
            }
    }

    void levelCompleted()
    {
        
        if (theLev.levelMusic != null)
        {
           // EazySoundManager.StopAllMusic();
            EazySoundManager.PlayMusic(levelCompleteIntro, 0.5f, false, false, 0.0f, 0.0f);
            StartCoroutine(waitforIntro());
        }
        //Temp variables to hold records/stars for end screen text. Will probably use these to call functions when we make this into it's own script.
        float finishTime;
        int tempNumStars;
        int nextStarReq;

        finishTime = Mathf.Floor(elapsedTime);

        //Just using this to make it so background objects can't be moved.
        isPaused = true;


        Debug.Log("You found all the items!");

        //Don't do these 2 lines of code, fix this later!
        GameObject botScrollZoom = GameObject.Find("Bottom_Scroll_Zoomed");
        botScrollZoom.SetActive(false);

        expandButton.SetActive(false);

        pauseButton.interactable = false;
        retryButton.interactable = false;
        hintButton.interactable = false;
        levelComplete = true;
      

        /*
         *****************************************
         *****************************************
         *****************************************
         PLAYERPREF RELATED STUFF FOR SAVING GAME DATA.
         */
        if (finishTime < bestTime)
        {
            PlayerPrefs.SetFloat(bestTimeKey, elapsedTime);
        }



        //Calculate the amount of stars a player gets for beating the level.
        //If it's their first time getting x amount of stars, increase the 'record' for this level.
        //Also, if it's the first time they're receiving the stars, increase their grand total that
        //persists throughout the game. Player will be able to see this later.
        if (finishTime <= timeFor3Stars)
        {
            Debug.Log("You got 3 stars!");
            tempNumStars = 3;
            switch(bestStars) {
                case 0:
                    {
                        PlayerPrefs.SetInt(starsKey, (bestStars + 3));
                        PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 3));
                        PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 3));
                        //Give player another hint if it's their first time getting 3 stars on this level.
                        PlayerPrefs.SetInt(numHintsKey, numHintsRemaining + 1);
                        Debug.Log("It's your first time getting 3 stars, you get 1 hint!");
                        break;
                    }
                case 1:
                    {
                        PlayerPrefs.SetInt(starsKey, (bestStars + 2));
                        PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 2));
                        PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 2));
                        //Give player another hint if it's their first time getting 3 stars on this level.
                        PlayerPrefs.SetInt(numHintsKey, numHintsRemaining + 1);
                        Debug.Log("It's your first time getting 3 stars, you get 1 hint!");
                        break;
                    }
                case 2:
                    {
                        PlayerPrefs.SetInt(starsKey, (bestStars + 1));
                        PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 1));
                        PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 1));
                        //Give player another hint if it's their first time getting 3 stars on this level.
                        PlayerPrefs.SetInt(numHintsKey, numHintsRemaining + 1);
                        Debug.Log("It's your first time getting 3 stars, you get 1 hint!");
                        break;
                    }
                default:
                    {
                    Debug.Log("You already have 3 stars for this level. Good job!");
                    break;
                    }
            }
        }
        else if (finishTime <= timeFor2Stars)
        {
            tempNumStars = 2;
            Debug.Log("You got 2 stars, try for three!");
            switch (bestStars)
            {
                case 0:
                    {
                        PlayerPrefs.SetInt(starsKey, (bestStars + 2));
                        PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 2));
                        PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 2));
                        break;
                    }
                case 1:
                    {
                        PlayerPrefs.SetInt(starsKey, (bestStars + 1));
                        PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 1));
                        PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 1));
                        break;
                    }
                case 2:
                    {
                        Debug.Log("You have 2 stars on this level already. Good job!");
                        break;
                    }
                default:
                    {
                        Debug.Log("You already have 3 stars for this level. Good job!");
                        break;
                    }
            }
        }
        else
        {
                tempNumStars = 1;
                switch (bestStars)
                {
                    case 0:
                        {
                            PlayerPrefs.SetInt(starsKey, (bestStars + 1));
                            PlayerPrefs.SetInt(totalStarsObtainedKey, (totalStars + 1));
                            PlayerPrefs.SetInt(totalThemeStarsKey, (totalThemeStars + 1));
                            break;
                        }
                    case 1:
                        {
                            Debug.Log("You cleared this level, try harder to get more stars!");
                            break;
                        }
                    case 2:
                        {
                            Debug.Log("You have 2 stars on this level already. Good job!");
                            break;
                        }
                    default:
                        {
                            Debug.Log("You already have 3 stars for this level. Good job!");
                            break;
                        }
                }
        }

        
       // Debug.Log("The best time after completing the level is: " + PlayerPrefs.GetFloat(bestTimeKey).ToString());


        /*
        *****************************************
        *****************************************
        *****************************************
        PLAYERPREF RELATED STUFF FOR SAVING GAME DATA.
         *^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         *^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         *^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
         *^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        */


        /*
         ********************************************************
         Level complete stuff. Will probably move this into it's own script.
         */
   
        levelCompleteHolder.SetActive(true);
        levelCompleteHolder.GetComponent<PanelAnimator>().StartAnimIn();
        GameObject BGChild = levelCompleteHolder.transform.GetChild(0).gameObject;

      //  GameObject bannerChild = BGChild.transform.GetChild(0).gameObject;
        GameObject menuBtnChild = BGChild.transform.GetChild(3).gameObject;
        GameObject RetryBtnChild = BGChild.transform.GetChild(4).gameObject;
        GameObject PlayNextBtnChild = BGChild.transform.GetChild(5).gameObject;

        //star1 = left most star, star2 = right most star, star3 = middle star.
        GameObject star1 = BGChild.transform.GetChild(0).gameObject;
        GameObject star2 = BGChild.transform.GetChild(1).gameObject;
        GameObject star3 = BGChild.transform.GetChild(2).gameObject;

        //Disable buttons so that the player can't touch them while star animation is playing.
        menuBtnChild.GetComponent<Button>().interactable = false;
        RetryBtnChild.GetComponent<Button>().interactable = false;
        PlayNextBtnChild.GetComponent<Button>().interactable = false;
        shareButton.interactable = false;

        currentTime = TimeSpan.FromSeconds(finishTime);
        string finishTimestr = "Finish Time: " + currentTime.ToString("mm':'ss");
        finishTimeEndText.text = finishTimestr;
   

        totalThemeStars = PlayerPrefs.GetInt(totalThemeStarsKey);
        bool enoughStars = false;
        if (nextLev != null)
        {
            nextStarReq = nextLev.starRequirement;
            if (totalThemeStars < nextStarReq)
            {
                enoughStars = false;
                Debug.Log("The stars needed for the next level are: " + nextStarReq + " You have: " + totalThemeStars);
            }
            else
            {
                enoughStars = true;
                Debug.Log("Yay, you have enough stars!");
            }
               
        }
        else
        {
            Debug.LogWarning("nextLev is null! Is this the final level in this theme?");
        }

        StartCoroutine(animateStars(star1, star2, star3, tempNumStars, menuBtnChild, RetryBtnChild, PlayNextBtnChild, enoughStars));
      
       
            
       

        //Test to ensure there is a level after this one. If not; don't let the 'next level' button appear. 
        //DONE!

        //Have a retry button to restart the scene if the player wants to try again.
        //DONE!

        //Have a quit to menu button for the player to quit to the menu.
        //DONE!

        //If we implement it, have the player able to share their score to social media from this as well. 

        //Save the player's best time if they beat it.
        //DONE!

        //Show animation of how many stars out of 3 the player received.
        //SEMI-DONE!

        /*
        ********************************************************
        Level complete stuff. Will probably move this into it's own script.
        */
    }

    IEnumerator animateStars(GameObject star1, GameObject star2, GameObject star3, int tempNumStars, GameObject menuButton, GameObject RetryEndButton, GameObject PlayNextButton, bool enoughStars)
    {
        switch (tempNumStars)
        {
            case 1:
                {
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(starParticlePrefab, new Vector3 (star1.GetComponent<Transform>().transform.position.x,star1.GetComponent<Transform>().transform.position.y, 0f) , Quaternion.identity);
                    EazySoundManager.PlaySound(star1Sound, 0.6f);
                    star1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                    yield return new WaitForSeconds(0.5f);
                    star2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRightBlack");
                    star3.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMidBlack");
                    Debug.Log("We made it to 1");
                    Debug.Log(tempNumStars);
                    break;
                }
            case 2:
                {
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(starParticlePrefab, new Vector3(star1.GetComponent<Transform>().transform.position.x, star1.GetComponent<Transform>().transform.position.y, 0f), Quaternion.identity);
                    EazySoundManager.PlaySound(star1Sound, 0.6f);
                    star1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(starParticlePrefab, new Vector3(star2.GetComponent<Transform>().transform.position.x, star2.GetComponent<Transform>().transform.position.y, 0f), Quaternion.identity);
                    EazySoundManager.PlaySound(star2Sound, 0.6f);
                    star2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                    yield return new WaitForSeconds(0.2f);
                    star3.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMidBlack");
                    Debug.Log("We made it to 2");
                    break;
                }
            case 3:
                {
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(starParticlePrefab, new Vector3(star1.GetComponent<Transform>().transform.position.x, star1.GetComponent<Transform>().transform.position.y, 0f), Quaternion.identity);
                    EazySoundManager.PlaySound(star1Sound, 0.6f);
                    star1.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarLeft");
                    yield return new WaitForSeconds(0.5f);
                    Instantiate(starParticlePrefab, new Vector3(star2.GetComponent<Transform>().transform.position.x, star2.GetComponent<Transform>().transform.position.y, 0f), Quaternion.identity);
                    EazySoundManager.PlaySound(star2Sound, 0.6f);
                    star2.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarRight");
                    yield return new WaitForSeconds(0.8f);
                    Instantiate(starParticlePrefab, new Vector3(star3.GetComponent<Transform>().transform.position.x, star3.GetComponent<Transform>().transform.position.y, 0f), Quaternion.identity);
                    EazySoundManager.PlaySound(star3Sound, 0.6f);
                    star3.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI_Updated/StarMid");
                    Debug.Log("We made it to 3");
                    break;
                }
        }

        yield return new WaitForSeconds(0.5f);

        //Make the buttons work now.
        //Check if we should have the next level button available.

     
        if (!DoesSceneExist(nextSceneName))
        {
            PlayNextButton.SetActive(false);
        }
        else
        {
            if (enoughStars)
            {
                PlayNextButton.GetComponent<Button>().interactable = true;
            }
        }

        menuButton.GetComponent<Button>().interactable = true;
        RetryEndButton.GetComponent<Button>().interactable = true;
        shareButton.interactable = true;

        //Do something based on the amount of stars.

        //If noAds has not been purchased.
        if (noAdsNum == 0 && adsManager != null)
        {
            adsManager.GetComponent<Banner_Ads>().showBanner();
        }
        
       
    }

    IEnumerator waitforIntro()
    {
        yield return new WaitForSeconds(2.743f);
        EazySoundManager.PlayMusic(levelCompleteTheme, 0.5f, true, false, 0.0f, 1.0f);
    }

    public void shareButtonClicked()
    {
        Debug.Log("Share button clicked.");
        currentTime = TimeSpan.FromSeconds(elapsedTime);
        string finishTimestr = currentTime.ToString("mm':'ss");

        if(shareButton != null)
        {
            shareButton.GetComponent<Share_Button>().clickShareButton(finishTimestr, theLev.levelName);
            Debug.Log("Share test");
        }
        else
        {
            Debug.LogWarning("Share button not found!");
        }
        


        //finishTimeEndText.text = finishTimestr;
    }
   public void loadNextLevel()
    {
        if (noAdsNum == 0 && adsManager != null)
        {
            if(levelsTillAdNum >= 3)
            {
                adsManager.GetComponent<AdsManager>().playInterstitialAd();
                PlayerPrefs.SetInt(levelsTillAdKey, 0);
                levelsTillAdNum = 0;
            }
        }
        PlayerPrefs.SetInt(levelsTillAdKey, (levelsTillAdNum + 1));
        Debug.Log("Button clicked.");
       string tempLoad = "Main_Menu";
       if(DoesSceneExist(nextSceneName))
       {
           GetComponent<Load_Level>().LoadLevel(nextSceneName);
       }
       else
       {
           //Kick player back to main menu...This should never happen but failsafe.
           GetComponent<Load_Level>().LoadLevel(tempLoad);
       }
       

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

    //For restart button.
    public void restartScene()
   {
       //Get the type of stage. If it's challenge mode, pass it to something that doesn't get destroyed so when the stage restarts it knows to change stagetype to challenge.
        //Else, make it just load the level regularly.
       if (noAdsNum == 0 && adsManager != null)
       {
           if (levelsTillAdNum >= 3)
           {

               PlayerPrefs.SetInt(levelsTillAdKey, 0);
               levelsTillAdNum = 0;
               adsManager.GetComponent<AdsManager>().playInterstitialAd();
               // Debug.Log("ispaused is: " + isPaused);
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           }

           else
           {
               PlayerPrefs.SetInt(levelsTillAdKey, (levelsTillAdNum + 1));
               SceneManager.LoadScene(SceneManager.GetActiveScene().name);
               //levelsTillAdNum = 0;
           }
       }
       else
       {
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }

      
   }

    /*
     ****************************************************************************
     ****************************************************************************
     *********************TOP UI RELATED BUTTONS!!!!*****************************
     */

    //Gives pop-up menu for pausing the game.
    public void pause()
    {
        if(pauseSound != null)
        {
            EazySoundManager.PlaySound(pauseSound, 0.5f);
        }
       // pauseMenuUI.SetActive(true);
        retryButton.interactable = false;
        pauseButton.interactable = false;
        hintButton.interactable = false;
        isPaused = true;
      //  StartCoroutine(waitPause());
      
    }

    IEnumerator waitPause()
    {
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0f;
        //Debug.Log("Game paused.");
    }

    public void hint()
    {

        hintButton.interactable = false;
        pauseButton.interactable = false;
        retryButton.interactable = false;
        isPaused = true;
        Time.timeScale = 0f;

        if(numHintsRemaining > 0)
        {
            hintPanelText.text = "Would you like to use a hint? (" + numHintsRemaining.ToString() + " remaining)";
        }
        else
        {
            hintPanelText.text = "You don't have anymore hints. Would you like to watch a video ad to obtain one?";
        }

        hintPanel.SetActive(true);

    }

    public void hintYes()
    {
        Time.timeScale = 1f;
        //We have enough hints, so let the user use one.
        if (numHintsRemaining > 0)
        {
            if(currentObListNum > currentObsInPlay.Count)
            {
                //We went through all the elements, so make this 0 again to restart.
                currentObListNum = 0;
            }

            for (int i = currentObListNum; i < currentObsInPlay.Count; i++)
            {
                //If the object we're checking does not already have a hint arrow attached to it...Go to next item.
                if (!currentObsInPlay[i].GetComponent<Draggable_Item>().testArrowObject())
                {
                    currentObListNum++;
                    Debug.Log("Already has an arrow!");
                }
                else
                {
                    GameObject tempArrow = Instantiate(hintCursorPrefab, currentObsInPlay[i].transform.position, transform.rotation);
                    //tempArrow.transform.SetParent(GameObject.Find("Gameplay_UI_Canvas_V2").transform,false);
                    tempArrow.transform.position = currentObsInPlay[i].transform.position;
                    Vector3 TempPos = tempArrow.transform.position;
                    TempPos.y = (currentObsInPlay[i].transform.position.y + 1);
                    tempArrow.transform.position = TempPos;
                   
                    //Debug.Log(tempArrow.GetComponent<RectTransform>().anchorMax);
                    //Debug.Log(currentObsInPlay[i].transform.position);
                    currentObsInPlay[i].GetComponent<Draggable_Item>().setArrowHint(tempArrow);
                    currentObListNum++;
                    PlayerPrefs.SetInt(numHintsKey, (numHintsRemaining - 1));
                    numHintsRemaining -= 1;
                    break;
                }
            }

               
        }
            //Not enough hints! Gotta let them watch an ad and give 1 hint.
        else
        {
            if(adsManager != null)
            {
                adsManager.GetComponent<AdsManager>().playRewardedVideoAd();
            }
            
           
        } 

        hintButton.interactable = true;
        pauseButton.interactable = true;
        retryButton.interactable = true;
        isPaused = false;
        hintPanel.SetActive(false);
    }

    public void hintNo()
    {
        Time.timeScale = 1f;
        hintButton.interactable = true;
        pauseButton.interactable = true;
        retryButton.interactable = true;
        isPaused = false;
        hintPanel.SetActive(false);
    }

    IEnumerator waitForAd()
    {
        yield return new WaitForSeconds(1.0f);
    }

    /*
     ****************************************************************************
     ****************************************************************************
     *********************TOP UI RELATED BUTTONS!!!!*****************************
     */


    //Scrolling for bottom UI to reset itself.
    public void ScrollToTop()
    {
        botScrollArea.normalizedPosition = new Vector2(0, 1);
        ZoomedScrollArea.normalizedPosition = new Vector2(0, 1);
    }
    public void ScrollToBottom()
    {
        botScrollArea.normalizedPosition = new Vector2(0, 0);
        ZoomedScrollArea.normalizedPosition = new Vector2(0, 0);
    }


    /*
     * TUTORIAL RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!
     *************************************************************
     *************************************************************
     *************************************************************
     *************************************************************
     *************************************************************
     *************************************************************
     *************************************************************
     *************************************************************
     * TUTORIAL RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!
     */

    public void initializeTutorial()
    {
        if (noAdsNum == 0 && adsManager != null)
        {
            adsManager.GetComponent<Banner_Ads>().hideBanner();
        }
        if (theLev.levelMusic != null)
        {
            EazySoundManager.StopAllMusic();
            EazySoundManager.PlayMusic(theLev.levelMusic, themeVolume, true, false, 0.5f, 0.5f);
        }

        pauseButton.interactable = false;
        retryButton.interactable = false;

        expandButton.SetActive(false);
        control = false;
        //Displaying the initial top UI timers and objective.
        timeFor2Stars = theLev.timeForTwoStars;
        timeFor3Stars = theLev.timeForThreeStars;

       // currentTimeText.text = "Current Time: 00:00";
        currentTimeTextMesh.text = "Current Time: 00:00";
       // objectiveText.text = "Objective: " + theLev.objective;
        objectiveTextMesh.text = "Objective: " + theLev.objective;

        timeForStarsFormat = TimeSpan.FromSeconds(timeFor3Stars);
        timeForStarsStr = ": " + timeForStarsFormat.ToString("mm':'ss");
      //  timeForStarsText.text = timeForStarsStr;
        timeForStarsTextMesh.text = timeForStarsStr;

        isPaused = false;
        isZoomed = false;


        //Initialize the end level stuff to false.
        levelCompleteHolder.SetActive(false);


        //Spawn in the items for the level.
        //GameObject firstOb;
        Instantiate(theLev.requiredItems[0].item, new Vector3(theLev.requiredItems[0].xPos, theLev.requiredItems[0].yPos, 0), Quaternion.Euler(0f, 0f, theLev.requiredItems[0].zRot));

        //Calculate how many items are needed to complete the level.
        //The total number of combo items + the number of regular items that are not
        //materials for a combo item.
        int numMats = 0;
        for (int j = 0; j < theLev.comboItemsNeeded.Count; j++)
        {
            if(theLev.comboItemsNeeded[j].isAlsoMat)
            {
                numMats += 1;
            }
            for (int k = 0; k < theLev.requiredItems.Count; k++)
            {
                
                if ((theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat1 || theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat2))
                {
                    numMats += 1;
                }
            }

        }


        itemsLeft = (theLev.comboItemsNeeded.Count + (theLev.requiredItems.Count - numMats));
        //numItemsLeftText.text = "Items left: " + itemsLeft;
        numItemsLeftTextMesh.text = "Items remaining: " + itemsLeft;

        //Fill in the bottom UI with items that are needed to complete the level by using Images.
        //These images will be transparent until the corresponding item is found, then they
        //will be filled in.
        bottomUIParent = GameObject.Find("Sprite_Holder").transform;
        zoomedUIParent = GameObject.Find("Sprite_Holder2").transform;

        //Bottom UI initialization to hide it. Will have to actually hide it instead of just making it transparent.
        shrinkButton.SetActive(false);
        GameObject child = ZoomedScrollArea.transform.GetChild(0).gameObject;
        Image theBG;
        theBG = child.GetComponent<Image>();
        Color tempZoomColor;
        tempZoomColor = theBG.color;
        tempZoomColor.a = 0.0f;
        theBG.color = tempZoomColor;


        GameObject tempIcon;
        Image tempImg;
        Color tempColor;

        if (theLev.icons.Count != 0)
        {
            for (int l = 0; l < theLev.icons.Count; l++)
            {
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.transform.SetParent(bottomUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                tempColor.a = 0.5f;
                tempImg.color = tempColor;


                //Update the zoomed in version as well.
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.name = tempIcon.name + "2";
                tempIcon.transform.SetParent(zoomedUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                tempColor.a = 0.5f;
                tempImg.color = tempColor;
             
               // Debug.Log(theLev.icons[l] + " " + tempIcon.name);
            }
        }

        ScrollToTop();
          
    }

    //For hidden item.
    public GameObject spawnItem(int num)
    {
        GameObject tempObj;
        tempObj = (GameObject)Instantiate(theLev.requiredItems[num].item, new Vector3(theLev.requiredItems[num].xPos, theLev.requiredItems[num].yPos, 0), Quaternion.identity);
        invisObjects.Add(tempObj);
        Debug.Log(invisObjects.Count);
        Debug.Log(invisObjects[0].name);

        return tempObj;
    }

    //For combo items.
    public void spawnItem(int num1, int num2)
    {
        expandButton.SetActive(true);
        expandButton.GetComponent<Button>().interactable = true;
        GameObject tempObj1;
        GameObject tempObj2;

        tempObj1 = (GameObject)Instantiate(theLev.requiredItems[num1].item, new Vector3(theLev.requiredItems[num1].xPos, theLev.requiredItems[num1].yPos, 0), Quaternion.identity);
        tempObj2 = (GameObject)Instantiate(theLev.requiredItems[num2].item, new Vector3(theLev.requiredItems[num2].xPos, theLev.requiredItems[num2].yPos, 0), Quaternion.identity);
    }

    /*
  * TUTORIAL RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!
  *************************************************************
  *************************************************************
  *************************************************************
  *************************************************************
  *************************************************************
  *************************************************************
  *************************************************************
  *************************************************************
  * TUTORIAL RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!
  */


    /*
     * CHALLENGE MODE RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     ***********************************************************************
     *********************************************************************** 
     ************************************************************************
     ************************************************************************
     * CHALLENGE MODE RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
     */

    public void initializeChallenge()
    {
        //Don't check NoAdsNum here because no matter what we want to hide it.
        if (adsManager != null)
        {
            adsManager.GetComponent<Banner_Ads>().hideBanner();
        }
        if (theLev.levelMusic != null)
        {
            EazySoundManager.StopAllMusic();
            EazySoundManager.PlayMusic(theLev.levelMusic, themeVolume, true, false, 0.5f, 0.5f);
        }

        gameOverActive = false;
        control = true;

        objectiveTextMesh.text = "Objective: " + theLev.objective;

        //No hints in challenge mode.
        numHintsRemaining = 0;
        hintsLeftText.text = "N/A";
        
        //Time limit related stuff.
        timeLimit = theLev.challengeTimeLimit;
        timeLeft = timeLimit;
        TimeSpan timeLeftFormat = TimeSpan.FromSeconds((timeLeft+1));
        string timeLeftStr = "Time left: " + timeLeftFormat.ToString("mm':'ss");


        hintButton.enabled = false;
        pauseMenuUI.SetActive(true);
        isPaused = false;
        isZoomed = false;

        //Might be plus 1...? Since we need the NEXT scene name. 
        //IF YOU HAVE A PROBLEM GETTING TO THE NEXT SCENE DOUBLE CHECK THIS!!!!!
        nextSceneName = theLev.levelDifficulty + "_" + theLev.levelTheme + "_" + (theLev.levelNum + 1).ToString();

        //  Debug.Log(nextSceneName);

        //Spawn in the items for the level.

        //Between 1-3.
        rndSeed = Random.Range(1, 4);
        Debug.Log("Random seed is: " + rndSeed);

        GameObject tempAddToListObject;
        switch(rndSeed)
        {
                //Spawn in position 1.
            case 1:
                {
                    for (int i = 0; i < theLev.requiredItems.Count; i++)
                    {
                        if (theLev.requiredItems[i].zRot != 0 || theLev.requiredItems[i].yRot != 0)
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint1, Quaternion.Euler(0f, theLev.requiredItems[i].yRot, theLev.requiredItems[i].zRot));
                        }
                        else
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint1, Quaternion.identity);
                        }

                        //Add the object we just spawned in into the list of current objects that are in play.
                        currentObsInPlay.Add(tempAddToListObject);


                        tempAddToListObject.transform.SetParent(backgroundImage.transform, false);
                        tempAddToListObject.GetComponent<Draggable_Item>().initialPos = tempAddToListObject.transform.position;

                        if (theLev.requiredItems[i].isHidden)
                        {
                            //  Debug.Log(theLev.requiredItems[i].item.name + " is invisible!");
                            numInvis.Add(i);
                        }
                    }
                    break;
                }
                //Spawn in position 2.
            case 2:
                {
                    for (int i = 0; i < theLev.requiredItems.Count; i++)
                    {
                        if (theLev.requiredItems[i].zRot != 0 || theLev.requiredItems[i].yRot != 0)
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint2, Quaternion.Euler(0f, theLev.requiredItems[i].yRot, theLev.requiredItems[i].zRot));
                        }
                        else
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint2, Quaternion.identity);
                        }

                        //Add the object we just spawned in into the list of current objects that are in play.
                        currentObsInPlay.Add(tempAddToListObject);


                        tempAddToListObject.transform.SetParent(backgroundImage.transform, false);
                        tempAddToListObject.GetComponent<Draggable_Item>().initialPos = tempAddToListObject.transform.position;

                        if (theLev.requiredItems[i].isHidden)
                        {
                            //  Debug.Log(theLev.requiredItems[i].item.name + " is invisible!");
                            numInvis.Add(i);
                        }
                    }
                    break;
                }
                //Spawn in position 3.
            case 3:
                {
                    for (int i = 0; i < theLev.requiredItems.Count; i++)
                    {
                        if (theLev.requiredItems[i].zRot != 0 || theLev.requiredItems[i].yRot != 0)
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint3, Quaternion.Euler(0f, theLev.requiredItems[i].yRot, theLev.requiredItems[i].zRot));
                        }
                        else
                        {
                            tempAddToListObject = Instantiate(theLev.requiredItems[i].item, theLev.requiredItems[i].spawnPoint3, Quaternion.identity);
                        }

                        //Add the object we just spawned in into the list of current objects that are in play.
                        currentObsInPlay.Add(tempAddToListObject);


                        tempAddToListObject.transform.SetParent(backgroundImage.transform, false);
                        tempAddToListObject.GetComponent<Draggable_Item>().initialPos = tempAddToListObject.transform.position;

                        if (theLev.requiredItems[i].isHidden)
                        {
                            //  Debug.Log(theLev.requiredItems[i].item.name + " is invisible!");
                            numInvis.Add(i);
                        }
                    }
                    break;
                }
                //Something went wrong, might have to kick the player out of the level to avoid a crash.
            default:
                {
                    break;
                }
        }

        hintsLeftText.text = "N/A";
       
        
        //Initialize this variable here since we just finished populating the list.
        currentObListNum = 0;

        //numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);
        
        // Debug.Log("The player has: " + numHintsRemaining + " hints left!");

        /*
        for (int sk = 0; sk < currentObsInPlay.Count; sk++)
        {
            Debug.Log(currentObsInPlay[sk].name);
        }
        */

        //Calculate how many items are needed to complete the level.
        //The total number of combo items + the number of regular items that are not
        //materials for a combo item.
        int numMats = 0;
        for (int j = 0; j < theLev.comboItemsNeeded.Count; j++)
        {
            if (theLev.comboItemsNeeded[j].isAlsoMat)
            {
                numMats += 1;
            }
            for (int k = 0; k < theLev.requiredItems.Count; k++)
            {

                if ((theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat1 || theLev.requiredItems[k].item.gameObject.name == theLev.comboItemsNeeded[j].mat2))
                {
                    numMats += 1;
                }
            }

        }


        //Make all items that need to be invisible not show up.
        string tempName;
        GameObject tempObject;
        for (int k = 0; k < numInvis.Count; k++)
        {
            tempName = theLev.requiredItems[numInvis[k]].item.gameObject.name + "(Clone)";
            tempObject = GameObject.Find(tempName);
            tempObject.GetComponent<Draggable_Item>().initialPos = new Vector3(theLev.requiredItems[numInvis[k]].xPos, theLev.requiredItems[numInvis[k]].yPos, 0f);
            tempObject.transform.position = tempObject.GetComponent<Draggable_Item>().initialPos;
            invisObjects.Add(tempObject);
            invisObjects[k].SetActive(false);
        }


        // Debug.Log(numMats);
        itemsLeft = (theLev.comboItemsNeeded.Count + (theLev.requiredItems.Count - numMats));
        // numItemsLeftText.text = "Items left: " + itemsLeft;
        numItemsLeftTextMesh.text = "Items remaining: " + itemsLeft;

        //Fill in the bottom UI with items that are needed to complete the level by using Images.
        //These images will be transparent until the corresponding item is found, then they
        //will be filled in.
        bottomUIParent = GameObject.Find("Sprite_Holder").transform;
        zoomedUIParent = GameObject.Find("Sprite_Holder2").transform;

        //Bottom UI initialization to hide it. Will have to actually hide it instead of just making it transparent.
        shrinkButton.SetActive(false);
        GameObject child = ZoomedScrollArea.transform.GetChild(0).gameObject;
        Image theBG;
        theBG = child.GetComponent<Image>();
        Color tempZoomColor;
        tempZoomColor = theBG.color;
        tempZoomColor.a = 0.0f;
        theBG.color = tempZoomColor;


        GameObject tempIcon;
        Image tempImg;
        Color tempColor;

        if (theLev.icons.Count != 0)
        {
            for (int l = 0; l < theLev.icons.Count; l++)
            {
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.transform.SetParent(bottomUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                //tempColor.a = 0.65f;

                //Red color
                tempColor = new Color32(243, 40, 0, 125);
                tempImg.color = tempColor;


                //Update the zoomed in version as well.
                tempIcon = Instantiate(theLev.icons[l], transform.position, transform.rotation);
                tempIcon.name = tempIcon.name + "2";
                tempIcon.transform.SetParent(zoomedUIParent, false);
                tempImg = tempIcon.GetComponent<Image>();
                tempColor = tempImg.color;
                //tempColor.a = 0.65f;

                //Red color
                tempColor = new Color32(243, 40, 0, 125);
                tempImg.color = tempColor;

                // Debug.Log(theLev.icons[l] + " " + tempIcon.name);
            }
        }

        ScrollToTop();

    }

    public void currentTimerChallenge()
    {
        if(timeLeft < 0f && !gameOverActive)
        {
            //Call game over function.
            gameOverActive = true;
            timeLeftText.text = "Time Left: 00:00";
            Debug.Log("You lose!");
            gameOver();

        }
        else
        {
            if(timeLeft <= (timeLimit/4))
            {
                //Red
                Color32 newColor = new Color32(255, 0, 0, 255);
                timeLeftText.color = newColor;

            }
            timeLeft -= 1 * Time.deltaTime;
            //Account for extra second.
            currentTime = TimeSpan.FromSeconds((timeLeft+1));
            string timeLeftStr = "Time Left: " + currentTime.ToString("mm':'ss");
            timeLeftText.text = timeLeftStr;
        }
    }

    public void gameOver()
    {
        EazySoundManager.StopAllMusic();

        //Destroy the gameobjects so that we don't accidently clear afterwards.

        for (int j = 0; j < currentObsInPlay.Count; j++)
        {
            Destroy(currentObsInPlay[j]);
            currentObsInPlay.Remove(currentObsInPlay[j]);
            break;
        }

        isPaused = true;
        expandButton.SetActive(false);
        pauseButton.interactable = false;
        retryButton.interactable = false;
        hintButton.interactable = false;

        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<PanelAnimator>().StartAnimIn();

        
    }

    public void levelCompletedChallenge()
    {
        //levelComplete = true;

        if (theLev.levelMusic != null)
        {
            // EazySoundManager.StopAllMusic();
            EazySoundManager.PlayMusic(levelCompleteIntro, 0.5f, false, false, 0.0f, 0.0f);
            StartCoroutine(waitforIntro());
        }
        float finishTime;

        //might want to floor this..? Not sure yet.
        finishTime = timeLeft;

        //Just using this to make it so background objects can't be moved.
        isPaused = true;

        //Don't do these 2 lines of code, fix this later!
        GameObject botScrollZoom = GameObject.Find("Bottom_Scroll_Zoomed");
        botScrollZoom.SetActive(false);

        expandButton.SetActive(false);

        pauseButton.interactable = false;
        retryButton.interactable = false;
        hintButton.interactable = false;
        levelComplete = true;

        challengeCompleteHolder.SetActive(true);
        challengeCompleteHolder.GetComponent<PanelAnimator>().StartAnimIn();
        GameObject BGChild = levelCompleteHolder.transform.GetChild(0).gameObject;

        currentTime = TimeSpan.FromSeconds((timeLeft+1));
        string timeLeftStr = "Time Left: " + currentTime.ToString("mm':'ss");

        challengeTimeLeftText.text = timeLeftStr;

        /*
         *****************************************
         *****************************************
         *****************************************
         PLAYERPREF RELATED STUFF FOR SAVING GAME DATA.
         */

        string bestFinishTimeKey = theLev.name + "_Best_Finish_Time_Left";
        float bestFinishTime;
        bestFinishTime = PlayerPrefs.GetFloat(bestFinishTimeKey);
      

        //You got a better record. Set it!
        if(timeLeft > bestFinishTime)
        {
            PlayerPrefs.SetFloat(bestFinishTimeKey, timeLeft);
            //Debug.Log("You got a better time! the time left was: " + timeLeft + " and your other record was: " + bestFinishTime);
        }

        string completedChallengeAlreadyKey = theLev.name + "_Completed";
        int isItComplete;
        isItComplete = PlayerPrefs.GetInt(completedChallengeAlreadyKey);

        //We completed the level for the first time, check it off. Use this key later for checking if it's been completed.
        if(isItComplete == 0)
        {
           
            PlayerPrefs.SetInt(completedChallengeAlreadyKey, 1);
            isItComplete = PlayerPrefs.GetInt(completedChallengeAlreadyKey);
           // Debug.Log("First time completing the level! Complete is: " + isItComplete);
        }
        else
        {
            Debug.Log("Hey you already beat this level!");
        }
        


        /*
         *****************************************
         *****************************************
         *****************************************
         PLAYERPREF RELATED STUFF FOR SAVING GAME DATA.
         */


    }



    /*
 * CHALLENGE MODE RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 ***********************************************************************
 *********************************************************************** 
 ************************************************************************
 ************************************************************************
 * CHALLENGE MODE RELATED FUNCTIONS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 */
}
