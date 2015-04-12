using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {


    public bool isLocked = true;
    //private char[] pattern = {'W','S','A','D'};
	private char[] pattern = {'A','B','X','Y'};
    private string key = "";
	public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject Y;
	public GameObject temp;
	private Renderer Ar;
	private Renderer Br;
	private Renderer Xr;
	private Renderer Yr;
	private Renderer tempr;
	private GameObject g;


	private int count=0;
	private int i=0;

	void Start () {
		for (int i = 0; i < 6; ++i) {
			key += pattern [Random.Range (0, 4)];
		}
		tempr = temp.GetComponent<Renderer> ();

		g = GetComponent<GameObject> ();
		Ar = A.GetComponent<Renderer> ();
		Br = B.GetComponent<Renderer> ();
		Xr = X.GetComponent<Renderer> ();
		Yr = Y.GetComponent<Renderer> ();
		tempr = temp.GetComponent<Renderer> ();
		Ar.enabled = false;
		Br.enabled = false;
		Xr.enabled = false;
		Yr.enabled = false;
		tempr.enabled = false;

	}
	
	// Update is called once per frame
	void Update()
	{
		if (count == 1 && i < 6) {

			if (key [i] == 'A') {
				//Debug.Log("in1");
				//Ar.transform.position = new Vector3(1,1,1);
				Ar.enabled = true;
				if (Input.GetKeyDown (KeyCode.A)) {
					i++;
					Ar.enabled = false;
				}

			} else if (key [i] == 'B') {
				Br.enabled = true;
				if (Input.GetKeyDown (KeyCode.B)) {
					i++;
					Br.enabled = false;
				}

			} else if (key [i] == 'X') {
				Xr.enabled = true;
				if (Input.GetKeyDown (KeyCode.X)) {
					i++;
					Xr.enabled = false;
				}
			} else if (key [i] == 'Y') {
				Yr.enabled = true;
				if (Input.GetKeyDown (KeyCode.Y)) {
					i++;
					Yr.enabled = false;
				}
			}	
		}

		if (i == 6) {

			gameObject.SetActive(false);
			//g.SetActive(false);
			tempr.enabled = true;
			isLocked = false;
		}

	}

	IEnumerator DelayFunction()
	{
		yield return new WaitForSeconds (5);
	}


	void OnCollisionEnter2D(Collision2D col)
    {
		if (col.gameObject.tag == "Player") {
			count = 1;
		
		}
	}

}


