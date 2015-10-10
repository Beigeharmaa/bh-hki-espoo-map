using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {

  public string text = "Press SPACE to start recording";
  public Text feedback;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

  // method
  public void setText(string key) {
    feedback.text = key;
  }

}
