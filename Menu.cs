using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private delegate void MenuDelegate();
	private MenuDelegate menuFunction;

	private float screenHeight;
	private float screenWidth;
	private float buttonHeight;
	private float buttonWidth;

	public Texture menuBackGround;

	// Use this for initialization
	void Start () {
		screenHeight = Screen.height;
		screenWidth = Screen.width;
		
		buttonHeight = screenHeight * 0.1f;
		buttonWidth = screenWidth * 0.3f;

		menuFunction = mainMenu;

		//eindscherm laten zien als uitgespeeld
	}
	void OnGUI()
	{
		menuFunction();
		//GUI.DrawTexture(new Rect(0,0,500,1000), menuBackGround);
	}

	void mainMenu()
	{
		GUI.Box (new Rect (Screen.width/15-80/2, 120/2, 250, 200), "Welcome To My Side Scroller\r\n\r\nControls:\r\nMove: A,S,D & Space");
		GUI.Box (new Rect (Screen.width/15-80/2, 500, 150, 25), "Made By: Daniël Brand");
		if (GUI.Button (new Rect ((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.1f, buttonWidth, buttonHeight), "Start Level"))
		{
			Application.LoadLevel("Level1");
		}
		if(GUI.Button(new Rect((screenWidth - buttonWidth) * 0.5f, screenHeight * 0.3f, buttonWidth, buttonHeight), "Quit"))
		{
			Application.Quit();
		}
	}
}
