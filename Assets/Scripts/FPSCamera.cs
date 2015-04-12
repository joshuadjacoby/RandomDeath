using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {

    public float mouseSensitivity = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float rot = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(0, rot, 0);

	}
}
