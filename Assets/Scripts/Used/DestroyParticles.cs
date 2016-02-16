using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour {

	public float lifetime = 0f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
}
