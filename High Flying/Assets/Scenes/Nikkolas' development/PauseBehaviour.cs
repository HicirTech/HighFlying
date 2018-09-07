using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour {

	[SerializeField]
	private GameObject pausePanel;
	private bool paused;



	// Use this for initialization
	void Start () {
		pausePanel.SetActive(false);
		paused = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			if(!paused){
				PauseGame();
			}
			if(paused){
				ContinueGame();
			}
		}
	}

	//Pause game. Here you have to individually disable scripts because they will keep running whilst timeScale = 0
	void PauseGame(){
		paused = true;
		Time.timeScale = 0;
		pausePanel.SetActive(true);

		
	}

	//Unpause game. Here you have to re-enable scripts because they will not start automatically with timeScale = 1
	void ContinueGame(){
		paused = false;
		Time.timeScale = 1;
		pausePanel.SetActive(false);
	}
}
