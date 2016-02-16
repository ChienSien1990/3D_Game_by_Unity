using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	public PlayerActions player;
	private string levelName;
	void Start(){
				levelName = Application.loadedLevelName;
		}
	void OnTriggerEnter(Collider obj){
		if (obj.transform.tag == "Player") {
			if(player.capsuleCheck() == true && levelName == "Level10"){
				Application.LoadLevel("ED1");
			}
			else if(player.capsuleCheck() == true && StartMenu.levelSelection== false){
				Application.LoadLevel("AgentBuilding");
			}
			else if(player.capsuleCheck() == true && StartMenu.levelSelection== true){
				Application.LoadLevel("StartMenu");
			}
		}
	}
}
