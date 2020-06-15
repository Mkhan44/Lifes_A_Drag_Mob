using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable_Item : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Vector3 initialPos;
    public Vector3 initialScale;


    void Awake()
    {
        initialPos = gameObject.transform.position;
        if (initialPos == new Vector3(2.4f, -3.5f, 0f))
        {
            initialPos = new Vector3(0.0f, 0.0f, 0.0f);
        }
        initialScale = new Vector3 (transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        transform.localScale = new Vector3((initialScale.x - 0.2f), (initialScale.y - 0.2f), 1f);
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
        if(gameObject.transform.position.y > -2.85f)
        {
            transform.localScale = initialScale;
        }
        
       //transform.position = Combine_Collision.centerCollide;
        
    }
}
