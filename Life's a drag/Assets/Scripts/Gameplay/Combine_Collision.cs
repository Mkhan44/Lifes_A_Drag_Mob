using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine_Collision : MonoBehaviour
{
    public bool collided;
    public string objectName;
    public string tagToCompare;
    public int numCollisions = 0;


    void OnTriggerEnter2D(Collider2D other)
    {
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
      
        //Use this code to make sure that players can't stack multiple items in 1 box later.
        if (numCollisions == 2)
        {
            Debug.Log("The name of the excess item is: " + other.gameObject.name);
        }
      
    }

    void OnCollisionStay2D(Collision2D other)
    {
      
    }

    void OnTriggerExit2D(Collider2D other)
    {
        numCollisions -= 1;
        if(numCollisions == 0)
        { 
            tagToCompare = "";
            objectName = "";
            collided = false;
            Debug.Log("We left the collision!");
        }

    }
       
}
