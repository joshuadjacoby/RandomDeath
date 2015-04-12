using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
    private int health;
    private Rigidbody r;
    private bool canMove;
    private bool slow;
    private float slowTimer;
    public Vector3 start;
    private float mouseSensitivity;
    private bool canSprint;
    private bool isSprinting;
    private float sprintTimer;
    private float sprintCooldown;

    // Use this for initialization
    void Start()
    {
        health = 1;
        r = GetComponent<Rigidbody>();
        canMove = true;
        slow = false;
        slowTimer = 3.0f;
        start = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensitivity = 200f;
        canSprint = true;
        isSprinting = false;
        sprintTimer = 2.0f;
        sprintCooldown = 4.0f;
    }

    void ApplyDamage(int i)
    {
        health -= i;
    }

    /*void toggleTrap()
    {
        canMove = !canMove;
    }*/

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

        transform.Rotate(0, Input.GetAxis("Mouse X") == 0 ? Input.GetAxis("Right Horizontal")*5 : Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0);
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
                //Debug.Log(slowTimer);
            }
            else if (slow && slowTimer <= 0)
            {
                slow = false;
                slowTimer = 3.0f;
            }
            if (canSprint && Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Left Analog"))
            {
                isSprinting = true;
            }
            else if (!canSprint)
            {
                sprintCooldown -= Time.deltaTime;
            }
            if (isSprinting)
            {
                speed *= 2f;
                sprintTimer -= Time.deltaTime;
            }
            if (sprintTimer <= 0)
            {
                isSprinting = false;
                sprintTimer = 2.0f;
                canSprint = false;
            }
            if (sprintCooldown <= 0)
            {
                canSprint = true;
                sprintCooldown = 4.0f;
            }
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            r.velocity = transform.TransformDirection(new Vector3(x * speed, 0, y * speed));
            
        }
        else
        {
            r.velocity = Vector3.zero;
        }
    }
}
