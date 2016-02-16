using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//This class needs a character controller
[RequireComponent(typeof(CharacterController))]

public class PlayerActions : MonoBehaviour {
	// static variable to count number of player deaths
	public static int deathCount;
	public static int capsuleCount = 0;

	// setting the player invisible
	public MeshRenderer[] meshes;
	// variables needed for User Interface
	#region FIELDS
	public float speed;
	private int currentHealth;
	private int maxHealth=100;
	public RectTransform healthTransform;
	public float Scale=1;
	public Text healthText;
	public Image visualHealth;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private float currentXValue;
	public float cooldown;
	private bool onCD;
	public Text Bullets;
	public Text TrapMine;
	#endregion
	
	// keep track of where player is
	private Vector3 respawnPoint;
	/*public static bool agentBuildingLast = false;
	public static bool storeBuildingLast = false; */
	public static bool inCar;
	public Transform playerCar;
	
	// Player Unlockables.... Prob will not be used
	public static bool shotGunUnlocked = false;
	public static bool greenUnlocked = false;
	public static bool cyanUnlocked = false;
	public static bool whiteUnlocked = false;
	public static bool blackUnlocked = false;
	
	// Positioning variables
	protected Vector3 spawningPoint;
	
	// Level checker
	private string levelName;
	private string levelObjective;
	private string obj = "Objective: ";
	
	// Handling variables
	public float rotationSpeed = 450.0f;
	public float walkSpeed;
	public float runSpeed;
	public bool isInvisible = false;
	private int colorChange = 0;

	// check whether if there is the pause 
	private static bool ispaused = false;

	//private int startHealth = 100;
	private bool canShoot;
	private int ammo;
	private int ammoClip;
	private int tripMines = 0;
	private bool saveddata=false;
	// for checking end level
	private bool hasCapsule = false;
	
	//System
	private Quaternion targetRotation;
	private Quaternion target;
	
	// GameObjects and Components
	public GameObject deathExplosion;
	public PlayerGun gun;
	private CharacterController controller;
	private Camera cam;
	public GameObject PlayerBody;
	public GameObject PlayerHealth;
	private Vector3 fullHealth;
	private Material healthMaterial;
	private Material lowHealthMaterial;
	public Text objectiveText;

	// displaying in game controls
	public Texture2D controls;
	private bool showControls = false;
	//audio components.. for playing gun sounds, etc
	public AudioClip gunShot;
	public AudioClip reload;
	
	// check if the player justDrove a car
	private bool justDrove = false;
	
	// keep track of the playerCars position
	private Vector3 carPosition;
	
	// Use this for initialization
	void Start () {
		inCar = false;
		// get all the players mesh Renderers in an array
		meshes = GetComponentsInChildren<MeshRenderer> ();
		// save current level name
		levelName = Application.loadedLevelName;
		
		// for handling where the player spawns on the map... this spawns him infront of agent building
		if (levelName == "CityLevel") {
			levelObjective = "Find AgentBuilding";	
		}
		if (levelName == "CityLevel" && CityLevel.firstEntrance == false) {
			respawnPoint = new Vector3(25.23f, 0.65f, 6.69f);
			transform.position = respawnPoint;
			levelObjective = "Free Roam";	
		}
		// example way of setting the level objective
		if (levelName == "Level1" || levelName == "Level2" || levelName == "Level3" || levelName == "Level4" || levelName == "Level5" || levelName == "Level6"|| levelName == "Level7" || levelName == "Level8" || levelName == "Level9" || levelName == "Level10") {
			levelObjective = "Steal the Capsule";	
		}
		// set objective
		if (levelName == "AgentBuilding")
			levelObjective = "Choose the Mission";
		// set objective

		// set objective
		if (levelName == "SmallTown") {
			levelObjective = "Kill Enemies";	
		}
		
		// get the player car's position but only on the city map
		if (playerCar == null && levelName == "CityLevel") {
			playerCar = GameObject.FindGameObjectWithTag("PlayerCar").transform;	
		}
		
		objectiveText.text = obj+levelObjective;
		ammoClip = 20;
		ammo = 60;
		tripMines = 3;
		//	health = startHealth;
		//save the size of the full health bar in a variable. It can be called later
		fullHealth = PlayerHealth.transform.localScale;
		canShoot = true;
		spawningPoint = transform.position;
		controller = GetComponent<CharacterController> ();
		cam = Camera.main;
		hasCapsule = false;
		
		//player UI
		//Sets all start values
		onCD = false;
		cachedY = healthTransform.position.y; //Caches the healthbar's start pos
		maxXValue = healthTransform.position.x; //The max value of the xPos is the start position
		minXValue = healthTransform.position.x-(healthTransform.rect.width /(float)3.33); //The minValue of the xPos is startPos - the width of the bar
		//healthTransform= healthTransform.position.z - 1100;
		currentHealth = maxHealth; //Sets the current healt to the maxHealth
	}
	
	// Update is called once per frame
	void Update () {
		
		// track the playerCars position
		if(levelName == "CityLevel"){
			carPosition = playerCar.position;
		}
		
		// call keyboard movement... it moves the player each frame
		KeyboardMovement();
		
		// check ammo checks the players ammo usage
		checkAmmo ();
		
		// if the player presses U key then call dropMine method... it places a mine beneath the player position
		if(Input.GetKeyDown (KeyCode.U)){
			dropMine ();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (ispaused == false){
				ispaused = true;
				Time.timeScale=0;
			}
			
		}
		TrapMine.text = ""+tripMines;
		
		// if the player is driving and drives off the map, then the player dies, repawn the player and reset the static variable
		if (PlayerDriving.carFell == true) {
			PlayerDriving.carFell = false;
			Die ();
		}
		
		if (inCar == false && justDrove == true) {
			//when the player leaves the vehicle
			// change the players position to where the car is
			transform.position = spawningPoint;
			//transform.position = new Vector3(25.23f, 0.65f, 6.69f);
			justDrove = false;
			// make all the player's meshes visible again
			foreach(MeshRenderer mesh in meshes){
				mesh.enabled = true;
			}
		}
		// used to test out the die function or if they fall off ledge
		if(transform.position.y < -10.0f){
			Die ();
		}
		// This function changes the players shirt color... kind of pointless. Simply for aesthetics I guess
		// probably wasting cpu resources by being called every frame
		changeColor();
	}
	#region PROPERTIES
	public int health
	{
		get { return currentHealth; }
		set
		{
			currentHealth = value;
			HandleHealthbar();
		}
	}
	
	#endregion
	// when the player collides with an object
	/// <summary>
	/// Handles the healthbar by moving it and changing color
	/// </summary>
	private void HandleHealthbar()
	{   
		//Writes the current health in the text field
		healthText.text = "Health: " +currentHealth;
		
		//Maps the min and max position to the range between 0 and max health
		currentXValue = Map(currentHealth, 0, maxHealth, minXValue, maxXValue);
		
		//Sets the position of the health to simulate reduction of health
		healthTransform.position = new Vector3(currentXValue, cachedY,1100);
		
		if (currentHealth > maxHealth / 2) //If we have more than 50% health we use the Green colors
		{
			visualHealth.color = new Color32((byte)Map(currentHealth, maxHealth / 2,maxHealth, 255, 0), 255, 0, 255);
		}
		else //If we have less than 50% health we use the red colors
		{
			visualHealth.color = new Color32(255, (byte)Map(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
		}
		
	}
	
	
	
	/// <summary>
	/// Keeps track of the  damage CD
	/// </summary>
	/// <returns></returns>
	IEnumerator CoolDownDmg()
	{
		onCD = true; 
		yield return new WaitForSeconds(cooldown); //Waits a while before we are able to take dmg again
		onCD = false;
	}
	
	/// <summary>
	/// This method maps a range of number into another range
	/// </summary>
	/// <param name="x">The value to evaluate</param>
	/// <param name="in_min">The minimum value of the evaluated variable</param>
	/// <param name="in_max">The maximum value of the evaluated variable</param>
	/// <param name="out_min">The minum number we want to map to</param>
	/// <param name="out_max">The maximum number we want to map to</param>
	/// <returns></returns>
	public float Map(float x, float in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}
	
	// when the player collides with an object
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "EnemyBullet") {
			if (!onCD && currentHealth > 1)
			{
				//StartCoroutine(CoolDownDmg()); //Makes sure that we can't take damage right away
				if(StartMenu.difficulty=="Easy")
					health -= 5;
				else if(StartMenu.difficulty=="Normal")
					health -= 10;
				else if(StartMenu.difficulty=="Hard")
					health -= 20;//Uses the Health Property to so that we recolor and rescale the health when we change it
			}
			//ResizeHealthBar();
			checkHealth ();
		}
		if(obj.transform.tag == "Car"){
			if (!onCD && currentHealth > 1)
			{
				StartCoroutine(CoolDownDmg()); //Makes sure that we can't take damage right away
				if(StartMenu.difficulty=="Easy")
					health -= 20;
				else if(StartMenu.difficulty=="Normal")
					health -= 50;
				else if(StartMenu.difficulty=="Hard")
					health -= 100;
				//Uses the Health Property to so that we recolor and rescale the health when we change it
			}
			//ResizeHealthBar();
			checkHealth ();
		}
		if (obj.transform.tag == "TripMine") {
			Physics.IgnoreCollision(obj.collider, transform.collider);	
		}
	}
	void ResizeHealthBar(){
		Vector3 resize = PlayerHealth.transform.localScale;
		resize.x -= 0.075f;
		if (resize.x > 0.5f) {
			PlayerHealth.renderer.material.color = Color.green;		
		}else{
			PlayerHealth.renderer.material.color = Color.red;		
		}
		PlayerHealth.transform.localScale = resize;
	}
	// Player Regains Full Health
	public void MaxHealthIncrease(){
		health = 100;
		//PlayerHealth.transform.localScale = fullHealth;
		//PlayerHealth.renderer.material.color = Color.green;
	}
	// check if the player's health is zero or less
	void checkHealth(){
		if (health <= 0) {
			Die ();		
		}
	}
	// public methods need to be accessed by other classes
	public int getHealth(){
		return health;
	}
	public void increaseTripMines(){
		tripMines += 5;
	}
	public void AddAmmo(){
		ammo += 20;
	}
	public void CapsulePickUp(){
		hasCapsule = true;
		capsuleCount++;
		//print (hasCapsule);
	}
	public bool capsuleCheck(){
		return hasCapsule;
	}
	// this function is called when the player dies... this is the basic die function, it can be changed and molded to certain needs
	void Die(){
		inCar = false;
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		
		PlayerHealth.transform.localScale = fullHealth;
		PlayerHealth.renderer.material.color = Color.green;

		health = 100;
		ammoClip = 20;
		ammo = 20;
		// this respawns the player at his original spawn position
		// make all the player's meshes visible again
		foreach(MeshRenderer mesh in meshes){
			mesh.enabled = true;
		}
		transform.position = spawningPoint;
		deathCount--;
		Time.timeScale = 0;	
	}
	
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "MaxHealth") {
			MaxHealthIncrease();	
		}
		if (obj.transform.tag == "TripMineItem") {
			increaseTripMines ();	
		}
		
		if (obj.transform.tag == "AmmoItem") {
			AddAmmo();	
		}
		if (obj.transform.tag == "PlayerCar") {
			// render all the meshes invisible
			foreach(MeshRenderer mesh in meshes){
				mesh.enabled = false;
			}
			transform.position = spawningPoint;
			//transform.position = new Vector3(-96.0f, 0.5f, -83.0f);
			inCar = true;
			justDrove = true;
		}
		
	}
	
	void OnGUI(){
			if (ispaused == true && StartMenu.levelSelection == false) {
				GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 260));
				GUI.Box (new Rect (0, 0, 220, 260), "PAUSE");
				if(showControls == false){
					if (GUI.Button (new Rect (10, 35, 200, 50), "Resume Game")) {
							Time.timeScale = 1;
							ispaused = false;
							saveddata = false;
							showControls = false;

					} else if (GUI.Button (new Rect (10, 85, 200, 50), "Controls")) {
						showControls = true;

					}else if (GUI.Button (new Rect (10, 135, 200, 50), "Save Data")) {
							showControls = false;
							saveddata = true;
							//GUI.Label (new Rect(10,10,100,50),"CONTINUE?");
							//print("Data Saved! here is the capsule:");
							StartMenu.savedCapsule = capsuleCount;
			
					} else if (levelName != "CityLevel" && levelName != "AgentBuilding") {
							if (GUI.Button (new Rect (10, 185, 200, 50), "Back to Agent Building")) {
									//code to start first level
									Time.timeScale = 1;
									ispaused = false;
									//CityLevel.firstEntrance=true;
									Application.LoadLevel ("AgentBuilding");
									saveddata = false;
							}
					} else {
							if (GUI.Button (new Rect (10, 185, 200, 50), "Quit Game")) {
									//code to start first level
									Time.timeScale = 1;
									ispaused = false;
									//CityLevel.firstEntrance=true;
									Application.LoadLevel ("StartMenu");
									saveddata = false;
							}
					}
					if (saveddata == true)
							GUI.Label (new Rect (10, 240, 200, 50), "Data Saved! capsule is " + StartMenu.savedCapsule);
				}else{
					// Draw Player Controls on Screen
					GUI.DrawTexture(new Rect(10, 85, 200, 150), controls);
					if (GUI.Button (new Rect (10, 35, 200, 50), "Back")) {
						showControls = false;
					}
				}
				GUI.EndGroup ();
			}
			else if (ispaused == true && StartMenu.levelSelection == true) {
					GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 250));
					GUI.Box (new Rect (0, 0, 220, 250), "PAUSE");
					if (GUI.Button (new Rect (10, 35, 200, 50), "Resume Level")) {
							//resume game
							Time.timeScale = 1;
							ispaused = false;
					}
					if (GUI.Button (new Rect (10, 155, 200, 50), "Quit Game")) {
							//return to main menu
							Time.timeScale = 1;
							ispaused = false;
							//CityLevel.firstEntrance=true;
							Application.LoadLevel ("StartMenu");
							saveddata = false;
					}
				GUI.EndGroup ();

		}else if (levelName != "CityLevel" && ispaused == false) {
			levelObjective = "Kill Enemies. Survive";
			/*GUI.Label(new Rect(10, 10, 200, 50), obj+levelObjective);
			GUI.Label(new Rect(10, 30, 200, 70), "Ammo: " + ammoClip +"/" + ammo);
			GUI.Label(new Rect(10, 50, 200, 90), "Mines: " + tripMines);
			GUI.Label(new Rect(10, 70, 200, 90), "Health: " + health);
			GUI.Label(new Rect(350, 10, 200, 50), "Wave: " + SmallTown.wave);
			GUI.Label(new Rect(350, 30, 200, 50), "Kills: " + SmallTown.enemyKills);*/
			
			if (Time.timeScale == 0 && deathCount>0 && StartMenu.levelSelection == false) {
				GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 210));
				GUI.Box (new Rect (0,0,220,210), "CONTINUE?");
				//	GUI.Label (new Rect(Screen.width/2-75,Screen.height/2-80,200,50),"CONTINUE?");
				
				if (GUI.Button (new Rect (10,70,200,50), "Play Again")) {
					//code to start first level
					Time.timeScale = 1;
					//deathCount--;
					Application.LoadLevel (Application.loadedLevel);
				}
				if (GUI.Button (new Rect (10,130,200,50), "Back to Agent Building")) {
					//code to start first level
					Time.timeScale = 1;
					//CityLevel.firstEntrance=true;
					Application.LoadLevel ("AgentBuilding");
				}
				GUI.Label (new Rect(15,190,200,50),"Life Counts="+deathCount);
				GUI.EndGroup();
			}
			if (Time.timeScale == 0 && deathCount>0 && StartMenu.levelSelection == true) {
				GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 210));
				GUI.Box (new Rect (0,0,220,210), "CONTINUE?");
				//	GUI.Label (new Rect(Screen.width/2-75,Screen.height/2-80,200,50),"CONTINUE?");
				
				if (GUI.Button (new Rect (10,70,200,50), "Play Again")) {
					//restart level
					Time.timeScale = 1;
					//deathCount--;
					Application.LoadLevel (Application.loadedLevel);
				}
				if (GUI.Button (new Rect (10,130,200,50), "Back to Start Menu")) {
					//code to start first level
					Time.timeScale = 1;
					//CityLevel.firstEntrance=true;
					Application.LoadLevel ("StartMenu");
				}
				//GUI.Label (new Rect(15,190,200,50),"Life Counts="+deathCount);
				GUI.EndGroup();
			}
		}
		else if (levelName== "CityLevel" && Time.timeScale==0 && deathCount>0 && ispaused == false){
			GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 210));
			GUI.Box (new Rect (0,0,220,210), "CONTINUE?");
			//	GUI.Label (new Rect(Screen.width/2-75,Screen.height/2-80,200,50),"CONTINUE?");
			
			if (GUI.Button (new Rect (10,70,200,50), "Play Again")) {
				//code to start first level
				Time.timeScale = 1;

				Application.LoadLevel (Application.loadedLevel);
			}
			if (GUI.Button (new Rect (10,130,200,50), "Quit Game")) {
				//code to start first level
				Time.timeScale = 1;
				CityLevel.capStolen = false;
				CityLevel.enemySpawned = false;
				CityLevel.firstEntrance=true;
				capsuleCount = 0;
				deathCount = 3;
				Application.LoadLevel ("StartMenu");
			}
			GUI.Label (new Rect(15,190,200,50),"Life Counts="+deathCount);
			GUI.EndGroup();
		}
		//Game Over
		if (deathCount == 0 && ispaused == false && StartMenu.levelSelection==false  ) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 140));
			GUI.Box (new Rect (0,0,220,140), "GAME OVER");
			Time.timeScale = 0;

			if (GUI.Button (new Rect (10,50,200,50), "Quit Game")) {
				//code to start first level
				Time.timeScale = 1;
				CityLevel.firstEntrance=true;
				CityLevel.capStolen = false;
				CityLevel.enemySpawned = false;

				capsuleCount = 0;
				if(StartMenu.difficulty=="Easy")
					deathCount=5;
				else if(StartMenu.difficulty=="Normal")
					deathCount=3;
				else if(StartMenu.difficulty=="Hard")
					deathCount=1;
				// load start menu
				Application.LoadLevel ("StartMenu");
			}
			GUI.EndGroup();
			
		}
		else if (deathCount == 0 && ispaused == false && StartMenu.levelSelection==true ) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 110, Screen.height / 2 - 105, 220, 200));
			GUI.Box (new Rect (0,0,220,200), "GAME OVER");
			Time.timeScale = 0;
			
			if (GUI.Button (new Rect (10,120,200,50), "Quit Game")) {
				//code to start first level
				Time.timeScale = 1;
				CityLevel.firstEntrance=true;
				CityLevel.capStolen = false;
				CityLevel.enemySpawned = false;
				
				capsuleCount = 0;
				if(StartMenu.difficulty=="Easy")
					deathCount=5;
				else if(StartMenu.difficulty=="Normal")
					deathCount=3;
				else if(StartMenu.difficulty=="Hard")
					deathCount=1;
				// load start menu
				Application.LoadLevel ("StartMenu");
			}
			if (GUI.Button (new Rect (10,50,200,50), "Play Again")) {
				//code to start first level
				Time.timeScale = 1;
				deathCount=1;
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.EndGroup();
			
		}
	}
	
	public void DestroyPlayer(){
		transform.position = spawningPoint;
	}
	// change the players shirt color
	void changeColor(){
		if (Input.GetKeyDown (KeyCode.C)) {
			if(colorChange > 5){
				colorChange = 0;
			}else{
				colorChange++;
			}
		}
		if(colorChange == 0){
			PlayerBody.renderer.material.color = Color.blue;
		}
		if(colorChange == 1 && cyanUnlocked == true){
			PlayerBody.renderer.material.color = Color.cyan;
		}
		if(colorChange == 2 && whiteUnlocked == true){
			PlayerBody.renderer.material.color = Color.white;
		}
		if(colorChange == 3 && greenUnlocked == true){
			PlayerBody.renderer.material.color = Color.green;
		}
		if(colorChange == 3 && blackUnlocked == true){
			PlayerBody.renderer.material.color = Color.black;
		}
	}
	// call function to drop mine
	void dropMine(){
		
		if (tripMines > 0){
			gun.dropMine();	
			tripMines--;
		}
	}
	// make the player invisible
	/*void invisible(){
		if (Input.GetKeyDown (KeyCode.G)) {
			isInvisible = !isInvisible;
		}
	}*/
	// method to allow the player to aim and control with the mouse
	/* Not used
	void MouseControl(){

		// create a vector3 and store the mouse's current position in it
		Vector3 mousePos = Input.mousePosition;
		// get the y distance between the camera and the player for Z axis
		mousePos = cam.ScreenToWorldPoint (new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
		// make the rotation follow the mouse's position in the speed of Time.delta (seconds)
		targetRotation = Quaternion.LookRotation (mousePos - new Vector3(transform.position.x, transform.position.y, transform.position.z - 5));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

		// This gets the player position everyframe
		Vector3 input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			gun.shoot ();
		}

		// This is the motion and speed check of the player
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f : 1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;
		controller.Move(motion *3* Time.deltaTime);
	}
	*/
	//checks if the player needs to reload
	void checkAmmo(){
		Bullets.text = ammoClip + "/" + ammo;
		if (ammoClip <= 0) {
			canShoot = false;
		}
	}
	//reloads the weapon
	void ReloadWeapon(){
		audio.PlayOneShot(reload);
		if(ammo > 0){
			ammo -= (20 - ammoClip);
			print ("ammo: " + ammo + "ammoClip: " +ammoClip);
			ammoClip = 20;
		}
		
		canShoot = true;
	}
	//slows down the game speed but player still moves fast... doesnt work as planned
	/*void BulletTime(){
		if(Input.GetKeyDown (KeyCode.B) && bulletTime){
			walkSpeed *= 2;
			runSpeed *= 2;
			Time.timeScale = 0.5f;
		}
		if(Input.GetKeyDown (KeyCode.B) && !bulletTime){
			Time.timeScale = 1f;
			walkSpeed /= 2;
		}

	}*/
	
	//method for keyboard controls and aiming
	void KeyboardMovement(){
		
		// This gets the player position everyframe
		// On stealth missions the player cannot move diagonally 
		Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		if(Input.GetAxisRaw("Horizontal") == 0){
			input = new Vector3(0, 0, Input.GetAxis("Vertical"));
		}
		// player can move diagonally on other levels
		if (levelName == "CityLevel" || levelName == "SmallTown" || levelName == "SurvivorCity") {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		}
		// This rotates the player as it moves and if no input it will stay at the rotation it is at
		if(Input.GetKeyDown(KeyCode.W)){
			transform.rotation = Quaternion.Euler(0,0,0);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			transform.rotation = Quaternion.Euler(0,180,0);
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
		}
		
		if(Input.GetKeyDown(KeyCode.D)){
			transform.rotation = Quaternion.Euler(0,90,0);
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
		}
		
		if(Input.GetKeyDown(KeyCode.A)){
			transform.rotation = Quaternion.Euler(0,270,0);
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
		}
		
		// aiming and shooting controls
		if(Input.GetKeyDown(KeyCode.I)){
			if(ammoClip > 0){
				ammoClip -= 1;
			}
			transform.rotation = Quaternion.Euler(0,0,0);
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
			if(canShoot){
				audio.PlayOneShot(gunShot);
				gun.shoot();
			}
		}
		if(Input.GetKeyDown(KeyCode.K)){
			if(ammoClip > 0){
				ammoClip -= 1;
			}
			transform.rotation = Quaternion.Euler(0,180,0);
			
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
			if(canShoot){
				audio.PlayOneShot(gunShot);
				gun.shoot();
			}
		}
		
		if(Input.GetKeyDown(KeyCode.L)){
			if(ammoClip > 0){
				ammoClip -= 1;
			}
			transform.rotation = Quaternion.Euler(0,90,0);
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
			if(canShoot){
				audio.PlayOneShot(gunShot);
				gun.shoot();
			}
		}
		
		if(Input.GetKeyDown(KeyCode.J)){
			if(ammoClip > 0){
				ammoClip -= 1;
			}
			transform.rotation = Quaternion.Euler(0,270,0);
			
			//PlayerHealth.transform.rotation = Quaternion.Euler (0,0,0);
			if(canShoot){
				audio.PlayOneShot(gunShot);
				gun.shoot();
			}
		}
		/* crouch function... but it sucks and allows player to go through walls
		if(Input.GetKeyDown (KeyCode.N)){
			isCrouched = !isCrouched;
			if (isCrouched) {
				Vector3 temp = PlayerBody.transform.position;
				temp.y = 0.75f;
				PlayerBody.transform.position = temp;
				Vector3 crouchBody = PlayerBody.transform.localScale;
				crouchBody.y = 0.5f;
				crouchBody.x = 1.25f;
				walkSpeed = 2.5f;
				runSpeed = 2.5f;
				PlayerBody.transform.localScale = crouchBody;
			}else{
				walkSpeed = 20.0f;
				runSpeed = 30.0f;
				PlayerBody.transform.localScale = standing;
			}
		}*/
		//call the reload weapon function
		if (Input.GetKeyDown (KeyCode.O)) {
			ReloadWeapon();		
		}
		// This is the motion and speed check of the player
		Vector3 motion = input;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)? .7f : 1;
		motion *= (Input.GetButton ("Run")) ? runSpeed : walkSpeed;
		motion += Vector3.up * -8;
		controller.Move(motion *3* Time.deltaTime);
	}
}
