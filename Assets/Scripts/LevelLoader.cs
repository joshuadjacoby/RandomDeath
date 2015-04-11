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

        for (int i = 0; i < tiles.Length; i++) {
            Debug.Log(tiles[i]);

        }
        
        
        


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
