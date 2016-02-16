using UnityEngine;
using System.Collections;

public class EnemyCount : MonoBehaviour {

	public static int enemyCount = 0;
	public SmallTown st;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (enemyCount == 0) {
			//start new wave;
			//SmallTown.wave++;
			//st.spawnEnemies();
		}
	}
}
