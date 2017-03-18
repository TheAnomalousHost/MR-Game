using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	//0 X 0 = camera start pos
	//min/max X pos = 3.6 * 20 = -72/72
	//min/max Y pos = 4.3 * 20 = -86/86

	private const float panSpeed = 10f;
	private const float zoomSpeed = 0.5f;

	int[] cameraBoundsX = new int[] {-72, 72};
	int[] cameraBoundsY = new int[] {-86, 86};
	int[] zoomBounds = new int[] {1, 50};

	private Vector3 lastMousePosition;
	private Camera cam;
	private const int boundary = 5;
	private int screenWidth;
	private int screenHeight;

	//private bool nonMapScreenOpen;//for disabling pan and zoom if looking at anything other than the map

	void Awake(){
		cam = GetComponent<Camera> ();
	}


	void Start () {
		//nonMapScreenOpen = false;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	

	void Update () {
		//mousewheel zooms in and out, at least with map tiles/hexes
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize - zoomSpeed, zoomBounds[0]);

		} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			Camera.main.orthographicSize = Mathf.Min (Camera.main.orthographicSize + zoomSpeed, zoomBounds[1]);
		}
			
		PanCamera ();
	}

	//mouse scrolls when at the edge/corner of the screen
	void PanCamera(){
		Vector3 updatedCameraPosition = transform.position;

		//determine if camera should be moving along x axis, and if so, in which direction
		if (Input.mousePosition.x > screenWidth - boundary && transform.position.x < cameraBoundsX[1]) {
			updatedCameraPosition.x += panSpeed * Time.deltaTime;
		} else if (Input.mousePosition.x < 0 + boundary && transform.position.x > cameraBoundsX[0]) {
			updatedCameraPosition.x -= panSpeed * Time.deltaTime;
		}

		//determine if camera should be moving along y axis, and if so, in which direction
		if (Input.mousePosition.y > screenHeight - boundary && transform.position.y < cameraBoundsY[1]) {
			updatedCameraPosition.y += panSpeed * Time.deltaTime;
		} else if (Input.mousePosition.y < 0 + boundary && transform.position.y > cameraBoundsY[0]) {
			updatedCameraPosition.y -= panSpeed * Time.deltaTime;
		}

		//move camera, if necessary
		transform.position = updatedCameraPosition;


	}
}
