using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour {

    public GameObject gameTilePrefab;
    public GameObject black;
    
    private List<GameTile> gameTiles = new List<GameTile>();
    private int lastNumber;
 

	// Use this for initialization
	public void Show (string[] radicals) {
        if (!black.activeSelf)
        {
            black.SetActive(true);
            for (int i = 0; i < radicals.Length; i++)
            {
                GameObject obj = Instantiate(gameTilePrefab, transform);

                obj.GetComponent<GameTile>().number = i;
                obj.GetComponent<GameTile>().kanji.text = radicals[i];
                obj.GetComponent<GameTile>().power.text = "";
                obj.GetComponent<GameTile>().tileManager = this;
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-440 + 200 * (i % 5), 960 / 2 + 100);
                gameTiles.Add(obj.GetComponent<GameTile>());
            }
        }
    }

    public void DeleteAllTiles()
    {
        black.SetActive(false);
        foreach (GameTile tile in gameTiles)
        {
            Destroy(tile.gameObject);
        }
        gameTiles.Clear();
    }

   
    public void RecieveClick(int number)
    {
        MainFightingScript.MFS.RadicalPush(number);
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
