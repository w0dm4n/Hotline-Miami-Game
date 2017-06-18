using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

	public bool 			isDying;
	public float			time;
	public bool				blink;
	public int				blinkedTimes;
	public Player			player;
	public bool				seeingPlayer;
	public float			followTimer;
	public float			shootTimer;
	public Animator			Animator;
	public bool				isMooving;
	public AudioSource		AudioSource;
	public AudioClip		DeathSound;
	public EquipedWeapon	Weapon;
	public Bullet			FireBase;
	public Vector3 			startPosition;


	public void moveLegs()
	{
		if (!isMooving) {
			Animator.Play ("Movement legs");
			isMooving = true;
		}
	}

	public void stopLegs()
	{
		if (isMooving) {
			Animator.Play ("Legs animation");
			isMooving = false;
		}
	}

	void Start ()
	{
		isDying	= false;
		time	= Time.fixedTime;
		blink	= false;
		blinkedTimes = 0;
		player = GameObject.Find ("Player").GetComponent<Player>();
		seeingPlayer = false;
		isMooving = false;
		Weapon.weaponObject = GameObject.Instantiate (Weapon.weaponObject);
		startPosition = transform.localPosition;
		FollowingPath = true;
	}

	public void Die()
	{
		if (!isDying) {
			player.Score += 1;
			isDying = true;
		}
	}

	void DieLoop()
	{
		if (blinkedTimes > 4) {
			GameObject.DestroyObject (this.gameObject);
			return;
		}
		float diff = Time.fixedTime - time;
		if (diff > 0.1f) {
			AudioSource.PlayOneShot (DeathSound);
			if (blink) {
				this.transform.localScale = new Vector3 (0f, 0f, this.transform.localScale.z);
				blink = false;
			} else {
				this.transform.localScale = new Vector3 (1f, 1f, this.transform.localScale.z);
				blink = true;
			} 
			this.transform.localRotation = new Quaternion(this.transform.localRotation.x, this.transform.localRotation.y, this.transform.localRotation.z + 100, this.transform.localRotation.w);
			time = Time.fixedTime;
			blinkedTimes++;
		}
	}

	void followPlayer()
	{
		float diff = Time.fixedTime - followTimer;
		transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (player.transform.localPosition.x, player.transform.localPosition.y, transform.position.z), 0.05f);

		transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.localPosition.y - transform.position.y), (player.transform.localPosition.x - transform.position.x)) * Mathf.Rad2Deg + 90);
		followTimer = Time.fixedTime;
		moveLegs ();
	}

	void shootPlayer()
	{
		float diff = Time.fixedTime - shootTimer;
		if (diff > Weapon.weaponObject.fireSpeed) {
			Weapon.Fire (player.transform.localPosition);
			shootTimer = Time.fixedTime;
		}
	}

	public GameObject		FollowPath;
	public bool				FollowingPath;
	void followPath()
	{
		if (FollowingPath) {
			if (transform.localPosition.x != FollowPath.transform.localPosition.x && transform.localPosition.y != FollowPath.transform.localPosition.y) {
				moveLegs ();
				transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (FollowPath.transform.localPosition.x, FollowPath.transform.localPosition.y, transform.position.z), 0.05f);
				transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((FollowPath.transform.localPosition.y - transform.position.y), (FollowPath.transform.localPosition.x - transform.position.x)) * Mathf.Rad2Deg + 90);
			} else {
				FollowingPath = false;
			}
		} else {
			if (transform.localPosition.x != startPosition.x && transform.localPosition.y != startPosition.y) {
				moveLegs ();
				transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (startPosition.x, startPosition.y, transform.position.z), 0.05f);
				transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((startPosition.y - transform.position.y), (startPosition.x - transform.position.x)) * Mathf.Rad2Deg + 90);
			} else {
				FollowingPath = true;
			}
		}
	}

	void Update ()
	{
		if (isDying) {
			DieLoop ();
		} else {
			if (seeingPlayer) {
				followPlayer ();
				shootPlayer ();
			} else {
				followPath ();
			}
			// AI Action
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		/*if (coll.gameObject.name == "Player") {
			seeingPlayer = false;
		}*/
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "Player") {
			seeingPlayer = true;
		}
	}
}
