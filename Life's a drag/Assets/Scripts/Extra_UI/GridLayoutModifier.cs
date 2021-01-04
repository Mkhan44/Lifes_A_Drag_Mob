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

    public ScrollRect levelSelector;

    //make ios boolean...if true ignore other layout changes.

    public enum sceneType
    {
        menu,
        level
    }

    public sceneType theScene;
    
    void Start()
    {
        adjustScroll();

       
    }

    private void OnEnable()
    {
       
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

        setWidth();
        // screenWidth = 1440f;
        //Check if we're on IOS. If this returns true then we are. If not then we aren't.

        if (!safeZoneCheck.getSystemType())
        {
          
          
           // Debug.LogWarning("The screen width is: " + screenWidth);

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

        //We're on IOS.
        else
        {
            //Iphone 7 , Iphone 6, Iphone 8
            if (screenWidth == 750f)
            {
                Debug.Log("ScreenWidth is 750!");
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 5;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();
                            thisLayout.padding.top = 50;
                            thisLayout.padding.bottom = 25;
                            thisLayout.padding.right = 40;

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(-20, thisLayout.spacing.y);

                            thisLayout.spacing = newSpacing;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 4;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(0, thisLayout.spacing.y);
                            thisLayout.spacing = newSpacing;
                            break;
                        }
                }

            }

            //Iphone X.
            else if (screenWidth == 1125f)
            {
                Debug.Log("ScreenWidth is 1125!");
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 5;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();
                            thisLayout.padding.top = 50;
                            thisLayout.padding.bottom = 25;
                            thisLayout.padding.right = 0;

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(40, thisLayout.spacing.y);

                            thisLayout.spacing = newSpacing;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 4;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(0, thisLayout.spacing.y);
                            thisLayout.spacing = newSpacing;
                            break;
                        }
                }
            }
            //Iphone 11.
            else if (screenWidth == 828f)
            {
                {
                    Debug.Log("ScreenWidth is 828!");
                    switch (theScene)
                    {
                        case sceneType.menu:
                            {
                                numColumns = 5;
                                GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();
                                thisLayout.padding.top = 50;
                                thisLayout.padding.bottom = 25;
                                thisLayout.padding.right = 0;

                                Vector2 newSpacing = thisLayout.spacing;
                                newSpacing = new Vector2(0, thisLayout.spacing.y);

                                thisLayout.spacing = newSpacing;
                                break;
                            }
                        case sceneType.level:
                            {
                                numColumns = 4;
                                GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();

                                Vector2 newSpacing = thisLayout.spacing;
                                newSpacing = new Vector2(0, thisLayout.spacing.y);
                                thisLayout.spacing = newSpacing;
                                break;
                            }
                    }
                }
            }

            //XR
            else
            {
                switch (theScene)
                {
                    case sceneType.menu:
                        {
                            numColumns = 5;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();
                            thisLayout.padding.top = 50;
                            thisLayout.padding.bottom = 25;

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(-5, thisLayout.spacing.y);

                            thisLayout.spacing = newSpacing;
                            break;
                        }
                    case sceneType.level:
                        {
                            numColumns = 5;
                            GridLayoutGroup thisLayout = gameObject.GetComponent<GridLayoutGroup>();

                            Vector2 newSpacing = thisLayout.spacing;
                            newSpacing = new Vector2(-15, thisLayout.spacing.y);
                            thisLayout.spacing = newSpacing;
                            break;
                        }
                }
            }

              
        }

        scrollArea.constraintCount = numColumns;


    }


    public void setWidth()
    {
        screenWidth = (float)Screen.currentResolution.width;
      //  Debug.LogWarning(screenWidth);
    }

    public void ScrollToTop()
    {

        if (levelSelector != null)
        {
            levelSelector.normalizedPosition = new Vector2(0, 0.75f);
        }
        
        //ZoomedScrollArea.normalizedPosition = new Vector2(0, 1);
    }

    //This function needs to be called from Populate_level_buttons or from Current_level_manager.
    public void scaleIcons(List<GameObject> itemsToScale)
    {
       

            //Loop through each and scale them accordingly.
            for (int i = 0; i<itemsToScale.Count; i++)
        {
            //Scale it down to fit.


            //Iphone 7 , Iphone 6, Iphone 8
            if (screenWidth == 750f)
            {
                itemsToScale[i].GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1.0f);
            }
            //Iphone X.
            else if (screenWidth == 1125f)
            {
                itemsToScale[i].GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            //Iphone 11.
            else if (screenWidth == 828f)
            {
                itemsToScale[i].GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            //Iphone XR.
            else
            {
                itemsToScale[i].GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1.0f);
            }
           
        }
    }

}
