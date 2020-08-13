using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable_Item : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector3 initialPos;
    public Vector3 initialScale;
    public Vector3 shrinkScale;
    GameObject levelMan;

    enum typeOfObject
    {
        requiredItem,
        backgroundItem
    }
    
    enum DragStatus
    {
        canDrag,
        cantDrag
    }


    DragStatus canWeDrag;
    typeOfObject thisItemIs;

    void Awake()
    {
        //May want to change this later considering this is hardcoded essentially.
        levelMan = GameObject.Find("LevelManager");

        initialPos = gameObject.transform.position;
        if (initialPos == new Vector3(2.4f, -3.5f, 0f))
        {
            initialPos = new Vector3(0.0f, 0.0f, 0.0f);
        }
        initialScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (gameObject.tag == "RequiredItem")
            thisItemIs = typeOfObject.requiredItem;
        else
            thisItemIs = typeOfObject.backgroundItem;

        shrinkScale = new Vector3(0.5f, 0.5f, 1f);
    }
    void Update()
    {
        //May move this later.
        checkDragStatus();
    }
    void OnMouseDown()
    {
        //if(!levelMan.GetComponent<Current_level_manager>().isPaused)
        if (canWeDrag == DragStatus.canDrag)
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            //Literally using the same code atm, may have to switch this up.
            switch (thisItemIs)
            {
                case typeOfObject.requiredItem:
                    {

                        if (transform.localScale.x >= 1.0f && transform.localScale.y >= 1.0f)
                        {
                            shrinkScale = new Vector3((initialScale.x - 0.5f), (initialScale.y - 0.5f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        //Figure out scales here so we can see how much to shrink it.
                        else if (transform.localScale.x >= 0.5f && transform.localScale.y >= 0.5f)
                        {
                            //shrinkScale = new Vector3((initialScale.x - 0.3f), (initialScale.y - 0.3f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        else if (transform.localScale.x < 0.5f && transform.localScale.x >= 0.2f && transform.localScale.y < 0.5f && transform.localScale.y >= 0.2f)
                        {
                          //  Debug.Log("We're in else if");
                            //Barely shrink cause it's already small.
                            shrinkScale = new Vector3((initialScale.x - 0.05f), (initialScale.y - 0.05f), 1f);
                            transform.localScale = shrinkScale;
                        }

                        else
                        {
                            Debug.Log("We're in else");
                            //Don't shrink.
                            shrinkScale = initialScale;
                            transform.localScale = shrinkScale;

                            //shrinkScale = new Vector3((initialScale.x - 0.2f), (initialScale.y - 0.2f), 1f);
                            //transform.localScale = shrinkScale;
                        }

                        /*
                        if (transform.localScale.x >= 0.8f && transform.localScale.y >= 0.8f)
                        {
                            shrinkScale = new Vector3((initialScale.x - 0.3f), (initialScale.y - 0.3f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        /*
                    else if (transform.localScale.x <= 0.3f && transform.localScale.y <= 0.3f)
                    {
                        //Barely shrink cause it's already small.
                        new Vector3((initialScale.x - 0.1f), (initialScale.y - 0.1f), 1f);
                        shrinkScale = initialScale;
                    }
                         
                        else
                        {
                            //Only shrink a little bit.
                            shrinkScale = new Vector3((initialScale.x - 0.1f), (initialScale.y - 0.1f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        */
                        
                        break;
                    }
                case typeOfObject.backgroundItem:
                    {
                        if (transform.localScale.x >= 1.0f && transform.localScale.y >= 1.0f)
                        {
                            shrinkScale = new Vector3((initialScale.x - 0.5f), (initialScale.y - 0.5f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        //Figure out scales here so we can see how much to shrink it.
                        else if (transform.localScale.x >= 0.5f && transform.localScale.y >= 0.5f)
                        {
                            shrinkScale = new Vector3((initialScale.x - 0.3f), (initialScale.y - 0.3f), 1f);
                            transform.localScale = shrinkScale;
                        }
                        else if (transform.localScale.x < 0.5f && transform.localScale.x >= 0.2f && transform.localScale.y < 0.5f && transform.localScale.y >= 0.2f)
                        {
                            //Debug.Log("We're in else if");
                            //Barely shrink cause it's already small.
                            shrinkScale = new Vector3((initialScale.x - 0.05f), (initialScale.y - 0.05f), 1f);
                            transform.localScale = shrinkScale;
                        }
                         
                        else
                        {
                           // Debug.Log("We're in else");
                            //Don't shrink.
                            shrinkScale = initialScale;
                            transform.localScale = shrinkScale;

                            //shrinkScale = new Vector3((initialScale.x - 0.2f), (initialScale.y - 0.2f), 1f);
                            //transform.localScale = shrinkScale;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
       
     
        
    }

    void OnMouseDrag()
    {
        if (canWeDrag == DragStatus.canDrag)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            curPosition.y = Mathf.Clamp(curPosition.y, -3.75f, 3.4f);
            curPosition.x = Mathf.Clamp(curPosition.x, -2.55f, 2.55f);
            transform.position = curPosition;
        }
        
    }

    void OnMouseUp()
    {
        if (canWeDrag == DragStatus.canDrag)
        {
            if (gameObject.transform.position.y > -2.85f)
            {
                transform.localScale = initialScale;
            }

            switch (thisItemIs)
            {
                case typeOfObject.requiredItem:
                    {
                        break;
                    }
                case typeOfObject.backgroundItem:
                    {
                        if (transform.position.y > -2.95f)
                        {
                            transform.position = initialPos;
                        }
                        break;
                    }
                default:
                    {
                        //  transform.position = initialPos;
                        break;
                    }

            }
        }
       
    }

    void checkDragStatus()
    {
        if (levelMan.GetComponent<Current_level_manager>().isPaused || levelMan.GetComponent<Current_level_manager>().isZoomed || !levelMan.GetComponent<Current_level_manager>().control)
        {
            canWeDrag = DragStatus.cantDrag;
          //  Debug.Log("We can't drag!");
        }
        else
        {
            canWeDrag = DragStatus.canDrag;
            //Debug.Log("We can drag!");
        }

        
    }
}
