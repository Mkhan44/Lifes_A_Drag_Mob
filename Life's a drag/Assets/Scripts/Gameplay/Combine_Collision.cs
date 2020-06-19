using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine_Collision : MonoBehaviour
{
    public bool collided;
    public string objectName;
    public string tagToCompare;
    public int numCollisions = 0;
    public bool boxFilled = false;
    public GameObject tempObject;
    public Vector3 centerCollide;
    public bool isResult;

    void Start()
    {
        if(gameObject.tag == "ResultArea")
        {
            isResult = true;
        }
        centerCollide = transform.GetComponent<BoxCollider2D>().bounds.center;
        Debug.Log("The center of the collider is: " + centerCollide);
    }

    void FixedUpdate()
    {
        //Ensures that only 1 item can be in a combo box at any given time.
        if(numCollisions > 1)
        {
            tempObject.transform.position = tempObject.GetComponent<Draggable_Item>().initialPos;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       
        StartCoroutine(shrink(other));

        numCollisions += 1;
        if (!collided)
        {
            string tempName = "";
            collided = true;
            tagToCompare = other.tag;
            tempName = (other.gameObject.name);
            if (tagToCompare == "RequiredItem")
            {
                objectName = tempName.Replace("(Clone)", "");
            }
            else
            {
                objectName = tempName;
            }
            Debug.Log("Object's name is: " + objectName);
            Debug.Log("Tag we got is: " + tagToCompare);
            Debug.Log("Collided is now: " + collided);

            /*
            float tempX;
            float tempY;
            float tempPosX;
            float tempPosY;



            tempPosX = other.gameObject.transform.position.x;
            tempPosY = other.gameObject.transform.position.y;
            tempX = other.gameObject.transform.localScale.x;
            tempY = other.gameObject.transform.localScale.y;
            Debug.Log("The item's x scale is: " + tempX + " y scale is: " + tempY);
            Debug.Log("The item's x position is: " + tempPosX + " y position is: " + tempPosY);
             */
        }
      
        tempObject = other.gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (boxFilled == true)
        {
            other.transform.position = centerCollide;
        }
        else
        {
          boxFilled = true;
        }
       // Debug.Log(boxFilled);
      
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if(!Input.GetMouseButton(0))
       {
           Debug.Log("Mouse isn't held down so grow the item!");
          StartCoroutine(grow(other));
       }
       

        numCollisions -= 1;
        if(numCollisions == 0)
        { 
            tagToCompare = "";
            objectName = "";
            collided = false;
            boxFilled = false;
           // Debug.Log(boxFilled);
           // Debug.Log("We left the collision!");
        }

    }

    IEnumerator grow(Collider2D other)
    {
        
        yield return new WaitForSeconds(0.1f);
        //Turn the item back to it's original scale.
        if(other != null)
        {
            other.transform.localScale = other.GetComponent<Draggable_Item>().initialScale;
        }
        

    }

    IEnumerator shrink(Collider2D other)
    {
        yield return new WaitForSeconds(0.1f);
        //Decrease the item's size so it fits into the box. May have to make this dynamic based on the item.
        if (other != null)
        {
            other.transform.localScale = other.GetComponent<Draggable_Item>().shrinkScale;
           // other.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }
       
}
