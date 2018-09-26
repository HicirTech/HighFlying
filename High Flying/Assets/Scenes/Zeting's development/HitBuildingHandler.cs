using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBuildingHandler : MonoBehaviour {

	// Use this for initialization

	[SerializeField] AudioClip whenHit;

	void OnCollisionEnter(Collision col){
	
		
		if(col.gameObject.tag.Equals("Finish"))
		{
			print("fin");
			Invoke("Landing",2f);
		}
		else{
			print("hit");
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.PlayOneShot(this.whenHit);
		}
	}
	public void Landing()
	{
		SceneManager.LoadScene("MainPlay");
	}
}

