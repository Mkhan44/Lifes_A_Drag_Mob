using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Populate_Level_Buttons : MonoBehaviour
{
    //Populate this list with the amount of buttons that we will be using
    //This way we can manually control the number of levels in a theme at any given point.
    public List<GameObject> numButtons = new List<GameObject>();
    public GameObject levelSelectManager;

    public void Start()
    {
   
    }

    void OnEnable()
    {
        StartCoroutine(waitTime());
    }
    
    //Cycle through each button in the list, find it's child star picture object, and then populate it with the amount of stars
    //which the player has obtained for that level.

    public void displayStars()
    {
        GameObject starChild0;
        GameObject starChild1;
        GameObject starChild2;
        GameObject starChild3;
        string diffTheme;
        string fullLevName;
        string starsKey;
        int starsObtained;

        //This string is now in the format of "difficulty_theme_"
        diffTheme = levelSelectManager.GetComponent<Level_Select_Manager>().getDiffAndTheme();

        for(int i = 0; i < numButtons.Count; i++)
        {
            //This should be equivilant to the star image.
            starChild0 = numButtons[i].transform.GetChild(0).gameObject;
            starChild1 = numButtons[i].transform.GetChild(1).gameObject;
            starChild2 = numButtons[i].transform.GetChild(2).gameObject;
            starChild3 = numButtons[i].transform.GetChild(3).gameObject;

            //Get the name of the level, we'll use this + star key to get the amt of stars.
            fullLevName = diffTheme + (i + 1).ToString();

           // Debug.Log("The level name is: " + fullLevName);
            starsKey = fullLevName + "_Best_Stars";

            starsObtained = PlayerPrefs.GetInt(starsKey);

           // Debug.Log("The starsKey is: " + starsKey);
            //Debug.Log("The stars obtained for stage " + (i + 1) + " are: " + starsObtained);

            //Swap the image.
            switch(starsObtained)
            {
                case 0:
                    {
                        starChild0.SetActive(true);
                        break;
                    }
                case 1:
                    {
                        starChild1.SetActive(true);
                        break;
                    }
                case 2:
                    {
                        starChild2.SetActive(true);
                        break;
                    }
                case 3:
                    {
                        starChild3.SetActive(true);
                        break;
                    }
            }

        }

    }

    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(0.2f);
        displayStars();
    }
    
}
