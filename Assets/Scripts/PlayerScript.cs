using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public static int health;
    public Rigidbody2D r;
    // Use this for initialization
    void Start() {
        health = 1;
        r = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (health <= 0)
            gameObject.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate() {
        float speed = 2f;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        r.velocity = new Vector2(x*speed, y*speed);

        r.AddForce(new Vector2(), ForceMode2D.Force);


    }

}
