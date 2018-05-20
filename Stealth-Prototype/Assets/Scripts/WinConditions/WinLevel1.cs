using UnityEngine;

public class WinLevel1 : MonoBehaviour {

    private Manager manager;
    private GameObject player;
    private bool playerIsActive;
    private PlayerController playerController;
    private SelectionPanelController select;
    private InventoryManager inv;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        manager = FindObjectOfType<Manager>();

        select = FindObjectOfType<SelectionPanelController>();
        inv = FindObjectOfType<InventoryManager>();
        playerIsActive = true;
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

    bool CheckRequirements()
    {
        return (inv.CheckForItem("usb"));
    }

    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < playerController.playerReach && playerIsActive)
        {
            if (CheckRequirements())
            {
                manager.WonGame();
            }

        }
    }

    void OnMouseOver()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < playerController.playerReach && playerIsActive)
        {
            if (CheckRequirements())
            {
                select.Display("Click to Leave", Color.green);
            }
            else
            {
                select.Display("Find the Computer and come back", Color.gray);
            }
            
        }
    }

    void OnMouseExit()
    {
        select.Close();
    }

}