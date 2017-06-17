using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	public Texture2D	weaponTexture;
	public Texture2D	fireTexture;
	public bool			isOnGround = true;
	public Vector3 		goTo;
	public bool			isPushed = false;
	public float		dropSpeed = 1f;
	public float 		pushForce = 0.0f;
	public float		fireSpeed = 2f;
	public float		range;
	public AudioClip	fireSound;
	public float 		timePerShot;
	public float		shotTime;
	public bool			HTHWeapon;

	void Start ()
	{
		WeaponFactory.wp_factory.addWeapon (this);
	}

	public void unGround()
	{
		this.transform.localScale = new Vector3 (0f, 0f, 0f);
	}

	public void Ground()
	{
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
		this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, -1);
	}

	public void startPushing(Vector3 pos)
	{
		isPushed = true;
		goTo = pos;
	}

	public void Fire(Vector3 firePos, Player player)
	{
		float diff = Time.fixedTime - shotTime;
		if (diff >= timePerShot) {
			Bullet ball = GameObject.Instantiate (player.FireBase);
			ball.initiate (fireTexture, player, firePos, this);
			shotTime = Time.fixedTime;
		}
	}

	void Update ()
	{
		if (isPushed && pushForce < 0.2f) {
			transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (goTo.x, goTo.y, transform.position.z), dropSpeed);
			pushForce += 0.1f;
		}
		else if (isPushed) {
			isPushed = false;
			pushForce = 0;
		}
	}
}
