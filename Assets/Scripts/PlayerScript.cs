using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public static int health;
    public Rigidbody2D r;
	// Use this for initialization
	void Start () {
        health = 1;
        r = GetComponent<Rigidbody2D>();
	}
	
    void Update()
    {
        if (health <= 0)
            gameObject.SetActive(false);
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey("w") || Input.GetKey("up"))
            transform.position += new Vector3(0,0.05f,0);
        if (Input.GetKey("a") || Input.GetKey("left"))
            transform.position += new Vector3(-0.05f, 0, 0);
        if (Input.GetKey("s") || Input.GetKey("down"))
            transform.position += new Vector3(0, -0.05f, 0);
        if (Input.GetKey("d") || Input.GetKey("right"))
            transform.position += new Vector3(0.05f, 0, 0);
	}

}
