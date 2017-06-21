using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public List<AIController> onPoint;

	void Start ()
	{
		
	}

	void Update ()
	{
		foreach (AIController Enemy in onPoint) {
			if (Enemy.seeingPlayer) {
				Enemy.transform.localPosition = Vector3.MoveTowards (transform.position, new Vector3 (transform.localPosition.x, transform.localPosition.y, Enemy.transform.position.z), 0.05f);
				Enemy.transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 ((transform.localPosition.y - Enemy.transform.position.y), (transform.localPosition.x - Enemy.transform.position.x)) * Mathf.Rad2Deg + 90);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			AIController Enemy = coll.gameObject.GetComponent<AIController> ();
			if (Enemy) {
				Enemy.onWayPoint = false;
				onPoint.Remove (Enemy);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Enemy") {
			AIController Enemy = coll.gameObject.GetComponent<AIController> ();
			if (Enemy) {
				//Enemy.onWayPoint = true;
				//onPoint.Add (Enemy);
			}
		}
	}
}
