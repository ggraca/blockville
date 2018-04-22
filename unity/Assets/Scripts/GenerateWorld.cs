using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class Tile {
	public int id;
	public int x;
	public int y;
	public int building;
	public string owner;
}

[System.Serializable]
public class Wrapper {
   public List<Tile> tiles;
   public int myMoney;
}

public class GenerateWorld : MonoBehaviour {

	public GameObject defaultPrefab;
	public GameObject greenBorderPrefab;
	public GameObject greyBorderPrefab;
	public List<GameObject> prefabs = new List<GameObject>();
	public string url;
	public float refreshCooldown = 10f;
	private float nextCall;
	private Hashtable world;
	public Game game;
	public int tileWidth = 16;
	public int tileSpacing = 4;
	public int gameWidth = 25;
	public int gameHeight = 25;
	public Text fundsTxt;
	
	void Start () {
		game = game.GetComponent<Game>();
		world = new Hashtable();
		nextCall = Time.time + refreshCooldown;
		
		/*
		for(int x = -gameWidth; x<=gameWidth; x++){
			for(int y = -gameHeight; y<=gameHeight; y++){
				Tile t = new Tile();
				t.x = x;
				t.y = y;
				t.owner = "";
				t.building = 0;
				GameObject tile = GenerateTile(t);
				Vector2 pos = new Vector2(x, y);
				world[hash(pos)] = tile;
			}
		}*/
		
		StartCoroutine(GetWorld());
	}

	void Update(){
		if(Time.time > nextCall){
			nextCall = Time.time + refreshCooldown;
			StartCoroutine(GetWorld());
		}
	}

	int hash(Vector2 v){
		return Mathf.RoundToInt(v.x) * 10000 + Mathf.RoundToInt(v.y);
	}
	
	IEnumerator GetWorld() {
		WWWForm form = new WWWForm();
        form.AddField("username", game.username);

		UnityWebRequest request = UnityWebRequest.Post(url + "/world", form);
		yield return request.SendWebRequest();

		string s = request.downloadHandler.text;
		Debug.Log(s);
		Wrapper wrapper = JsonUtility.FromJson<Wrapper>(s);

		for(int i = 0; i < wrapper.tiles.Count; i++){
			int id = wrapper.tiles[i].id;
			int x = wrapper.tiles[i].x;
			int y = wrapper.tiles[i].y;
			Vector2 pos = new Vector2(x, y);

			if(world[hash(pos)] != null){
				GameObject o = (GameObject) world[hash(pos)];
				world.Remove(pos);
				Destroy(o);
			}

			GameObject tile = GenerateTile(wrapper.tiles[i]);
			world[hash(pos)] = tile;
		}

		fundsTxt.text = wrapper.myMoney.ToString();
	}

	GameObject GenerateTile(Tile tile){
		GameObject p, go, border, border_p;

		int x = tile.x * (tileWidth + tileSpacing);
		int z = tile.y * (tileWidth + tileSpacing);
		int rotationMultiplier = Random.Range(0, 3);
		int building = tile.building;

		// Set building
		p = defaultPrefab;
		if(prefabs[building] != null){
			p = prefabs[building];
		}

		// Add ownership identifier
		border_p = greyBorderPrefab;
		if(game.username != "" && game.username == tile.owner){
			border_p = greenBorderPrefab;
		}

		border = Instantiate(border_p, new Vector3(x, 0, z), Quaternion.identity);
		go = Instantiate(p, new Vector3(x, 0, z), Quaternion.identity);
		TileObject to = go.GetComponent<TileObject>();
		to.tileProperties = tile;
		//go.transform.Rotate(0, rotationMultiplier*90, 0);
		border.transform.parent = go.transform;
		
		return go;
	}
}
