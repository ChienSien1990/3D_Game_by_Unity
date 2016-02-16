using UnityEngine;
using System.Collections;

public class EDScripty2 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80), "Success! Great job Agent, You saved the world!");
		
		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Credits")) {
			//code to start first level
			
			Application.LoadLevel ("Credits1");
			
		}
		
	}
}
