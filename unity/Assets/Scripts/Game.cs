using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour {

	public GameObject selectObject;
	public GameObject hoverObject;
	public GameObject dimmer;
	public InputField inputField;
	public string username;
	private string url;
	
	void Start() {
		url = GameObject.Find("Floor").GetComponent<GenerateWorld>().url;

		selectObject = (GameObject) Instantiate(selectObject, new Vector3(0, 50, 0), Quaternion.identity);
		selectObject.SetActive(false);
		hoverObject = (GameObject) Instantiate(hoverObject, new Vector3(0, 50, 0), Quaternion.identity);
		hoverObject.SetActive(false);

		if(username == ""){
			dimmer.SetActive (true);
			inputField.gameObject.SetActive (true);
		}
	}

	void OnGUI() {
		if(inputField.isFocused && inputField.text != "" && Input.GetKey(KeyCode.Return)) {
			username = inputField.text;
			StartCoroutine(Login(username));
			dimmer.SetActive (false);
			inputField.gameObject.SetActive (false);
		}
	}

	IEnumerator Login(string username) {
		WWWForm form = new WWWForm();
        form.AddField("username", username);

		UnityWebRequest request = UnityWebRequest.Post(url + "/signin", form);
		yield return request.SendWebRequest();
	}

	public void hover(Vector3 pos) {
		hoverObject.transform.position = pos;
		hoverObject.SetActive(true);
	}

	public void hanghover() {
		hoverObject.SetActive(false);
	}

	public void select(Vector3 pos) {
		selectObject.transform.position = pos;
		selectObject.SetActive(true);
	}

	public void unselect() {
		selectObject.SetActive(false);
	}
}
