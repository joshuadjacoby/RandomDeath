using UnityEngine;
using System.Collections;

public class TrapdoorScript : MonoBehaviour {
    public static bool inTrap = false;
    public static bool doorUnlocked = false;
    private Transform mesh;
    private bool lowering = false;

    // Use this for initialization
    void Start() {
        mesh = transform.Find("Mesh");
    }

    // Update is called once per frame
    void Update() {
        if (lowering) {
            mesh.localPosition = new Vector3(mesh.localPosition.x, mesh.localPosition.y - Time.deltaTime, mesh.localPosition.z);
            if (mesh.localPosition.y < -1) {
                Destroy(gameObject);
            }
        }
        
    }

    public void lowerDoor() {

        lowering = true;
    }

}
