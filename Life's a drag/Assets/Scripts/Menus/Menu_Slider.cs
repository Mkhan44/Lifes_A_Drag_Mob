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

    // Start is called before the first frame update
    void Start()
    {
        initialPos = gameObject.transform.position;
        yClampMin = gameObject.transform.position.y;
        yClampMax = gameObject.transform.position.y;
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
        transform.position = new Vector2 (Mathf.Clamp(Input.mousePosition.x, 80, 250), Mathf.Clamp(Input.mousePosition.y, yClampMin, yClampMax));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPos;
    }


 
}
