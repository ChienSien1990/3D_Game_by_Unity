using UnityEngine;
using System.Collections;

public class CityCitizenCount : MonoBehaviour {

	public static int citizenCount = 0;
	public CityLevel city;
	// Use this for initialization
	// Update is called once per frame
	void Update () {
		if (citizenCount < 500) {
			//start new wave;
			city.spawnCitizen();
		}
	}
}
