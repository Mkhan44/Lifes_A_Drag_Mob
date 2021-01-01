using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script adjusts the columns in the grid layout group according to screen resolution.
public class GridLayoutModifier : MonoBehaviour
{
    float screenWidth;
    public GridLayoutGroup scrollArea;
    int numColumns;

    [SerializeField]
    IOS_Safezone safeZoneCheck;

    //make ios boolean...if true ignore other layout changes.

    public enum sceneType
    {
        menu,
        level
    }

    public sceneType theScene;
    
    /*
    void OnEnable()
    {
        adjustScroll();
    }
    */
    void Start()
    {
        adjustScroll();
    }

    //COMMENT OUT THIS FUNCTION AFTER TESTING IS OVER WITH.

    /*
    void FixedUpdate()
    {
        adjustScroll();
    }
    */


    //Adjusts the constraint based on the screen resolution.
    void adjustScroll()
    {

        //Check if we're on IOS. If this returns true then we are. If not then we aren't.

        if(!safeZoneCheck.getSystemType())
        {
            setWidth();

            // screenWidth = 1440f;
            Debug.Log("The screen width is: " + screenWidth);

            //720P resolution.
            if (screenWidth == 720f)
            {
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 3;
                            gameObject.GetComponent<GridLayoutGroup>().padding.top = 130;
                            gameObject.GetComponent<GridLayoutGroup>().padding.bottom = 25;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 3;
                            break;
                        }
                }

            }
            //1440P resolution.
            else if (screenWidth == 1440f)
            {
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 5;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 5;
                            break;
                        }
                }
            }
            //Default.
            else
            {
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 5;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 4;
                            break;
                        }
                }
            }

            scrollArea.constraintCount = numColumns;

            //Decrease the area apart of each item so they don't get cut off.
            //ONLY DO THIS IN LEVELS, NOT IN THE MENU BECAUSE IT WILL BE TOO SCRUNCHED UP!

            if (theScene == sceneType.level)
            {
                if (numColumns >= 5)
                {
                    Vector2 tempSpacing = scrollArea.spacing;

                    float xSpace = 15f;

                    scrollArea.spacing = new Vector2(xSpace, tempSpacing.y);
                }
            }
        }
        else
        {
            switch (theScene)
            {
                case sceneType.menu:
                    {
                        numColumns = 3;
                        gameObject.GetComponent<GridLayoutGroup>().padding.top = 125;
                        gameObject.GetComponent<GridLayoutGroup>().padding.bottom = 25;
                        break;
                    }
                case sceneType.level:
                    {
                        numColumns = 3;
                        break;
                    }
            }
        }
       
       

    }


    public void setWidth()
    {
        screenWidth = (float)Screen.currentResolution.width;
    }

}
