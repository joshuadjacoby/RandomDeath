using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {


    public bool isLocked = false;
    private char[] pattern = {'W','S','A','D'};
    private string key = "";

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 6; ++i)
            key += pattern[Random.Range(0, 4)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //if (Input.GetKey("e"))
                Debug.Log(key);
        }

    }
}
