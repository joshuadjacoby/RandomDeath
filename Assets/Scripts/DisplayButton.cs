using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayButton : MonoBehaviour {

	private char[] pattern = {'A','B','X','Y'};
    private string key;
	public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject Y;
	private Renderer Ar;
	private Renderer Br;
	private Renderer Xr;
	private Renderer Yr;
	private GameObject g;
	private GameObject currentDoor;
	private Collider c;
	private int j;
	private bool isCompleted = false;
	private bool touchingDoor = false;

	void Start () {
		Reset ();
	}

	void Reset()
	{
		key = "";
		for (int i = 0; i < 6; ++i) {
			key += pattern [Random.Range (0, 4)];
		}
		//tempr = temp.GetComponent<Renderer> ();

		j = 0;
		
		c = GetComponent<Collider> ();
		//X = g.transform.GetChild (2);
		g = GetComponent<GameObject> ();
		Ar = A.GetComponent<Renderer> ();
		Br = B.GetComponent<Renderer> ();
		Xr = X.GetComponent<Renderer> ();
		Yr = Y.GetComponent<Renderer> ();
		//tempr = temp.GetComponent<Renderer> ();
		Ar.enabled = false;
		Br.enabled = false;
		Xr.enabled = false;
		Yr.enabled = false;
		touchingDoor = false;
		//tempr.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (j < 6 && touchingDoor && isCompleted == false) {

			if (key [j] == 'A') {
				Ar.enabled = true;

				if (Input.GetKeyDown (KeyCode.A) || Input.GetButtonDown("Fire1")) {
					j++;
					Ar.enabled = false;
			
				}

			} else if (key [j] == 'B') {

				Br.enabled = true;
				if (Input.GetKeyDown (KeyCode.B) || Input.GetButtonDown("Fire2")) {
					j++;
					Br.enabled = false;
				}

			} else if (key [j] == 'X') {

				Xr.enabled = true;
				if (Input.GetKeyDown (KeyCode.X) || Input.GetButtonDown("Fire3")) {
					j++;
					Xr.enabled = false;
				}
			} else if (key [j] == 'Y') {

				Yr.enabled = true;
				if (Input.GetKeyDown (KeyCode.Y) || Input.GetButtonDown("Jump")) {
					j++;
				
					Yr.enabled = false;
				}
			}	
		}

		if (j == 6) {

			// DOOR UNLOCKED
			j=0;
			isCompleted = true;
			currentDoor.SetActive(false);
			Reset ();
			//TrapdoorScript.inTrap = false;
			//c.gameObject.BroadcastMessage("doorUnlock");
			//TrapdoorScript.doorUnlocked = true;
		}

	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "TrapDoor") 
		{
			currentDoor = col.gameObject;
			touchingDoor = true;
			
		}

	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "TrapDoor") 
		{
			currentDoor = null;
			Reset ();
		}
	}



}