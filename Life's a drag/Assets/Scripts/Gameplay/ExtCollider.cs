using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtCollider : MonoBehaviour
{

    public GameObject parentRef;
    public Vector3 parentCenter;



    // Start is called before the first frame update
    void Start()
    {
        parentCenter = parentRef.GetComponent<Combine_Collision>().centerCollide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("We found an item!");
        if (Input.GetMouseButtonUp(0))
        {
            other.transform.position = parentCenter;
        }
    }

}
