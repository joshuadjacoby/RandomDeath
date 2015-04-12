using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	private GameObject player; 
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null) {
			transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
		}
	}
}
