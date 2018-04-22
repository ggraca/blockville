using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	float speed = 50;
	void FixedUpdate () {
		if(Input.GetAxis("Vertical") > 0) {
			transform.position = transform.position + (transform.forward + transform.right) * Time.deltaTime * speed;
		}
		else if(Input.GetAxis("Vertical") < 0) {
			transform.position = transform.position + (- transform.forward - transform.right) * Time.deltaTime * speed;
		}
		if(Input.GetAxis("Horizontal") > 0) {
			transform.position = transform.position + (- transform.forward + transform.right) * Time.deltaTime * speed;
		}
		else if(Input.GetAxis("Horizontal") < 0) {
			transform.position = transform.position + (transform.forward - transform.right) * Time.deltaTime * speed;
		}

		if(Input.GetKeyDown("q")) {
			transform.Rotate(0, 90, 0);
		}
		if(Input.GetKeyDown("e")) {
			transform.Rotate(0, -90, 0);
		}
	}
}
