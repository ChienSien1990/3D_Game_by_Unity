using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// for saving data
	public static int savedCapsule;
	// for level selection.. allows for going back to main menu when complete
	public static bool levelSelection;

	// map display
	public Texture2D mapDisplay;
	// variables for menu placement and menu logic
	public float guiPlacementY, guiPlacementOffset;
	private bool inMain = true;
	private bool inHowTo = false;
	private bool inLevelSelect = false;
	private bool ininstruct=false;
	public static string difficulty="Normal";
	private bool insetting=false;
	
	void OnGUI(){
		// main menu
		if (inMain) {
			//displays buttons
			GUI.BeginGroup (new Rect (Screen.width / 2 - 150, Screen.height / 2+20 , 300, 260));
			if (GUI.Button (new Rect (10, 5, 270, 40), "Play Game")) {
				//code to start first level
				levelSelection = false;
				savedCapsule = 0;
				PlayerActions.capsuleCount = 0;
				CityLevel.firstEntrance = true;
				if (difficulty=="Easy") {
					PlayerActions.deathCount=5;

				}
				else if (difficulty=="Normal") {
					PlayerActions.deathCount=3;

				}
				else if (difficulty=="Hard") {
					difficulty="Hard";

				}
				Application.LoadLevel("OP1");
			}
			if (GUI.Button (new Rect (10, 45, 270, 40), "Load Game")) {
				//code to start first level
				if(savedCapsule > 0){
					levelSelection = false;
					PlayerActions.capsuleCount = savedCapsule;
					CityLevel.firstEntrance = false;
					Application.LoadLevel("CityLevel");
				}
			}
			if (GUI.Button (new Rect (10, 85, 270, 40), "Level Selection")) {
				inMain = false;
				inHowTo = false;
				inLevelSelect = true;
				ininstruct=false;
			}if (GUI.Button (new Rect (10, 125, 270, 40), "Instructions")) {
				inMain = false;
				inLevelSelect = false;
				inHowTo = false;
				ininstruct=true;
			}
			if (GUI.Button (new Rect (10, 165, 270, 40), "Settings")) {
				inMain = false;
				inLevelSelect = false;
				inHowTo = false;
				ininstruct=false;
				insetting =true;

			}
			if (GUI.Button (new Rect (10, 205, 270, 40), "Exit Game")) {
				inMain = false;
				inLevelSelect = false;
				inHowTo = false;
				ininstruct=false;
				Application.Quit ();
			}
			GUI.EndGroup();
		}
		if (ininstruct) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 150, Screen.height / 2+20 , 300, 260));
			GUI.Box (new Rect (0,0,290,260), "Instructions");
			if (GUI.Button (new Rect (10, 45, 270, 40), "How To Play")) {
				inMain = false;
				inLevelSelect = false;
				inHowTo = true;
			}
			if (GUI.Button (new Rect (10, 85, 270, 40), "Control")) {
				inMain = false;
				inLevelSelect = false;
				inHowTo = false;
				Application.LoadLevel("Instruction");
			}
			if (GUI.Button (new Rect (10,125,270,40), "Back to Main Menu")) {
				//code to start level
				inMain = true;
				inLevelSelect = false;
				ininstruct=false;
			}
			GUI.EndGroup ();
		
		}
		if (insetting) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 150, Screen.height / 2+20 , 300, 260));
			GUI.Box (new Rect (0,0,290,260), "Difficulty");
			if (GUI.Button (new Rect (10, 45, 270, 40), "Easy")) {
				PlayerActions.deathCount=5;
				difficulty="Easy";
			}
			if (GUI.Button (new Rect (10, 85, 270, 40), "Normal")) {
				PlayerActions.deathCount=3;
				difficulty="Normal";
			}
			if (GUI.Button (new Rect (10,125,270,40), "Hard")) {
				difficulty="Hard";
				PlayerActions.deathCount=1;
			}
			if (GUI.Button (new Rect (10,165,270,40), "Back to Main Menu")) {
				inMain = true;
				inLevelSelect = false;
				ininstruct=false;
				insetting = false;
			}
			GUI.Label(new Rect (10, 225, 270,40), " Difficulty: " +difficulty+" selected"); 
			GUI.EndGroup ();
			
		}
		if (inHowTo) {
			if (GUI.Button (new Rect (300, 560, 200, 40), "Back to Main Menu")) {
				inMain = true;
				inLevelSelect = false;
				inHowTo = false;
				ininstruct=false;
			}
			GUI.DrawTexture(new Rect(-200, -90, 1200, 700), mapDisplay);
		}
		// enter level selection screen
		if (inLevelSelect) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 150, Screen.height / 2+20 , 300, 260));
			GUI.Box (new Rect (0,0,290,260), "LEVEL SELECT");
			PlayerActions.deathCount=1;
			//GUI.Label(new Rect(0,0, 100, 25), "Level Select");
			if (GUI.Button (new Rect (20,50,50,50), "1")) {
				//code to start first level
				levelSelection = true;
				Application.LoadLevel ("Level1");
			}
			if (GUI.Button (new Rect (70,50,50,50), "2")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level2");
			}
			if (GUI.Button (new Rect (120,50,50,50), "3")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level3");
			}
			if (GUI.Button (new Rect (170,50,50,50), "4")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level4");
			}
			if (GUI.Button (new Rect (220,50,50,50), "5")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level5");
			}
			if (GUI.Button (new Rect (20,100,50,50), "6")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level6");
			}
			if (GUI.Button (new Rect (70,100,50,50), "7")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level7");
			}
			if (GUI.Button (new Rect (120,100,50,50), "8")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level8");
			}
			if (GUI.Button (new Rect (170,100,50,50), "9")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level9");
			}
			if (GUI.Button (new Rect (220,100,50,50), "10")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("Level10");
			}
			if (GUI.Button (new Rect (20,150,250,50), "SmallTown Survival")) {
				//code to start level
				levelSelection = true;
				Application.LoadLevel ("SmallTown");
			}
			if (GUI.Button (new Rect (20,200,250,50), "Back to Main Menu")) {
				//code to start level
				inMain = true;
				inLevelSelect = false;
			}
			GUI.EndGroup();
		}
	}
}

