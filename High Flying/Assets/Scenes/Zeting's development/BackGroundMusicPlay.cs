using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicPlay : MonoBehaviour {

	// Use this for initialization
	AudioSource audioSource;
	[SerializeField] AudioClip music;
		
		void Start(){
		this.audioSource = GetComponent<AudioSource>();		
		PlayMusic();
		}
	

	void PlayMusic()
	{
		this.audioSource.PlayOneShot(this.music);
	}
	void StopPlay()
	{
		this.audioSource.Stop();
	}
	private void Awake()
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
