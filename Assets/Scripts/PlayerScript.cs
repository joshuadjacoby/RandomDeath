using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public int health;
    private Rigidbody r;
    public bool canMove;
    public bool slow;
    public float slowTimer;
    public Vector3 start;

    // Use this for initialization
    void Start()
    {
        health = 1;
        r = GetComponent<Rigidbody>();
        canMove = true;
        slow = false;
        slowTimer = 3.0f;
        start = transform.position;
    }

    void ApplyDamage(int i)
    {
        health -= i;
    }

    void toggleTrap()
    {
        canMove = !canMove;
    }

    void toggleSlow()
    {
        slow = !slow;
    }

    void ZeroHealth()
    {
        health = 0;
    }


    public void ResetPlayer()
    {
        health = 1;
        canMove = true;
        transform.position = start;
        gameObject.SetActive(true);
    }


    void Update()
    {
        if (health <= 0)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            float speed = 2.0f;

            if (slow && slowTimer > 0)
            {
                speed *= .3f;
                slowTimer -= Time.deltaTime;
                Debug.Log(slowTimer);
            }
            else if (slow && slowTimer <= 0)
            {
                slow = false;
                slowTimer = 3.0f;
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            r.velocity = new Vector3(x * speed, 0, y * speed);
        }
        else
        {
            r.velocity = Vector3.zero;
        }
    }
}
