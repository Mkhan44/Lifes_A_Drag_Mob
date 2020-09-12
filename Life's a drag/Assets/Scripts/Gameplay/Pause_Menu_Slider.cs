﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Hellmade.Sound;

public class Pause_Menu_Slider : Menu_Slider, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Pause_Menu pauseRef;
    public Level_Select_Manager levelRef;
    public GameObject dialougeBoxRef;
    public enum typeOfButton
    {
        Resume,
        Options,
        Main_Menu,
        Tutorial
    }


    //Let's the game know what button is gonna be dragged.
    public typeOfButton buttonType;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void OnBeginDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }

    void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Debug.Log("Overrided!");
        if (enoughDrag)
        {
            switch (buttonType)
            {
                case typeOfButton.Resume:
                    {
                        pauseRef.Resume();
                        break;
                    }
                case typeOfButton.Options:
                    {
                        pauseRef.Options();
                        break;
                    }
                case typeOfButton.Main_Menu:
                    {
                        pauseRef.MainMenu();
                        gameObject.GetComponent<RectTransform>().anchoredPosition = initialPos;
                        break;
                    }
                case typeOfButton.Tutorial:
                    {
                        dialougeBoxRef.SetActive(true);
                        Time.timeScale = 0f;
                        break;
                    }
            }
            enoughDrag = false;
        }
      
        
 
       // throw new System.NotImplementedException();
    }
}
