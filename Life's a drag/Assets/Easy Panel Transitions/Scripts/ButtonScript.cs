//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour {
    
    public UnityEvent onInteract = new UnityEvent();

    public void OnClick()
    {
        // Trigger the event
        onInteract.Invoke();
    }
}
