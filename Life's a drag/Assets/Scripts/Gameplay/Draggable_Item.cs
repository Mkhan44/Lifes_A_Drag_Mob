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
    float lerpSpeed = 1.0f;
    public bool shouldWeLerp;
    public bool inBox;
    public bool mouseOnMe; 
    GameObject levelMan;
    GameObject hintArrow;

    int sortingLayer;
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
        sortingLayer = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        /*
        Vector3 tempVec = new Vector3(0f,0f,0f);

        if(initialPos == tempVec)
        {
            initialPos = gameObject.transform.position;
        }
       */
        initialScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (gameObject.tag == "RequiredItem")
            thisItemIs = typeOfObject.requiredItem;
        else
            thisItemIs = typeOfObject.backgroundItem;

        shrinkScale = new Vector3(0.5f, 0.5f, 1f);

        shouldWeLerp = false;
        inBox = false;
    }

    void Start()
    {
        Vector3 tempVec = new Vector3(0f, 0f, 0f);

        if (initialPos == tempVec)
        {
            initialPos = gameObject.transform.position;
        }
       
    }
    void Update()
    {
        //May move this later.
        checkDragStatus();

        if(shouldWeLerp && (this.transform.position != initialPos))
        {
            StartCoroutine(rubberBand());
            canWeDrag = DragStatus.cantDrag;
        }

        if(this.transform.position == initialPos)
        {
            shouldWeLerp = false;
            inBox = false;
            //canWeDrag = DragStatus.canDrag;
        }

    }
    void OnMouseDown()
    {
        if(hintArrow != null)
        {
            hintArrow.SetActive(false);
           // Debug.Log("Set the arrow to false!");
        }
        //if(!levelMan.GetComponent<Current_level_manager>().isPaused)
        
        if (canWeDrag == DragStatus.canDrag)
        {
            //This if is to check whether or not the item is already in the box, to avoid spammage.

            if(inBox && Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (levelMan.GetComponent<Current_level_manager>().pauseButton != null)
            {
                levelMan.GetComponent<Current_level_manager>().pauseButton.interactable = false;
            }
           
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            //Change sorting order to be in front of everything.
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 99;

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

        StartCoroutine(waitPause());
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
                        if (transform.position.y > -2.95f)
                        {
                            shouldWeLerp = true;
                            //transform.position = initialPos;
                        }
                        break;
                    }
                case typeOfObject.backgroundItem:
                    {
                        if (transform.position.y > -2.95f)
                        {
                            shouldWeLerp = true;
                            //transform.position = initialPos;
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

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = sortingLayer;
       
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

    public bool testArrowObject()
    {
        if(hintArrow == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setArrowHint(GameObject arrowPrefab)
    {
        hintArrow = arrowPrefab;
    }

   public IEnumerator rubberBand()
    {
        float i = 0.0f;
        float rate = 0.0f;

            rate = (1.0f / 20.0f) * 1.0f;
            while (i < 1.0f && this.transform.position != initialPos)
            {
                i += Time.deltaTime * rate;
                this.transform.position = Vector3.Lerp(this.transform.position, initialPos, (i));
                yield return null;
            }
        
    }

    public IEnumerator waitPause()
   {
       yield return new WaitForSeconds(0.3f);
       levelMan.GetComponent<Current_level_manager>().pauseButton.interactable = true;

   }
}
