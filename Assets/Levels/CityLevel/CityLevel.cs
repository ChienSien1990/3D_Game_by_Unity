using UnityEngine;
using System.Collections;

public class CityLevel : MonoBehaviour {

	// for story progression
	public static bool firstEntrance = true;

	// for enemy objects
	public GameObject enemy;
	public Transform[] enemySpawnPoints;

	// for citizen objects
	public GameObject citizen;
	public Transform[] citizenSpawn;
	private int citizenSpawnCounter;

	// for cars
	public GameObject blueCar;
	public GameObject redCar;
	public GameObject orangeCar;
	public Transform[] carSpawnPoints;
	private int carColor;
	private int spawnCount;
	private Quaternion spawnRotation;

	// for collecting the capsule on level
	public GameObject capsule;
	public Transform capsuleSpawn;
	public static bool capStolen = false;
	public static bool enemySpawned = false;

	// for npc
	public GameObject agentBob;
	public Transform pos;

	// for player
	public GameObject player;
	public Transform playerPos1;
	public Transform playerPos2;

	// Use this for initialization
	void Start () {
		// check to see if its the first time the player has been on the map
		if(firstEntrance == true){
			// spawn agent bob to initiate the storyLine
			Instantiate (agentBob, pos.transform.position, pos.transform.rotation);
		}
		// check if the player has stolen the capsule or not
		if(capStolen == false){
			Instantiate (capsule, capsuleSpawn.transform.position, capsuleSpawn.transform.rotation);
		}

		CarCount.carCount = 0;
		CityCitizenCount.citizenCount = 0;
		spawnCount = 0;
		citizenSpawnCounter = 0;

	}

	// called once per frame
	void Update(){

		// when the player steals the capsule, spawn the enemies to hunt him down
		if (capStolen == true && enemySpawned == false) {
			spawnEnemies();
		}

	}

	public void spawnCitizen(){
		Instantiate (citizen, citizenSpawn[citizenSpawnCounter].position, Quaternion.identity);
		citizenSpawnCounter++;
		if (citizenSpawnCounter >= 9) {
			citizenSpawnCounter = 0;
		}
		CityCitizenCount.citizenCount++;
	}
	public void spawnCar(){

		// increment the spawnCount so cars are spawn in different areas throughout the map
		spawnCount++;
		// there are 15 spawn points, so when this count goes above 15, set it back to zero
		if (spawnCount >= 15) {
			spawnCount = 0;	
		}
		// change cars rotation
		if (spawnCount < 4) {
			spawnRotation = Quaternion.Euler (0,180,0);	
		}
		if (spawnCount >= 4 && spawnCount < 9) {
			spawnRotation = Quaternion.Euler (0,0,0);	
		}
		if (spawnCount >= 9 && spawnCount < 12) {
			spawnRotation = Quaternion.Euler (0,90,0);	
		}
		if (spawnCount >= 12 && spawnCount < 15) {
			spawnRotation = Quaternion.Euler (0,270,0);
		}

		// spawn 1 car, generate a random number to choose which color car gets spawned
		carColor = Random.Range (0, 3);
		if (carColor == 0) {
			Instantiate(blueCar, carSpawnPoints[spawnCount].position, spawnRotation);
		}
		if (carColor == 1) {
			Instantiate(redCar, carSpawnPoints[spawnCount].position, spawnRotation);
		}
		if (carColor == 2) {
			Instantiate(orangeCar, carSpawnPoints[spawnCount].position, spawnRotation);
		}

		// increment the total car count
		CarCount.carCount++;

	}

	// spawn Enemies!!!
	void spawnEnemies(){
		// i is the number of enemies
		int i = 0;
		// j is the spawn point position... there are 4
		int j = 0;
		for (i = 0; i < 15; i++) {
			Instantiate (enemy, enemySpawnPoints[j].transform.position, Quaternion.identity);	
			j++;
			if(j > 3){
				j = 0;
			}
		}
		enemySpawned = true;
	}


}
