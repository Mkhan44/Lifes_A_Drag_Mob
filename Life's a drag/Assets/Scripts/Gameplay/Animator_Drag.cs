using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Drag : MonoBehaviour
{
    public Animator animator;
    private Vector3 mouseDragStart;
    private Vector3 mouseDragEnd;
    private float mouseDistance;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        mouseDragStart = Input.mousePosition;
        Debug.Log("mouseDragStart posx = " + mouseDragStart.x);
    }

    void OnMouseDrag()
    {
        if(counter == 0)
        {
            
            //Do the animation
            counter++;
        }
    }

    void OnMouseUp()
    {
        mouseDragEnd = Input.mousePosition;
        Debug.Log("mouseDragEnd posx = " + mouseDragEnd.x);
        mouseDistance = Mathf.Abs(mouseDragEnd.x - mouseDragStart.x);
        Debug.Log("Differencet in posx = " + mouseDistance);

        if(mouseDistance > 1)
        {
            animator.SetFloat("Test", 0.2f);
        }
       // animator.SetFloat("Test", 0.0f);
    }

}
