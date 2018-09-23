using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBuildingHandler : MonoBehaviour {

    // Use this for initialization

    public System.Action onLevelComplete = delegate { };

	[SerializeField] AudioClip whenHit;

	void OnCollisionEnter(Collision col){
	
		
		if(col.gameObject.tag.Equals("Finish"))
		{
			print("fin");
            LevelComplete();
		}
		else{
			print("hit");
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.PlayOneShot(this.whenHit);
		}
	}

    private void LevelComplete()
    {
        onLevelComplete();
        //Invoke("Landing", 2f);
    }

    public void Landing()
	{
		SceneManager.LoadScene("MainPlay");
	}
}

