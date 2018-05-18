using UnityEngine;
using UnityEngine.UI;

public class PauseText : MonoBehaviour {
    //hides or displays PAUSED text

    //text component
    private Text pausedText;

    //bool to keep track
    private bool paused;

	void Start () {
        //set component
        pausedText = GetComponent<Text>();

        //disable
        pausedText.enabled = false;

        //set paused to false
        paused = false;
	}

    #region Set-up Events

    //pause event acts as a toggle

    void OnEnable ()
    {
        FindObjectOfType<Manager>().pauseEvent += OnPause;
    }

    void OnDisable ()
    {
        FindObjectOfType<Manager>().pauseEvent -= OnPause;
    }

    #endregion

    void OnPause ()
    {
        //toggle between
        if (paused)
        {
            pausedText.enabled = false;

            paused = false;
        }
        else
        {
            pausedText.enabled = true;

            paused = true;
        }
    }
}
