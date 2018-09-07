using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour {

	[Tooltip("Enable or disable this script")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Value for coins")][SerializeField]
	public int coins;
	private Text coinsUI;
	private Text pointsUI;
	private Vector3 pointsUIPosition;
	private Vector3 newPointsUIPos;

	// Use this for initialization
	void Start () {
		//Initiate values
		coins = 0;

		coinsUI = GameObject.Find("/IngameUI/Coins").GetComponent<Text>();
		coinsUI.text = "Coins: "+coins;

		pointsUI = GameObject.Find("/IngameUI/Points").GetComponent<Text>();
		pointsUIPosition = pointsUI.transform.position;		
		newPointsUIPos = new Vector3(0.0f, -30.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(enable){
			changeCoinValue(coins);
			pointsUI.transform.position = pointsUIPosition;
		}else{
			coinsUI.text = "";
			pointsUI.transform.position = pointsUIPosition-newPointsUIPos;
		}
	}


	//Main Functions
	void changeCoinValue(int currentCoints){
		coinsUI.text = "Coins: "+currentCoints;
	}

	//Detect if get coins
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "CoinRing"){
			coins += 5;
			Debug.Log("Coin ring passed - Collision Occured, coins increased to: "+coins);
		}
		if(col.gameObject.tag == "CoinToCollect"){
			coins += 1;
			Debug.Log("Coin collected - Collision Occured, coin increased to: "+coins);
		}
	}
}
