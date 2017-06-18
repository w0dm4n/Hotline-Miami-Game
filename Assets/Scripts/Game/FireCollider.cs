using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour {

	public AIController	CurrentEnemy;
	public Player 		Player;

	void Start ()
	{
		
	}

	void Update ()
	{
		
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Bullet") {
			Bullet ball = coll.gameObject.GetComponent<Bullet> ();
			if (ball.Player != null)
				CurrentEnemy.seeingPlayer = true;
		}
	}
}
