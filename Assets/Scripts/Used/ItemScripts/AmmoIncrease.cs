using UnityEngine;
using System.Collections;

public class AmmoIncrease : MonoBehaviour {

	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			this.gameObject.SetActive(false);
		}
	}
}
