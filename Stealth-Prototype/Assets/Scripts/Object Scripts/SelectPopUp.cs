using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SelectPopUp : MonoBehaviour {
    //shows a selection pop up when looked at
    //communicates with SelectionPanelController

    //reference the script that controls displaying selector information
    private SelectionPanelController select;

    //what will be displayed on hover over
    public string text;
    public Color color;

    //get the position of the player to test distance between
    private Transform player;

    //move selection distance from player to this script
    public float selectionDistance = 3;

    //if the player is active or not
    private bool playerIsActive = true;

    //if the message should display after clicking
    public bool onlyShowOnce;
    private bool hasShown;

	void Start () {
        select = FindObjectOfType<SelectionPanelController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hasShown = false;
        playerIsActive = true;
	}

    //set up subscriptions and unsubscriptions of events
    void OnEnable()
    {
        FindObjectOfType<Manager>().GameOver += OnGameOver;
    }

    void OnDisable ()
    {
        FindObjectOfType<Manager>().GameOver -= OnGameOver;
    }

    void OnGameOver ()
    {
        playerIsActive = false;
    }

    void OnMouseOver ()
    {
        if (!hasShown)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < selectionDistance && playerIsActive)
            {
                select.Display(text, color);
            }
        }
    }

    void OnMouseDown ()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < selectionDistance && playerIsActive)
        {
            if (onlyShowOnce)
            {
                hasShown = true;
            }
        }
    }

    void OnMouseExit ()
    {
        select.Close();
    }
	

}
