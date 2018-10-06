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
		else if(col.gameObject.tag.Equals("CoinToCollect")){
			print("points");
		}
		else if(col.gameObject.tag.Equals("ColliderWall")){
			print("Stopped on colliderwall");
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

