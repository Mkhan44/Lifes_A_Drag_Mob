using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Required_Item_Collision : MonoBehaviour
{
    bool collided = false;
    public GameObject levelManagerInstance;
    string tagToCompare = "";
   
    public List<string> requiredItemNames = new List<string>();
    public List<string> comboItemNames = new List<string>();

    void Start()
    {
        findItemNames();
    }
    void Update()
    {
        /*

        if ((!Input.GetMouseButton(0)) && collided)
        {
            for()
            if (tagToCompare == "Item1")
            {
                    
            }

            else
            {
                Debug.Log("No, that doesn't belong in Item1's slot!");
            }
        }
         */
    }

    /*
    ******************************************
    *
    !!!!!!!!!!!!!!!FUNCTIONS!!!!!!!!!!!!!!!!!!!
    * 
    ******************************************
    */

    void findItemNames()
    {
        string currentRegItemName;
        string currentComboItemName;
        int countReg;
        int countCombo;
        countReg = levelManagerInstance.GetComponent<Current_level_manager>().theLev.requiredItems.Count;
        countCombo = levelManagerInstance.GetComponent<Current_level_manager>().theLev.comboItemsNeeded.Count;

        //Adding the names of the GameObjects required to clear the level (Both regular items in the level and Combo items. Excluding any materials for combo items since we don't want those.)
        //May have to refine this if we need like x2 of something where one can be used as a material (I.e. x2 paper , but paper + pen = report. You would still want at least 1x paper not excluded from the list.)
        for (int i = 0; i < countReg; i++)
        {
            currentRegItemName = levelManagerInstance.GetComponent<Current_level_manager>().theLev.requiredItems[i].item.gameObject.name;
            
            for (int k = 0; k < countCombo; k++)
            {
                if (( currentRegItemName == levelManagerInstance.GetComponent<Current_level_manager>().theLev.comboItemsNeeded[k].mat1) ||
                    (currentRegItemName == levelManagerInstance.GetComponent<Current_level_manager>().theLev.comboItemsNeeded[k].mat2))
                {
                   // Debug.Log("The item: " + levelManagerInstance.GetComponent<Current_level_manager>().theLev.requiredItems[i].item.gameObject.name + " was excluded from the list because it's a material for a combo item.");
                    break;
                }
                else
                {
                    requiredItemNames.Add(currentRegItemName);
                  //  Debug.Log("The item: " + levelManagerInstance.GetComponent<Current_level_manager>().theLev.requiredItems[i].item.gameObject.name + " was ADDED to the list because it's NOT a material for a combo item.");
                    break;
                }
            }

        }
        for (int j = 0; j < countCombo; j++)
        {
            currentComboItemName = levelManagerInstance.GetComponent<Current_level_manager>().theLev.comboItemsNeeded[j].theItem.gameObject.name;
            comboItemNames.Add(currentComboItemName);
           // Debug.Log("The item: " + levelManagerInstance.GetComponent<Current_level_manager>().theLev.comboItemsNeeded[j].theItem.gameObject.name + " was added to the list.");
        }

        /*
        for (int deb = 0; deb < requiredItemNames.Count; deb++ )
        {
            Debug.Log(requiredItemNames[deb]);
            for (int debs = 0; debs < comboItemNames.Count; debs++ )
            {
                Debug.Log("The final item list for Combo items is: " + comboItemNames[debs]);
            }
        }
       */
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collided = true;

        tagToCompare = other.tag;
        Debug.Log("Tag we got is: " + tagToCompare);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        tagToCompare = "";
        collided = false;
        // Debug.Log("We left the collision!");
    }

}
