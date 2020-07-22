using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject currentLevelMan;
    public void Resume()
    {
        Time.timeScale = 1f;
        //Play sound effect here.
        currentLevelMan.GetComponent<Current_level_manager>().isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("Options menu opened.");

        //Set options stuff true (should have a back button to get back to main pause menu)
    }

    public void MainMenu()
    {
        Debug.Log("Return to main menu.");

        //Returns the user to the main menu.
        //Give user a pop-up to tell them that if they leave they will not have their progress saved.
    }

    public void mainMenuYes()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu");
    }



}
