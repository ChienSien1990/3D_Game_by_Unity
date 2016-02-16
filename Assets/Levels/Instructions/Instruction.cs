using UnityEngine;
using System.Collections;

public class Instruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI(){
		
				
						//displays buttons
						if (GUI.Button (new Rect (Screen.width/2 - 310, Screen.height/2 - 155, 150,50), "Back to Menu")) {
								//code to start first level
								
										Application.LoadLevel ("StartMenu");
								
						}
				
		}
}
