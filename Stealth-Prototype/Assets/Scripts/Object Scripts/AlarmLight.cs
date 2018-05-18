using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour {

    private AudioSource audioSource;
    private Animator anim;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("isActive", false);

        //subscribe to alarm activation event
        FindObjectOfType<Manager>().alarmEvent += ActivateAlarm;
	}

	public void PlayAlarmSound ()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

    }

    public void ActivateAlarm ()
    {
        anim.SetBool("isActive", true);

        //unsubscribe when done
        FindObjectOfType<Manager>().alarmEvent -= ActivateAlarm;
    }

}

