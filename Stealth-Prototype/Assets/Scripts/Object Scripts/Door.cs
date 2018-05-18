using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour {
    //script to control the door, play sounds/animations


    //animator used to open/close dooor
    private Animator anim;

    //selection panel, replace with SelectionPanelController
    private SelectionPanelController select;

    //player
    private GameObject player;
    private bool playerIsActive;
    private float selectionDistance;

    //control the door
    private bool doorLocked = false;
    private bool doorOpen;
    public bool relockDoor;
    public int relockTime;

    //sounds
    private AudioSource audioSource;
    public AudioClip openDoorSound;
    public AudioClip unlockDoorSound;

    //awake because the keyreader accesses some things at Start
	void Awake () {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        select = FindObjectOfType<SelectionPanelController>();

        //start door closed
        doorOpen = false;

        //get information from player
        player = GameObject.FindGameObjectWithTag("Player");
        selectionDistance = player.GetComponent<PlayerController>().playerReach;
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

    //allow enemies to open door
    void OnTriggerEnter (Collider other)
    {
        //Debug.Log(other.transform.name + " has entered trigger");
        if (other.tag == "Enemy")
        {
            //Debug.Log("enemy approaching door");
            doorOpen = true;
            anim.SetBool("open", true);
        }
    }

    //close door behind enemy
    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log("enemy leaving door");
            doorOpen = false;
            
            anim.SetBool("open", false);
        }
    }

    //play open door sound
    void OpenSound ()
    {
        //Debug.Log("opening door sound");
        audioSource.PlayOneShot(openDoorSound);
    }
    
    //closes selection window
    void OnMouseExit ()
    {
        select.Close();
    }

    //returns true if door is open, else false;
    public bool CheckDoorOpen()
    {
        if (doorOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //returns true if door is locked, else false
    public bool CheckDoorLock()
    {
        if (doorLocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //replace with new procedure, displays info on selectionPanel
    void OnMouseOver ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < selectionDistance && playerIsActive)
        {
            if (doorOpen)
            {
                select.Display("Close Door", Color.white);
            }
            if (!doorOpen && !doorLocked)
            {
                select.Display("Open Door", Color.white);
            }
            if (!doorOpen && doorLocked)
            {
                select.Display("Door Locked", Color.red);
            }
        }
    }

    //unlocks door
    public void UnlockDoor ()
    {
        audioSource.PlayOneShot(unlockDoorSound);
        doorLocked = false;

        if (relockDoor)
        {
            StartCoroutine(RelockDoor());
        }
    }

    //locks door
    public void LockDoor ()
    {
        audioSource.PlayOneShot(unlockDoorSound);
        doorLocked = true;
    }

    //relocks door when it is closed if configured
    IEnumerator RelockDoor()
    {
        //Debug.Log("waiting intial " + relockTime + "seconds...");
        yield return new WaitForSeconds(relockTime);

        while (!doorLocked)
        {
            yield return new WaitForSeconds(2);

            if (doorOpen)
            {
                //Debug.Log("door is open, waiting...");
                yield return new WaitForSeconds(2);
            }
            else
            {
                //Debug.Log("door closed, relocking...");
                LockDoor();
            }

        }

    }

    //checks state of door and changes it
    void OnMouseDown ()
    {
        //Debug.Log("selected door");

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < selectionDistance && playerIsActive)
        {
            if (!doorOpen && !doorLocked)
            {
                doorOpen = true;
                anim.SetBool("open", true);
            }
            else
            {
                doorOpen = false;
                anim.SetBool("open", false);
            }
        }
    }
	
}
