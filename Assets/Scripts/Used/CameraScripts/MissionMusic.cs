using UnityEngine;
using System.Collections;

public class MissionMusic : MonoBehaviour {

	public  AudioSource playerFound;
	public  AudioSource playerSneak;
	public static bool chaseMusic = false;
	public static int chaseCount = 0;
	
	// Use this for initialization
	void Start () {
		chaseCount = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		// when the enemy spots the player, play the intense chase music
		if (chaseMusic == true) {
			if(playerSneak.isPlaying){
				playerSneak.Stop();
				
				playerFound.Play ();
			}
		}
		// when no enemy is chasing the player, play the sneak music
		if (chaseCount <= 0 && chaseMusic == false) {
			if(playerFound.isPlaying){
				playerFound.Stop ();
				
				playerSneak.Play();
			}	
		}
	}
}
