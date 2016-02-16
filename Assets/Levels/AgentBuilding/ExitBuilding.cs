using UnityEngine;
using System.Collections;

public class ExitBuilding : MonoBehaviour {
	
	private bool show = false;
	void OnGUI(){
		if (show == true) {
			if (GUI.Button (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "Exit" )){
				//enter agent building
				Application.LoadLevel("CityLevel");
			}
		}
	}
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			// add gui pop up text to inform player
			show = true;
		}
	}
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			// add gui pop up text to inform player
			show = false;
		}
	}
}
