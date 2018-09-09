using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicPlay : MonoBehaviour {

	// Use this for initialization
	AudioSource audioSource;
	private bool isPlaying{get;set;}
	[SerializeField] AudioClip music;
		
		void Start(){
		this.audioSource = GetComponent<AudioSource>();		
		PlayMusic();
		}
	public void switchPlay()
	{
		if(this.isPlaying)
		{
			this.StopPlay();
		}
		else
		{
			this.PlayMusic();
		}
	}


	private void PlayMusic()
	{
		if(!this.isPlaying)
		{
			this.audioSource.PlayOneShot(this.music);
			this.isPlaying=!this.isPlaying;
		}
	}

	private void StopPlay()
	{
		if(this.isPlaying)
		{
			this.audioSource.Stop();
			this.isPlaying=!this.isPlaying;
		}
	}
	private void Awake()
    {
		checkDouble();
	}

	private void checkDouble()
	{
		if(GameObject.FindObjectsOfType<BackGroundMusicPlay>().Length>1)
			{
				Destroy(gameObject);
			}
			else{
				DontDestroyOnLoad(this.gameObject);
			}
	}
}
