using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

	[Tooltip("Enable or disable this script")][SerializeField]
	public bool enable = true;
	[Tooltip("Value for coins")][SerializeField]
	public int coins;
	private TextMesh textObject;

	// Use this for initialization
	void Start () {
		//Initiate values
		coins = 0;

		textObject = GameObject.Find("/Character/Coins").GetComponent<TextMesh>();
		textObject.text = "Coins: "+coins;
	}
	
	// Update is called once per frame
	void Update () {
		if(enable == true){
			changeCoinValue(coins);
		}else if(enable == false){
			textObject.text = "";
		}
	}


	//Main Functions
	void changeCoinValue(int currentCoints){
		textObject.text = "Coins: "+currentCoints;
	}

	//Detect if get coins
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "CoinRing" || col.gameObject.tag == "CoinToCollect"){
			coins += 1;
			Debug.Log("Coin collected - Collision Occured, coins increased to: "+coins);
		}
	}
}
