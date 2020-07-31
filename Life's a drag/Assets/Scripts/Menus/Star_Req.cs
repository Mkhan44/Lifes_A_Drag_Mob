using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_Req : MonoBehaviour
{
    public string theme;
    public bool hasRequirement;
    public int requiredStars;


    public string getTheme()
    {
        return theme;
    }
    public int getStarReq()
    {
        if (!hasRequirement)
        {
            requiredStars = 0;
        }
        return requiredStars;
    }
}
