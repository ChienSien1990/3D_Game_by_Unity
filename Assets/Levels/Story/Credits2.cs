using UnityEngine;
using System.Collections;

public class Credits2: MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		
		
		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 100,50), "Back To Menu")) {
			//code to start first level
			
			Application.LoadLevel ("StartMenu");
			
		}
		
	}
}
