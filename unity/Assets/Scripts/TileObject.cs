using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {
	private Game game;

	void Start() {
		game = GameObject.Find("Game/Canvas").GetComponent<Game>();
	}

	void OnMouseEnter() {
        game.select(transform.position);
    }

    void OnMouseExit() {
        game.unselect();
    }
}
