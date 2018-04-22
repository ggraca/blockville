﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	
	void Start () {
		game = game.GetComponent<Game>();
		world = new Hashtable();
		nextCall = Time.time + refreshCooldown;
		StartCoroutine(GetWorld());
	}

	void Update(){
		if(Time.time > nextCall){
			nextCall = Time.time + refreshCooldown;
			StartCoroutine(GetWorld());
		}
	}
	
	IEnumerator GetWorld() {
		UnityWebRequest request = UnityWebRequest.Get(url);        
		yield return request.SendWebRequest();

		string s = request.downloadHandler.text;
		Wrapper wrapper = JsonUtility.FromJson<Wrapper>(s);

		for(int i = 0; i < wrapper.tiles.Count; i++){
			int id = wrapper.tiles[i].id;

			if(world[id] != null){
				GameObject o = (GameObject) world[id];
				world.Remove(id);
				Destroy(o);
			}

			GameObject tile = GenerateTile(wrapper.tiles[i]);
			world.Add(id, tile);	
		}
	}

	GameObject GenerateTile(Tile tile){
		GameObject p, go, border, border_p;

		int x = tile.x * (tileWidth + tileSpacing);
		int z = tile.y * (tileWidth + tileSpacing);
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
		border.transform.parent = go.transform;
		
		return go;
	}
}