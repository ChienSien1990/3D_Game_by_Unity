using UnityEngine;
using System.Collections;

public class EnableDriving : MonoBehaviour {

	public PlayerActions playerAct;
	public PlayerDriving playerDrive;
	public CarCamera cam;
	public MissionMusic gameCam;
	
	void OnTriggerStay(Collider obj){
		if(obj.transform.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.R)){
				playerAct.enabled = !playerAct.enabled;
				cam.enabled = !cam.enabled;
				gameCam.enabled = !gameCam.enabled;
				playerDrive.enabled = !playerDrive.enabled;
			}
		}
	}
}
