using UnityEngine;
using System.Collections;

public class Blinking : MonoBehaviour {

	private bool isBlinking = true;
	private MeshRenderer mesh;
	private int i = 0;
	// Use this for initialization
	void Start () {
		if (mesh == null) {
			mesh = GetComponent<MeshRenderer>();		
		}
		StartCoroutine ("blink");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator blink(){
		isBlinking = false;
		for (int i=0;i<6;i++)
		{
			yield return new WaitForSeconds(0.15f);
			mesh.enabled = !mesh.enabled;
		}
		
		mesh.enabled = true;
		yield return new WaitForSeconds (0.05f);
		isBlinking = false;
		
	}
}
