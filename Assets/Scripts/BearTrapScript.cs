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

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
			col.gameObject.BroadcastMessage("toggleSlow");
        }
    }
}
