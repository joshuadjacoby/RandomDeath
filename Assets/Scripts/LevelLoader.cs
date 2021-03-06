﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

    private int currentLevel = 0;
    public int[,] tiles;
    private int numberOfLevels;

    public Texture2D[] textures;
    public Texture2D atlas;
    public Rect[] rects;

    private PlayerScript player;

    private Mesh mesh;

    private List<int> tris = new List<int>();
    private List<Vector3> verts = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();
    private int triNum = 0;
    public string[] levelNames;

    private bool showText = false;
    private float showTextTimer = 0f;

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
    private Object lockedDoor;
    private Object exit;
    private Object zombie;



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
        exit = Resources.Load("prefabs/exit");
        zombie = Resources.Load("prefabs/zombie");
        lockedDoor = Resources.Load("prefabs/LockedDoor");
        LoadLevel();


    }

    public void LoadNextLevel() {
        //++currentLevel;
        currentLevel = Random.Range(1, 24);

        LoadLevel();
    }

    public void LoadLevel() {

        PlayerScript.deathRenderer.enabled = false;
        showText = true;
        showTextTimer = 3f;

        if (mesh != null) {
            Destroy(mesh);
        }

        GameObject findThings = GameObject.Find("things");
        if (findThings != null) {
            Destroy(findThings);
        }


        Texture2D tex = (Texture2D)Resources.Load("Levels/level" + currentLevel);
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
            tiles[i % width, i / width] = tiles1D[i];
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
                        go = (GameObject)Instantiate(lockedDoor, new Vector3(x + .5f, .5f, y + .5f), Quaternion.identity);
                        go.name = "Locked Door";
                        go.transform.parent = things.transform;
                        break;

                    case SPAWN_ENEMY:
                        go = (GameObject)Instantiate(zombie, new Vector3(x + .5f, 0, y + .5f), Quaternion.identity);
                        go.name = "Zambie";
                        go.transform.parent = things.transform;
                        break;

                    case ENTRANCE:
                        player.start = new Vector3(x + .5f, .5f, y + .5f);
                        player.ResetPlayer();

                        break;

                    case EXIT:
                        go = (GameObject)Instantiate(exit, new Vector3(x + .5f, .5f, y + .5f), Quaternion.identity);
                        go.name = "Exit";
                        go.transform.parent = things.transform;

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
            index = Random.value < .75 ? Random.Range(9, 15) : WALL;
        }

        if (index == SPIKES || index == LOCKED_DOOR) {
            index = GROUND;
        }

        Rect r = rects[index];

        uvs.Add(new Vector2(r.xMin, r.yMin));
        uvs.Add(new Vector2(r.xMin, r.yMax));
        uvs.Add(new Vector2(r.xMax, r.yMax));
        uvs.Add(new Vector2(r.xMax, r.yMax));
        uvs.Add(new Vector2(r.xMax, r.yMin));
        uvs.Add(new Vector2(r.xMin, r.yMin));

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
            case EXIT:
                return 1;
            default:
                return 0;
        }
    }

    void Update() {
        showTextTimer -= Time.deltaTime;
        if (showTextTimer < 0) {
            showText = false;
        }
    }

    void OnGUI() {
        if (showText) {
            GUI.skin.label.fontSize = 50;
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Normal;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), levelNames[currentLevel]);
            //"Level\n" +
        }
    }


}
