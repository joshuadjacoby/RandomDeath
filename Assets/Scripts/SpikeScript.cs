using UnityEngine;
using System.Collections;

public class SpikeScript : MonoBehaviour {
    private int damage;

    private Transform player;
    private Transform mesh;

    private Vector3 localBottom;
    private Vector3 localLowerPoke;
    private Vector3 localFullPoke;
    private bool fullPoke = false;
    private float fullPokeTimer = 0f;

    // Use this for initialization
    void Start() {
        damage = 1;

        player = GameObject.Find("Player").transform;

        int attribs = 24;

        Mesh m = new Mesh();
        Vector3[] verts = new Vector3[attribs];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(1, 0, 1);
        verts[2] = new Vector3(1, 1, 1);
        verts[3] = new Vector3(1, 1, 1);
        verts[4] = new Vector3(0, 1, 0);
        verts[5] = new Vector3(0, 0, 0);

        verts[6] = new Vector3(0, 0, 0);
        verts[7] = new Vector3(0, 1, 0);
        verts[8] = new Vector3(1, 1, 1);
        verts[9] = new Vector3(1, 1, 1);
        verts[10] = new Vector3(1, 0, 1);
        verts[11] = new Vector3(0, 0, 0);

        verts[12] = new Vector3(1, 0, 0);
        verts[13] = new Vector3(0, 0, 1);
        verts[14] = new Vector3(0, 1, 1);
        verts[15] = new Vector3(0, 1, 1);
        verts[16] = new Vector3(1, 1, 0);
        verts[17] = new Vector3(1, 0, 0);

        verts[18] = new Vector3(1, 0, 0);
        verts[19] = new Vector3(1, 1, 0);
        verts[20] = new Vector3(0, 1, 1);
        verts[21] = new Vector3(0, 1, 1);
        verts[22] = new Vector3(0, 0, 1);
        verts[23] = new Vector3(1, 0, 0);

        Vector2[] uvs = new Vector2[attribs];
        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(1, 1);
        uvs[3] = new Vector2(1, 1);
        uvs[4] = new Vector2(0, 1);
        uvs[5] = new Vector2(0, 0);

        uvs[6] = new Vector2(0, 0);
        uvs[7] = new Vector2(0, 1);
        uvs[8] = new Vector2(1, 1);
        uvs[9] = new Vector2(1, 1);
        uvs[10] = new Vector2(1, 0);
        uvs[11] = new Vector2(0, 0);

        uvs[12] = new Vector2(0, 0);
        uvs[13] = new Vector2(1, 0);
        uvs[14] = new Vector2(1, 1);
        uvs[15] = new Vector2(1, 1);
        uvs[16] = new Vector2(0, 1);
        uvs[17] = new Vector2(0, 0);

        uvs[18] = new Vector2(0, 0);
        uvs[19] = new Vector2(0, 1);
        uvs[20] = new Vector2(1, 1);
        uvs[21] = new Vector2(1, 1);
        uvs[22] = new Vector2(1, 0);
        uvs[23] = new Vector2(0, 0);

        int[] tris = new int[attribs];
        for (int i = 0; i < attribs; i++) {
            tris[i] = i;
        }

        m.vertices = verts;
        m.uv = uvs;
        m.triangles = tris;

        m.RecalculateBounds();
        m.RecalculateNormals();

        mesh = transform.Find("Mesh");
        mesh.GetComponent<MeshFilter>().mesh = m;

        localBottom = new Vector3(mesh.localPosition.x, -1.2f, mesh.localPosition.z);
        localLowerPoke = new Vector3(mesh.localPosition.x, -1.05f, mesh.localPosition.z);
        localFullPoke = new Vector3(mesh.localPosition.x, -.5f, mesh.localPosition.z);

    }

    void Update() {
        //float sqrMag = (transform.position - player.position).sqrMagnitude;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);
        bool pokeout = false;
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].tag == "Player" || colliders[i].tag == "Zambie") {
                pokeout = true;
            }
        }

        if (fullPoke) {
            fullPokeTimer -= Time.deltaTime;
            if (fullPokeTimer < 0f) {
                fullPoke = false;
            }

            mesh.localPosition = Vector3.Lerp(mesh.localPosition, localFullPoke, .5f);

        } else {
            if (pokeout) {
                mesh.localPosition = Vector3.Lerp(mesh.localPosition, localLowerPoke, .1f);
            } else {
                mesh.localPosition = Vector3.Lerp(mesh.localPosition, localBottom, .1f);
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Zambie") {
            col.gameObject.BroadcastMessage("ApplyDamage", damage);
            fullPokeTimer = 2f;
            fullPoke = true;
        }
    }
}
