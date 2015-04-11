using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private GameObject player;
	private bool isAgro;
	private float agroRange; // RS: cannot be de-agro'ed
	private float speed;
	private int damage;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		isAgro = false;
		agroRange = 5.0f;
		speed = 1.0f;
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (!isAgro)
			isAgro = Vector2.Distance (player.transform.position, transform.position) < agroRange;
		if (isAgro)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, step);
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player")
			//Debug.Log ("ayy");
			col.gameObject.BroadcastMessage ("ApplyDamage" , damage);
	}
	                                     
	                                     
}
