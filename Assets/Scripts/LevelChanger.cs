using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

	private LevelLoader l =  new LevelLoader();

	private string level;

	void Start()
	{
		//l = GameObject.FindGameObjectWithTag ("Level");
	}

	void Update()
	{
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			Debug.Log ("HEY");
			//level = "levels/level2";
			l.LoadLevel("levels/level2");

		}
	}

	
}


