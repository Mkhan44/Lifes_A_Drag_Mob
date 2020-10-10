using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Hellmade.Sound;
public class Menu_Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    protected RectTransform thisTrans;
    Vector2 deltaStuff;

    public Vector3 initialPos;
    public float yClampMin;
    protected float yClampMax;
    public float xClampMax;
    public float xClampMin;
    protected float xInitial;
    public GameObject nextLoad;
    public GameObject menuSlider;
    protected bool enoughDrag;
    public AudioClip slideSound;

    protected bool canDrag = true;

    //This will be the variable that determines how far this button can be dragged so we can make different sizes.
    public float xEnd;
   protected virtual void Start()
    {
        thisTrans = this.GetComponent<RectTransform>();
        initialPos = thisTrans.anchoredPosition;
       // initialPos = gameObject.transform.position;
        yClampMin = initialPos.y;
        yClampMax = yClampMin;
        xInitial = initialPos.x;
        Debug.Log(xInitial);
       if(xEnd == 0)
       {
           xEnd = 220f;
       }
       if(this.GetComponent<Button>().interactable == false)
       {
           canDrag = false;
       }
       else
       {
           canDrag = true;
       }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag)
        {
            deltaStuff = eventData.delta / canvas.scaleFactor;
            //Debug.Log(eventData.delta / canvas.scaleFactor);
            //thisTrans.anchoredPosition = new Vector2 (Mathf.Clamp(Input.mousePosition.x, xInitial, (xInitial+100)), Mathf.Clamp(Input.mousePosition.y, yClampMin, yClampMax));

            //Might want to optimize this more...Not sure yet.
            thisTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
            thisTrans.anchoredPosition = new Vector2(Mathf.Clamp(thisTrans.anchoredPosition.x, xInitial, (xInitial + xEnd)), Mathf.Clamp(thisTrans.anchoredPosition.y, yClampMin, yClampMax));

            Debug.Log(thisTrans.anchoredPosition);
        }

    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (thisTrans.anchoredPosition.x >= (xInitial + (xEnd - 40f)))
        {

            enoughDrag = true;
            Debug.Log("Good enough!");
            if (slideSound != null)
            {
                EazySoundManager.PlaySound(slideSound, 0.7f);
            }
            if(menuSlider != null)
            {
                slideOut();
            }
            else
            {
                thisTrans.anchoredPosition = initialPos;
            }
          
            /*
            if(nextLoad == null)
            {
                Debug.LogWarning("Nothing to load!");
                thisTrans.anchoredPosition = initialPos;
            }
            else
            {
                //Call this to make the thingy slide out.
                slideOut();
            }
        }
             */
        }
        else
        {
            thisTrans.anchoredPosition = initialPos;

        }
       
    }


    public void slideOut()
    {
        if (nextLoad != null)
        {
            nextLoad.SetActive(true);
        }

        //thisTrans.anchoredPosition = initialPos;
        //Make it slide out the opposite way.
        menuSlider.GetComponent<PanelAnimator>().outAnimEndPosition.x = (menuSlider.GetComponent<PanelAnimator>().outAnimEndPosition.x * -1);
        menuSlider.GetComponent<PanelAnimator>().StartAnimOut();
        StartCoroutine(waitForAni());
        gameObject.GetComponent<Button>().onClick.Invoke();

        // menuSlider.SetActive(false);
    }
    public IEnumerator waitForAni()
    {
        float timeToWait;
        timeToWait = menuSlider.GetComponent<PanelAnimator>().outAnimDuration;
        yield return new WaitForSeconds(timeToWait);
        //Set it back to the original for next time.
        menuSlider.GetComponent<PanelAnimator>().outAnimEndPosition.x = (menuSlider.GetComponent<PanelAnimator>().outAnimEndPosition.x * -1);
        menuSlider.GetComponent<RectTransform>().anchoredPosition = menuSlider.GetComponent<PanelAnimator>().outAnimEndPosition;
        thisTrans.anchoredPosition = initialPos;
        if(nextLoad != null)
        {
            nextLoad.GetComponent<PanelAnimator>().StartAnimIn();
            menuSlider.SetActive(false);
        }
       
    }

    public void setNextLoad(string test)
    {
        nextLoad = GameObject.Find(test);
        Debug.Log("Function was called!");
    }




 
}
