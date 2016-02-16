using UnityEngine;
using System.Collections;

public class Credits: MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		

		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("Credits2");
			
		}
		
	}
}
