using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {

	private float accel;
	private bool stop = false;
	private string sideCheck;
	[SerializeField] private TextMesh debugText;

	// Update is called once per frame
	void Update () {
		float accel = Input.acceleration.x;
		if(!stop){
			transform.Translate(accel, 0, 0);
		}else{
			switch(sideCheck){
				case("right"):
					if(accel < 0) stop = false;
					break;
				case("left"):
					if(accel > 0) stop = false;
					break;
				default:
					break;
			}
		}

		debugText.text = "Acceleration: "+accel+"\nX Position: "+transform.position.x+"\nY Position: "+transform.position.y+"\nFrozen: "+stop;
	}

	void OnCollisionEnter(Collision col){
		//if collide, stop
		if(col.gameObject.tag=="ColliderWall") stop = true;accel=0;
		//get collider name
		if(col.gameObject.name == "right" || col.gameObject.name == "left") sideCheck = col.gameObject.name;
		
	}
}
