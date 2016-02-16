using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmallTown : MonoBehaviour {

	public GameObject enemy;
	public GameObject healthDrop;
	public GameObject ammoDrop;
	public GameObject tripMineDrop;
	public GameObject healthDrop1;
	public GameObject ammoDrop1;
	public GameObject tripMineDrop1;
	//private GameObject[] enemies;
	public Transform[] spawnPoints;
	public Transform[] itemSpawnPoints;
	public static int wave;
	public static int enemyKills;
	private int enemies;
	private int itemSpawn;
	private int cycleSpawn;

	//private bool startNewWave = false;
	private int i;
	private int j;

	// Text Objects to display in Canvas
	public Text TimerText;
	public Text WaveNumberText;
	public Text EnemyKilledText;

	// Timer Variables for spawning waves of enemies
	private float timer;
	private int timeIncrease;

	// Use this for initialization
	void Start () {
		healthDrop.SetActive (false);
		tripMineDrop.SetActive (false);
		ammoDrop.SetActive (false);
		healthDrop1.SetActive (false);
		tripMineDrop1.SetActive (false);
		ammoDrop1.SetActive (false);
		EnemyCount.enemyCount = 0;
		wave = 0;
		enemies = 1;
		cycleSpawn = 3;
		enemyKills = 0;
		timeIncrease = 15;
		timer = 10;
		WaveNumberText.text = "Wave: " + wave.ToString();
		//startNewWave = true;
	}
	
	// Update is called once per frame
	void Update () {
		EnemyKilledText.text = "Enemies Killed: " + enemyKills.ToString();
		// subtract by 1 second each second
		timer -= Time.deltaTime;

		if (itemSpawn % 2 == 0) {
			spawnItems ();
		}

		// When the timer becomes zero, spawn more enemies
		if (timer > 0){ 
			// updates timer to the screen
			TimerText.text= "Next Wave: " +timer.ToString("F0");
		}else{
			spawnEnemies ();
			// reset timer to 30 seconds
			timer=timeIncrease;
			timeIncrease += 3;
			wave++;
			enemies++;
			WaveNumberText.text = "Wave: " + wave.ToString();
		}
	}

	void spawnItems(){
		itemSpawn++;
		cycleSpawn++;
		if (cycleSpawn > 4) {
			cycleSpawn = 3;
		}
 		if(wave < 8){

			healthDrop.transform.position = itemSpawnPoints[cycleSpawn].transform.position;
			tripMineDrop.transform.position = itemSpawnPoints[cycleSpawn+1].transform.position;
			ammoDrop.transform.position = itemSpawnPoints[cycleSpawn - 1].transform.position;
			healthDrop.SetActive (true);
			tripMineDrop.SetActive (true);
			ammoDrop.SetActive (true);

		}else if(wave >= 8){

			healthDrop.transform.position = itemSpawnPoints[cycleSpawn].transform.position;
			tripMineDrop.transform.position = itemSpawnPoints[cycleSpawn+1].transform.position;
			ammoDrop.transform.position = itemSpawnPoints[cycleSpawn - 1].transform.position;
			healthDrop1.transform.position = itemSpawnPoints[cycleSpawn - 2].transform.position;
			tripMineDrop1.transform.position = itemSpawnPoints[cycleSpawn+2].transform.position;
			ammoDrop1.transform.position = itemSpawnPoints[cycleSpawn - 3].transform.position;

			healthDrop.SetActive (true);
			tripMineDrop.SetActive (true);
			ammoDrop.SetActive (true);
			healthDrop1.SetActive (true);
			tripMineDrop1.SetActive (true);
			ammoDrop1.SetActive (true);
		}
	}
	public void spawnEnemies(){
		itemSpawn++;
		for (i = 0; i < enemies; i++) {
			j = Random.Range (0,3);
			Instantiate(enemy, spawnPoints[j].position, Quaternion.identity);	
			EnemyCount.enemyCount++;
		}
	}
}
