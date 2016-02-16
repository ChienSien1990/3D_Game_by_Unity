using UnityEngine;
using System.Collections;

public class CarNPC : MonoBehaviour {

	// particle explosion
	public GameObject carExplosion;

	NavMeshAgent car;
	// car speed
	private float fastSpeed;
	private float slowSpeed;

	//spawn point
	private Vector3 spawningPoint;

	// variables
	private int health;
	private int count;
	private int damage;
	private int spawnCounter;
	private string direction;


	// Use this for initialization
	void Start () {
		// give the car a random fast speed and random slow speed
		fastSpeed = Random.Range (10, 20);
		slowSpeed = Random.Range (5, 10);

		// give the car a health
		health = Random.Range (50, 200);

		if (transform.rotation == Quaternion.Euler (0, 0, 0)) {
			direction = "up";	
		}
		if (transform.rotation == Quaternion.Euler (0, 180, 0)) {
			direction = "down";	
		}
		if (transform.rotation == Quaternion.Euler (0, 270, 0)) {
			direction = "right";	
		}
		if (transform.rotation == Quaternion.Euler (0, 90, 0)) {
			direction = "left";
		}
		// find the spawn point of the car and determine direction base off spawn point
		/*
		for(spawnCounter = 0; spawnCounter < 15; spawnCounter++){
			if (transform.position == CityLevel.carSpawnPoints [spawnCounter].position) {
				if(CityLevel.carSpawnPoints[spawnCounter].name == "CarSpawnNorth"){
					transform.rotation = Quaternion.Euler(0,0,0);	
				}
				if(CityLevel.carSpawnPoints[spawnCounter].name == "CarSpawnSouth"){
					transform.rotation = Quaternion.Euler(0,180,0);	
				}
				if(CityLevel.carSpawnPoints[spawnCounter].name == "CarSpawnEast"){
					transform.rotation = Quaternion.Euler(0,0,0);	
				}
				if(CityLevel.carSpawnPoints[spawnCounter].name == "CarSpawnWest"){
					transform.rotation = Quaternion.Euler(0,0,0);	
				}
			}
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		if(direction == "up"){
			transform.position += Vector3.back * fastSpeed * Time.deltaTime;
		}
		if(direction == "down"){
			transform.position += Vector3.forward * fastSpeed * Time.deltaTime;
		}
		if(direction == "right"){
			transform.position += Vector3.right * fastSpeed * Time.deltaTime;
		}
		if(direction == "left"){
			transform.position += Vector3.left * fastSpeed * Time.deltaTime;
		}
	}

	// when the car collides with an object
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "PlayerBullet") {
			health -= 10;	
		}
		if (obj.transform.tag == "EnemyBullet") {
			health -= 10;	
		}
		if (obj.transform.tag == "Player") {
			health -= 20;	
			//transform.position -= Vector3.forward;
		}
		if (obj.transform.tag == "Enemy") {
			health -= 20;	
			//transform.position -= Vector3.forward;
		}
		if (obj.transform.tag == "Building") {
			destroyObject();	
		}
		// if two cars collide, both get destroyed
		if(obj.transform.tag == "Car"){
			destroyObject();
		}

	}

	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "CarDestroy") {
			destroyObject();	
		}
	}
	// create particle effects and destory object (car)
	void destroyObject(){
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(carExplosion, transform.position, Quaternion.identity);

		// update the car Count
		CarCount.carCount--;
		//get the object out of memory
		Destroy (gameObject);
	}
}
