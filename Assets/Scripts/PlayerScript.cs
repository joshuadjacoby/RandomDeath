using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{
    public int health;
    private Rigidbody r;
    public bool canMove;
    public bool slow;
    public float slowTimer;
    public Vector3 start;
    public float mouseSensitivity = 300f;
    public float controllerSensitivity = 300f;
    private float verticleLook;
    private Transform cameraTransform;

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
        cameraTransform = transform.Find("MainCamera").transform;
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

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float controllerX = Input.GetAxis("Right Horizontal") * controllerSensitivity * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float controllerY = Input.GetAxis("Right Vertical") * controllerSensitivity * Time.deltaTime;

        float rot = mouseX == 0 ? controllerX : mouseX;
        verticleLook -= mouseY == 0 ? controllerY : mouseY;
        verticleLook = Mathf.Clamp(verticleLook, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(verticleLook, 0, 0);
        transform.Rotate(0, rot, 0);
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
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Left Analog"))
            {
                speed *= 2f;
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
