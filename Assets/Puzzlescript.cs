using UnityEngine;
using System.Collections;

public class Puzzlescript : MonoBehaviour {

	public Camera playerCamera;
	public GameObject cursor;
	protected GameObject lastCursor;
    protected bool mouseDown = false;
    protected Vector3 lastMousePosition;
    protected GameObject activePuzzle;

	// Use this for initialization
	void Start () {
        lastMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
        bool onPuzzle = false;
        bool onPuzzleStart = false;

		Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            if (activePuzzle != null) {
                (GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
                activePuzzle = null;
                Debug.Log("Ending draw!");
            }
        }
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

		if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.gameObject.CompareTag("puzzle_start")) {
                onPuzzleStart = true;
            }
			if (hit.collider.gameObject.CompareTag("puzzle_screen")) {
				//Debug.Log("Puzzle Screen located!");
                onPuzzle = true;
			}
		}

        if (Input.GetMouseButtonDown(0)) {
            mouseDown = true;
            if (onPuzzleStart) {
                if (lastCursor == null) {
                    lastCursor = GameObject.Instantiate(cursor);
                }
                (GetComponent("FirstPersonController") as MonoBehaviour).enabled = false;
                activePuzzle = hit.collider.transform.parent.gameObject;
                lastCursor.transform.position = activePuzzle.GetComponent<MeshCollider>()
                    .ClosestPointOnBounds(hit.collider.transform.position);
                Debug.Log("Starting draw....");
            }
        }

        if (activePuzzle != null) {
            Vector3 mousePosition = Input.mousePosition;
            if (mousePosition != lastMousePosition) {
                Vector3 mouseDelta = mousePosition - lastMousePosition;
                lastMousePosition = mousePosition;
                //Debug.Log("Moving...: " + mouseDelta);
                Vector3 newPosition = lastCursor.transform.position + mouseDelta / 100;
                lastCursor.transform.position = activePuzzle.GetComponent<MeshCollider>()
                    .ClosestPointOnBounds(newPosition);
            }
        }
	}
}
