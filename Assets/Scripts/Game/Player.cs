using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public Animator			Animator;
	public EquipedWeapon	Weapon;
	public Camera			Camera;
	public CameraController	CameraController;
	public Bullet			FireBase;
	public AudioSource		AudioSource;
	public AudioClip		gameMusic;
	public AudioClip		equipedSound;
	public Text				WeaponText;
	public int 				Score;
	public AudioClip		DeathSound;
	public bool				DebugMode = false;
	public Canvas			Menu;
	public Text 			LifePointsText;
	public Text				ScoreText;
	public int				LifePoints;

	void Start ()
	{
		Camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		CameraController = Camera.GetComponent<CameraController> ();
		LifePoints = 1;
	}

	public void moveLegs()
	{
		Animator.Play ("Movement legs");
	}

	public void stopLegs()
	{
		Animator.Play ("Legs animation");
	}

	void moveDown()
	{
		transform.localPosition -= new Vector3 (0f, 0.15f, 0f);
	}

	void moveUp()
	{
		transform.localPosition += new Vector3 (0f, 0.15f, 0f);
	}

	void moveLeft()
	{
		transform.localPosition -= new Vector3 (0.15f, 0f, 0f);
	}

	public void Die()
	{
		LifePoints--;
		if (LifePoints == 0) {
			Menu.transform.localScale = new Vector3 (0.01302f, 0.01302f, 0.01302f);
			CameraController.Menu.transform.localPosition = new Vector3 (CameraController.Menu.transform.localPosition.x, CameraController.Menu.transform.localPosition.y, 1f);
			CameraController.pauseGame = true;
		}
	}

	void moveRight()
	{
		transform.localPosition += new Vector3 (0.15f, 0f, 0f);
	}

	void findWeaponOnTheGround(Vector3 pos)
	{
		foreach (Weapon weapon in WeaponFactory.wp_factory.weapons) {
			float x = pos.x - weapon.transform.position.x;
			float y = pos.y - weapon.transform.position.y;
			x = (x < 0f) ? -x : x;
			y = (y < 0f) ? -y : y;
		
			if ((x < 1f && x > 0f) && (y < 1f && y > 0f)) {
				Weapon.weaponObject = weapon;
				weapon.unGround ();
				AudioSource.PlayOneShot (equipedSound);
				break;
			}
		}
	}

	public void actionByKey(KeyCode Key)
	{
		switch (Key) {
		case Constants.MOVE_DOWN:
			moveDown ();
			break;
		case Constants.MOVE_UP:
			moveUp ();
			break;
		case Constants.MOVE_LEFT:
			moveLeft ();
			break;
		case Constants.MOVE_RIGHT:
			moveRight ();
			break;
		case Constants.EQUIP_WEAPON:
			if (Weapon.weaponObject == null) {
				findWeaponOnTheGround (this.transform.localPosition);
			}
			break;
		case Constants.DROP_WEAPON:
			if (Weapon.weaponObject != null) {
				Weapon.Drop (Camera.ScreenToWorldPoint (Input.mousePosition));
				WeaponText.text = "NO WEAPON";
			}
			break;
		}
	}

	void Update ()
	{
		if (!AudioSource.isPlaying)
			AudioSource.PlayOneShot (gameMusic);
		LifePointsText.text = LifePoints.ToString ();
		ScoreText.text = "Score : " + Score.ToString ();
	}
}
