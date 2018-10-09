using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSomethingSound : MonoBehaviour {
	AudioSource source;
	[SerializeField]AudioClip clip;
	private void OnCollisionEnter(Collision collision)
    {
		source = this.gameObject.AddComponent<AudioSource>();
		source.playOnAwake=false;
        print("C");
		AudioListener.volume=1;
		source.PlayOneShot(clip);
		
    }

    private void OnTriggerEnter(Collider other)
    {
		source = this.gameObject.AddComponent<AudioSource>();
		source.playOnAwake=false;
		print("T");
		AudioListener.volume=1;
		source.PlayOneShot(clip);
    }
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
