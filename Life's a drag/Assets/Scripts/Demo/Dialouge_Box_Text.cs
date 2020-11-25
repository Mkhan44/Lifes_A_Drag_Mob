//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialouge_Box_Text : MonoBehaviour
{
    public TextMeshProUGUI theText;

    [TextArea(3,10)]
    public List<string> phrases = new List<string>();
    public TextMeshProUGUI buttonText;
    int currentTextNum;
    int numPhrases;
    public GameObject thePanel;
    string numHintsKey = "remainingHints";
    int numHintsLeft;
   

    // Start is called before the first frame update
    void Start()
    {
        currentTextNum = 0;
      
        numPhrases = (phrases.Count - 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextMessage()
    {
        //Increment to next message.
        currentTextNum += 1;

        if (currentTextNum > numPhrases)
        {
            closePanel();
            return;
        }

        if (currentTextNum == numPhrases)
        {
            buttonText.text = "Close";
            
        }

       
        theText.text = phrases[currentTextNum];

       
    }

    public void closePanel()
    {
        thePanel.SetActive(false);
    }

    public void resetText()
    {
        theText.text = phrases[0];
        buttonText.text = "Next";
        currentTextNum = 0;
    }
}
