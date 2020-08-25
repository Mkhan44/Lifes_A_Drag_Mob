using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    // Start is called before the first frame update
   protected virtual void Start()
    {
        thisTrans = this.GetComponent<RectTransform>();
        initialPos = thisTrans.anchoredPosition;
       // initialPos = gameObject.transform.position;
        yClampMin = initialPos.y;
        yClampMax = yClampMin;
        xInitial = initialPos.x;
        Debug.Log(xInitial);
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
        deltaStuff = eventData.delta / canvas.scaleFactor;
        //Debug.Log(eventData.delta / canvas.scaleFactor);
       //thisTrans.anchoredPosition = new Vector2 (Mathf.Clamp(Input.mousePosition.x, xInitial, (xInitial+100)), Mathf.Clamp(Input.mousePosition.y, yClampMin, yClampMax));

        //Might want to optimize this more...Not sure yet.
        thisTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
        thisTrans.anchoredPosition = new Vector2 (Mathf.Clamp(thisTrans.anchoredPosition.x, xInitial, (xInitial + 250)) , Mathf.Clamp(thisTrans.anchoredPosition.y, yClampMin, yClampMax)); 
      
        Debug.Log(thisTrans.anchoredPosition);


    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (thisTrans.anchoredPosition.x >= (xInitial + 240))
        {

            enoughDrag = true;
            Debug.Log("Good enough!");
            slideOut();
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

        thisTrans.anchoredPosition = initialPos;
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
        if(nextLoad != null)
        {
            nextLoad.GetComponent<PanelAnimator>().StartAnimIn();
        }
        
    }




 
}
