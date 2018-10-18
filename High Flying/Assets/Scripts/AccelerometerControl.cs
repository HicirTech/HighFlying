using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterSceneCommunication;

public class AccelerometerControl : MonoBehaviour {

	[SerializeField][Tooltip("Click to enable or disable")]
	private bool enable = true; //enable or disable the accelerometer
    private VariableContainer theContainer = null;
	private float accel;
	private bool stop = false;
	private string sideCheck;
	[SerializeField] private TextMesh debugText;


	void Start(){

        //If the variable container can not be found then tell the user, otherwise get and use the value
        if (GameObject.FindObjectsOfType<VariableContainer>().Length != 1)
        {
            Debug.Log("Could not find variable container");
        }
        else
        {
            theContainer = GameObject.FindObjectOfType<VariableContainer>();
        }

        //If the device supports accelerometer control and the user wants to utilise it then enable accelerometer control
        if (SystemInfo.supportsGyroscope && theContainer.isAccelerometerEnabled) 
        {
			enable=true;
			debugText.text="";
		}else{
 			enable = false;
			debugText.text="Phone does not support Gyroscope";
		}
	}

	// Update is called once per frame
	void Update () {
		if(enable){
			float accel = Input.acceleration.x;
			if(!stop){
				transform.Translate(accel, 0, 0);
			}else{
				//check for the side that has been collided with
				switch(sideCheck){
					case("right"):
						if(accel < 0) stop = false;
						break;
					case("left"):
						if(accel > 0) stop = false;
						break;
					default:
						throw new System.Exception("For some fucking reason, you're hitting something other than right or left.... which is impossible");
						break;
				}
			}
			//Debug some info on screen
			//debugText.text = "Acceleration: "+accel+"\nX Position: "+transform.position.x+"\nY Position: "+transform.position.y+"\nFrozen: "+stop;
		}
		//If it's disabled, do nothing
	}

	void OnCollisionEnter(Collision col){
		if(enable){
			//if collide, stop
			if(col.gameObject.tag=="ColliderWall") stop = true;accel=0;
			//get collider name
			if(col.gameObject.name == "right" || col.gameObject.name == "left") sideCheck = col.gameObject.name;
		}else{
			print("accelerator system is disabled");
		}
	}
}
