using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	AudioSource[] enemySounds;
	MeshRenderer[] meshes;
	BoxCollider[] boxes;
	EnemyController script;
	// nav mesh controls enemy pathfinding
	NavMeshAgent agent;
	public GameObject deathExplosion;
	public GameObject enemyEyes;
	private GameObject player;
	private Vector3 spawningPoint;
	//for enemy shooting ability
	public EnemyGun enemyGun;
	public GameObject HealthBar;
	
	private float x;
	private float y;
	public int distance;

	private float patrolSpeed = 3.0f;
	private float chaseSpeed = 8.0f;
	private int health = 60;
	private int healthDamage;
	public bool shooting;
	private int shootingChance = 0;
	private int shotCount = 0;
	private int reloadCount = 0;
	private bool reload = false;

	// for enemy patroling areas
	public Transform[] patrolPoints;
	private int currentPoint;
	// for enemy vision
	public bool playerSpotted = false;
	private Vector3 direction;
	private float visionLength = 10.0f;
	private float visionSide = 7.5f;
	private float visionShort = 3.0f;
	private int rotateCheck;
	private PlayerGun playerAct;

	private int rayCount;
	// Use this for initialization
	void Start () {
		// this is to count the rays that spot the player
		rayCount = 0;
		script = GetComponent<EnemyController> ();
		meshes = GetComponentsInChildren<MeshRenderer> ();
		boxes = GetComponentsInChildren<BoxCollider> ();
		enemySounds = GetComponents<AudioSource> ();

		//set spawn to original point on map for respawn
		agent = GetComponent<NavMeshAgent> ();
		transform.position = patrolPoints [0].position;
		currentPoint = 0;
		spawningPoint = transform.position;
		healthDamage = health / 10;
		// make sure the enemy has the player gameObject to find
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
		playerAct = player.GetComponentInChildren<PlayerGun> ();
		direction = (new Vector3(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f),0.0f)).normalized;
		transform.Rotate(direction);
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerSpotted) {
			Patrol ();	
		}
		if (playerSpotted) {
			chasePlayer();
		}
		visionCheck ();
		//check z distance bewteen player and enemy
		//if(transform.position.z - player.transform.position.z > -12 && transform.position.z - player.transform.position.z < 12){
		//enemy reloading
		if (shotCount == 20) {
			reload = true;
			reloadCount++;
			if(reloadCount == 50){
				reload = false;
				reloadCount = 0;
				shotCount = 0;
			}
			
		}
		//enemy shooting
		if(reload == false){
			if(shooting == true){
				shootingChance++;
				if(shootingChance == 25){
					shootPlayer();
					shotCount++;
					shootingChance = 0;
				}
			}
		}
		//}
	}

	// enemy vision
	void visionCheck(){
		RaycastHit hit;
		Vector3 straight = transform.forward;
		Vector3 right = transform.right + transform.forward;
		Vector3 left = -transform.right + transform.forward;

		//Quaternion angleLeft = Quaternion.AngleAxis (-15, new Vector3(0, 1, 0));
		///Quaternion angleRight = Quaternion.AngleAxis (15, new Vector3(0, 1, 0));
		//Vector3 offsetLeft = straight * angleLeft;
		//Vector3 offsetRight = straight * angleRight;

		Ray enemyVision = new Ray (transform.position, straight);
		Ray visionRight = new Ray (transform.position, right);
		Ray visionLeft = new Ray (transform.position, left);
		Ray MoreRight = new Ray (transform.position, transform.right);
		Ray MoreLeft = new Ray (transform.position, -transform.right);
		
		if (!playerSpotted) {
			if(Physics.Raycast(enemyVision, out hit, visionLength)){
				if(hit.collider.tag == "Player"){
					//Debug.DrawLine (transform.position, hit.point);
					enemySounds[2].PlayOneShot(enemySounds[2].clip);
					playerSpotted = true;
					MissionMusic.chaseMusic = true;
					MissionMusic.chaseCount++;
					rayCount++;
				}
			}
			if(Physics.Raycast(visionRight, out hit, visionSide)){
				if(hit.collider.tag == "Player"){
					//Debug.DrawLine (transform.position, hit.point);
					enemySounds[2].PlayOneShot(enemySounds[2].clip);
					playerSpotted = true;
					MissionMusic.chaseMusic = true;
					MissionMusic.chaseCount++;
					rayCount++;
				}
			}
			if(Physics.Raycast(visionLeft, out hit, visionSide)){
				if(hit.collider.tag == "Player"){
					//Debug.DrawLine (transform.position, hit.point);
					enemySounds[2].PlayOneShot(enemySounds[2].clip);
					playerSpotted = true;
					MissionMusic.chaseMusic = true;
					MissionMusic.chaseCount++;
					rayCount++;
				}
			}
			if(Physics.Raycast(MoreLeft, out hit, visionShort)){
				if(hit.collider.tag == "Player"){
					//Debug.DrawLine (transform.position, hit.point);
					enemySounds[2].PlayOneShot(enemySounds[2].clip);
					playerSpotted = true;	
					MissionMusic.chaseMusic = true;
					MissionMusic.chaseCount++;
					rayCount++;
				}
			}
			if(Physics.Raycast(MoreRight, out hit, visionShort)){
				if(hit.collider.tag == "Player"){
					//Debug.DrawLine (transform.position, hit.point);
					enemySounds[2].PlayOneShot(enemySounds[2].clip);
					playerSpotted = true;
					MissionMusic.chaseMusic = true;
					MissionMusic.chaseCount++;
					rayCount++;
				}
			}
		}
	}
	void Investigate(){
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, patrolSpeed * Time.deltaTime);
	}
	// cycle through way points
	void Patrol(){
		agent.speed = patrolSpeed;
		if(agent.remainingDistance < agent.stoppingDistance){
			if (currentPoint == patrolPoints.Length - 1) {
				currentPoint = 0;
			}else{
				currentPoint++;
			}
		}
		//transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, speed * Time.deltaTime);
		agent.destination = patrolPoints [currentPoint].position;
		transform.LookAt (patrolPoints [currentPoint].position);
	}
	void OnTriggerStay(Collider obj){
		if (obj.transform.tag == "Player" && playerSpotted == true) {
			shooting = true;	
		}
	}

	// this is when the enemy stops chasing the player
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			shooting = false;
			playerSpotted = false;
			MissionMusic.chaseMusic = false;
			MissionMusic.chaseCount -= rayCount;
			rayCount = 0;
		}
	}
	// if enemy gets hit with the bullet
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "PlayerBullet") {
			MissionMusic.chaseMusic = true;
			MissionMusic.chaseCount++;
			rayCount++;
			//enemyDieSound.PlayOneShot (enemyDieSound.clip);
			health = health - 10;
			Vector3 resize = HealthBar.transform.localScale;
			resize.x -= (float)1.5/healthDamage;
			HealthBar.transform.localScale = resize;
			checkHealth ();
			playerSpotted = true;
		}
		if (obj.transform.tag == "Car") {
			destroyObject ();
		}
		if (obj.transform.tag == "Player") {
			MissionMusic.chaseMusic = true;
			MissionMusic.chaseCount++;
			rayCount++;
			playerSpotted = true;
		}
		if (obj.transform.name == "PlayerCar") {
			//this creates an instance of the particals at the players position and looks like hes exploding into pixels
			Instantiate(deathExplosion, transform.position, Quaternion.identity);
			destroyObject ();
		}
		if (obj.transform.tag == "TripMine") {
			//this creates an instance of the particals at the players position and looks like hes exploding into pixels
			Instantiate(deathExplosion, transform.position, Quaternion.identity);
			destroyObject();
		}
	}
	// the mine is a trigger not a collision
	/*void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "TripMine") {
			destroyObject();
		}
	}*/
	void checkHealth(){
		if (health <= 0) {
			//Die ();
			StartCoroutine(playDeathSound());
		}
	}
	IEnumerator playDeathSound(){
		foreach (MeshRenderer mesh in meshes) {
			mesh.enabled = false;	
		}
		foreach (BoxCollider box in boxes) {
			box.enabled = false;	
		}
		script.enabled = false;
		enemySounds[1].PlayOneShot (enemySounds[1].clip);
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (1f);
		destroyObject ();
	}
	void shootPlayer(){
		enemySounds[0].PlayOneShot (enemySounds[0].clip);
		enemyGun.shoot ();
		shooting = false;
	}
	// enemy chase player method
	void chasePlayer(){
		agent.speed = chaseSpeed;
		// move towards player
		//transform.position = Vector3.MoveTowards(rigidbody.transform.position, player.transform.position,speed*Time.deltaTime);
		agent.SetDestination (player.transform.position);
		//transform.rotation = Vector3.RotateTowards (rigidbody.transform.rotation, player.transform.position, 12, 360);
		transform.LookAt (player.transform);
	}
	void destroyObject(){
		MissionMusic.chaseCount -= rayCount;
		rayCount = 0;

		//get the object out of memory
		Destroy (gameObject);
	}
	// this function is called when the player dies... this is the basic die function, it can be changed and molded to certain needs
	//This respawns the enemy
	void Die(){
		health = 100;
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		//enemyDieSound.PlayOneShot (enemyDieSound.clip);
		// this respawns the player at his original spawn position
		transform.position = spawningPoint;
	}
}
