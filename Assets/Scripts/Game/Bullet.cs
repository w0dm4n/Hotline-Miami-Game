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

	void Start () 
	{
		
	}

	public void initiate(Texture2D texture, Player player, Vector3 goTo, Weapon weapon)
	{
		BulletTexture = texture;
		pos = goTo;
		weaponObject = weapon;
		range = weaponObject.range;

		transform.localPosition = player.transform.position;
		transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (pos.x, pos.y, transform.position.z), 1f);
		Rect rec = new Rect (0, 0, BulletTexture.width, BulletTexture.height);
		SpriteRenderer.sprite = Sprite.Create (BulletTexture, rec, new Vector2 (0.5f, 0.5f), 100);
		initialized = true;
		player.AudioSource.PlayOneShot (weapon.fireSound);
		dead = false;
	}

	public void checkCollider()
	{
	}

	void Update ()
	{
		checkCollider ();
		if (range > 0 && (transform.localPosition.x != pos.x && transform.localPosition.y != pos.y) && !dead) {
			transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (pos.x, pos.y, transform.position.z), weaponObject.fireSpeed);
			range--;
		} else if (initialized) {
			GameObject.DestroyObject (this.gameObject);
		}
	}

	/*void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log (coll.gameObject.tag);
	}*/

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Wall")
			dead = true;
	}
}
