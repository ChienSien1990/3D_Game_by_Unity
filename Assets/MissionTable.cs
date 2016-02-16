using UnityEngine;
using System.Collections;

public class MissionTable : MonoBehaviour {
	
	private bool showMenu = false;
	private int level;
	
	void OnGUI(){
		if (showMenu == true) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 150, Screen.height / 2-280 , 300, 210));
			GUI.Box (new Rect (0,0,300,210), "LEVEL SELECT" +
				"\n Data Capsules Collected: "+ PlayerActions.capsuleCount);
			//GUI.Label(new Rect(0,0, 100, 25), "Level Select");
			if (GUI.Button (new Rect (25,50,50,50), "1")) {
				//code to start first level
				Application.LoadLevel ("Level1");
			}
			if (GUI.Button (new Rect (75,50,50,50), "2") && PlayerActions.capsuleCount >= 1) {
				//code to start first level
				Application.LoadLevel ("Level2");
			}
			if (GUI.Button (new Rect (125,50,50,50), "3") && PlayerActions.capsuleCount >= 2) {
				//code to start first level
				Application.LoadLevel ("Level3");
			}
			if (GUI.Button (new Rect (175,50,50,50), "4") && PlayerActions.capsuleCount >= 3) {
				//code to start first level
				Application.LoadLevel ("Level4");
			}
			if (GUI.Button (new Rect (225,50,50,50), "5") && PlayerActions.capsuleCount >= 4) {
				//code to start first level
				Application.LoadLevel ("Level5");
			}
			if (GUI.Button (new Rect (25,100,50,50), "6") && PlayerActions.capsuleCount >= 5) {
				//code to start first level
				Application.LoadLevel ("Level6");
			}
			if (GUI.Button (new Rect (75,100,50,50), "7") && PlayerActions.capsuleCount >= 6) {
				//code to start first level
				Application.LoadLevel ("Level7");
			}
			if (GUI.Button (new Rect (125,100,50,50), "8") && PlayerActions.capsuleCount >= 7) {
				//code to start first level
				Application.LoadLevel ("Level8");
			}
			if (GUI.Button (new Rect (175,100,50,50), "9") && PlayerActions.capsuleCount >= 8) {
				//code to start first level
				Application.LoadLevel ("Level9");
			}
			if (GUI.Button (new Rect (225,100,50,50), "10") && PlayerActions.capsuleCount >= 9) {
				//code to start first level
				Application.LoadLevel ("Level10");
			}
			if (GUI.Button (new Rect (25,150,250,50), "SmallTown Survival")) {
				//code to start first level
				Application.LoadLevel ("SmallTown");
			}
			GUI.EndGroup();
		}
	}
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			showMenu = true;	
		}
	}
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			showMenu = false;	
		}
	}
}
