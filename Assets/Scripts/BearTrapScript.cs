using UnityEngine;
using System.Collections;

public class BearTrapScript : MonoBehaviour {
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.BroadcastMessage("toggleSlow");
        }
    }
}
