using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown ("Fire3")) {

			Application.LoadLevel("Main");
		}
	}

    void OnMouseDown() {
     //if (this.name == "PlayBT")
     //{
   		Application.LoadLevel("Main");
     //}
    }
}