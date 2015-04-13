using UnityEngine;
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

		// RS: Movement stuff
		lastSeen = player.transform.position;
		direction = Random.Range (-2, 4);
		distance = Random.Range (0.5f, 3.0f);
		wandering = false;
		speed = 0.90f;
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

	// Update is called once per frame
	void Update () {
        if (health <= 0)
            Destroy(gameObject);

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
