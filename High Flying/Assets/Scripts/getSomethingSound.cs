using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSomethingSound : MonoBehaviour {
	AudioSource source;
	[SerializeField]AudioClip clip;
	private void OnCollisionEnter(Collision collision)
    {
			playSound();		
    }

    private void OnTriggerEnter(Collider other)
    {
			playSound();
    }

		/// <summary>
		/// if get some thing, 
		/// add audiosource
		/// and play clip
		/// </summary>
		private void playSound()
		{
			source = this.gameObject.AddComponent<AudioSource>();
			source.playOnAwake=false;
			AudioListener.volume=1;
			source.PlayOneShot(clip);
		}
}
