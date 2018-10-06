using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		float accel = Input.acceleration.x;
		transform.Translate(accel, 0, 0);
	}
}
