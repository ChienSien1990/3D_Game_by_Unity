using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	private Vector3 cameraTarget;
	private Transform target;

	// Use this for initialization
	void Start () {
		// at start grab the player's position because we will be following it with the camera
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		//backView = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z + 10);
		//rightView = new Vector3 (target.transform.position.x + 10, transform.position.y, target.transform.position.z);
		//leftView = new Vector3 (target.transform.position.x - 10, transform.position.y, target.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		// follow the player target at speed over seconds
		cameraTarget = new Vector3 (target.transform.position.x, target.transform.position.y + 20, target.transform.position.z - 10);
		transform.position = Vector3.Lerp (transform.position, cameraTarget, Time.deltaTime * 8);

	}
}
