//Code written by Mohamed Riaz Khan of Bukugames.
//All code is written by me (Above name) unless otherwise stated via comments below.

using System.Collections;
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
    string levelType;
    public enum typeOfButton
    {
        Resume,
        Options,
        Main_Menu,
        Tutorial,
        Play_Level,
        LvlSelectBack
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
        if(menuSlider != null)
        {
            menuSlider.SetActive(true);
        }
    
       // Debug.Log("Overrided!");
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
                       // Time.timeScale = 0f;
                        break;
                    }
                case typeOfButton.Play_Level:
                    {
                        dialougeBoxRef.GetComponent<Dialouge_Level_Select_Box>().loadLevel();
                        break;
                    }
            }
            enoughDrag = false;
        }
      
        
 
       // throw new System.NotImplementedException();
    }
}
