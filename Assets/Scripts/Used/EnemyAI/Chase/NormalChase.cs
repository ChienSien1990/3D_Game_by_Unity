using UnityEngine;
using System.Collections;

public class NormalChase: MonoBehaviour {

	AudioSource[] enemySounds;
	MeshRenderer[] meshes;
	BoxCollider[] boxes;
	NormalChase script;
	NavMeshAgent agent;
	public GameObject deathExplosion;
	public GameObject ammoDrop;
	public GameObject tripMineDrop;
	public GameObject enemyEyes;
	private GameObject player;
	private Vector3 spawningPoint;

	//for enemy shooting ability
	public EnemyGun enemyGun;
	public GameObject HealthBar;
	private string levelName;
	
	private float x;
	private float y;
	
	private int health = 60;
	private int healthDamage;
	public bool shooting;
	private int shootingChance = 0;
	private int shotCount = 0;
	private int reloadCount = 0;
	private bool reload = false;
	
	// Use this for initialization
	void Start () {
		levelName = Application.loadedLevelName;
		enemySounds = GetComponents<AudioSource> ();
		meshes = GetComponentsInChildren<MeshRenderer> ();
		boxes = GetComponentsInChildren<BoxCollider> ();
		script = GetComponent<NormalChase> ();
		agent = GetComponent<NavMeshAgent> ();
		spawningPoint = transform.position;
		healthDamage = health / 10;
		// make sure the enemy has the player gameObject to find
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}
		chasePlayer();		
		//check z distance bewteen player and enemy
		//if(transform.position.z - player.transform.position.z > -12 && transform.position.z - player.transform.position.z < 12){

		//enemy reloading
		if (shotCount == 20) {
			reload = true;
			reloadCount++;
			if(reloadCount == 100){
				reload = false;
				reloadCount = 0;
				shotCount = 0;
			}

		}
		//enemy shooting
		if(reload == false){
			if(shooting == true){
				shootingChance++;
				if(shootingChance == 50){
					shootPlayer();
					shotCount++;
					shootingChance = 0;
				}
			}
		}
		//}
	}
	
	void OnTriggerStay(Collider obj){
		if (obj.transform.tag == "Player") {
			shooting = true;	
		}
	}
	
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			shooting = false;
		}
		if (obj.transform.tag == "Player" && levelName == "CityLevel") {
			Destroy (gameObject);	
		}
	}

	// if enemy gets hit with the bullet
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "PlayerBullet") {
			health = health - 10;
			Vector3 resize = HealthBar.transform.localScale;
			resize.x -= (float)1.5/healthDamage;
			HealthBar.transform.localScale = resize;
			checkHealth ();
		}
		if (obj.transform.tag == "Car") {
			destroyObject ();
		}
		if (obj.transform.name == "PlayerCar") {
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
			StartCoroutine(playDeathSound());
		}
	}

	void shootPlayer(){
		enemySounds[0].PlayOneShot (enemySounds[0].clip);
		enemyGun.shoot ();
		shooting = false;
	}

	// enemy chase player method
	void chasePlayer(){
		// move towards player
		//transform.position = Vector3.MoveTowards(rigidbody.transform.position, player.transform.position,speed*Time.deltaTime);
		agent.SetDestination (player.transform.position);
		//transform.rotation = Vector3.RotateTowards (rigidbody.transform.rotation, player.transform.position, 12, 360);
		transform.LookAt (player.transform);
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

	void destroyObject(){
		if(levelName == "SmallTown"){
			EnemyCount.enemyCount--;
			SmallTown.enemyKills++;
		}
		int chance = Random.Range (0, 100);
		if (chance < 33) {
			if(chance % 2 == 0){
				Instantiate(ammoDrop, transform.position, Quaternion.identity);	
			}else{
				Instantiate(tripMineDrop, transform.position, Quaternion.identity);	
			}
		}
		//get the object out of memory
		Destroy (gameObject);
	}

	// this function is called when the player dies... this is the basic die function, it can be changed and molded to certain needs
	//This respawns the enemy
	void Die(){
		health = 100;
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		int random = Random.Range (0, 100);
		Instantiate(ammoDrop, transform.position, Quaternion.identity);
		// this respawns the player at his original spawn position
		transform.position = spawningPoint;
	}
}
