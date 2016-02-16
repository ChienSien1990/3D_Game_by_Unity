using UnityEngine;
using System.Collections;

public class TripMineExplode : MonoBehaviour {

	public GameObject tripMine;
	public GameObject particleExplosion;
	AudioSource explosionSound;
	MeshRenderer mesh;
	BoxCollider box;

	// instantiation
	void Start(){
		explosionSound = GetComponent<AudioSource> ();
		mesh = GetComponent<MeshRenderer> ();
		box = GetComponent<BoxCollider> ();
	}
	// when the enemy steps on the tripmine
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "Enemy") {
			mesh.enabled = false;
			box.enabled = false;
			StartCoroutine(destroyMine());
		}
		if (obj.transform.tag == "Player") {
			Physics.IgnoreCollision(obj.collider, transform.collider);	
		}
	}

	IEnumerator destroyMine(){
		explosionSound.PlayOneShot(explosionSound.clip);
		Instantiate (particleExplosion, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1.5f);
		Destroy (gameObject);
	}
}
