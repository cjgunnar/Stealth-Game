using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class EndGamePanel : MonoBehaviour {

    private Image endScreen;
    private Text title;
    private Text time;
    private Text detection;
    private Text alarm;

    //private Button mainMenu;
    private Button next;

    private Statistics stats;

	// Use this for initialization
	void Awake () {        
        endScreen = GetComponent<Image>();
        
        title = GameObject.Find("Condition Panel").GetComponent<Text>();
        time = GameObject.Find("Time").GetComponent<Text>();
        detection = GameObject.Find("Detected").GetComponent<Text>();
        alarm = GameObject.Find("Alarm").GetComponent<Text>();
        //mainMenu = GameObject.Find("Main Menu Button").GetComponent<Button>();
        next = GameObject.Find("Next Button").GetComponent<Button>();

        stats = FindObjectOfType<Statistics>();
        //Debug.Log(stats);
    }

    public void StartGame ()
    {
        endScreen.gameObject.SetActive(false);
    }
	
    public void DisplayGameLost ()
    {
        title.text = "FAILURE";
        title.color = Color.red;
        float timeToCompleteMin = Mathf.Round((Time.timeSinceLevelLoad) / 60);
        float timeToCompleteSec = Mathf.Round(Time.timeSinceLevelLoad) % 60;

        time.text = "Time Elapsed: " + timeToCompleteMin.ToString() + " min, " + timeToCompleteSec.ToString() + " sec";

        alarm.enabled = false;
        detection.enabled = false;

        next.interactable = false;
        next.gameObject.GetComponentInChildren<Text>().color = Color.gray;
    }

    public void DisplayGameWon ()
    {
        title.text = "SUCCESS";
        title.color = Color.green;
        float timeToCompleteMin = Mathf.Round((Time.timeSinceLevelLoad) / 60);
        float timeToCompleteSec = Mathf.Round(Time.timeSinceLevelLoad) % 60;

        time.text = "Time to Complete: " + timeToCompleteMin.ToString() + " min, " + timeToCompleteSec.ToString() + " sec";
        //time.text = "Time to Complete: " + timeToCompleteMin.ToString() + "min and " + timeToCompleteSec.ToString() + " seconds";

        if (!stats.activatedAlarm)
        {
            alarm.text = "Alarm never set off!";
            alarm.enabled = true;
        }
        else
        {
            alarm.text = "Alarm set off";
            alarm.enabled = true;
        }

        if (stats.timesSpotted >= 1)
        {
            detection.text = "Spotted " + stats.timesSpotted.ToString() + " times";
        }
        else
        {
            detection.text = "Undetected!";
        }
    }


}
