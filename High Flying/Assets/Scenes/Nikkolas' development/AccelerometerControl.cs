using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {

	[SerializeField]private float maxX = 1;
	
	// Update is called once per frame
	void Update () {
		float accel = Input.acceleration.x;
		transform.Translate(accel, 0, 0);
	}
}
