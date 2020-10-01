using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Hellmade.Sound;   
public class First_Tutorial : MonoBehaviour
{
    //Reference to the level manager.
    public GameObject levManager;

    //This will determine how many taps it takes to get through all the text.
    int numInstructions;
    int currentInstruction;

    //Boolean to check if the current condition has been met.
    bool conditionMet;

    //Text that will change as we progress the tutorial.
    public TextMeshProUGUI tutorialText;
    public Button tapButton;
    TextMeshProUGUI buttonText;

    //number of phrases that will be displayed in the textbox.
    //Makes it so we can dynamically change stuff.
    public List<string> phrases = new List<string>();

    //List of bools to know which instructions require something to be done before player can advance.
    //This should be the same length as that of phrases.
    public List<bool> requirementChecks = new List<bool>();

    //Images for the tutorial.
    public Image comboBox1Cursor;
    public Image comboBox2Cursor;
    public Image requiredBoxCursor;
    public Image expandButonCursor;
    public Image timerCursor;
    public Image hintCursor;

    public GameObject box;

    string numHintsKey = "remainingHints";
    int numHintsRemaining;

    string firstTimeTutKey = "firstTImeTutKey";
    int firstTimeTutVal;

    void Start()  
    {
        comboBox1Cursor.enabled = false;
        comboBox2Cursor.enabled = false;
        requiredBoxCursor.enabled = false;
        expandButonCursor.enabled = false;
        timerCursor.enabled = false;
        hintCursor.enabled = false;

        numInstructions = (phrases.Count - 1);
        Debug.Log(numInstructions);
        currentInstruction = 0;
        tutorialText.text = phrases[currentInstruction];
        conditionMet = true;
        buttonText = tapButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        levManager.GetComponent<Current_level_manager>().hintButton.interactable = false;
        numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);
        firstTimeTutVal = PlayerPrefs.GetInt(firstTimeTutKey);
    }

    void Update()
    {
        if(!conditionMet)
        {
            tapButton.interactable = false;
            buttonText.enabled = false;
        }
    }

    //User tapped the button. Advance to the next message.
 public void tapMessage()
    {
       
         currentInstruction += 1;
         Debug.Log(currentInstruction);
         if (currentInstruction > numInstructions)
         {
             //Tutorial is done.
             Debug.Log("We went through all the tutorial messages.");
             // currentInstruction = 0;
             // tutorialText.text = phrases[currentInstruction];
             conditionMet = false;
             EazySoundManager.StopAllMusic();
             if (levManager.GetComponent<Current_level_manager>().noAdsNum == 0 && levManager.GetComponent<Current_level_manager>().adsManager != null)
             {
                 levManager.GetComponent<Current_level_manager>().adsManager.GetComponent<Banner_Ads>().showBanner();
             }
             levManager.GetComponent<Load_Level>().LoadLevel("Main_Menu");
            // SceneManager.LoadScene("Main_Menu");
         }
         else
         {
             if (requirementChecks[currentInstruction])
             {
                 Debug.Log("Player needs to complete requirement to progress.");
                 tutorialText.text = phrases[currentInstruction];
                 conditionMet = false;

                 //Let the user interact with the game world.
                 levManager.GetComponent<Current_level_manager>().control = true;
             }
             else
             {
                 tutorialText.text = phrases[currentInstruction];
             }
         }

     //Make these into a switch or something...This is super messy.
     if (currentInstruction == 3)
     {
         requiredBoxCursor.enabled = true;
     }
     if (currentInstruction == 5)
     {
         requiredBoxCursor.enabled = false;
     }

     if(currentInstruction == 10)
     {
         comboBox1Cursor.enabled = true;
         comboBox2Cursor.enabled = true;
     }

     if (currentInstruction == 13)
     {
         expandButonCursor.enabled = true;
         levManager.GetComponent<Current_level_manager>().expandButton.SetActive(true);
         levManager.GetComponent<Current_level_manager>().expandButton.GetComponent<Button>().interactable = false;
     }

     if(currentInstruction == 14)
     {
         expandButonCursor.enabled = false;
         
     }

     if(currentInstruction == 15)
     {
        levManager.GetComponent<Current_level_manager>().expandButton.GetComponent<Button>().interactable = false;
        comboBox1Cursor.enabled = false;
        comboBox2Cursor.enabled = false;
     
     }

     if (currentInstruction == 16)
     {
         timerCursor.enabled = true;

     }

     if(currentInstruction == 18)
     {
         timerCursor.enabled = false;
         hintCursor.enabled = true;
     }

     if(currentInstruction == 20)
     {
         if (firstTimeTutVal > 0)
         {
             currentInstruction = 21;
         }
     }
     if (currentInstruction == 21)
     {
         if (firstTimeTutVal == 0)
         {
             if(PlayerPrefs.GetInt(numHintsKey) == 999)
             {
                 //Do nothing.
             }
             else
             {
                 PlayerPrefs.SetInt(numHintsKey, (numHintsRemaining + 5));
             }
             
             PlayerPrefs.SetInt(firstTimeTutKey, 1);
         }
         hintCursor.enabled = false;
     }

     //This should only be called if it's your first time viewing the tutorial.
     if(currentInstruction == 22)
     {
         hintCursor.enabled = false;
     }


         spawnItems();
       
    }

    public void requirementCompleted()
    {
        conditionMet = true;
        buttonText.enabled = true;
        tapButton.interactable = true;
        tapMessage();
    }

    public void spawnItems()
    {
        switch(currentInstruction)
        {
                //Spawn in the invisible stuff.
            case 7:
                {
                    GameObject hiddenOb;
                    hiddenOb = levManager.GetComponent<Current_level_manager>().spawnItem(1);
                    box.SetActive(true);


                    hiddenOb.SetActive(false);
                    break;
                }
            case 14:
                {
                    //Spawn combo items.
                    levManager.GetComponent<Current_level_manager>().spawnItem(2, 3);


                    break;
                }
            default:
                {
                    break;
                }
                
        }

     
    }

}
