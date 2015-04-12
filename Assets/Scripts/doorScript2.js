#pragma strict

/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;
*/
//public class DoorScript : MonoBehaviour {


    //public bool isLocked = false;
    //private char[] pattern = {'W','S','A','D'};
	var pattern : char[] =	["A"[0], "B"[0], "X"[0], "Y"[0]];
	//pattern = {'A','B','X','Y'};
	var key : String ;
    key = "ABXXYA";
	/*public GameObject A;
	public GameObject B;
	public GameObject X;
	public GameObject Y;*/
	var A : GameObject;
	var B : GameObject;
	var X : GameObject;
	var Y : GameObject;
	private var Ar : Renderer;
	private var Br : Renderer;
	private var Xr : Renderer;
	private var Yr : Renderer;
	
	var count : int;
	var i : int;
	count =0;
	i =0 ;
	
	function Start () {
	    /*while(i<6) {
			key += pattern [Random.Range (0, 4)];
			i++;
		}*/
		Ar = A.GetComponent(Renderer);
		Br = B.GetComponent(Renderer);
		Xr = X.GetComponent(Renderer);
		Yr = Y.GetComponent(Renderer);
		//tempr = temp.GetComponent<Renderer> ();
		
		
		Ar.enabled = false;
		Br.enabled = false;
		Xr.enabled = false;
		Yr.enabled = false;
		//tempr.enabled = false;
	}
	
	// Update is called once per frame
	function Update()
	{
	}

	function OnCollisionEnter2D(col : Collision2D)
    {
        if (col.gameObject.tag == "Player")
        {
			Debug.Log(key);
			//t = Time.time;
			//displayValue = 1;
			for(i=0; i < key.Length;i++)
			{
				if(key[i] == 'A')
				{
					//Debug.Log("in1");
					//Ar.transform.position = transform.position + new Vector3();
					Ar.enabled = true;
					WaitForSeconds(5);
					Ar.enabled = false;
				}
				else if(key[i] == 'B')
				{
					Br.enabled = true;
					WaitForSeconds(5);
					Br.enabled = false;
				}
				else if(key[i] == 'X')
				{
					Xr.enabled = true;
					WaitForSeconds(5);
					Xr.enabled = false;
				}
				else if(key[i] == 'Y')
				{
					Yr.enabled = true;
					WaitForSeconds(5);
					Yr.enabled = false;
				}
				
/*
				Ar.enabled = false;
				Br.enabled = false;
				Xr.enabled = false;
				Yr.enabled = false;
*/
				//	DoorText(key);
			}
        }

    }


//}
