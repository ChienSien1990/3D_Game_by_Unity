using UnityEngine;
using System.Collections;

public class OPScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI(){
		GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 200, 700, 80),"Research results were very promising for the Virtual World created by scientists to develop a better society\n for the real world.");
		
		//displays buttons
		if (GUI.Button (new Rect (Screen.width/2 +200, Screen.height/2 -270, 80,50), "Next")) {
			//code to start first level
			
			Application.LoadLevel ("OP2");
			
		}
		
	}
}
