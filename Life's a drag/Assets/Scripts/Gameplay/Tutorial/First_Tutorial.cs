using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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


    void Start()  
    {
        numInstructions = phrases.Count;
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
         if(requirementChecks[currentInstruction])
         {
             Debug.Log("Player needs to complete requirement to progress.");
             tutorialText.text = phrases[currentInstruction];
             conditionMet = false;

             //Let the user interact with the game world.
             levManager.GetComponent<Current_level_manager>().control = true;
         }
         else
         {
             if (currentInstruction > (numInstructions - 1))
             {
                 //Tutorial is done.
                 Debug.Log("We went through all the tutorial messages.");
                 // currentInstruction = 0;
                 // tutorialText.text = phrases[currentInstruction];
                 conditionMet = false;
             }
             else
             {
                 tutorialText.text = phrases[currentInstruction];
             }
         }
       
    }

    public void requirementCompleted()
    {
        conditionMet = true;
    }

}
