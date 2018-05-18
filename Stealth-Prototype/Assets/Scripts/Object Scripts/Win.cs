using UnityEngine;

public class Win : MonoBehaviour {
    //deprecated, replaced with WinLevel1, WinLevel2, etc

    private Manager manager;
    private GameObject player;
    private bool playerIsActive;
    private PlayerController playerController;
    private SelectionPanelController select;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        manager = FindObjectOfType<Manager>();

        select = FindObjectOfType<SelectionPanelController>();   
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

    void OnMouseDown ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < playerController.playerReach && playerIsActive)
        {
            manager.WonGame();
        }
    }

    void OnMouseOver ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < playerController.playerReach && playerIsActive)
        {
            select.Display("Click to Win", Color.green);
        }
    }

    void OnMouseExit ()
    {
        select.Close();
    }

}
