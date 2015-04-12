using UnityEngine;
using System.Collections;

public class TrapdoorScript : MonoBehaviour
{
	public static bool inTrap = false;

    // Use this for initialization
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {
		/*if (inTrap == true) {

			transform.position += new Vector3(0,0,0);

		}*/
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") 
        {
			col.gameObject.transform.position = Vector3.MoveTowards(col.gameObject.transform.position, transform.position, 1);
			col.gameObject.BroadcastMessage("toggleTrap");
			inTrap = true;

        }
    }

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Player") 
		{
			//col.gameObject.BroadcastMessage("toggleTrap");
			inTrap = false;
			
		}
	}
}
