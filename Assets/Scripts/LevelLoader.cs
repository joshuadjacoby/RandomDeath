using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour {

    public int currentLevel = 0;
    public int[][] tiles;
    public int numberOfLevels;


	// Use this for initialization
	void Start () {
	    // load all levels

        LoadLevel();

        
	}

    private void LoadLevel() {
        
        Texture2D tex = (Texture2D)Resources.Load("level1");
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


        Sprite ground = Resources.Load<Sprite>("ground");
        Sprite wall = Resources.Load<Sprite>("wall");
        Sprite spikes = Resources.Load<Sprite>("spikes");

        float tileSize = 32f / ground.pixelsPerUnit;

        GameObject tilesGameObject = new GameObject("tiles");
        
        for (int i = 0; i < tiles.Length; i++) {
            GameObject go = new GameObject();
            go.transform.parent = tilesGameObject.transform;
            
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();


            
            go.transform.position = new Vector2((i % width) * tileSize, (i / width) * tileSize);
            switch (tiles[i]) {
                case 0:
                    sr.sprite = ground;
                    break;
                case 1:
                    sr.sprite = wall;
                    break;
                case 2:
                    sr.sprite = spikes;
                    break;
                default:
                    break;
            }
        }

        // make tiles twice as big
        tilesGameObject.transform.localScale = new Vector3(2f, 2f, 2f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
