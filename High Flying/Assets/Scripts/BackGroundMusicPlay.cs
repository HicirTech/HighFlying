using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackGroundMusicPlay : MonoBehaviour {
	
	[SerializeField] AudioClip CityNoise;
	[SerializeField] AudioClip Wind;
	public bool isPlaying ;
	AudioClip music;
	AudioSource audioSource;
	Scene currentScene;
	string nameOfStart="";
	
	
	private void Awake()
    {
		//music will not play when it awake
		//will check double player in the game
		checkDouble();
		isPlaying=false;
	}

	void Start()
	{
		setupPlayer();
	}


	/// <summary>
	/// set up a audio source with clip 
	/// play loop of background music
	/// and call begin play function
	///  </summary>
	public void setupPlayer()
	{
		this.currentScene= SceneManager.GetActiveScene();
		this.audioSource = GetComponent<AudioSource>();		
		this.SetClipForPlay();
		this.audioSource.loop=true;
		this.PlayMusic();
	}
	/// <summary>
	/// if it is playing
	/// then stop play
	/// else
	/// begin play background music
	/// </summary>
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

	/// <summary>
	/// will check if game change level
	/// </summary>
	void Update()
	{
		this.CheckUpdate();			
	}

	
	/// <summary>
	/// play music when it is not playing
	/// </summary>
	/// 
	/// <returns>
	/// is the music playing
	/// </returns>
	private bool PlayMusic()
	{
		if(!this.isPlaying)
		{
			this.audioSource.Play();
			this.isPlaying=!this.isPlaying;
		}
		return isPlaying;
	}

	/// <summary>
	/// check what level the game is 
	/// and based on it switch music
	/// </summary>
	private void CheckUpdate()
	{ 

		this.currentScene= SceneManager.GetActiveScene();
		
		if(!currentScene.name.Equals(nameOfStart))//if this change happened
		{
			if((nameOfStart.Contains("Main")&&currentScene.name.Contains("City"))||currentScene.name.Contains("City")||currentScene.name.Contains("Main"))
			{	
				this.DoUpdateMusic();
			}
		}
	}

	/// <summary>
	/// update current music
	/// </summary>
	private void DoUpdateMusic()
	{
		this.StopPlay();;
		this.SetClipForPlay();
		this.PlayMusic();
		nameOfStart=this.currentScene.name;
	}

	/// <summary>
	/// base on city level change to city noise
	/// or other level will paly wind sound
	/// </summary>
	private void SetClipForPlay()
	{
		this.audioSource.clip=(this.currentScene.name.Contains("City"))?CityNoise:Wind;
	}
	

	/// <summary>
	/// stop playing music
	/// </summary>
	/// 
	/// <returns>
	/// is it playing
	/// </returns>
	private bool StopPlay()
	{
		if(this.isPlaying)
		{
			this.audioSource.Stop();
			this.isPlaying=!this.isPlaying;
		}
		return isPlaying;
	}


	/// <summary>
	/// check if a level have two 
	/// backgroundmusicplayer
	/// if yes destory one
	/// else keep it
	/// </summary>
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
