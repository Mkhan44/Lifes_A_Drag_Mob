using System.Collections;
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
            case "YoutTube":
                {
                    Application.OpenURL("https://www.facebook.com/Bukugames");
                    break;
                }
            default:
                {
                    Debug.LogWarning("Invalid string!");
                    break;
                }

        }
    }
}
