using UnityEngine;
using System.Collections;

public class AgentBob : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");		
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
	}
}
