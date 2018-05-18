using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class EnemyAnimation : MonoBehaviour {
    //controls animation for enemy character

    //variables
    public float maxSpeed;
    public float maxWalkSpeed;

    //animator component of enemy
    private Animator anim;

    //audio source component of enemy
    new private AudioSource audio;

    //sound files
    public AudioClip[] targetFound;
    public AudioClip[] targetFoundAgain;
    public AudioClip[] step;
    public AudioClip[] heardSomethingFirstTime;
    public AudioClip[] heardSomethingMultipleTimes;
    public AudioClip[] caught;
    public AudioClip[] lostPlayer;

    //different sound when he sees you again
    private bool isFirstSighting;
    
    //prevent audiofile spam
    public float timeBetweenBarks;
    private float timeSinceLastBark;
    private AudioClip selected;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        timeSinceLastBark = Time.time;
        isFirstSighting = true;

        AnimationIdle(); //incase he is at a post
    }

    //used by events in animation to play walk sound
    void WalkStep()
    {
        selected = step[Random.Range(0, step.Length)];
        audio.PlayOneShot(selected, 1.0f);
    }

    //animation controls
    public void AnimationWalk()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", true);
    }

    public void AnimationRun()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
    }

    public void AnimationIdle()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
    }

    public void SetAnimatorSpeed (float speed)
    {
        //turn into percent
        speed = speed / maxSpeed;
        Mathf.Clamp(speed, 0, 1);
        anim.SetFloat("Speed", speed);
    }

    //enemy barks
    public void Caught ()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
        selected = caught[Random.Range(0, caught.Length)];
        audio.PlayOneShot(selected, 1.0f);
    }

    public void ChasingBarks ()
    {
        if ((Time.time - timeSinceLastBark) > timeBetweenBarks)
        {
            if (audio.isPlaying)
            {
                audio.Stop();
            }
            if (isFirstSighting)
            {
                selected = targetFound[Random.Range(0, targetFound.Length)];
                isFirstSighting = false;
            }
            else
            {
                selected = targetFoundAgain[Random.Range(0, targetFoundAgain.Length)];
            }

            audio.PlayOneShot(selected, 1.0f);

            timeSinceLastBark = Time.time;
        }
    }

    public void PlayerSpottedBark ()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
        if (isFirstSighting)
        {
            selected = targetFound[Random.Range(0, targetFound.Length)];
            isFirstSighting = false;
        }
        else
        {
            selected = targetFoundAgain[Random.Range(0, targetFoundAgain.Length)];
        }
        audio.PlayOneShot(selected, 1.0f);
    }

    public void LostPlayer ()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        selected = lostPlayer[Random.Range(0, lostPlayer.Length)];

        if (selected != null)
        {
            audio.PlayOneShot(selected);
        }
        else
        {
            Debug.LogError("ERROR: Missing sound file!");
        }
    }

    //automatically chooses between first time and multiple time
    public void SomethingHeard ()
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }

        if (isFirstSighting)
        {
            selected = heardSomethingFirstTime[Random.Range(0, heardSomethingFirstTime.Length)];
        }
        else
        {
            if (heardSomethingMultipleTimes.Length != 0)
            {
                selected = heardSomethingMultipleTimes[Random.Range(0, heardSomethingMultipleTimes.Length)];
            }
            else
            {
                Debug.Log("Error: sound file not found");
            }

        }

        if (selected != null)
        {
            audio.PlayOneShot(selected, 1.0f);
        }
        else
        {
            Debug.LogError("Missing sound file!");
        }
    }
}
