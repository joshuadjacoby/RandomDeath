using UnityEngine;
using System.Collections;

public class PitfallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// RS: upon entering trigger, send player to another room
	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player")
			Debug.Log ("im falling!");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
