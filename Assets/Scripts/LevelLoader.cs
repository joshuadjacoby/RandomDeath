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

    private PlayerScript player;

    private Mesh mesh;

    private List<int> tris = new List<int>();
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private int triNum = 0;

    // ids that correspond to the .png level reader
    private const int GROUND = 0;
    private const int WALL = 1;
    private const int SPIKES = 2;
    private const int CRATE = 3;
    private const int BEAR_TRAP = 4;
    private const int LOCKED_DOOR = 5;
    private const int SPAWN_ENEMY = 6;
    private const int ENTRANCE = 7;
    private const int EXIT = 8;

    private Object spikes;
    private Object bearTrap;
   
    


    // Use this for initialization
    void Start() {
        // load all levels
        player = GameObject.Find("Player").GetComponent<PlayerScript>();

        atlas = new Texture2D(1024, 1024);
        rects = atlas.PackTextures(textures, 2, 1024);
        atlas.filterMode = FilterMode.Point;
        atlas.wrapMode = TextureWrapMode.Clamp;

        Material mat = (Material)Resources.Load("materials/level");
        mat.SetTexture(0, atlas);

        GetComponent<MeshRenderer>().material = mat;


        spikes = Resources.Load("prefabs/spikes");
        bearTrap = Resources.Load("prefabs/bear trap");
        
		string l = "Levels/level1"; 

        LoadLevel(l);


    }

    public void LoadLevel(string level) {

		Texture2D tex = (Texture2D)Resources.Load(level);
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

        GameObject things = new GameObject("things");

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

                    case GROUND:
                        break;
                    case WALL:
                        break;
                    case SPIKES:
                        GameObject go = (GameObject)Instantiate(spikes, new Vector3(x + .5f, .5f, y + .5f), Quaternion.identity);
                        go.name = "Spikes";
                        go.transform.parent = things.transform;
                        break;
                    case CRATE:
                        break;

                    case BEAR_TRAP:
                        go = (GameObject)Instantiate(bearTrap, new Vector3(x + .5f, .5f, y + .5f), Quaternion.identity);
                        go.name = "Bear Trap";
                        go.transform.parent = things.transform;
                        break;

                    case LOCKED_DOOR:

                        break;

                    case SPAWN_ENEMY:

                        break;

                    case ENTRANCE:

                        break;

                    case EXIT:
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
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;

    }

    public void addUvsTris(int index) {
        if (index == WALL) {
            index = Random.value < .2 ? 9 : WALL;
        }

        Rect r = rects[index];

        uvs.Add(new Vector2(r.xMin , r.yMax ));
        uvs.Add(new Vector2(r.xMin , r.yMin ));
        uvs.Add(new Vector2(r.xMax , r.yMin ));
        uvs.Add(new Vector2(r.xMax , r.yMin ));
        uvs.Add(new Vector2(r.xMax , r.yMax ));
        uvs.Add(new Vector2(r.xMin , r.yMax ));

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
            player.ResetPlayer();
        }
    }

}
