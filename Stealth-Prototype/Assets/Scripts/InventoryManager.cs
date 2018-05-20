using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [HideInInspector]
    public Item currentItem;

    [HideInInspector]
    public Image display;

    private int currentSlot;

    [SerializeField]
    List<Item> inv;

    [SerializeField]
    Item hands;

    bool playerIsActive;

    void Awake()
    {
        inv = new List<Item>();

        inv.Add(hands);

        display = GameObject.Find("CurrentItem").GetComponent<Image>();
    }

    void Start()
    {
        playerIsActive = true;
        selectSlot(0);
    }

    //subscribe and unsubscribe from events
    void OnEnable()
    {
        FindObjectOfType<Manager>().GameOver += OnGameOver;
    }

    void OnDisable()
    {
        FindObjectOfType<Manager>().GameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        playerIsActive = false;
    }

    void Update()
    {
        //if player isn't active, don't check for input
        if (!playerIsActive) return;

        //check for input to select different items
        if (Input.GetKeyDown(KeyCode.Q))
        {
            previousSlot();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            nextSlot();
        }
    }

    public void nextSlot()
    {
        if (currentSlot < inv.Count - 1)
        {
            selectSlot(currentSlot + 1);
        }
    }

    public void previousSlot()
    {
        if (currentSlot > 0)
        {
            selectSlot(currentSlot - 1);
        }
    }

    public void selectSlot(int slot)
    {
        currentItem = inv[slot];
        currentSlot = slot;
        display.sprite = currentItem.image;
    }

    public bool isEquipped(string itemName)
    {
        return (currentItem.name == itemName);
    }

    public Item getEquipped()
    {
        return inv[currentSlot];
    }

    public void useItem()
    {
        if (currentItem.quantity > 0)
        {
            currentItem.quantity--;
        }
        else
        {
            RemoveItem(currentItem);
        }
    }

    public void AddItem(Item item)
    {
        //already has one, just add quantity
        if (CheckForItem(item))
        {
            //add to quantity
            getItem(item.name).quantity++;
        }
        else
        {
            //Debug.Log("added [" + item + "] to inventory");
            inv.Add(item);
            selectSlot(inv.Count - 1); //last position
        }

        //DEBUG
        //printInventory();
    }

    public bool CheckForItem(Item TypeOfItem)
    {
        return (inv.Contains(TypeOfItem));
    }

    public bool CheckForItem(string itemName)
    {
        foreach (Item item in inv)
        {
            if (item.name == itemName) return true;
        }
        return false;
    }

    public Item getItem(string itemName)
    {
        foreach (Item item in inv)
        {
            if (item.name == itemName) return item;
        }
        return null;
    }

    public void RemoveItem(Item TypeOfItem)
    {
        if (inv.Contains(TypeOfItem))
        {
            inv.Remove(TypeOfItem);

            if (currentSlot > 0) currentSlot--;
        }
        else
        {
            Debug.Log("could not remove item, item not in inventory");
        }

    }

    public void printInventory()
    {
        if (inv.Count == 0)
        {
            Debug.Log("no items");
        }
        else
        {
            Debug.Log("Contains " + inv.Count + " items:");
            foreach (Item item in inv)
            {
                Debug.Log("\t" + item);
            }
        }
    }

}
