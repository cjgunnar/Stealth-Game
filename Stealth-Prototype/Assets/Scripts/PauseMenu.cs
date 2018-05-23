using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    //hides or displays PAUSED text

    //text component
    private Image pausedPanel;

    //bool to keep track
    private bool paused;

	void Start ()
    {
        Debug.Log("child count: " + transform.childCount);

        //set component
        pausedPanel = gameObject.GetComponent<Image>();

        ClosePauseMenu();
	}

    #region Set-up Events

    //pause event acts as a toggle

    void OnEnable ()
    {
        FindObjectOfType<Manager>().pauseEvent += OnPause;
    }

    void OnDisable ()
    {
        //Debug.Log("PauseMenu disabled");
        FindObjectOfType<Manager>().pauseEvent -= OnPause;
    }

    #endregion

    #region Pause Actions

    public void OpenPauseMenu()
    {
        pausedPanel.enabled = true;

        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ClosePauseMenu()
    {
        pausedPanel.enabled = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    #endregion

    void OnPause ()
    {
        //toggle between
        if (paused)
        {
            ClosePauseMenu();
        }
        else
        {
            OpenPauseMenu();
        }

        //toggle pause
        paused = !paused;
    }
}
