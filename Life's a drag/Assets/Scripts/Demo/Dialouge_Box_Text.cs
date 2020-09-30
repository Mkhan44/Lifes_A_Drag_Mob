using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialouge_Box_Text : MonoBehaviour
{
    public TextMeshProUGUI theText;
    public List<string> phrases = new List<string>();
    public TextMeshProUGUI buttonText;
    int currentTextNum;
    int numPhrases;
    public GameObject thePanel;
    string numHintsKey = "remainingHints";
    int numHintsLeft;
    string demoMessageDispKey = "demoMessage";

    // Start is called before the first frame update
    void Start()
    {
        currentTextNum = 0;
        numHintsLeft = PlayerPrefs.GetInt(numHintsKey);
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

        switch(currentTextNum)
        {
            case 1:
                {
                    if(numHintsLeft > 0)
                    {
                        PlayerPrefs.SetInt(numHintsKey, 999);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(numHintsKey, numHintsLeft+999);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void closePanel()
    {
        PlayerPrefs.SetInt(demoMessageDispKey, 1);
        thePanel.SetActive(false);
    }
}
