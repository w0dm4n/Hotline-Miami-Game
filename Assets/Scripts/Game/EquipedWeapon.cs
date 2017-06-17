using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipedWeapon : MonoBehaviour {

	public Weapon			weaponObject;
	public SpriteRenderer	SpriteRenderer;
	public bool				spriteCreated = false;
	public Player			Player;

	void Start () 
	{
		
	}

	public void unEquip()
	{
		spriteCreated = false;
		SpriteRenderer.sprite = null;
		weaponObject = null;
	}

	public void pushWeaponObject(Vector3 goTo)
	{
		weaponObject.transform.localPosition = Player.transform.localPosition;
		weaponObject.Ground ();
		weaponObject.startPushing (goTo);
	}

	public void Drop (Vector3 goTo)
	{
		pushWeaponObject (goTo);
		unEquip ();
	}

	public void Fire(Vector3 firePos)
	{
		if (weaponObject != null) {
			weaponObject.Fire (firePos, Player);
		}
	}

	void Update ()
	{
		if (weaponObject != null) {
			if (!spriteCreated) {
				Rect rec = new Rect (0, 0, weaponObject.weaponTexture.width, weaponObject.weaponTexture.height);
				SpriteRenderer.sprite = Sprite.Create (weaponObject.weaponTexture, rec, new Vector2 (0.5f, 0.5f), 100);
				spriteCreated = true;
			}
		}
	}
}
