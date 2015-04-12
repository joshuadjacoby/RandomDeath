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

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======

>>>>>>> 74fdf4fb19949a3a83c25b88c6287e6472c8c23b
>>>>>>> 380917bc0edfc8a396ed5ef3c9499b2fb69e9ce0
    void toggleTrap()
    {
        canMove = !canMove;
    }

	void ZeroHealth() {
		health = 0;
	}
<<<<<<< HEAD

    public void ResetPlayer() {
        health = 1;
        canMove = true;
        transform.position = start;
        gameObject.SetActive(true);
    }

=======
>>>>>>> 380917bc0edfc8a396ed5ef3c9499b2fb69e9ce0

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
