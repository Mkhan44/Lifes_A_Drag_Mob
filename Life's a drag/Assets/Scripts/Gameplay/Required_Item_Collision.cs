using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Required_Item_Collision : MonoBehaviour
{
    bool collided = false;
    public GameObject levelManagerInstance;
    string tagToCompare = "";
    bool isMat;
    public string objectName;
    public List<string> requiredItemNames = new List<string>();
    public List<string> comboItemNames = new List<string>();
    public List<string> matItemNames = new List<string>();

    void Start()
    {
        findItemNames();
    }
    void Update()
    {
        tryItem();
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
                    //This is a material, and therefore we will add it to the list.
                    matItemNames.Add(currentRegItemName);

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


        for(int a = 0; a < matItemNames.Count; a++)
        {
            Debug.Log(matItemNames[a]);
        }

        for (int b = 0; b < requiredItemNames.Count; b++)
        {
            Debug.Log(requiredItemNames[b]);
        }
       
       
    }

    //Attempting to see if an item is required to complete the level. If it is, de-activate the item and send a message to the level manager.
    //If not, put the item back to it's initial position (Unless it's a ComboItem) , and give an error message on the LevelManager side.
    void tryItem()
    {
        if ((!Input.GetMouseButton(0)) && collided)
        {
            if (tagToCompare == "RequiredItem")
            {
                
                for (int i = 0; i < requiredItemNames.Count; i++)
                {
                    if (objectName != requiredItemNames[i])
                    {
                        for (int j = 0; j < comboItemNames.Count; j++)
                        {
                            if (objectName == comboItemNames[j])
                            {
                                isItAMat();
                                Debug.Log("The item put in the field was: " + objectName + " And the combo item it matched with was " + comboItemNames[j]);
                                levelManagerInstance.GetComponent<Current_level_manager>().gotItem(objectName, tagToCompare, isMat);
                                objectName = "";
                                tagToCompare = "";
                                break;
                            }
                        }
                        isItAMat();
                        if(isMat)
                        {
                            Debug.Log("The item put in the field was: " + objectName + " And it's a material. ");
                            levelManagerInstance.GetComponent<Current_level_manager>().gotItem(objectName, tagToCompare, isMat);
                            objectName = "";
                            tagToCompare = "";
                            break;
                        }
                    }
                    else
                    {
                        isItAMat();
                        Debug.Log("The item put in the field was: " + objectName + " And the REGULAR item it matched with was " + requiredItemNames[i]);
                        levelManagerInstance.GetComponent<Current_level_manager>().gotItem(objectName, tagToCompare, isMat);
                        objectName = "";
                        tagToCompare = "";
                        break;
                    }
                }
            }
            else
            {
                isMat = false;
                levelManagerInstance.GetComponent<Current_level_manager>().gotItem(objectName, tagToCompare, isMat);
            }
        }
        isMat = false;
    }

    //Finds out whether or not the 
    void isItAMat()
    {
        for(int i = 0; i < matItemNames.Count; i++)
        {
            if(objectName == matItemNames[i])
            {
                Debug.Log("It's a material!");
                isMat = true;
                break;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string tempName = "";
        collided = true;

        tagToCompare = other.tag;
        tempName = (other.gameObject.name);
        objectName = tempName.Replace("(Clone)", "");
        Debug.Log("Object's name is: " + objectName);
        Debug.Log("Tag we got is: " + tagToCompare);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        tagToCompare = "";
        objectName = "";
        collided = false;
        // Debug.Log("We left the collision!");
    }

}
