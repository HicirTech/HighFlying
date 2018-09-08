using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour {

	[SerializeField]
	private GameObject pausePanel;
	private bool paused;

	private CoinSystem cSys;
	private HealthSystem hSys;
	private PointSystem pSys;
	
	Scene currentScene;

	// Use this for initialization
	void Start () {
		pausePanel.SetActive(false);
		paused = false;

		//get current scene for pausing functionality
		currentScene = SceneManager.GetActiveScene();
		Debug.Log("Current Scene: "+currentScene.name);
		
		//Initiate all scripts
		cSys = GameObject.Find("/Character").GetComponent<CoinSystem>();
		hSys = GameObject.Find("/Character").GetComponent<HealthSystem>();
		pSys = GameObject.Find("/Character").GetComponent<PointSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		//Only allow pausing in certain scenes
		if(currentScene.name == "City Level" || currentScene.name == "Ice Level"){
			
		}
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

		cSys.enableThis(false);
		hSys.enableThis(false);
		pSys.enableThis(false);
	}

	//Unpause game. Here you have to re-enable scripts because they will not start automatically with timeScale = 1
	void ContinueGame(){
		paused = false;
		Time.timeScale = 1;
		pausePanel.SetActive(false);

		cSys.enableThis(true);
		hSys.enableThis(true);
		pSys.enableThis(true);
	}
}
