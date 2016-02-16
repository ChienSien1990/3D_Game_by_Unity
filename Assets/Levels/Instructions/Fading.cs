using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;   // the texture that will overly the screen
	public float fadeSpeed = 0.8f;		

	private int drawDepth = -1000;	   // the texture/'s order in the draw hierarchy;
	private float alpha = 1.0f;		   // the texture's alpha value between 0 and 1
	private int fadeDir = -1;		   // the direction to fade: in = -1 or out = 1

	void OnGUI(){
				alpha += fadeDir * fadeSpeed * Time.deltaTime;
				alpha = Mathf.Clamp01 (alpha);

				GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
				GUI.depth = drawDepth;
				GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
		}
	public float BeginFade(int direction){
				fadeDir = direction;
				return (fadeSpeed);
		}

	void OnLevelWasLoaded(){
		BeginFade (-1);
	}

}
