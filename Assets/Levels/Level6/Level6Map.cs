using UnityEngine;
using System.Collections;

public class Level6Map : MonoBehaviour {

	public GameObject fastEnemy;
	public GameObject slowEnemy;

	public Transform[] spawnPoints;
	public static bool level6capsuleStolen;
	private bool enemySpawned;
	// Use this for initialization
	void Start () {
		level6capsuleStolen = false;
		enemySpawned = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (level6capsuleStolen == true && enemySpawned == false) {
			spawnEnemies();
			MissionMusic.chaseMusic = true;
		}
	}

	void spawnEnemies(){
		enemySpawned = true;
		int count = 0;
		int spawnPos = 0;
		for (count = 0; count < 5; count++) {
			if(count % 2 == 0){
				Instantiate(fastEnemy, spawnPoints[spawnPos].position, Quaternion.identity);
			}else{
				Instantiate(slowEnemy, spawnPoints[spawnPos].position, Quaternion.identity);
			}
			spawnPos++;
			if(spawnPos > 3){
				spawnPos = 0;
			}
		}
	}
}
