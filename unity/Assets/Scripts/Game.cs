using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour {

	public GameObject selector;
	public GameObject dimmer;
	public InputField inputField;
	public string username;
	private string url;
	
	void Start() {
		url = GameObject.Find("Floor").GetComponent<GenerateWorld>().url;

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

	public void select(Vector3 pos) {
		selector.transform.position = pos;
		selector.SetActive(true);
	}

	public void unselect() {
		selector.SetActive(false);
	}
}
