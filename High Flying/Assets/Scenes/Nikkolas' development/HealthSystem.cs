using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

	[Tooltip("Invincability Mode on/off")][SerializeField]
	public bool invincability = false;
	[Tooltip("Health Value - Between 1 - 5")][SerializeField][Range(1, 5)]
	public int health = 1;

	[Tooltip("Difficulty Rating - Between 1 - 5")][SerializeField][Range(1, 5)]
	public int difficultyRating = 1;
	private TextMesh textObject;

	// Use this for initialization
	void Start () {
		health = -difficultyRating+6;
		
		//Initiate the text
		textObject = GameObject.Find("/Character/Health").GetComponent<TextMesh>();
		textObject.text = "Health: "+health;
	}
	
	// Update is called once per frame
	void Update () {
		//y = -x + 6
		//health = -difficultyRating+6; - For developing. Turn this on to slide difficulty rating and see health change

		//If health is equal or less to zero, then restart the scene/level
		//Change this code with respawn or different code when a failure screen is up
		if(health <= 0){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Debug.Log("Scene Reloaded");
		}

		//Update health text every frame
		changeHealthValue(health);
	}


	void changeHealthValue(int currentHealthValue){
		textObject.text = "Health: "+currentHealthValue;
	}

	void OnCollisionEnter(Collision col){

		//if the collision is anything but all friendly tags and invincability is off.
		if(!(col.gameObject.tag == "Respawn" || 
			col.gameObject.tag == "Finish" || 
			col.gameObject.tag == "MainCamera" ||
			col.gameObject.tag == "Player" ||
			col.gameObject.tag == "GameController" ||
			col.gameObject.tag == "RTCam" ||
			col.gameObject.tag == "CoinRing" ||
			col.gameObject.tag == "LifeRing" ||
			col.gameObject.tag == "PointRing") && invincability == false){
				
				//Negate the health by 1
				health -= 1;
		}

		Debug.Log("Collision Occured");
	}

	//Debugging functions
	void OnCollisionExit(Collision collisionInfo){
		Debug.Log("Collision Out" + gameObject.name);
	}
}
