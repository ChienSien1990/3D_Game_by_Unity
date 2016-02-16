using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour {

	private GameObject target;
	// Use this for initialization
	void Start () {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player");	
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
	}
}
