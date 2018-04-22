using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {
	private Game game;

	void Start() {
		game = GameObject.Find("Game/Canvas").GetComponent<Game>();
	}

    void OnMouseDown() {
        game.select(transform.position);
    }

	void OnMouseEnter() {
        game.hover(transform.position);
    }

    void OnMouseExit() {
        game.hanghover();
    }
}
