using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Required_Item : ScriptableObject
{
    public GameObject item;

    //Tells the game whether or not to set the item as active on startup. Used for if an item is inside/behind something.
    public bool isHidden;

    //These will tell the game where in the map to spawn the item on startup.
    public float xPos;
    public float yPos;

    //Rotation on Z for items that need to be flipped in any way.
    public float zRot = 0f;

    public float yRot = 0f;
}
