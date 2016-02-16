using UnityEngine;
using System.Collections;

public class OPScript3 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80), "However, the scientists can still digitize humans and download them into the system\n through a backdoor.");

		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("OP4");
			
		}
		
	}
}
