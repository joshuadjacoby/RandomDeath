using UnityEngine;
using System.Collections;

public class CrateScript : MonoBehaviour {

	private float randomValue;
	private int count = 0;
	private Renderer rend;
	public GameObject player;
	//private PlayerScript p;
	//private bool enter = true;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		count++;
		//int timer = (int)Mathf.Floor (Time.time);
		if (count % 100 == 0) {
			randomValue = Random.value;
			Debug.Log (randomValue);

			if(randomValue > 0.5)
			{
				/*player.gameObject.BroadcastMessage("ZeroHealth");
				transform.position = player.transform.position;
				rend.enabled = true;
				*/
			}

		}

	}
}
