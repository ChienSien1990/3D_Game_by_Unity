using UnityEngine;
using System.Collections;

public class StoryMission : MonoBehaviour {

	// create a font style
	private GUIStyle guiStyle;
	public Font fnt;

	// boolean to check to show menu or not
	private bool show = false;

	// save level name so agent bob can give player correct hint
	private string levelName;

	void Start(){
		levelName = Application.loadedLevelName;
	}
	void OnGUI(){
		guiStyle = new GUIStyle(GUI.skin.button);
		//	guiStyle.font = font;
		//guiStyle.normal.textColor = Color.white;
		guiStyle.fontSize = 22;  //must be int, obviously...
		guiStyle.font = fnt;
		if (show == true) {
			if(levelName == "CityLevel"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Hey Steve! I'm glad you're here!\n Feel free to roam the city. \n " +
				                "Look for the building with the white dot on top, I will be inside. \n" +
				                "I an coordinate missions with you from the building.\n " +
				                "We need to steal back all the data in the Blue Capsules. \n " +
				                "Come whenever you are ready. \n" +
				                " By the Way... watch out for the RED guys! \n\n\n" +
				                "Click this button to accept!",guiStyle)) {
					//code to start first level
					show = false;
					CityLevel.firstEntrance = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level1"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 1. \n" +
					"Remember: Shooting an enemy\n " +
					"and walking into their vision cone will alert them." +
					"\n Level 1 Hint: \n You can steal the data capsule without alerting any enemies!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level2"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 2. \n" +
				                "Remember: Using Trip Mines\n will kill enemies without alerting them." +
				                "\n Level 2 Hint: \n You must use atleast one trip mine to remain stealthy!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level3"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 3. \n" +
				                "Remember: The MiniMap shows \nthe Enemy's Line of Sight." +
				                "\n Level 3 Hint: \n Wait for the right time to make your move!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level4"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 4. \n" +
				                "Remember: Take your time, \n But don't be afraid to sprint!\n " +
				                "Level 4 Hint: \n Sometimes the shortest path isn't the best path",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level5"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 5. \n" +
				                "Remember: Watch the enemy's path\n" +
				                "Before making a move.\n Level 5 Hint: \n This level can be completed without using your weapons!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level6"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 6. \n" +
				                "Remember: Be aware of your Surroundings. \n Level 6 Hint: \n Rumors have spread that other Data capsules have been stolen.\n" +
				                "Watch out for an ambush!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level7"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 7. \n" +
				                "Remember: Enemies patrol on Predetermined paths. \nLevel 1 Hint: \n Use a trip mine on one of the Entrance Guards" +
				                "\nto get inside more easily",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level8"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 8. \n\n" +
				                "Remember: \n You have tripmines in your arsenal. \n\n Level 8 Hint: \n To remain unseen and steal the capsule,\n" +
				                "You must use your tripmines to kill a few guards!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level9"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 9. \n" +
				                "\n Level 9 Hint: \n Tripmines are essential, use them wisely!",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
			if(levelName == "Level10"){
				if (GUI.Button (new Rect (Screen.width/2-300,Screen.height/2-250,600,400), "Welcome to Level 10. \n" +
				                "This is your Final Mission!\n" +
				                "If you get this last piece of data\n" +
				                "we can unlock the encrypted password.\n Level 10 Hint: " +
				                "\n Kill the Guard in the Middle hallway first.\n" +
				                "Plant a trip mine and leave until he's dead. \n" +
				                "Then re-enter the enemy base",guiStyle)) {
					//code to start first level
					show = false;
					Destroy(gameObject);
				}
			}
		}
	}

	// when the player is within range of agent bob, show GUI menu else dont show the menu
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			show = true;	
		}
	}
	void OnTriggerExit(Collider obj){
		if (obj.transform.tag == "Player") {
			show = false;	
		}
	}
}
