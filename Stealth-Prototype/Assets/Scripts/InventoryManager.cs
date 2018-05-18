using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    //GUI elements
    public RawImage Level1keycardImage;
    public RawImage Level2keycardImage;

    List<string> inv = new List<string>();

    void Start ()
    {
        //Level1keycardImage = GameObject.Find("Level 1 Key Card").GetComponent<RawImage>();
        Level1keycardImage.enabled = false;
        Level2keycardImage.enabled = false;
    }

    //fix this awful system
    void Update ()
    {
        if (inv.Contains("Level 1 Key Card")) {
            Level1keycardImage.enabled = true;
        }
        else
        {
            Level1keycardImage.enabled = false;
        }

        if (inv.Contains("Level 2 Key Card"))
        {
            Level2keycardImage.enabled = true;
        }
        else
        {
            Level2keycardImage.enabled = false;
        }
    }


    public void AddItem(string item)
    {
        Debug.Log("added " + item + " to inventory");
        inv.Add(item);
    }

    public bool CheckForItem (string TypeOfItem)
    {
        if (inv.Contains(TypeOfItem))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem (string TypeOfItem)
    {
        if (inv.Contains(TypeOfItem))
        {
            inv.Remove(TypeOfItem);
        }
        else
        {
            Debug.Log("could not remove item, item not in inventory");
        }

    }

    public void printInventory ()
    {
        if (inv.Count == 0)
        {
            Debug.Log("no items");
        }
        else
        {
            foreach (string item in inv)
            {
                Debug.Log(item);
            }
        }
    }

}
