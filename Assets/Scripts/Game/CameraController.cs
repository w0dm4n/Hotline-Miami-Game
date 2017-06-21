using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CameraController : MonoBehaviour {
	public Texture2D		CursorTexture;
	public Camera			Camera;
	public Player			Player;
	public float			time;
	public bool				firstColor;
	public Canvas			Menu;
	public Button			RestartButton;
	public Button			ExitButton;
	public bool				pauseGame;

	void ResizeSpriteToScreen(SpriteRenderer sr, GameObject theSprite, Camera theCamera, int fitToScreenWidth, int fitToScreenHeight)
	{
		theSprite.transform.localScale = new Vector3(1, 1, 1);

		float width = sr.bounds.size.x;
		float height = sr.bounds.size.y;

		var worldScreenHeight = theCamera.orthographicSize * 2.0;
		var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		if (fitToScreenWidth != 0)
			theSprite.transform.localScale = new Vector3 ((float)(worldScreenWidth / width / fitToScreenWidth) * 1.2f, (float)theSprite.transform.localScale.y, (float)theSprite.transform.localScale.z);    
		if (fitToScreenHeight != 0)
			theSprite.transform.localScale = new Vector3 ((float)theSprite.transform.localScale.x, (float)(worldScreenHeight / height / fitToScreenHeight) * 1.2f, (float)theSprite.transform.localScale.z);
	}

	void Start () 
	{
		Camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		pauseGame = false;
		RestartButton.onClick.AddListener (restartGame);
		ExitButton.onClick.AddListener (exitGame);
	}

	void restartGame()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
	}

	void exitGame()
	{
		SceneManager.LoadScene ("menu", LoadSceneMode.Single);
	}

	void catchKeys()
	{
		foreach (KeyCode key in Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKey (key))
				Player.actionByKey (key);
		}
		if (Input.GetKey (Constants.MOVE_DOWN) || Input.GetKey (Constants.MOVE_UP) || Input.GetKey (Constants.MOVE_LEFT) || Input.GetKey (Constants.MOVE_RIGHT))
			Player.moveLegs ();
		else {
			Player.stopLegs ();
		}
		if (Input.GetMouseButton (Constants.LEFT_CLICK)) {
			Player.Weapon.Fire (Camera.ScreenToWorldPoint (Input.mousePosition));
		}
	}

	void movePosition()
	{
		Vector3 mousePosition = Camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z - Camera.transform.position.z));
		Player.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg + 90);
		Camera.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y, Camera.transform.position.z);
	}

	void setBackgroundColor()
	{
		float diff = Time.fixedTime - time;
		if (diff > 2f) {
			if (firstColor) {
				Camera.backgroundColor = new Color (0.2f, 0f, 0.4f, 0f);
				firstColor = false;
			} else {
				Camera.backgroundColor = new Color (0f, 0f, 0f, 1f);
				firstColor = true;
			}
			time = Time.fixedTime;
		}
	}

	void Update ()
	{
		if (pauseGame)
			return;
		catchKeys ();
		movePosition ();
		setBackgroundColor ();
	}
}
