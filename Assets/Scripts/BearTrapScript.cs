using UnityEngine;
using System.Collections;

public class BearTrapScript : MonoBehaviour
{

    float timer = 5.0f;
    bool timerOn;
    bool isEnabled;
    // Use this for initialization
    void Start()
    {
        timerOn = false;
        isEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKey("space") || Input.GetButtonDown("Fire1") || Input.GetButtonUp("Fire1"))
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

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.BroadcastMessage("toggleSlow");
            timerOn = false;
            timer = 5.0f;
            isEnabled = true;
        }
    }
}
