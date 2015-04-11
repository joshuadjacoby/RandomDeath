using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public int health;
    public Rigidbody2D r;
    public bool canMove;

    // Use this for initialization
    void Start() {
        health = 1;
        r = GetComponent<Rigidbody2D>();
        canMove = true;
    }

	void ApplyDamage(int i) {
		health -= i;
	}

<<<<<<< HEAD
=======
//<<<<<<< HEAD
>>>>>>> 73e475d2f5d31b1325fa48b4575600435de6dc28
    void toggleTrap()
    {
        canMove = !canMove;
    }
<<<<<<< HEAD

	void ZeroHealth() {
		health = 0;
	}
=======
//=======
	void ZeroHealth() {
		health = 0;
	}
//>>>>>>> b3b1e669e351c5a206bc4dd214ab9728074c8bbf
>>>>>>> 73e475d2f5d31b1325fa48b4575600435de6dc28

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

            r.velocity = new Vector2(x * speed, y * speed);

            r.AddForce(new Vector2(), ForceMode2D.Force);
        }
        else
        {
            r.velocity = new Vector2(0, 0);
        }
    }
}
