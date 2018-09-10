using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBuildingHandler : MonoBehaviour {

	// Use this for initialization

	[SerializeField] AudioClip whenHit;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}	
	void OnCollisionEnter(Collision col){
	
		print("hit");
		AudioSource audio = gameObject.GetComponent<AudioSource>();
		audio.PlayOneShot(this.whenHit);
	
	}
}

