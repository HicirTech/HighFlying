using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {

	[Tooltip("Toggle to enable or disable")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Number of points player has")][SerializeField]
	public float points;
	private Text pointsUI;

	//score = ((Amount of coins collected in a mission * 1.75) * (Amount of total rings passed * 1.75)) * ((Point ring multiplier added up with each being 2)^difficulty rating)
	
	[Tooltip("This is the coins collected multiplier")][SerializeField][Range(0.0f, 5.0f)]
	private float coinsCollMult = 1.75f;
	[Tooltip("This is the rings passed multiplier")][SerializeField][Range(0.0f, 5.0f)]
	private float ringsPassMult = 1.75f;
	[Tooltip("This is the point ring multiplier")][SerializeField][Range(0.0f, 10.0f)]
	private float pointRMult = 2.0f;
	private int coinsCollectedCounter;
	private int ringsPassedCounter;
	private int pointRingCounter;
 	private int difficulty;

	// Use this for initialization
	void Start () {
		//Initiate all variables
		points = 1.0f;
		coinsCollectedCounter = 0;
		ringsPassedCounter = 0;
		pointRingCounter = 0;

		//Initiate the points text UI and fill
		pointsUI = GameObject.Find("/IngameUI/Points").GetComponent<Text>();
		pointsUI.text = "Points: "+points;

		//Get difficulty rating
		difficulty = GameObject.Find("/Character").GetComponent<HealthSystem>().difficultyRating;
	}
	
	// Update is called once per frame
	void Update () {
		//If script is enabled
		if(enable){
			calculatePoints();
			changePointsValue();
		}
		else{
			pointsUI.text = "";
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "CoinToCollect"){
			coinsCollectedCounter++;
			Debug.Log("Coin Collection - Collision Occured, coin collected counter increased to: "+coinsCollectedCounter);
		}
		if(col.gameObject.tag == "PointRing"){
			pointRingCounter++;
			Debug.Log("Point ring passed - Collision Occured, point ring counter increased to: "+pointRingCounter);
		}
		if(col.gameObject.tag == "PointRing" || col.gameObject.tag == "CoinRing" || col.gameObject.tag == "LifeRing"){
			ringsPassedCounter++;
			Debug.Log("Ring passed - Collision Occured, rings passed counter increased to: "+ringsPassedCounter);
		}
	}

	void calculatePoints(){
		points = Mathf.Round((((coinsCollectedCounter+1)*coinsCollMult)*(ringsPassedCounter*ringsPassMult))*Mathf.Pow((pointRingCounter*pointRMult), difficulty));
		Debug.Log("Mathf.Round(((("+coinsCollectedCounter+"+1)*"+coinsCollMult+")"+
				  "*("+ringsPassedCounter+"*"+ringsPassMult+"))"+
				  "*Mathf.Pow(("+pointRingCounter+"*"+pointRMult+"),"+difficulty+"))");
	}

	void changePointsValue(){
		pointsUI.text = "Points: "+points;
	}

}
