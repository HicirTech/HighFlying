using InterSceneCommunication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour {

    private VariableContainer theContainer;
	[Tooltip("Toggle to enable or disable this script")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Invincability Mode on/off")][SerializeField]
	public bool invincability = true;
	[Tooltip("Difficulty Rating - Between 1 - 5")][SerializeField][Range(1, 5)]
	public int difficultyRating = 1;
	[Tooltip("Intitial counter for invincability in frames")][SerializeField][Range(1, 100)]
	public int initialInvincCount;
	[System.NonSerialized]
	public int health = 1;
	private string[] tagsToCheck = {"Respawn","MainCamera","Player","GameController","RTCam","CoinRing","LifeRing","Finish","PointRing",
									"CoinToCollect","ColliderWall"};

	//Fill variable with GameObject
	[Tooltip("Drag and drop the health text field here: ")][SerializeField]
	public Text healthUI;
    public System.Action onDie = delegate { };

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
		}
	}
	void Update () {
		if(enable){
			//If you're invincable, count down invince, if not, normal health and check for death
			healthUI.text = (!invincability) ? "Health: "+health+die() : "Health: "+initialInvinc();
		}else{
			healthUI.text = "";
			Debug.Log("Health System currently paused");
		}
	}
	string die(){
		//If health is equal or less to zero, then pop up end screen
		if(health <= 0){
            Time.timeScale = 0;
            onDie(); //Pop up death screen
		}
		return "";
	}
	void OnCollisionEnter(Collision col){
		if(enable){
			//if collider not found in the array, then it's not friendly
			if(!isInArray(tagsToCheck, col.gameObject.tag)){
				health -= 1;
				Debug.Log("Collision Occured"+col.gameObject.name+ "- Negate Life to: "+health);
			}else{
				Debug.Log("Hit something good");
			} 
		}
	}
	//Check if a given value is in an array
	public bool isInArray(string[] array, string a){
		bool query = false;
		foreach(string tag in array){
			if(a.Equals(tag)) return true;
		}
		return false;
	}
	void OnTriggerEnter(Collider col){
		if(enable){
			//If trigger with object with tag=Lifering: health is incremented
			if(col.gameObject.tag == "LifeRing" && invincability == false) health += 1;
			else Debug.Log("Health System's triggers currently paused");
		}
	}
	//Initial Invince calculator so when you start, you're initially invincable
	public string initialInvinc(){
		//If the initial start time is finished, then set invincability to false
		//Remove 1 per frame from counter of initial invincability
		if(initialInvincCount > 0) initialInvincCount -= 1;
		else if (initialInvincCount == 0){
			invincability = false;
			initialInvincCount = -1; //count finished, stop checking
			Debug.Log("Initial start invinc finished. Count set to: "+initialInvincCount+"\nInvincability now at "+invincability);
		}
		return "∞";
	}
	//Enable this function
	public void enableThis(bool enableThis){
		this.enable = enableThis;
	}
}