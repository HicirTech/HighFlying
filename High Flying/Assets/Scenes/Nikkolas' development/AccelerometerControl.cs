using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerControl : MonoBehaviour {

	[Tooltip("Drag and drop the character here")][SerializeField]
	GameObject character;
	
	// Update is called once per frame
	void Update () {
		character.transform.Translate(Input.acceleration.x, -Input.acceleration.z, 0);
	}
}
