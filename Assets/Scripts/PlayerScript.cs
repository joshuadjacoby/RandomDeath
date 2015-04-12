using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public int health;
    private Rigidbody r;
    public bool canMove;
    public Vector3 start;

    // Use this for initialization
    void Start() {
        health = 1;
        r = GetComponent<Rigidbody>();
        canMove = true;
        start = transform.position;
    }

	void ApplyDamage(int i) {
		health -= i;
	}

    void toggleTrap()
    {
        canMove = !canMove;
    }

	void ZeroHealth() {
		health = 0;
	}

    public void ResetPlayer() {
        health = 1;
        canMove = true;
        transform.position = start;
        gameObject.SetActive(true);
    }


    void Update() {
        if (health <= 0)
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            float speed = 2.0f;

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            r.velocity = new Vector3(x * speed, 0, y*speed);
        }
        else
        {
            r.velocity = Vector3.zero;
        }
    }
}
