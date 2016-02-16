using UnityEngine;
using System.Collections;

public class OPScript4 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80), "That’s where you come in, we need you to enter the world and steal the fragmented data\n capsules containing an encryption key.");

		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("CityLevel");
			
		}
		
	}
}
