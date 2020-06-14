﻿using System.Collections;
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
}