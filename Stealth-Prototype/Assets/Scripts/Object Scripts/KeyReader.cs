using System.Collections;
using UnityEngine;

public class KeyReader : MonoBehaviour {

    InventoryManager inventory;
    public GameObject door;
    private Door doorControl;
    private Animator anim;
    private AudioSource audioSource;

    public string correctKeyCardName;

    public AudioClip accepted;
    public AudioClip denied;

    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
        doorControl = door.GetComponent<Door>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        doorControl.LockDoor();
    }


    void OnMouseDown ()
    {
        //only check if door is locked and closed
        if (doorControl.CheckDoorLock() && !doorControl.CheckDoorOpen())
        {
            if (inventory.CheckForItem(correctKeyCardName) || inventory.CheckForItem("Level 2 Key Card") || inventory.CheckForItem("Level 1 Key Card"))
            {
                //if the player has any card
                anim.SetBool("hasCard", true);

                if (inventory.CheckForItem(correctKeyCardName))
                {
                    //if the player has the correct key card
                    //inventory.RemoveItem(correctKeyCardName);
                    anim.SetBool("correctCard", true);
                    StartCoroutine(unlockDelay());
                    audioSource.clip = accepted;
                    audioSource.PlayDelayed(1.5f);
                }

                else
                {
                    anim.SetBool("correctCard", false);
                    audioSource.clip = denied;
                    audioSource.PlayDelayed(1.5f);
                    //Debug.Log("incorrect key card");
                }

            }
        }

    }

    void EndOfAnimation ()
    {
        //Debug.Log("reached end of animation");
        anim.SetBool("hasCard", false);
    }

    IEnumerator unlockDelay ()
    {
        yield return new WaitForSeconds(3);
        doorControl.UnlockDoor();
    }

}
