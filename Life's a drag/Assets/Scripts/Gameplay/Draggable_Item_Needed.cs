using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable_Item_Needed : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector3 initialPos;
    public Vector3 initialScale;

    void Awake()
    {
        initialPos = gameObject.transform.position;
        initialScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = Mathf.Clamp(curPosition.y, -3.75f, 3.4f);
        curPosition.x = Mathf.Clamp(curPosition.x, -2.55f, 2.55f);
        transform.position = curPosition;
        
    }

    void OnMouseUp()
    {
        //transform.localScale = initialScale;
       //transform.position = Combine_Collision.centerCollide;
        
    }
}
