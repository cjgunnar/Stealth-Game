using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {

    private CharacterController controller;
    new private AudioSource audio;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded && !audio.isPlaying && controller.velocity.magnitude > .1f)
        {
            if (controller.velocity.magnitude > 4f) //sprint
            {
                audio.volume = .25f;
                
                audio.Play();
            }
            else if (controller.velocity.magnitude > 2f) //walk
            {
                audio.volume = .15f;
                audio.Play();
            }
            else //crouch
            {
                audio.volume = .05f;
                audio.Play();
            }  
        }
	}
}
