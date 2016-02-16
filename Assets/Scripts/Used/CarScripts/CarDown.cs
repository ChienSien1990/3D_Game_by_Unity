using UnityEngine;
using System.Collections;

public class CarDown : MonoBehaviour {
	public GameObject deathExplosion;
	public Transform[] DrivingPoints;
	public GameObject car;
	private float speed;
	private Vector3 spawningPoint;
	private int i = 0;
	private int damage = 0;
	private bool notColliding = true;
	// Use this for initialization
	void Start () {
		spawningPoint = transform.position;
		speed = Random.Range (8, 30);
	}
	
	// Update is called once per frame
	void Update () {
		if (damage >= 100) {
			Explode();	
		}
		if(notColliding){
			Drive ();
		}
	}
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "PlayerBullet") {
			damage += 10;	
		}
		if (obj.transform.tag == "EnemyBullet") {
			damage += 10;	
		}
		if (obj.transform.tag == "Player") {
			damage += 20;	
			//transform.position -= Vector3.forward;
		}
		if (obj.transform.tag == "Enemy") {
			damage += 20;	
			//transform.position -= Vector3.forward;
		}
		if(obj.transform.tag == "Car"){
			destroyObject();
		}
	}
	void OnCollision(Collision obj){
		if (obj.transform.tag == "Player") {
			//transform.position -= Vector3.forward*speed*Time.deltaTime;
			notColliding = false;
		}
		if (obj.transform.tag == "Enemy") {
			//transform.position -= Vector3.forward*speed*Time.deltaTime;
			notColliding = false;
		}
	}
	void OnCollisionExit(Collision obj){
		if (obj.transform.tag == "Player") {
			notColliding = true;
		}
		if (obj.transform.tag == "Enemy") {
			notColliding = true;
		}
	}

	void Explode(){
		destroyObject ();
	}
	void destroyObject(){
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		//get the object out of memory
		Destroy (gameObject);
	}
	void Drive(){

		transform.position -= Vector3.forward*speed*Time.deltaTime;

		if (transform.position.z < -83.0f) {
			transform.position = spawningPoint;	
			speed = Random.Range (8, 30);
		}

	}
}
