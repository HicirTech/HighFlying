using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

	[Tooltip("Toggle to enable or disable this script")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Invincability Mode on/off")][SerializeField]
	public bool invincability = true;
	[Tooltip("Difficulty Rating - Between 1 - 5")][SerializeField][Range(1, 5)]
	public int difficultyRating = 1;
	[Tooltip("Intitial counter for invincability in frames")][SerializeField][Range(1, 100)]
	public int initialInvincCount;
	private int savedInitialInvinc;
	[System.NonSerialized]
	public int health = 1;

	//Fill variable with GameObject
	[Tooltip("Drag and drop the health text field here: ")][SerializeField]
	public Text healthUI;
	

	// Use this for initialization
	void Start () {
		if(healthUI == null){
			print("Please fill the healthUI text field");
			enable = false;
		}else{
			//Initiate the variables
			health = -difficultyRating+6;
			invincability = true;

			//Initiate the text
			healthUI.text = "Health: "+health;

			if(savedInitialInvinc == null){
				savedInitialInvinc = initialInvincCount; //If it's the first time the level starts then saved dev set initialInvincCount
			}else{
				initialInvincCount = savedInitialInvinc; //If not, then use saved intial invinc count to set initial invinc count
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enable){
			initialInvinc();
			if(!invincability){
				//Restart game on death
				die();
				//Update health text every frame
				changeHealthValue();
			}else{
				healthUI.text = "Health: ∞";
			}
			
			//health = -difficultyRating+6; //For developing. Turn this on to slide difficulty rating and see health change
		}
		else{
			healthUI.text = "";
			Debug.Log("Health System currently paused");
		}
		
	}

	void changeHealthValue(){
		healthUI.text = "Health: "+health;
	}

	void die(){
		//If health is equal or less to zero, then restart the scene/level
		//Change this code with respawn or different code when a failure screen is up
		if(health <= 0){

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Debug.Log("Scene Reloaded");
		}
	}

	void OnCollisionEnter(Collision col){
		if(enable){
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
					Debug.Log("Collision Occured - Negate Life to: "+health);
			}
		}else{
			Debug.Log("Health System's collisions currently paused");
		}
	}

	void OnTriggerEnter(Collider col){
		if(enable){
			if(col.gameObject.tag == "LifeRing" && invincability == false){
				//Increase health by 1
				health += 1;
				Debug.Log("Collision Occured - Life ring collision, life increased to: "+health);
			}
		}else{
			Debug.Log("Health System's triggers currently paused");
		}
	}

	void initialInvinc(){
		//If the initial start time is finished, then set invincability to false
		//Remove 1 per frame from counter of initial invincability
		if(initialInvincCount != 0 && initialInvincCount > 0){
			initialInvincCount -= 1;
		}else if (initialInvincCount == 0){
			invincability = false;
			initialInvincCount = -1; //count finished, stop checking
			Debug.Log("Initial start invinc finished. Count set to: "+initialInvincCount+"\nInvincability now at "+invincability);
		}
	}

	public void enableThis(bool enableThis){
		this.enable = enableThis;
	}
}

