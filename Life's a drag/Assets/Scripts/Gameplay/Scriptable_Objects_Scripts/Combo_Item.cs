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
}
