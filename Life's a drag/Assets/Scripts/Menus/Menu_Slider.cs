using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Menu_Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;
    RectTransform thisTrans;
    Vector2 deltaStuff;

    public Vector3 initialPos;
    public float yClampMin;
    float yClampMax;
    public float xClampMax;
    public float xClampMin;
    float xInitial;
    public GameObject nextLoad;
    public GameObject menuSlider;

    // Start is called before the first frame update
    void Start()
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if(thisTrans.anchoredPosition.x >= (xInitial+240))
        {
            Debug.Log("Good enough!");
            if(nextLoad == null)
            {
                Debug.LogWarning("Nothing to load!");
                thisTrans.anchoredPosition = initialPos;
            }
            else
            {
                nextLoad.SetActive(true);
                thisTrans.anchoredPosition = initialPos;
                menuSlider.SetActive(false);
            }
        } 
        else
        {
            thisTrans.anchoredPosition = initialPos;
        }
        
    }


 
}
