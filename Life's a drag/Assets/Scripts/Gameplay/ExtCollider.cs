//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtCollider : MonoBehaviour
{

    public GameObject parentRef;
    public Vector3 parentCenter;
    GameObject collidedObject;


    // Start is called before the first frame update
    void Start()
    {
        parentCenter = parentRef.GetComponent<Combine_Collision>().centerCollide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collidedObject = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
       
    }

    void OnTriggerStay2D(Collider2D other)
    {
        parentCenter = parentRef.GetComponent<Combine_Collision>().centerCollide;
        Debug.Log("We found an item!");
        if (!Input.GetMouseButtonDown(0))
        {
            other.transform.position = parentCenter;
        }
    }

}
