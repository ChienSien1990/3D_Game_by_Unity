using UnityEngine;
using System.Collections;

public class PlayerDriving : MonoBehaviour {
	
	//For the 5 component script that are effected by this script
	public PlayerDriving driving;
	public PlayerActions playerAct;
	public CarCamera cam;
	public GameCamera gameCam;
	public MiniMapCamera miniCam;
	public CarMiniMapCam miniCarCam;

	// Starting Speed of the vehicle and boolean speed controller for fast or slow
	public float speed = 10;
	private bool fast = false;

	// Save the starting pos of vehicle and send static variable to alert player script that it died
	private Vector3 spawnPos;
	public static bool carFell = false;

	// Use this for instantiation
	void Start(){
		// Save the start position of the vehicle to respawn it if it falls
		spawnPos = transform.position;
	}

	// Update is called once per frame
	void Update () {

		// call the drive method every frame
		Drive ();

		// when R is pressed the player will "EXIT" the car
		if(Input.GetKeyDown(KeyCode.R)){
			//Instantiate (newPlayer, transform.position, Quaternion.identity)
			PlayerActions.inCar = false;
			// change all the scritps to make the player active again and the car inactive
			driving.enabled = !driving.enabled;
			playerAct.enabled = !playerAct.enabled;
			// these are camera scripts.. there are two cameras 1 game cam and 1 minimap cam with 2 scripts each
			cam.enabled = !cam.enabled;
			gameCam.enabled = !gameCam.enabled;
			miniCam.enabled = !miniCam.enabled;
			miniCarCam.enabled = !miniCarCam.enabled;
		}


	

		// Respawn the car at original position on map if it falls off the map
		if (transform.position.y < -20.0f) {
			PlayerActions.inCar = false;
			// change all the scritps to make the player active again and the car inactive
			driving.enabled = !driving.enabled;
			playerAct.enabled = !playerAct.enabled;
			// these are camera scripts.. there are two cameras 1 game cam and 1 minimap cam with 2 scripts each
			cam.enabled = !cam.enabled;
			gameCam.enabled = !gameCam.enabled;
			miniCam.enabled = !miniCam.enabled;
			miniCarCam.enabled = !miniCarCam.enabled;
			carFell = true;
			transform.position = spawnPos;	
		}

	}

	//When the player enters the trigger near the car, they take control over the vehicle
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			// enable drving script and corresponding cameras, while disabling the other cameras
			driving.enabled = true;
			playerAct.enabled = !playerAct.enabled;
			cam.enabled = !cam.enabled;
			gameCam.enabled = !gameCam.enabled;
			miniCam.enabled = !miniCam.enabled;
			miniCarCam.enabled = !miniCarCam.enabled;
		}
	}

	// This function drives the car
	void Drive(){
		// press space to change speeds, two speeds, fast and slow
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(!fast){
				speed *= 2;
				fast = true;
			}
			else{
				speed = 10;
				fast = false;
			}
		}
		// use WASD to control the vehicle
		if (Input.GetKey (KeyCode.W)) {
			transform.position += Vector3.forward*speed*Time.deltaTime;
			transform.rotation = Quaternion.Euler (0,180,0);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.position += Vector3.back*speed*Time.deltaTime;
			transform.rotation = Quaternion.Euler (0,0,0);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.position += Vector3.right*speed*Time.deltaTime;
			transform.rotation = Quaternion.Euler (0,270,0);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += Vector3.left*speed*Time.deltaTime;
			transform.rotation = Quaternion.Euler (0,90,0);
		}
	}
}
