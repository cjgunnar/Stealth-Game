using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Item item;

    public bool destroyAfter;
    private bool playerIsActive;
    private Transform player;
    public float selectionDistance = 3f;

    InventoryManager inventory;

    private SelectionPanelController select;

    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        select = FindObjectOfType<SelectionPanelController>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerIsActive = true;
    }

    //set up subscriptions and unsubscriptions of events
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

    void OnMouseDown()
    {
        float distance;
        distance = Vector3.Distance(transform.position, player.position);
        if (playerIsActive && distance < selectionDistance && enabled)
        {
            PickUpItem();
            select.Close();
        }

    }

    void PickUpItem()
    {
        //add to inventory
        inventory.AddItem(item);

        if (item != null)
        {
            Debug.Log("Selected: " + item);
        }
        else
        {
            Debug.Log("unrecognized selection");
        }

        //if destroy after, destroy
        //ex key card, grab and then it disappears
        if (destroyAfter)
        {
            Destroy(gameObject);
        }
        else
        {
            //if it does not get destroyed, disable this script to prevent collecting multiple
            enabled = false;
        }
    }
}
