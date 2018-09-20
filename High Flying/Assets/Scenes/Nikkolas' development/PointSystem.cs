using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour {

	[Tooltip("Toggle to enable or disable")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Number of points player has")][SerializeField]
	public float points;

	//Fill fields with GameObjects
	[Tooltip("Drag and drop the points text here: ")][SerializeField]
	private Text pointsUI;
	[Tooltip("Drag and drop the charcter here: ")][SerializeField]
	private GameObject character;

	//score = ((Amount of coins collected in a mission * 1.75) * (Amount of total rings passed * 1.75)) * ((Point ring multiplier added up with each being 2)^difficulty rating)
	public const float coinsCollMult = 1.75f;
	public const float ringsPassMult = 1.75f;
	public const float pointRMult = 2.0f;
	private int coinsCollectedCounter;
	private int ringsPassedCounter;
	private int pointRingCounter;
 	private int difficulty;

	// Use this for initialization
	void Start () {		
		if(pointsUI == null || character == null){
			print("Please fill the pointsUI text field and character GameObject field");
			enable = false;			
		}else{
			//Initiate all variables
			points = 1.0f;
			coinsCollectedCounter = 0;
			ringsPassedCounter = 0;
			pointRingCounter = 0;

			//Initiate the points text UI and fill
			pointsUI.text = "Points: "+points;

			//Get difficulty rating
			difficulty = character.GetComponent<HealthSystem>().difficultyRating;
		}
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
			Debug.Log("Point System currently paused");
		}
	}

	void OnTriggerEnter(Collider col){
		if(enable){
			//If you get a coin
			if(col.gameObject.tag == "CoinToCollect"){
				coinsCollectedCounter++;
				Debug.Log("Coin Collection - Collision Occured, coin collected counter increased to: "+coinsCollectedCounter);
			}
			//If you hit the point ring
			if(col.gameObject.tag == "PointRing"){
				pointRingCounter++;
				Debug.Log("Point ring passed - Collision Occured, point ring counter increased to: "+pointRingCounter);
			}
			//If you pass any ring
			if(col.gameObject.tag == "PointRing" || col.gameObject.tag == "CoinRing" || col.gameObject.tag == "LifeRing"){
				ringsPassedCounter++;
				Debug.Log("Ring passed - Collision Occured, rings passed counter increased to: "+ringsPassedCounter);
			}
		}else{
			Debug.Log("Point System's triggers currently paused");
		}
	}

	//Calculate the points that the player has based off this calculation:
	public void calculatePoints(){
		points = Mathf.Round((((coinsCollectedCounter+1)*coinsCollMult)*(ringsPassedCounter*ringsPassMult))*Mathf.Pow((pointRingCounter*pointRMult), difficulty));

		//Debug it
		//Debug.Log("Mathf.Round(((("+coinsCollectedCounter+"+1)*"+coinsCollMult+")"+
		//		  "*("+ringsPassedCounter+"*"+ringsPassMult+"))"+
		//		  "*Mathf.Pow(("+pointRingCounter+"*"+pointRMult+"),"+difficulty+"))");
	}

	//Change the text UI to display new point values
	void changePointsValue(){
		pointsUI.text = "Points: "+points;
	}

	public void enableThis(bool enableThis){
		this.enable = enableThis;
	}
	

	//Getters
	public int getCoinsCollectedCounter()
	{
		return this.coinsCollectedCounter;
	}
	public int getRingsPassedCounter()
	{
		return this.ringsPassedCounter;
	}
	public int getPointRingCounter(){
		return this.pointRingCounter;
	}
	public int getDifficulty()
	{
		return this.difficulty;
	}

	//Setters
	public void setCoinsCollectedCounter(int coinsCollected)
	{
		this.coinsCollectedCounter = coinsCollected;
	}
	public void setRingsPassedCounter(int ringsPassed)
	{
		this.ringsPassedCounter = ringsPassed;
	}
	public void setPointRingCounter(int pointRing)
	{
		this.pointRingCounter = pointRing;
	}
	public void setDifficulty(int diff)
	{
		this.difficulty = diff;
	}

}
