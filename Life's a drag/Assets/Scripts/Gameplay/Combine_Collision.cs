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


    void Start()
    {
        centerCollide = transform.GetComponent<BoxCollider2D>().bounds.center;
        Debug.Log("The center of the collider is: " + centerCollide);
        //Temp storage for the gameobject variable.
       // tempObject = this.gameObject;
    }

    void FixedUpdate()
    {
        //Ensures that only 1 item can be in a combo box at any given time.
        if(numCollisions > 1)
        {
            tempObject.transform.position = tempObject.GetComponent<Draggable_Item_Needed>().initialPos;
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
            objectName = tempName.Replace("(Clone)", "");
            Debug.Log("Object's name is: " + objectName);
            Debug.Log("Tag we got is: " + tagToCompare);
            Debug.Log("Collided is now: " + collided);
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
       
        StartCoroutine(grow(other));

        numCollisions -= 1;
        if(numCollisions == 0)
        { 
            tagToCompare = "";
            objectName = "";
            collided = false;
            boxFilled = false;
            Debug.Log(boxFilled);
            Debug.Log("We left the collision!");
        }

    }

    IEnumerator grow(Collider2D other)
    {
        
        yield return new WaitForSeconds(0.1f);
        //Turn the item back to it's original scale.
        if(other != null)
        {
            other.transform.localScale = other.GetComponent<Draggable_Item_Needed>().initialScale;
        }
        

    }

    IEnumerator shrink(Collider2D other)
    {
        yield return new WaitForSeconds(0.1f);
        //Decrease the item's size so it fits into the box. May have to make this dynamic based on the item.
        if (other != null)
        {
            other.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }
       
}
