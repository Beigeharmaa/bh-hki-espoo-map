using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float speed = 1.0F;
	private float startTime;
	private GameObject camera;
	private float journeyLength;
	private float midPointInX,currentDiffInX;
	private Transform endPoint;
	private GameObject gameObject;
	private Vector3 targetDir;
	private Vector3 difference;
	private Vector3 height;
	private float y;
	private string name = "HelsinkiCathedral";
	private bool switched = false;
	private bool stop = false;

	void Start(){
	}

	public void UpdateLocation (string name) {
		startTime = Time.time;
    Debug.Log (name + "/Camera");
		camera = GameObject.Find (name + "/Camera");
		journeyLength = Vector3.Distance(transform.position, camera.transform.position);
		Vector3 originDiff = transform.position - camera.transform.position;
		midPointInX = Mathf.Abs(originDiff.x)/2;
		height = new Vector3(0, 4f, 0);
		camera.transform.position += height;
		InvokeRepeating("UpdatePosition", 0, 0.01F);
	}

	public void UpdatePosition(){
		stop = false;
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		Vector3 currentDiff = transform.position - camera.transform.position;
		currentDiffInX = Mathf.Abs(currentDiff.x) - midPointInX;


		if (currentDiffInX < 0 && !switched) {
			camera.transform.position -= height;
			switched = true;
		}

		if (camera != null) {
			transform.position = Vector3.Lerp (transform.position, camera.transform.position, fracJourney);

			Vector3 targetDir = camera.transform.position - transform.position;
			float step = speed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, camera.transform.forward, step, 0.0F);
			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.rotation = Quaternion.LookRotation(newDir);
		}

		if (currentDiff.x == 0)
			stop = true;
	}

	void Update (){
		if (stop)
			CancelInvoke("UpdatePosition");
	}
}
