using UnityEngine;
using System.Collections;

public class OPScript2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80), "Unfortunately, not everyone wants to change society and the system has been hacked by a terrorist group\n and the scientists are locked out of the system.");
		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("OP3");
			
		}
		
	}
}
