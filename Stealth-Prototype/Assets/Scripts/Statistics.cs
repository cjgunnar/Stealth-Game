using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

    public int timesSpotted;
    public bool activatedAlarm;

	// Use this for initialization
	void Start () {
        timesSpotted = 0;
        activatedAlarm = false;
	}

    //subscribe to events
    void OnEnable ()
    {
        FindObjectOfType<Manager>().SpotPlayer += AddSpotted;
        FindObjectOfType<Manager>().alarmEvent += OnAlarmActivated;
    }

    void OnDisable ()
    {
        FindObjectOfType<Manager>().SpotPlayer -= AddSpotted;
        FindObjectOfType<Manager>().alarmEvent -= OnAlarmActivated;
    }

    void AddSpotted ()
    {
        timesSpotted += 1;
    }
	
    void OnAlarmActivated ()
    {
        activatedAlarm = true;
    }

}
