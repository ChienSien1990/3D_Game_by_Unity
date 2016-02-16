using UnityEngine;
using System.Collections;

public class DestroyBullets : MonoBehaviour {

	public float lifetime = 4.0f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	// attempting to destroy the bullet when it hits the enemy so it doesn't continue flying after it collides with enemy
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "Enemy") {
			Destroy(gameObject);	
		}
		if (obj.transform.tag == "Player") {
			Destroy(gameObject);	
		}
		if (obj.transform.tag == "Building") {
			Destroy(gameObject);	
		}
	}
}
