using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

	// create a transform at where the player is holding his weapon... bullets will spawn from the weapon
	public Transform bulletSpawn;
	public Transform tripMineSpawn;
	public GameObject bulletBall;
	public GameObject tripMine;

	//variables
	public float bulletImpulse = 25.0f;
	public void shoot(){

		//need to cast the Instantiated object as a game object
		GameObject theBullet = (GameObject)Instantiate (bulletBall, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		// this propels the bullet forward, however it is effected by physics so if it does not move fast, it will fall to the ground
		theBullet.rigidbody.AddForce (transform.forward * bulletImpulse, ForceMode.Impulse);
		/*Ray ray = new Ray (bulletSpawn.position, bulletSpawn.forward);
		RaycastHit hit;
		float shotDistance = 20.0f;
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
		if(Physics.Raycast (ray, out hit, shotDistance)){
			shotDistance = hit.distance;
		}

		// This line should draw the lazer shot but there is an error I cannot find
		Debug.DrawRay(ray.origin, ray.direction = forward, Color.red, 1);*/
	}



	public void dropMine(){
		GameObject theTripMine = (GameObject)Instantiate (tripMine, tripMineSpawn.transform.position, tripMineSpawn.transform.rotation);
		//Destroy (theTripMine);
	}
}
