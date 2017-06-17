using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {

	public List<Weapon>				weapons;
	public static WeaponFactory		wp_factory;

	void Awake ()
	{
		if (wp_factory == null)
			wp_factory = this;
	}

	public void addWeapon(Weapon newWeapon)
	{
		this.weapons.Add (newWeapon);
	}
}
