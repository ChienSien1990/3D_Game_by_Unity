using UnityEngine;
using System.Collections;

public class EnterBuilding : MonoBehaviour {

	private bool show = false;

	void OnGUI(){
		if (show == true && CityLevel.firstEntrance == false) {
			if (GUI.Button (new Rect (Screen.width/2-50,Screen.height/2-25,100,50), "Enter" )){
				//enter agent building
				CityLevel.firstEntrance = false;
				Application.LoadLevel("AgentBuilding");
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
