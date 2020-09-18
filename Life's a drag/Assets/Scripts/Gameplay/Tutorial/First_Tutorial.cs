using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
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
    public Image comboImg1;
    public Image comboImg2;

    public GameObject box;
    void Start()  
    {
        comboImg1.enabled = false;
        comboImg2.enabled = false;

        numInstructions = (phrases.Count - 1);
        Debug.Log(numInstructions);
        currentInstruction = 0;
        tutorialText.text = phrases[currentInstruction];
        conditionMet = true;
        buttonText = tapButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

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

     if(currentInstruction == 10)
     {
         comboImg1.enabled = true;
         comboImg2.enabled = true;
     }

     if(currentInstruction == 14)
     {
         comboImg1.enabled = false;
         comboImg2.enabled = false;
     }

     if(currentInstruction == 13)
     {
         levManager.GetComponent<Current_level_manager>().expandButton.SetActive(true);
         levManager.GetComponent<Current_level_manager>().expandButton.GetComponent<Button>().interactable = false;
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
