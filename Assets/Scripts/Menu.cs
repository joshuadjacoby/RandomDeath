using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown ("Fire3") || Input.GetMouseButtonDown(0)) {

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