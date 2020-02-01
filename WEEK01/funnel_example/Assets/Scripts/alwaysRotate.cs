using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysRotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		// rotate object based on Y axis
		this.transform.Rotate(Vector3.up * (Time.deltaTime * 15.0f));

	}
}
