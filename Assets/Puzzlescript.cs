using UnityEngine;
using System.Collections;

public class Puzzlescript : MonoBehaviour {

	public Camera playerCamera;
	public GameObject cursor;
	protected GameObject lastCursor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.CompareTag("puzzle_screen")) {
				Debug.Log("Puzzle Screen located!");
				if (lastCursor == null) {
					lastCursor = GameObject.Instantiate(cursor);
				}

				lastCursor.transform.position = (ray.origin + ray.direction * hit.distance);
			}
		}
	}
}
