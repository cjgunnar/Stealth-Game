using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmButton : MonoBehaviour {

    private SelectionPanelController select;

    private GameObject player;
    private Manager manager;
    private float selectionDistance;
    private bool playerIsActive;
    private bool isAlarmTriggered;

    // Use this for initialization
    void Start () {

        //objects and variables
        manager = FindObjectOfType<Manager>();
        player = GameObject.FindGameObjectWithTag("Player");
        selectionDistance = player.GetComponent<PlayerController>().playerReach;

        //subscribe to events
        manager.alarmEvent += OnAlarmActivate;
        manager.GameOver += OnGameOver;

        //start game with active player and no alarm
        playerIsActive = true;
        isAlarmTriggered = false;

        //UI
        select = FindObjectOfType<SelectionPanelController>();
    }
	
    void OnGameOver ()
    {
        playerIsActive = false;

        //unsubscribe when done
        manager.GameOver -= OnGameOver;
    }

    void OnAlarmActivate ()
    {
        isAlarmTriggered = true;

        //unsubscribe when done
        manager.alarmEvent -= OnAlarmActivate;
    }

    void OnMouseOver ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < selectionDistance && playerIsActive)
        {
            if (!isAlarmTriggered)
            {
                select.Display("Trigger Alarm", Color.white);

            }
            else
            {
                select.Close();
            }
        }
    }

    void OnMouseDown ()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < selectionDistance && !isAlarmTriggered && playerIsActive)
        {
            manager.TriggerAlarm();
        }
    }

    public void EnemyTriggerAlarm ()
    {
        if (!isAlarmTriggered)
        {
            manager.TriggerAlarm();
        }
    }


    void OnMouseExit ()
    {
        select.Close();
    }

}
