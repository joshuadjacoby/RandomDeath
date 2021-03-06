﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	private GameObject player;
	private Rigidbody r;

	private bool isAgro;
	private float agroRange; // RS: cannot be de-agro'ed
	private int damage;
    private int health;

	private int direction;
	private float distance;
	private Vector3 lastSeen;
	private bool wandering;
	private float speed;

    public Material normal;
    public Material grab;
    private MeshRenderer mr;

    public AudioClip[] clips;
    private AudioSource source;
    private float grtimer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		r = GetComponent<Rigidbody> ();

		// RS: fighting
		isAgro = false;
		agroRange = 5.0f;
		damage = 1;
        health = 2;

        mr = transform.Find("Mesh").GetComponent<MeshRenderer>();
        mr.material = normal;

        source = GetComponent<AudioSource>();
        grtimer = 0;
        
		// RS: Movement stuff
		lastSeen = player.transform.position;
		direction = Random.Range (-2, 4);
		distance = Random.Range (0.5f, 3.0f);
		wandering = false;
        if (Random.Range(1, 100) == 100)
            speed = 1.5f;
        else
		    speed = Random.Range (0.6f, 0.95f);
	}

	bool checkForPlayer () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, agroRange))
			return hit.collider.tag == "Player";
		else 
		    return false;    	
	}

    void ApplyDamage(int i)
    {
        health -= i;
    }

    void Growling()
    {
        if(Random.value < 0.0005)
            source.PlayOneShot(clips[0]);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (health <= 0)
            Destroy(gameObject);
        Growling();

		float step = speed * Time.deltaTime;
		if (!isAgro)
			isAgro = checkForPlayer();
		else {
            RaycastHit see;
            float xMove;
            float yMove;
            if(Physics.Raycast (transform.position, transform.forward, out see) 
                && see.collider.tag == "Player"){
			    xMove = player.transform.position.x - transform.position.x;
			    yMove = player.transform.position.z - transform.position.z;
                lastSeen = player.transform.position;
            }
            else {
                xMove = lastSeen.x - transform.position.x;
                yMove = lastSeen.z - transform.position.z;
            }
            r.velocity = Vector3.Normalize(new Vector3(step * xMove, 0, step * yMove));
		}
	}

	void OnCollisionEnter (Collision col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.BroadcastMessage("ApplyDamage", damage);
            mr.material = grab;
        }
	}
	                                     
	                                     
}
