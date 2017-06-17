using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Quaternion	startRotate;
	public bool 	resetDoor;
	public float	time;

	void Start ()
	{
		startRotate = transform.localRotation;
		resetDoor = false;
	}

	void Update ()
	{
		if (resetDoor) {
			float diff = Time.fixedTime - time;
			if (diff > 3f) {
				transform.localRotation = startRotate;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		time = Time.fixedTime;
		resetDoor = true;
	}
}
