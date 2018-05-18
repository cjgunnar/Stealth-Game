using UnityEngine;

public class AlarmPanel : MonoBehaviour {
    //controlling script for alarm panel object

    private Manager manager;

    private bool alarmActive;

    Animator anim;

	void Start () {
        manager = FindObjectOfType<Manager>();

        anim = GetComponent<Animator>();

        alarmActive = false;
	}

    //subscribe to events on enabling
    void OnEnable ()
    {
        FindObjectOfType<Manager>().alarmEvent += OnAlarm;
    }

    //unsubscribe from events when disabled
    void OnDisable ()
    {
        FindObjectOfType<Manager>().alarmEvent -= OnAlarm;
    }
	
    void OnAlarm ()
    {
        if (!alarmActive)
        {
            //quick debug
            Debug.Log("alarm activated, changing animation");

            //set alarmActive variable to true so it only runs once
            alarmActive = true;

            //play alarm active animation
            anim.SetTrigger("Active");
        }
            
    }

    //when clicked on
    void OnMouseDown ()
    {
        if (!alarmActive)
        {
            //tell manager to disable alarm
            manager.DisableAlarm();

            //plays disable animation
            anim.SetTrigger("Disabled");

            Debug.Log("disabled alarm");
        }
        
    }

}
