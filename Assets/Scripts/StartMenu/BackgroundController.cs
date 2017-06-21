using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundController : MonoBehaviour {

	public SpriteRenderer	backGround;
	public GameObject		backGroundObject;
	public Camera			Camera;
	public AudioSource		AudioSource;
	public AudioClip[]		AudioClips;
	public GameObject 		Logo;
	public float			time;
	bool					LogoUp;

	public Button			StartButton;
	public Button			ExitButton;
	public Texture2D		CursorTexture;

	void ResizeSpriteToScreen(SpriteRenderer sr, GameObject theSprite, Camera theCamera, int fitToScreenWidth, int fitToScreenHeight)
	{
		theSprite.transform.localScale = new Vector3(1, 1, 1);

		float width = sr.bounds.size.x;
		float height = sr.bounds.size.y;

		var worldScreenHeight = theCamera.orthographicSize * 2.0;
		var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		if (fitToScreenWidth != 0)
			theSprite.transform.localScale = new Vector3 ((float)(worldScreenWidth / width / fitToScreenWidth), (float)theSprite.transform.localScale.y, (float)theSprite.transform.localScale.z);    
		if (fitToScreenHeight != 0)
			theSprite.transform.localScale = new Vector3 ((float)theSprite.transform.localScale.x, (float)(worldScreenHeight / height / fitToScreenHeight), (float)theSprite.transform.localScale.z);
	}

	void Start ()
	{
		Camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		ResizeSpriteToScreen (backGround, backGroundObject, Camera, 1, 1);
		AudioSource.PlayOneShot (AudioClips[0], 1.0f);
		//Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);

		StartButton.onClick.AddListener (onStartGame);
		ExitButton.onClick.AddListener (onExitGame);
	}

	void updateBackgroundSize()
	{
		float diff = Time.fixedTime - time;
		if (diff > 0.3f) {
			if (LogoUp) {
				Logo.transform.localScale = new Vector3 (Logo.transform.localScale.x - 1, Logo.transform.localScale.y - 1, Logo.transform.localScale.z);
				LogoUp = false;
			} else {
				Logo.transform.localScale = new Vector3 (Logo.transform.localScale.x + 1, Logo.transform.localScale.y + 1, Logo.transform.localScale.z);
				LogoUp = true;
			}
			time = Time.fixedTime;
		}
	}

	void onStartGame()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
	}

	void onExitGame()
	{
		Debug.Log ("Exiting the game !");
		Application.Quit ();
	}

	void Update ()
	{
		updateBackgroundSize();
	}
}
