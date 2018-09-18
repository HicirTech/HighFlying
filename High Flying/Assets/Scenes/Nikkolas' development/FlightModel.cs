using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FlightModel : MonoBehaviour {

	[Tooltip("Tick to enable / disable")][SerializeField]
	bool enable;

	float velocity;
	Vector3 gravity;
	float forceUp;
	float mass;
	float directionalForce;


	// Use this for initialization
	void Start () {
		if(enable){
			velocity = 0;
			gravity = Physics.gravity;
			forceUp = 0;
			mass = GetComponent<Rigidbody>().mass;

			Debug.Log("Velocity is: "+velocity+", Gravity is: "+gravity+", forceUp is: "+forceUp+", and mass is: "+mass);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(enable){

			directionalForce = mass+(-1*(forceUp));

			GetComponent<Rigidbody>().AddForce(gravity*directionalForce);
			checkForwardButtonPressed();

			Debug.Log("Velocity is: "+velocity+", Gravity is: "+gravity+", forceUp is: "+forceUp+", and mass is: "+mass);
		}
	}

	//Check for user pressing forward button
	void checkForwardButtonPressed(){
		//Increase velocity
		if(Input.GetKeyDown(KeyCode.T)){
			Debug.Log("Keydown");
			if(velocity <= 10.0f){
				velocity += 10f * Time.deltaTime;
				if(forceUp <= GetComponent<Rigidbody>().mass-1){
					forceUp += 30f * Time.deltaTime;
				}else{
					forceUp = GetComponent<Rigidbody>().mass-1;
				}
			}else{
				velocity = 10.0f;
			}
		}else{
			if(velocity >= 1.0f){
				velocity -= 10f * Time.deltaTime;
				if(forceUp >= 0){
					forceUp -= 30f * Time.deltaTime;
				}else{
					forceUp = 0f;
				}
			}else{
				velocity = 1.0f;
			}
		}
	}
}
