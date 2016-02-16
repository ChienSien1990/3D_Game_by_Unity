using UnityEngine;
using System.Collections;

public class CarCount : MonoBehaviour {

	public static int carCount = 0;
	public CityLevel city;
	// Update is called once per frame

	void Update () {
		// allow 14 cars to be on the map at any given time
		if (carCount < 14) {
			// spawn another car
			city.spawnCar();
		}
	}
}
