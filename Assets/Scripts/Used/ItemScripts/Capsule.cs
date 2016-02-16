using UnityEngine;
using System.Collections;

public class Capsule : MonoBehaviour {

	public PlayerActions player;
	
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			player.CapsulePickUp();
			this.gameObject.SetActive(false);
			if(Application.loadedLevelName == "CityLevel"){
				CityLevel.capStolen = true;
			}

			if(Application.loadedLevelName == "Level6"){
				Level6Map.level6capsuleStolen = true;
			}
		}
	}
}
