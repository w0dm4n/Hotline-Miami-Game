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

	void Start () 
	{
		Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
		Camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
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
		if (Input.GetMouseButtonDown (Constants.LEFT_CLICK)) {
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
		catchKeys ();
		movePosition ();
		setBackgroundColor ();
	}
}
