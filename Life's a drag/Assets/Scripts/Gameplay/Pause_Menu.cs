using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject currentLevelMan;
    public GameObject warningRef;
    string levelsTillAdKey = "levelsTillAdPlays";
    int levelsTillAdNum;
    public void Resume()
    {
        Time.timeScale = 1f;
        //Play sound effect here.
        currentLevelMan.GetComponent<Current_level_manager>().isPaused = false;
        currentLevelMan.GetComponent<Current_level_manager>().retryButton.interactable = true;
        currentLevelMan.GetComponent<Current_level_manager>().pauseButton.interactable = true;
       // pauseMenuUI.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("Options menu opened.");

        //Set options stuff true (should have a back button to get back to main pause menu)
    }

    public void MainMenu()
    {
        warningRef.SetActive(true);
        Debug.Log("Return to main menu.");

        //Returns the user to the main menu.
        //Give user a pop-up to tell them that if they leave they will not have their progress saved.
    }

    public void mainMenuYes()
    {
  /*
        if(currentLevelMan.GetComponent<Current_level_manager>().adsManager != null)
        {
            levelsTillAdNum = PlayerPrefs.GetInt(levelsTillAdKey);
            PlayerPrefs.SetInt(levelsTillAdKey, (levelsTillAdNum + 1));
        }
   */
        Time.timeScale = 1f;
        currentLevelMan.GetComponent<Load_Level>().LoadLevel("Main_Menu");
       // SceneManager.LoadScene("Main_Menu");
    }



}
