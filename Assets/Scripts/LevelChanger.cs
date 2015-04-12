using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {

    private LevelLoader l;

	private string level;

	void Start()
	{
        l = GameObject.Find("Level").GetComponent<LevelLoader>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
            l.LoadNextLevel();
            Destroy(this);
		}
	}

	
}


