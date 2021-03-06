﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Links : MonoBehaviour
{
    public string thePlatform;
    public TextMeshProUGUI noticeText;

    public void setPlat(string platform)
    {
        thePlatform = platform;
        noticeText.text = "Would you like to visit our " + platform + " page?";
    }

    public void loadPage()
    {
        switch(thePlatform)
        {
            case "Facebook":
                {
                    Application.OpenURL("https://www.facebook.com/Bukugames");
                    break;
                }
            case "Twitter":
                {
                    Application.OpenURL("https://twitter.com/buku_games");
                    break;
                }
            case "Instagram":
                {
                    Application.OpenURL("https://www.instagram.com/bukugames/");
                    break;
                }
            case "YouTube":
                {
                    Application.OpenURL("https://www.youtube.com/channel/UCVMp9BDVF6Bh7R8jG2R9Wqg?");
                    break;
                }
            case "Merch Store":
                {
                    Application.OpenURL("https://merch.streamelements.com/bukugames");
                    break;
                }
            default:
                {
                    Debug.LogWarning("Invalid string!");
                    break;
                }

        }
    }

    public void loadLicenses()
    {
        Application.OpenURL("https://github.com/Mkhan44/Lifes_A_Drag_Mob/blob/master/Licenses_LAD.txt");
    }
}
