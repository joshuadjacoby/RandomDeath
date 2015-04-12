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
        /*if (timerOn && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            isEnabled = false;
        }*/
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" /*&& isEnabled*/)
        {
			col.gameObject.BroadcastMessage("toggleSlow");

            //col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, transform.position, 1);
            //timerOn = true;
        }
    }

/*    void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.BroadcastMessage("toggleSlow");
            timerOn = false;
            isEnabled = true;
			timer = 5.0f;
        }
    }*/
}
