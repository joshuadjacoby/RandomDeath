﻿using UnityEngine;
using System.Collections;

public class BearTrapScript : MonoBehaviour {

    float timer = 5.0f;
    bool timerOn;
    bool isEnabled;
	// Use this for initialization
	void Start () {
        timerOn = false;
        isEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if (timerOn && timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }
        if (timer <= 0)
        {
            isEnabled = false;
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && isEnabled)
        {
            col.gameObject.BroadcastMessage("toggleTrap");
            col.gameObject.transform.position = Vector2.MoveTowards(col.gameObject.transform.position, transform.position, 1);
            timerOn = true;
            if (Input.GetKey("space") || Input.GetButton("Fire1"))
            {
                timer -= 0.05f;
                Debug.Log("YAY");
            }
        }
        else
        {
            col.gameObject.BroadcastMessage("toggleTrap");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
<<<<<<< HEAD

=======
>>>>>>> 380917bc0edfc8a396ed5ef3c9499b2fb69e9ce0
        if (col.gameObject.tag == "Player")
        {
            timerOn = false;
            timer = 5.0f;
            isEnabled = true;
        }
<<<<<<< HEAD
		if (col.gameObject.tag == "Player") {
		}
            //player.toggleTrap();
=======
<<<<<<< HEAD
=======
		if (col.gameObject.tag == "Player") {
		}
            //player.toggleTrap();

>>>>>>> 74fdf4fb19949a3a83c25b88c6287e6472c8c23b
>>>>>>> 380917bc0edfc8a396ed5ef3c9499b2fb69e9ce0
    }
}