using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAroundLocal (transform.up, 0.01f);

	}
}
