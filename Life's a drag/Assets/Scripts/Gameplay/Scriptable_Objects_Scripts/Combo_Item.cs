using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Combo_Item : ScriptableObject
{
    public GameObject theItem;
    public string mat1;
    public string mat2;

    //Initialize these to 0 for default just so the game knows what to do if nothing is input. Should change these via the inspector, however.
    public Vector3 initialPos = new Vector3(0f, 0f, 0f);

    //Check if this combo item is also a material, if it is, we won't count it towards the needed item count. Also won't have it a required item to complete level progression.
    public bool isAlsoMat;

    /*
     * Challenge mode.
     */ 

    //3 of the random spawns for this combo item.
    public Vector3 spawnPoint1;
    public Vector3 spawnPoint2;
    public Vector3 spawnPoint3;
}
