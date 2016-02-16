using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, 0, 30) * 5 *  Time.deltaTime);
	
	}

	// if the player picks up the object
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			CoinCounter.coinsCollected++;
			Destroy(gameObject);	
		}
	}
}
