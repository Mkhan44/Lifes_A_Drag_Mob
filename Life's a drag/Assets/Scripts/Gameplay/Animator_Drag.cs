using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Drag : MonoBehaviour
{
    public Animator animator;
    public GameObject levelManager;
    private Vector3 mouseDragStart;
    private Vector3 mouseDragEnd;
    bool aniFin = false;
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
      //  Debug.Log("mouseDragStart posy = " + mouseDragStart.y);
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

        //Based on the level, we'll need to know whether to compare the x coordinates or the y coordinates of the mouse position.
        mouseDragEnd = Input.mousePosition;
       // Debug.Log("mouseDragEnd posy = " + mouseDragEnd.y);
        //mouseDistance = Mathf.Abs(mouseDragEnd.x - mouseDragStart.x);
        mouseDistance =(mouseDragEnd.y - mouseDragStart.y);
       // Debug.Log("Differencet in posy = " + mouseDistance);

        if(mouseDistance < 1)
        {
            animator.SetBool("DoAnimation", true);
            if (!aniFin)
            {
                StartCoroutine(aniDone());
                aniFin = true;
            }
          
        }
    }

    IEnumerator aniDone()
    {
        //Hardcoded time to wait, will need a way to know how long to wait on a level per level basis in the future.
        yield return new WaitForSeconds(0.4f);

        levelManager.GetComponent<Current_level_manager>().turnOn();
    }

}
