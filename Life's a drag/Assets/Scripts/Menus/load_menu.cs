using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class load_menu : MonoBehaviour
{
    public Load_Level loadMen;

    void Awake()
    {
        loadMen.LoadLevel("Main_Menu");
    }
   
}
