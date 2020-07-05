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

    //If level has been cleared, show the player their best time on the UI.
    public float bestTime;

    //Times for getting certain amount of stars on these levels.
    public float timeForThreeStars;
    public float timeForTwoStars;

    //Objective for the level.
    public string objective;

    public List<Required_Item> requiredItems = new List<Required_Item>();

    //Tells the game which combo items will be needed.
    public List<Combo_Item> comboItemsNeeded = new List<Combo_Item>();


    //List of images that will be instantiated into the bottom UI as icons. (Probably a better way to do this...may refactor)
    //This list needs to be the size of all items that are required for LEVEL COMPLETION.
    public List<GameObject> icons = new List<GameObject>();

    //How long to wait for an animation to play before the item appears. In stages with multiple animations maybe we want to turn this into a list? Idk yet.
    public  float aniSecondsToWait;
}
