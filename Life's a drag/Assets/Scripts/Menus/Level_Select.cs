//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level_Select : MonoBehaviour
{

    public string levelSceneName;

    public void setName(string levelName)
    {
        levelSceneName = levelName;
    }

    public string getName()
    {
        return levelSceneName;
    }
    public void loadLevel()
    {
        string levelName;

        levelName = getName();
        SceneManager.LoadScene(levelName);
    }


}
