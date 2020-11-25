//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class First_Time_Tutorial : MonoBehaviour
{
    string firstTimeKey = "firstTimeKey";
    public GameObject firstTimeTut;

    string numHintsKey = "remainingHints";
    int numHintsRemaining;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        if (PlayerPrefs.GetInt(firstTimeKey) == 0)
        {
            Debug.Log("Hey, first time playing!");
            firstTimeTut.SetActive(true);
            PlayerPrefs.SetInt(firstTimeKey, 1);

            numHintsRemaining = PlayerPrefs.GetInt(numHintsKey);
            PlayerPrefs.SetInt(numHintsKey, (numHintsRemaining + 10));

        }
        else
        {
            Debug.Log("Hey, NOT first time playing!");
        }
    }
}
