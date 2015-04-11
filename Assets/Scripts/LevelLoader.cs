using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

    public int currentLevel = 0;
    public int[][] tiles;
    public int numberOfLevels;
    private GameObject player;
    private Vector2 playerStart;

	// Use this for initialization
	void Start () {
	    // load all levels
        player = GameObject.Find("Player");
        playerStart = player.transform.position;

        LoadLevel();

        
	}

    private void LoadLevel() {
        
        Texture2D tex = (Texture2D)Resources.Load("levels/level1");
        Color32[] colors = tex.GetPixels32();

        Dictionary<Color32, int> table = new Dictionary<Color32, int>();
        int n = 0;
        int width = tex.width;
        for (int i = colors.Length - tex.width; true; i++) {
            Color32 color = colors[i];
            if (!table.ContainsKey(color)) {
                table.Add(color, n++);
            } else {
                break;
            }
        }
        
        int[] tiles = new int[colors.Length - tex.width];
        for (int i = 0; i < colors.Length - tex.width; i++) {
            tiles[i] = table[colors[i]];
        }


        Sprite ground = Resources.Load<Sprite>("sprites/ground");
        Sprite wall = Resources.Load<Sprite>("sprites/wall");
        Sprite spikes = Resources.Load<Sprite>("sprites/spikes");

        float tileSize = 32f / 100f;

        GameObject tilesGameObject = new GameObject("tiles");
        
        for (int i = 0; i < tiles.Length; i++) {
            GameObject go = new GameObject();

            go.transform.parent = tilesGameObject.transform;
            
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();


            go.transform.position = new Vector2((i % width) * tileSize, (i / width) * tileSize);
            switch (tiles[i]) {
                case 0:
                    sr.sprite = ground;
                    go.name = "ground";
                    break;
                case 1:
                    sr.sprite = wall;
                    go.name = "wall";
                    go.AddComponent<BoxCollider2D>();
                    go.AddComponent<Rigidbody2D>().isKinematic = true;
                    break;
                case 2:
                    sr.sprite = spikes;
                    go.name = "spikes";
                    BoxCollider2D bc2 = go.AddComponent<BoxCollider2D>();
                    bc2.isTrigger = true;
                    bc2.size = bc2.size * .5f;
                    go.AddComponent<Rigidbody2D>().isKinematic = true;
                    go.AddComponent<SpikeScript>();
                    break;
                default:
                    break;
            }
        }

        // make tiles twice as big
        tilesGameObject.transform.localScale = new Vector3(1/tileSize, 1/tileSize, 1/tileSize);
    }

    public void sendToDungeon()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.R)) {
            player.transform.position = playerStart;
            //PlayerScript.health = 1;
            player.SetActive(true);

        }
	}
}
