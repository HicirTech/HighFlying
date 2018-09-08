using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicPlay : MonoBehaviour {

	// Use this for initialization
	AudioSource audioSource;
	[SerializeField] AudioClip music;
		void Start () {
		this.audioSource = GetComponent<AudioSource>();		
		Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Play()
	{
		this.audioSource.PlayOneShot(this.music);
	}
	private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
