using System.Collections;
using UnityEngine;

public class PushToExitButton : MonoBehaviour {
    //allows player to push a button to unlock door

    //reference door controller
    public GameObject door;
    private Door doorControl;

    //see if player is close enough and if he is active
    private GameObject player;
    private bool playerIsActive;
    private float selectionDistance;

    //give it a sound when pushed
    private AudioSource audioSource;

    void Start () {
        doorControl = door.GetComponent<Door>();

        //get information from player
        player = GameObject.FindGameObjectWithTag("Player");
        selectionDistance = player.GetComponent<PlayerController>().playerReach;
        playerIsActive = true;

        //audio
        audioSource = GetComponent<AudioSource>();
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

    void OnMouseDown()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < selectionDistance && playerIsActive)
        {
            //make sure the door is closed and locked, and not making noise
            if (!audioSource.isPlaying && doorControl.CheckDoorLock() && !doorControl.CheckDoorOpen())
            {
                audioSource.Play();
                StartCoroutine(unlockDelay());
            }
        }
    }

    IEnumerator unlockDelay()
    {
        yield return new WaitForSeconds(3);
        doorControl.UnlockDoor();
    }

}
