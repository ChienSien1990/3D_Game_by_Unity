using UnityEngine;
using System.Collections;

public class MerchantTable : MonoBehaviour {

	private bool showEntrance = false;
	private int level;
	
	void OnGUI(){
		if (showEntrance == true) {
			GUI.Label(new Rect(250,15, 200, 50), "What do you want eh?");
			if(PlayerActions.shotGunUnlocked == false){
				if (GUI.Button (new Rect (250,50,200,50), "ShotGun   20 C")) {
					//code to start first level
					PlayerActions.shotGunUnlocked = true;
					CoinCounter.coinsCollected -= 20;
				}
			}
			if(PlayerActions.blackUnlocked == false){
				if (GUI.Button (new Rect (250,100,200,50), "Black Shirt   10 C")) {
					//code to start first level
					PlayerActions.blackUnlocked = true;
					CoinCounter.coinsCollected -= 10;
				}
			}
			if(PlayerActions.cyanUnlocked == false){
				if (GUI.Button (new Rect (250,150,200,50), "Cyan Shirt   10 C")) {
					//code to start first level
					PlayerActions.cyanUnlocked = true;
					CoinCounter.coinsCollected -= 10;
				}
			}
			if(PlayerActions.greenUnlocked == false){
				if (GUI.Button (new Rect (250,200,200,50), "Green Shirt   10 C")) {
					//code to start first level
					PlayerActions.greenUnlocked = true;
					CoinCounter.coinsCollected -= 10;
				}
			}
			if(PlayerActions.whiteUnlocked == false){
				if (GUI.Button (new Rect (250,250,200,50), "White Shirt   10 C")) {
					//code to start first level
					PlayerActions.whiteUnlocked = true;
					CoinCounter.coinsCollected -= 10;
				}
			}
			/*if (GUI.Button (new Rect (200,100,200,50), "New Survival Level")) {
				//code to start first level
				PlayerActions.shotGunUnlocked = true;
			}*/
		}
	}
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			showEntrance = true;	
		}
	}
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			showEntrance = false;	
		}
	}
}
