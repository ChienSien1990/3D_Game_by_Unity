using UnityEngine;
using System.Collections;

public class CloseDoor : MonoBehaviour {

	private MeshRenderer mesh;
	private BoxCollider box;

	// Use this for instantiation
	void Start(){

		// Get the two components need to make this disappear and reappear
		mesh = GetComponent<MeshRenderer> ();
		box = GetComponent<BoxCollider> ();
	}

	// Update is called once per frame
	void OnTriggerExit (Collider obj) {
		if (obj.transform.tag == "Player") {
			mesh.enabled = true;
			box.enabled = true;
		}
	}
}
