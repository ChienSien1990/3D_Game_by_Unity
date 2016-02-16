using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour {

	public GameObject explosion;
	private Vector3 wayPoint;
	private float Xaxis;
	private float Zaxis;
	private float speed;
	private int startPos;
	private int health;
	private int randomSpawn;
	private bool positionFound;
	private float distance;
	private int colorChoose;
	// Use this for initialization
	void Start () {
		startPos = Random.Range (0, 5);
		speed = Random.Range (1, 7);
		health = 50;
		positionFound = false;
		colorChoose = Random.Range (0, 4);
		if (colorChoose == 0) {
			renderer.material.color = Color.cyan;	
		}
		if (colorChoose == 1) {
			renderer.material.color = Color.magenta;	
		}
		if (colorChoose == 2) {
			renderer.material.color = Color.yellow;	
		}
		if (colorChoose == 3) {
			renderer.material.color = Color.green;	
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (positionFound == false) {
			Xaxis = Random.Range(-99,99);
			Zaxis = Random.Range (-85,115);
			wayPoint = new Vector3(Xaxis, 0.65f, Zaxis);
			transform.LookAt (wayPoint);
			//print ("New waypoint");
			positionFound = true;
		}

		wander ();

		if (health <= 0 || transform.position.y < -10.0f) {
			Die ();	
		}
	}

	void wander(){
		//print ("wandering towards wayPoint!");
		distance = Vector3.Distance (transform.position, wayPoint);
		if (distance <= 20) {
			//print ("found wayPoint!");
			positionFound = false;
		}

		transform.position = Vector3.MoveTowards (transform.position, wayPoint, speed * Time.deltaTime);
	}
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "Car") {
			Die ();	
		}
		if (obj.transform.tag == "PlayerCar") {
			Die ();	
		}

		if (obj.transform.tag == "Building") {
			EnterBuilding();
		}

		if (obj.transform.tag == "PlayerBullet") {
			Die ();	
		}
		if (obj.transform.tag == "EnemyBullet") {
			Die ();	
		}
	}
	void EnterBuilding(){
		CityCitizenCount.citizenCount--;
		Destroy (gameObject);
	}
	void Die(){
		Instantiate (explosion, transform.position, Quaternion.identity);
		//randomSpawn = Random.Range (0, 7);
		//health = 50;
		//transform.position = city.citizenSpawnPoints [randomSpawn].position;
		Destroy (gameObject);
		CityCitizenCount.citizenCount--;
	}
}
