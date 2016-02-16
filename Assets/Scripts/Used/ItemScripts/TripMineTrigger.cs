using UnityEngine;
using System.Collections;

public class TripMineTrigger : MonoBehaviour {
	public TripMineExplode tripMine;

	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Enemy") {
			Destroy (gameObject);
		}
	}
}
