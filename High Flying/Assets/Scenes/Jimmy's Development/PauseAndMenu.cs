using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PauseAndMenu : MonoBehaviour {
    public static bool pause;
    public GameObject PauseMenuPanel;
  
	// Use this for initialization
    void Start () {
        pause = false;
        PauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pauseAndResume()
    {
        pause = !pause;
        if (pause==true)
        {
            Time.timeScale = 0;
            PauseMenuPanel.SetActive(true);
        }
        else if (pause==false)
        {
            Time.timeScale = 1;
            PauseMenuPanel.SetActive(false);
        }
    }
}
