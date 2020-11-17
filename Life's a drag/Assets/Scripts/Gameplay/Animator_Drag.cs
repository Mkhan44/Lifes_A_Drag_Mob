using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class Animator_Drag : MonoBehaviour
{
    public Animator animator;
    public GameObject levelManager;
    private Vector3 mouseDragStart;
    private Vector3 mouseDragEnd;
    bool aniFin = false;
    private float mouseDistance;
    int counter;
    int soundID;
    Audio soundClip;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        if (levelManager.GetComponent<Current_level_manager>().theLev.levelAniSound != null)
        {
            float sfxVolume;
            if (levelManager.GetComponent<Current_level_manager>().theLev.sfxVolume == 0f || levelManager.GetComponent<Current_level_manager>().theLev.sfxVolume > 1f)
            {
                sfxVolume = 1f;
            }
            else
            {
                sfxVolume = levelManager.GetComponent<Current_level_manager>().theLev.sfxVolume;
            }
            soundID = EazySoundManager.PrepareSound(levelManager.GetComponent<Current_level_manager>().theLev.levelAniSound, sfxVolume);
            soundClip = EazySoundManager.GetAudio(soundID);
        }
        else
        {
            soundID = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        
        mouseDragStart = Input.mousePosition;
      //  Debug.Log("mouseDragStart posy = " + mouseDragStart.y);
    }

    void OnMouseDrag()
    {
        if(counter == 0)
        {
            
            //Do the animation
            counter++;
        }
    }

    void OnMouseUp()
    {

        //Based on the level, we'll need to know whether to compare the x coordinates or the y coordinates of the mouse position.
        mouseDragEnd = Input.mousePosition;
        //mouseDistance = Mathf.Abs(mouseDragEnd.x - mouseDragStart.x);
        mouseDistance = Mathf.Abs(mouseDragEnd.y - mouseDragStart.y);
        //Debug.Log("Difference in posy = " + mouseDistance);
        if (!levelManager.GetComponent<Current_level_manager>().isPaused && !levelManager.GetComponent<Current_level_manager>().isZoomed)
        {
            if (mouseDistance > 7)
            {
                animator.SetBool("DoAnimation", true);
                if (!aniFin)
                {
                   if(soundID != 0)
                   {
                       soundClip.Play();
                   }
                    StartCoroutine(aniDone());
                    aniFin = true;
                }

            }
        }
        
    }

    IEnumerator aniDone()
    {
        

        //Use this code below to figure out how long to wait before spawning the new item.
        if(levelManager.GetComponent<Current_level_manager>().theLev.aniSecondsToWait > 0)
        {
            yield return new WaitForSeconds(levelManager.GetComponent<Current_level_manager>().theLev.aniSecondsToWait);
            if (soundID != 0 && !levelManager.GetComponent<Current_level_manager>().theLev.dontStopSFXEarly)
            {
                soundClip.Stop();
            }
            
        }
        else
        {
            yield return new WaitForSeconds(3.0f);
            if (soundID != 0 && !levelManager.GetComponent<Current_level_manager>().theLev.dontStopSFXEarly)
            {
                soundClip.Stop();
            }
        }
       

        levelManager.GetComponent<Current_level_manager>().turnOn();

        //Get rid of collider so that way item won't get stuck.
        Destroy(gameObject.GetComponent<BoxCollider2D>());
    }

}
