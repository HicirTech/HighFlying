using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBuildingHandler : MonoBehaviour {

	// Use this for initialization

	[SerializeField] AudioClip whenHit;

	void OnCollisionEnter(Collision col){
	
		print("hit");
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		audio.PlayOneShot(this.whenHit);
	
	}
}

