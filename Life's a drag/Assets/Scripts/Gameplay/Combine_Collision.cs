using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combine_Collision : MonoBehaviour
{
    public bool collided;
    public string objectName;
    public string tagToCompare;


    void OnTriggerEnter2D(Collider2D other)
    {
        string tempName = "";
        collided = true;

        tagToCompare = other.tag;
        tempName = (other.gameObject.name);
        objectName = tempName.Replace("(Clone)", "");
        Debug.Log("Object's name is: " + objectName);
        Debug.Log("Tag we got is: " + tagToCompare);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        tagToCompare = "";
        objectName = "";
        collided = false;
        // Debug.Log("We left the collision!");
    }
       
}
