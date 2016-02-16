﻿using UnityEngine;
using System.Collections;

public class FastEnemy : MonoBehaviour {
	
	public GameObject deathExplosion;
	private Vector3 spawningPoint;
	public float enemytofind;
	private float x;
	private float y;
	public int distance;
	private GameObject player;
	private float speed = 7.0f;
	private int health = 40;
	private int healthDamage;
	private bool inRange;
	//for enemy shooting ability
	public EnemyGun enemyGun;
	public GameObject HealthBar;
	public bool shooting;
	private int shootingChance = 0;
	private int shotCount = 0;
	private int reloadCount = 0;
	private bool reload = false;
	// Use this for initialization
	void Start () {
		//set spawn to original point on map for respawn
		spawningPoint = transform.position;
		healthDamage = health / 10;
		// make sure the enemy has the player gameObject to find
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if(inRange || health < 60){
			chasePlayer ();
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
	}
	void OnTriggerStay(Collider obj){
		if (obj.transform.tag == "Player") {
			inRange = true;
			shooting = true;	
		}
	}
	
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			inRange = false;
			shooting = false;
		}
	}
	// if enemy gets hit with the bullet
	void OnCollisionEnter(Collision obj){
		if (obj.transform.tag == "PlayerBullet") {
			health = health - 20;
			Vector3 resize = HealthBar.transform.localScale;
			resize.x -= (float)1.5/healthDamage;;
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
			destroyObject();
		}
	}
	void shootPlayer(){
		enemyGun.shoot ();
		shooting = false;
	}
	// enemy chase player method
	void chasePlayer(){
		// move towards player
		transform.position = Vector3.MoveTowards(rigidbody.transform.position, player.transform.position,speed*Time.deltaTime);
		transform.LookAt (player.transform);
	}
	void destroyObject(){
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		//get the object out of memory
		Destroy (gameObject);
	}
	// this function is called when the player dies... this is the basic die function, it can be changed and molded to certain needs
	void Die(){
		health = 60;
		//this creates an instance of the particals at the players position and looks like hes exploding into pixels
		Instantiate(deathExplosion, transform.position, Quaternion.identity);
		// this respawns the player at his original spawn position
		transform.position = spawningPoint;
	}
}
