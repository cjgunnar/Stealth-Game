﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayLevel1 ()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitApplication ()
    {
        Application.Quit();
    }



}
