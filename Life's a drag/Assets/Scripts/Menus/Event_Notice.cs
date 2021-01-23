//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_Notice : MonoBehaviour
{
    string firstTimeKey = "firstTimeKey";

    public GameObject currentEvent;
    string eventPopupKey = "eventPopupKey";
    int eventNum;


    string numHintsKey = "remainingHints";
    int numHintsRemaining;

    bool firstTimeSeeingMenu;

    // Start is called before the first frame update
    void Awake()
    {
        firstTimeSeeingMenu = false;
        Debug.Log("CALLING AWAKE FOR EVENT");
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        Debug.Log("firstTimeSeeingMenu is: " + firstTimeSeeingMenu);
        if(!firstTimeSeeingMenu)
        {
          
            if (PlayerPrefs.GetInt(firstTimeKey) == 0)
            {
                //Let the first tutorial message play.

            }
            else
            {
                //Display event message...Do event things. Only if player has not already seen it.
                eventNum = PlayerPrefs.GetInt(eventPopupKey);
                if (eventNum == 0)
                {
                    /*

                    //Display it cause we didn't see it yet.
                    currentEvent.SetActive(true);
                    numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);

                    //CURRENT EVENT IS TO GIVE 20 HINTS...CHANGE THIS FOR FUTURE EVENTS!!!!
                    PlayerPrefs.SetInt(numHintsKey, (numHintsRemaining + 20));
                    PlayerPrefs.SetInt(eventPopupKey, 1);

                    */
                }
                else
                {
                    //You already have this. Don't do anything.
                }
            }
            
        }
        
        firstTimeSeeingMenu = true;
        Debug.Log("firstTimeSeeingMenu is: " + firstTimeSeeingMenu);
        
       
     
    }
}