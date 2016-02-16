using UnityEngine;
using System.Collections;

public class MaxHealthIncrease : MonoBehaviour {

	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			this.gameObject.SetActive(false);
		}
	}
}
