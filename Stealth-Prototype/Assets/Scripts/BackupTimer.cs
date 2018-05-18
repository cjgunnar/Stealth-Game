using System;
using UnityEngine;
using UnityEngine.UI;

public class BackupTimer : MonoBehaviour {

    //create an event
    public delegate void CallBackup();
    public CallBackup BackupCalled;

    //public variable of how long to count down for
    public float countdownTime;
    private bool outOfTime;

    //UI elements
    private Image background;
    private Text timer;
    private Text title;

	// Use this for initialization
	void Start () {
        background = GetComponent<Image>();

        //get text child (first one, zero position)
        timer = gameObject.transform.GetChild(0).GetComponent<Text>();

        //get title of the panel (second one, first position)
        title = gameObject.transform.GetChild(1).GetComponent<Text>();

        background.enabled = false;
        title.enabled = false;
        timer.enabled = false;

        outOfTime = false;
	}

    //subscribe and unsubscribe from events
    void OnEnable()
    {
        FindObjectOfType<Manager>().alarmEvent += OnAlarm;
    }

    void OnDisable()
    {
        FindObjectOfType<Manager>().alarmEvent -= OnAlarm;
    }

    void OnAlarm()
    {
        background.enabled = true;
        title.enabled = true;
        timer.enabled = true;
    }

    // Update is called once per frame
    void Update () {
		if (background.enabled)
        {
            //if this is the first frame, notify other scripts
            if (countdownTime <= 0)
            {
                if (!outOfTime)
                {
                    if (BackupCalled != null)
                    {
                        BackupCalled();
                        Debug.Log("BackupCalled() event executed");
                    }
                    outOfTime = true;
                }

                timer.text = "Backup has Arrived!";

            }

            //if the timer is not at 0, continue counting down
            else
            {
                countdownTime -= Time.deltaTime;
                timer.text = countdownTime.ToString("0.000");
            }


        }
	}
}
