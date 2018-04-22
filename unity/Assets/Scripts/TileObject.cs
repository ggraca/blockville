using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileObject : MonoBehaviour {
	private Game game;
    public Tile tileProperties;

	void Start() {
		game = GameObject.Find("Game/Canvas").GetComponent<Game>();
	}

    void OnMouseDown() {
        game.select(transform.position, tileProperties);
    }

	void OnMouseEnter() {
        game.hover(transform.position, tileProperties);
    }

    void OnMouseExit() {
        game.hanghover();
    }
}
