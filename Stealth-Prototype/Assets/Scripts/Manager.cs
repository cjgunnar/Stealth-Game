using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Manager : MonoBehaviour {

    [HideInInspector]
    public bool gameLost = false;
    [HideInInspector]
    public bool gameWon = false;

    private bool alarmTriggered = false;
    private bool alarmDisabled = false;

    private bool gamePaused = false;

    //game events
    public delegate void AlarmActivate();
    public AlarmActivate alarmEvent;

    public delegate void EndGame();
    public EndGame GameOver;

    //create events for statistics
    public delegate void PlayerSpot();
    public PlayerSpot SpotPlayer;

    public delegate void GamePause();
    public GamePause pauseEvent;

    //end game screen controller
    private EndGamePanel endScreen;

    //game objects
    private GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        endScreen = FindObjectOfType<EndGamePanel>();

        endScreen.StartGame();
    }

    public void DisableAlarm ()
    {
        alarmDisabled = true;
    }

    //triggers event for listeners
    public void TriggerAlarm()
    {
        if (!alarmTriggered && !alarmDisabled)
        {
            
            if (alarmEvent != null)
            {
                alarmEvent();
                Debug.Log("alarm event triggered");
            }

            alarmTriggered = true;
        }
        
    }

    //can replace with events
    public void LostGame ()
    {
        if (!gameLost && !gameWon)
        {
            player.tag = "Untagged";
            endScreen.gameObject.SetActive(true);
            endScreen.DisplayGameLost();
            gameLost = true;
            Cursor.lockState = CursorLockMode.None;
            GameOver();
            Debug.Log("game over event triggered: lost");
        }
    }

    //trigger events and misc
    public void WonGame()
    {
        if (!gameWon && !gameLost)
        {
            GameOver();
            Debug.Log("game over event triggered: won");
            //Debug.Log("Player wins!");
            gameWon = true;

            endScreen.gameObject.SetActive(true);
            endScreen.DisplayGameWon();

            Cursor.lockState = CursorLockMode.None;

            player.tag = "Untagged";
            player.GetComponent<Footsteps>().enabled = false;
        }
    }

    public void PlayerSpotted ()
    {
        //send statistic of spotting player
        if (SpotPlayer != null)
        {
            SpotPlayer();
        }
    }

    #region Game Functions and Scene Management

    //pauses or unpauses game using timeScale and controls cursor lock
    public void PauseGame ()
    {
        //don't pause when game is over
        if (gameWon || gameLost)
            return;

        //send event so that other components can magange their own toggles
        if (pauseEvent != null)
        {
            pauseEvent();
        }

        //toggle pause state
        if (!gamePaused)
        {
            //pause game
            Time.timeScale = 0;
            
            //unlock cursor
            Cursor.lockState = CursorLockMode.None;
            
            //set state to paused
            gamePaused = true;
        }
        else
        {
            //unpause game
            Time.timeScale = 1;

            //restrict cursor to center of screen
            Cursor.lockState = CursorLockMode.Locked;

            //set state to unpaused
            gamePaused = false;
        }
    }

    public void GoToMainMenu ()
    {
        //Debug.Log("returning to main menu");
        SceneManager.LoadScene("Main Menu");
    }

    public void NextLevel ()
    {
        //Debug.Log("load next level...");
    }

    public void RetryLevel ()
    {
        //reloads the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion
}
