using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinSystem : MonoBehaviour {

	[Tooltip("Enable or disable this script")][SerializeField]
	public bool enable = true; //Variable used for pause functionality
	[Tooltip("Value for coins")][SerializeField]
	public int coins;

	[Tooltip("Drag and drop coins text here: ")][SerializeField]
	private Text coinsUI;

	// Use this for initialization
	void Start () {
		if(coinsUI == null){
			print("Please fill the coins text field");
			enable = false;
		}else{
			//Initiate values
			coins = 0;

			//Initiate UI text
			coinsUI.text = "Coins: "+coins;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(enable){
			changeCoinValue(coins);
		}else{
			coinsUI.text = "";
			Debug.Log("Coin System currently paused");
		}
	}


	//Main Functions
	void changeCoinValue(int currentCoints){
		coinsUI.text = "Coins: "+currentCoints;
	}

	//Detect if get coins
	void OnTriggerEnter(Collider col){
		if(enable){
			if(col.gameObject.tag == "CoinRing"){
				coins += 5;
				Debug.Log("Coin ring passed - Collision Occured, coins increased to: "+coins);
			}
			if(col.gameObject.tag == "CoinToCollect"){
				coins += 1;
				Debug.Log("Coin collected - Collision Occured, coin increased to: "+coins);
			}
		}else{
			Debug.Log("Coin system's triggers currently paused");
		}
	}

	public void enableThis(bool enableThis){
		this.enable = enableThis;
	}
}
