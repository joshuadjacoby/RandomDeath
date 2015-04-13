using UnityEngine;
using System.Collections;

public class TrapdoorScript : MonoBehaviour
{
	public static bool inTrap = false;
	public static bool doorUnlocked = false;
	private GameObject g;

    // Use this for initialization
    void Start() 
    {
		g = GetComponent<GameObject> ();
    }

    // Update is called once per frame
    void Update()
    {

		/*if(doorUnlocked == true)
		{
			doorUnlock();

		}*/
   }

    /*void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") 
        {
			//col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, transform.position, 1);
			//col.gameObject.BroadcastMessage("toggleTrap");
			inTrap = true;

        }
    }

	public void doorUnlock()
	{
		g.SetActive (false);
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Player") 
		{
			inTrap = false;
			col.gameObject.BroadcastMessage("Reset");
			
		}
	}*/
}
