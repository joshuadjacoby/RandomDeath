using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    private Transform main;

	// Use this for initialization
	void Start () {
        main = GameObject.Find("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(main);
        Vector3 eulers = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, eulers.y, 0);
	}
}
