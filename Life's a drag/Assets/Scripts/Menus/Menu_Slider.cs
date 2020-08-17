using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Menu_Slider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{


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
        initialPos = gameObject.transform.position;
        yClampMin = gameObject.transform.position.y;
        yClampMax = gameObject.transform.position.y;
        xInitial = gameObject.transform.position.x;
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
        transform.position = new Vector2 (Mathf.Clamp(Input.mousePosition.x, xInitial, (xInitial+100)), Mathf.Clamp(Input.mousePosition.y, yClampMin, yClampMax));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.position.x >= (xInitial+90))
        {
            Debug.Log("Good enough!");
            if(nextLoad == null)
            {
                Debug.LogWarning("Nothing to load!");
                transform.position = initialPos;
            }
            else
            {
                nextLoad.SetActive(true);
                transform.position = initialPos;
                menuSlider.SetActive(false);
            }
        }
           
        else
        {
            transform.position = initialPos;
        }
        
    }


 
}
