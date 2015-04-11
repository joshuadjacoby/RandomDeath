using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {


	private int damage;
	// Use this for initialization
	void Start () {
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player")
			col.gameObject.BroadcastMessage ("ApplyDamage", damage);

    }
}
