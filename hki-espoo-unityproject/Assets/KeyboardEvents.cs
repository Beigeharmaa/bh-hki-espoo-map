using UnityEngine;
using System.Collections;

public class KeyboardEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown (KeyCode.Alpha1))
		{
		Debug.Log ("1");
			// Init MoveCamera location
			MoveCamera moveCamera = GetComponent<MoveCamera>();
			moveCamera.UpdateLocation("espoo");

		}
	
	if (Input.GetKeyDown (KeyCode.Alpha2))
		{
			Debug.Log ("2");
			// Init MoveCamera location
			MoveCamera moveCamera = GetComponent<MoveCamera>();
			moveCamera.UpdateLocation("church");

		}
	
	if (Input.GetKeyDown (KeyCode.Alpha3))
		{
			Debug.Log ("3");
			// Init MoveCamera location
			MoveCamera moveCamera = GetComponent<MoveCamera>();
			moveCamera.UpdateLocation("suomenlinna");

		}

	if (Input.GetKeyDown (KeyCode.Alpha4))
		{
			Debug.Log ("4");
			// Init MoveCamera location
			MoveCamera moveCamera = GetComponent<MoveCamera>();
			moveCamera.UpdateLocation("fjord");

		}



	}


}
