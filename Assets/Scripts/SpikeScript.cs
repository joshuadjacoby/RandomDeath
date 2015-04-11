﻿using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerScript.health--;
        }

    }
}