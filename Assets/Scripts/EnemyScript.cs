using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private GameObject player;
	private Rigidbody r;

	private bool isAgro;
	private float agroRange; // RS: cannot be de-agro'ed
	private int damage;
    private int health;

	/*
	private float lastWalk; // RS: last time zombie finished walking
	private float walkTime;
	private float walkCD;
	*/
	private int direction;
	private float distance;
	private Vector3 startingPlace;
	private bool wandering;
	private float speed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		r = GetComponent<Rigidbody> ();

		// RS: fighting
		isAgro = false;
		agroRange = 3.0f;
		damage = 1;
        health = 2;

		// RS: Movement stuff
		/*
		startingPlace = transform.position;
		direction = Random.Range (-4, 4);
		distance = Random.Range (1, 5);
		walkCD = 1;
		lastWalk = Time.time;
		*/
		startingPlace = transform.position;
		direction = Random.Range (-2, 4);
		distance = Random.Range (0.5f, 3.0f);
		startingPlace = transform.position;
		wandering = false;
		speed = 15.0f;
	}

	bool checkForPlayer () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, agroRange))
			return hit.collider.tag == "Player";
		else 
		    return false;    	
		//return Vector2.Distance (player.transform.position, transform.position) < agroRange;
	}

    void ApplyDamage(int i)
    {
        health -= i;
    }

	// Update is called once per frame
	void Update () {
        if (health <= 0)
            Destroy(gameObject);

		float step = speed * Time.deltaTime;
		if (!isAgro)
			isAgro = checkForPlayer();
		else {
			float xMove = player.transform.position.x - transform.position.x;
			float yMove = player.transform.position.z - transform.position.z;
			r.velocity = new Vector3(step * xMove, 0, step * yMove);
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Player")
			col.gameObject.BroadcastMessage ("ApplyDamage" , damage);
	}
	                                     
	                                     
}
