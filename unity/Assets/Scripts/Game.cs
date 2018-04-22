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
	public GameObject buttonsGroup;
	public GameObject buyButton;
	public string username;
	private string url;
	private Tile selectedTile;
	private bool isHoveringUi = false;

	void Start() {
		url = GameObject.Find("Floor").GetComponent<GenerateWorld>().url;

		selectObject = (GameObject) Instantiate(selectObject, new Vector3(0, 50, 0), Quaternion.identity);
		selectObject.SetActive(false);
		hoverObject = (GameObject) Instantiate(hoverObject, new Vector3(0, 50, 0), Quaternion.identity);
		hoverObject.SetActive(false);
		buttonsGroup.SetActive(false);
		buyButton.SetActive(false);

		if(username == ""){
			dimmer.SetActive (true);
			inputField.gameObject.SetActive (true);
		}else{
			StartCoroutine(Login(username));
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

		Debug.Log("signing in");
		UnityWebRequest request = UnityWebRequest.Post(url + "/signIn", form);
		yield return request.SendWebRequest();
	}

	public IEnumerator buyTile(){
		WWWForm form = new WWWForm();
        form.AddField("username", username);
		form.AddField("x", selectedTile.x);
		form.AddField("y", selectedTile.y);
		Debug.Log("Creating at " + selectedTile.x + " " + selectedTile.y);

		Debug.Log("Hello there");

		UnityWebRequest request = UnityWebRequest.Post(url + "/occupyTile", form);
		yield return request.SendWebRequest();
	}

	public void hover(Vector3 pos, Tile tile) {
		if(isHoveringUi) return;
		
		if(tile.owner == username || tile.owner == ""){
			hoverObject.transform.position = pos;
			hoverObject.SetActive(true);
			Debug.Log("It's over " + tile.x + " " + tile.y);
		}else{
			hoverObject.SetActive(false);
		}
	}

	public void hanghover() {
		hoverObject.SetActive(false);
	}

	public void select(Vector3 pos, Tile tile) {
		if(isHoveringUi) return;

		if(tile.owner == username || tile.owner == ""){
			selectObject.transform.position = pos;
			selectObject.SetActive(true);
			selectedTile = tile;
			Debug.Log("Selected " + tile.x + " " + tile.y);

			if(tile.building == 0 && tile.owner == username){
				buttonsGroup.SetActive(true);
				buyButton.SetActive(false);
			}else if(tile.owner == ""){
				buttonsGroup.SetActive(false);
				buyButton.SetActive(true);
			}else{
				buttonsGroup.SetActive(false);
				buyButton.SetActive(false);
			}
		}
	}

	public void unselect() {
		selectObject.SetActive(false);
	}

	public void hoveringUi(bool hovering) {
		isHoveringUi = hovering;
	}
}
