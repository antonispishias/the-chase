using UnityEngine;
using System.Collections;

public class FaderScript : MonoBehaviour {

	//texture that will overlay the screen.
	public Texture2D fadeOutTexture;
	
	//fading speed. 
	public float fadeSpeed = 0.8f;   
	
	//the texture's alpha value (0 to 1).
	private float alpha = 1.0f;      
	
	//order in the draw's hierarchy (highest priority).
	private int drawDepth = -1000;   
	
	//fade direction: in = -1 (towards texture not visible), 
	//				  out = 1 (towards texture visible).
	private int fadeDir = -1;   
	
	void OnGUI() {
		//Modify the alpha value gradually and 
		// use deltaTime to talk in seconds.
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		
		//Set colour of our texture. Keep the color the same and 
		// change the alpha channel.
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		Rect dimension = new Rect (0, 0, Screen.width, Screen.height);
		GUI.DrawTexture (dimension, fadeOutTexture);
	}
	
	public float BeginFade(int direction){
		fadeDir = direction;
		return 1.0f/fadeSpeed;
	}
	
	void OnLevelLoaded(){
		BeginFade (-1);
	}  
}
