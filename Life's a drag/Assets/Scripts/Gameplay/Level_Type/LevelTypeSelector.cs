using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTypeSelector : MonoBehaviour
{

    public static LevelTypeSelector instance;
    string levelType;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
  
    public void setLevelType(string typePassed)
    {
        levelType = typePassed;
        Debug.Log("Level type is: " + levelType);
    }
    public string getLevelType()
    {
        return levelType;
    }

}
