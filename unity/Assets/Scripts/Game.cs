using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public GameObject selector;
	public GameObject dimmer;
	public InputField inputField;
	public string username;
	
	void Start() {
		selector = (GameObject) Instantiate(selector, new Vector3(0, 50, 0), Quaternion.identity);
		selector.SetActive(false);

		if(username == ""){
			dimmer.SetActive (true);
			inputField.gameObject.SetActive (true);
		}
	}

	void OnGUI() {
		if(inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return)) {
			username = inputField.text;
			dimmer.SetActive (false);
			inputField.gameObject.SetActive (false);
		}
	}

	public void select(Vector3 pos) {
		selector.transform.position = pos;
		selector.SetActive(true);
	}

	public void unselect() {
		selector.SetActive(false);
	}
}
