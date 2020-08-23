using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause_Menu_Slider : Menu_Slider, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnBeginDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }

    void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Debug.Log("Overrided!");
       // throw new System.NotImplementedException();
    }
}
