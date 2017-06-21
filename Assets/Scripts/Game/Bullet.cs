using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Texture2D		BulletTexture;
	public SpriteRenderer 	SpriteRenderer;
	public Vector3			pos;
	public Weapon			weaponObject;
	public float			range;
	public bool				initialized;
	public bool				dead;
	public Player 			Player;
	public AIController		Enemy;
	public Vector2			direction;

	public void initiate(Texture2D texture, Player player, Vector3 goTo, Weapon weapon, AIController Enemy)
	{
		BulletTexture = texture;
		pos = goTo;
		weaponObject = weapon;
		range = weaponObject.range;
		this.Player = player;
		this.Enemy = Enemy;
		transform.rotation = transform.rotation * Quaternion.Euler (0, 0, 90);
	
		if (player) {
			transform.localPosition = player.transform.position;
			transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (pos.x, pos.y, transform.position.z), 1f);
			Rect rec = new Rect (0, 0, BulletTexture.width, BulletTexture.height);
			SpriteRenderer.sprite = Sprite.Create (BulletTexture, rec, new Vector2 (0.5f, 0.5f), 100);
			initialized = true;
			player.AudioSource.PlayOneShot (weapon.fireSound);
		} else if (Enemy) {
			transform.localPosition = Enemy.transform.position;
			transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (pos.x, pos.y, transform.position.z), 1f);
			Rect rec = new Rect (0, 0, BulletTexture.width, BulletTexture.height);
			SpriteRenderer.sprite = Sprite.Create (BulletTexture, rec, new Vector2 (0.5f, 0.5f), 100);
			initialized = true;
			Enemy.AudioSource.PlayOneShot (weapon.fireSound);
		}
		direction = new Vector2 (goTo.x - transform.position.x, goTo.y - transform.position.y);
		direction.Normalize ();
		dead = false;
	}

	void Update ()
	{
		if (range > 0 && !dead) {
			transform.Translate (direction * weaponObject.fireSpeed, Space.World);
			range--;
		} else if (initialized) {
			GameObject.DestroyObject (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Wall")
			dead = true;
		else if (coll.gameObject.tag == "Enemy") {
			if (this.Enemy == null) // AI can't kill AI
				coll.gameObject.GetComponent<AIController> ().Die ();
		} else if (coll.gameObject.name == "Player") {
			if (this.Player == null) {
				coll.gameObject.GetComponent<Player> ().Die ();
				dead = true;
			}
		}
	}
}
