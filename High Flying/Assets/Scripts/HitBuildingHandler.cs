using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBuildingHandler : MonoBehaviour {

    // Use this for initialization

    public System.Action onLevelComplete = delegate { };

	[SerializeField] AudioClip whenHit;

	void OnCollisionEnter(Collision col){
			
		if(col.gameObject.tag.Equals("Finish")) // if game finish
		{
			print("fin");
            LevelComplete();
		}
		else if(col.gameObject.tag.Equals("CoinToCollect"))// if hit point 
		{
			print("points");
		}
		else if(col.gameObject.tag.Equals("ColliderWall"))// if hif wall 
		{
			print("Hit on colliderwall");
		}
		else{ //hit any others
			print("hit");
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.PlayOneShot(this.whenHit);
		}
	}

    private void LevelComplete()
    {
        onLevelComplete();

    }

    public void Landing()
	{
		SceneManager.LoadScene("MainPlay");
	}
}

