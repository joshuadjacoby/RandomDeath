using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private GameObject player;
	private Rigidbody2D r;

	private bool isAgro;
	private float agroRange; // RS: cannot be de-agro'ed
	private int damage;

	private float lastWalk; // RS: last time zombie finished walking
	private float walkTime;
	private float walkCD;
	private float speed;
	private int direction;
	private float distance;
	private bool wandering;
	private Vector2 startingPlace;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

		// RS: fighting
		isAgro = false;
		agroRange = 5.0f;
		damage = 1;

		// RS: Movement stuff
		startingPlace = transform.position;
		speed = 15.0f;
		wandering = false;
		direction = Random.Range (-4, 4);
		distance = Random.Range (1, 5);
		r = GetComponent<Rigidbody2D> ();
		walkCD = 1;
		lastWalk = Time.time;
	}

	private bool WalkTime() {
		return Time.time - lastWalk >= walkCD;
	}

	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (!isAgro) {
			isAgro = Vector2.Distance (player.transform.position, transform.position) < agroRange;
			/*
			if (!isAgro && !wandering && WalkTime ()) {
				switch (direction) {

				case 0: // RS: Move up
					r.velocity = new Vector2 (0, step);
					break;
				case 1: // RS: Move left
					r.velocity = new Vector2 (-step, 0);
					break;
				case 2: // RS: Move down
					r.velocity = new Vector2 (0, -step);
					break;
				case 3: // RS: Move right
					r.velocity = new Vector2 (step, 0);
					break;
				default:
					break;
				}
				lastWalk = Time.time;
			} else if (wandering) {
				if (Vector2.Distance (transform.position, startingPlace) < 0.5f)
					wandering = false;
			} */
		} else {
			float xMove = player.transform.position.x - transform.position.x;
			float yMove = player.transform.position.y - transform.position.y;

			r.velocity = new Vector2(step * xMove, step * yMove);
		}
			//transform.position = Vector2.MoveTowards (transform.position, player.transform.position, step);
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player")
			col.gameObject.BroadcastMessage ("ApplyDamage" , damage);
	}
	                                     
	                                     
}
