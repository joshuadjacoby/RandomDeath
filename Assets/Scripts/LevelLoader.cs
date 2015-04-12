using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

    public int currentLevel = 0;
    public int[,] tiles;
    public int numberOfLevels;

    public Texture2D[] textures;
    public Texture2D atlas;
    public Rect[] rects;

    private GameObject player;
    private Vector2 playerStart;

    private Mesh mesh;

    private List<int> tris = new List<int>();
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private int triNum = 0;

    // ids that correspond to the .png level reader
    private const int GROUND = 0;
    private const int WALL = 1;
    private const int CRATE = 2;
    private const int SPIKES = 3;

    // Use this for initialization
    void Start() {
        // load all levels
        player = GameObject.Find("Player");
        playerStart = player.transform.position;

        atlas = new Texture2D(1024, 1024);
        atlas.filterMode = FilterMode.Point;
        rects = atlas.PackTextures(textures, 0, 1024);

        Material mat = (Material)Resources.Load("sprites/level");
        mat.SetTexture(0, atlas);

        GetComponent<MeshRenderer>().material = mat;

        LoadLevel();


    }

    private void LoadLevel() {

        Texture2D tex = (Texture2D)Resources.Load("levels/level1");
        Color32[] colors = tex.GetPixels32();

        Dictionary<Color32, int> table = new Dictionary<Color32, int>();
        int n = 0;
        int width = tex.width;
        int height = tex.height - 1;
        for (int i = colors.Length - tex.width; true; i++) {
            Color32 color = colors[i];
            if (!table.ContainsKey(color)) {
                table.Add(color, n++);
            } else {
                break;
            }
        }

        int[] tiles1D = new int[colors.Length - tex.width];
        for (int i = 0; i < colors.Length - tex.width; i++) {
            tiles1D[i] = table[colors[i]];
        }

        tiles = new int[width, height];

        // turn into 2D array
        for (int i = 0; i < tiles1D.Length; i++) {
            tiles[i % width, i / height] = tiles1D[i];
        }

        tris.Clear();
        verts.Clear();
        uvs.Clear();
        triNum = 0;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {

                int h = getHeight(x, y); //gets the height of this tile

                // add 2 triangles for this tile
                verts.Add(new Vector3(x, h, y));
                verts.Add(new Vector3(x, h, y + 1));
                verts.Add(new Vector3(x + 1, h, y + 1));
                verts.Add(new Vector3(x + 1, h, y + 1));
                verts.Add(new Vector3(x + 1, h, y));
                verts.Add(new Vector3(x, h, y));

                addUvsTris(tiles[x, y]);

                // if this tile is high and neighbors are low then add walls down your sides
                if (h == 1) {
                    if (getHeight(x + 1, y) == 0) { // right neighbor
                        verts.Add(new Vector3(x + 1, 0, y));
                        verts.Add(new Vector3(x + 1, h, y));
                        verts.Add(new Vector3(x + 1, h, y + 1));
                        verts.Add(new Vector3(x + 1, h, y + 1));
                        verts.Add(new Vector3(x + 1, 0, y + 1));
                        verts.Add(new Vector3(x + 1, 0, y));

                        addUvsTris(tiles[x, y]);
                    }

                    if (getHeight(x - 1, y) == 0) { // left neighbor
                        verts.Add(new Vector3(x, 0, y + 1));
                        verts.Add(new Vector3(x, h, y + 1));
                        verts.Add(new Vector3(x, h, y));
                        verts.Add(new Vector3(x, h, y));
                        verts.Add(new Vector3(x, 0, y));
                        verts.Add(new Vector3(x, 0, y + 1));

                        addUvsTris(tiles[x, y]);
                    }

                    if (getHeight(x, y + 1) == 0) { // top neighbor
                        verts.Add(new Vector3(x + 1, 0, y + 1));
                        verts.Add(new Vector3(x + 1, h, y + 1));
                        verts.Add(new Vector3(x, h, y + 1));
                        verts.Add(new Vector3(x, h, y + 1));
                        verts.Add(new Vector3(x, 0, y + 1));
                        verts.Add(new Vector3(x + 1, 0, y + 1));

                        addUvsTris(tiles[x, y]);
                    }

                    if (getHeight(x, y - 1) == 0) { // bottom neighbor
                        verts.Add(new Vector3(x, 0, y));
                        verts.Add(new Vector3(x, h, y));
                        verts.Add(new Vector3(x + 1, h, y));
                        verts.Add(new Vector3(x + 1, h, y));
                        verts.Add(new Vector3(x + 1, 0, y));
                        verts.Add(new Vector3(x, 0, y));

                        addUvsTris(tiles[x, y]);
                    }
                }

                // this is where you can spawn certain prefabs based on what tile you are
                switch (tiles[x, y]) {

                    case 0:

                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;

                }

            }
        }

        // build mesh from lists
        mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

    }

    public void addUvsTris(int index) {
        if (index == 1) {
            index = Random.value < .2 ? 4 : 1;
        }
        
        Rect r = rects[index];

        uvs.Add(new Vector2(r.xMin, r.yMax));
        uvs.Add(new Vector2(r.xMin, r.yMin));
        uvs.Add(new Vector2(r.xMax, r.yMin));
        uvs.Add(new Vector2(r.xMax, r.yMin));
        uvs.Add(new Vector2(r.xMax, r.yMax));
        uvs.Add(new Vector2(r.xMin, r.yMax));

        tris.Add(triNum++);
        tris.Add(triNum++);
        tris.Add(triNum++);
        tris.Add(triNum++);
        tris.Add(triNum++);
        tris.Add(triNum++);
    }


    private int getHeight(int x, int y) {
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1)) {
            return 1;
        }
        switch (tiles[x, y]) {
            case WALL:
            case CRATE:
                return 1;
            default:
                return 0;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            player.transform.position = playerStart;
            PlayerScript.health = 1;
            player.SetActive(true);

        }
    }
}
