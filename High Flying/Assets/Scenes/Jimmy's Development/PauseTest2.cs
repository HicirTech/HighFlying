using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest2 : MonoBehaviour {

    public static bool pause;
    //public GameObject PauseMenuPanel;

    // Use this for initialization
    void Start()
    {
        pause = false;
        //PauseMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause;
        }

        if (pause == true)
        {
            Time.timeScale = 0;
            Debug.Log("pause=true " + Time.timeScale);
            //PauseMenuPanel.SetActive(true);
        }
        else if (pause == false)
        {
            Time.timeScale = 1;
            Debug.Log("pause=false " + Time.timeScale);
            //PauseMenuPanel.SetActive(false);
        }
    }
}
