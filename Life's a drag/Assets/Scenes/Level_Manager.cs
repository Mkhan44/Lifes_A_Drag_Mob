using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level_Manager : ScriptableObject
{
    //These 4 are what will be used to identify which level we're currently on.
    public string levelName;
    public string levelTheme;
    public string levelDifficulty;
    public int levelNum;

    //Tells the level how many regular items will be needed to spawn in.
    public int nonComboItemsToSpawn;

    //Number of combo items needed to complete the level.
    public int numComboItems;

    //If level has been cleared, show the player their best time on the UI.
    public float bestTime;

    //Times for getting certain amount of stars on these levels.
    public float timeForThreeStars;
    public float timeForTwoStars;

    //Objective for the level.
    public string objective;

}
