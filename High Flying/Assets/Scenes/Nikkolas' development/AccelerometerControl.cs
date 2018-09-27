using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {
<<<<<<< HEAD

	[Tooltip("Drag and drop the character here")][SerializeField]
	GameObject character;
	
	// Update is called once per frame
	void Update () {
		character.transform.Translate(Input.acceleration.x, -Input.acceleration.z, 0);
=======
	// Update is called once per frame
	void Update () {
		transform.Translate(Input.acceleration.x, Input.acceleration.y, 0);
>>>>>>> Jimmy-dev
	}
}
