using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	void FixedUpdate () {
		if(Input.GetAxis("Vertical") > 0) {
			transform.position = transform.position + transform.forward + transform.right;
		}
		else if(Input.GetAxis("Vertical") < 0) {
			transform.position = transform.position - transform.forward - transform.right;
		}
		if(Input.GetAxis("Horizontal") > 0) {
			transform.position = transform.position - transform.forward + transform.right;
		}
		else if(Input.GetAxis("Horizontal") < 0) {
			transform.position = transform.position + transform.forward - transform.right;
		}

		if(Input.GetKeyDown("q")) {
			transform.Rotate(0, 90, 0);
		}
		if(Input.GetKeyDown("e")) {
			transform.Rotate(0, -90, 0);
		}
	}
}
