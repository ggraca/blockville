using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {

	public GameObject prefab;
	public int width = 10;
	public int height = 10;
	
	void Start () {
		for(int i = -width/2; i < width/2; i++){
			for(int j = -height/2; j < height/2; j++){
				Instantiate(prefab, new Vector3(i*(16 + 4),0,j*(16 + 4)), Quaternion.identity);
			}
		}
	}
	
}
