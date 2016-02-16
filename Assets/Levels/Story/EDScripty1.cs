using UnityEngine;
using System.Collections;

public class EDScripty1 : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI(){
		
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80), "Now that we have all the parts of the encryption key back, we can take back \nfull control over the system.");
		
		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("ED2");
			
		}
		
	}
}
