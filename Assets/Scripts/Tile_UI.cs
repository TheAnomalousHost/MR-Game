using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile_UI : MonoBehaviour {

	/*
	 * Currently the UI system for placing tiles from a selection onto the non-UI map.
	 * 
	 * 
	 * 
	 * 
	*/

	GameObject gameManager;

	public bool selected;
	//Color32 non_selectedColor = new Color(120f, 120f, 120f);
	//Color32 selectedColor = new Color(255f, 255f, 255f);
	Color non_selectedColor = new Color(0.4f, 0.4f, 0.4f);
	Color selectedColor = new Color(1f, 1f, 1f);
	ColorBlock cb;

	public int rotatedPosition;//value at or between 1 and 6
	public bool roadAt1;//true if there is a road leading out of edge 1
	public bool roadAt2;
	public bool roadAt3;
	public bool roadAt4;
	public bool roadAt5;
	public bool roadAt6;


	/* Hex edge IDs:
	 * 
	 *       ___4___
	 *      /       \
	 *    3/         \5
	 *    /           \
	 *    \           /
	 *    2\         /6
	 *      \_______/
	 *          1
	 * 
	 * */

	public bool sixClearingTile;//if tile has 6 clearings, this must be true;

	public bool edge1_edge2;//true if there is a road on this hex that leads from edge 1 to edge 2, else false
	public bool edge1_edge3;
	public bool edge1_edge4;
	public bool edge1_edge5;
	public bool edge1_edge6;

	public bool edge2_edge1;
	public bool edge2_edge3;
	public bool edge2_edge4;
	public bool edge2_edge5;
	public bool edge2_edge6;

	public bool edge3_edge1;
	public bool edge3_edge2;
	public bool edge3_edge4;
	public bool edge3_edge5;
	public bool edge3_edge6;

	public bool edge4_edge1;
	public bool edge4_edge2;
	public bool edge4_edge3;
	public bool edge4_edge4;
	public bool edge4_edge5;
	public bool edge4_edge6;

	public bool edge5_edge1;
	public bool edge5_edge2;
	public bool edge5_edge3;
	public bool edge5_edge4;
	public bool edge5_edge6;

	public bool edge6_edge1;
	public bool edge6_edge2;
	public bool edge6_edge3;
	public bool edge6_edge4;
	public bool edge6_edge5;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameController");

		selected = false;
		//gameObject.GetComponent<Image> ().color = non_selectedColor;
		cb = new ColorBlock ();
		cb = gameObject.GetComponent<Button> ().colors;
		cb.normalColor = non_selectedColor;
		gameObject.GetComponent<Button> ().colors = cb;
		//Debug.Log ("b color = " + gameObject.GetComponent<Button> ().colors.normalColor.b);

		rotatedPosition = 1;//starting position is downwards (South)
	}
	

	void Update () {
		if (selected) {
			if (Input.GetMouseButtonUp (1)) {//rotate tile with left click
				gameObject.GetComponent<RectTransform> ().Rotate (0, 0, -60f);//rotate
				rotatedPosition++;
				if (rotatedPosition > 6) {
					rotatedPosition = 1;
				}

				//update where the roads are at
				if (roadAt1 && roadAt2 && roadAt3 && roadAt4 && roadAt5 && roadAt6) {//if roads are on all sides, which is currently only possible with the Borderland, no need to update this for the sake of placement
					//do nothing
				} else {//update road-edge positions
					RotateEdgeRoads ();
					RotateConnectedRoads ();
				}
			} else if (Input.GetMouseButtonUp (0)) {
				//TODO: Update UI message to say "Place the [insert tile name here]."
			}
		}
	}


	public int GetRotation(){
		return rotatedPosition;
	}

	private void RotateEdgeRoads(){
		bool temp1 = false;//temp proxy for roadAt1
		bool temp2 = false;
		bool temp3 = false;
		bool temp4 = false;
		bool temp5 = false;
		bool temp6 = false;

		if (roadAt1) {
			temp2 = true;
		}
		if (roadAt2) {
			temp3 = true;
		}
		if (roadAt3) {
			temp4 = true;
		}
		if (roadAt4) {
			temp5 = true;
		}
		if (roadAt5) {
			temp6 = true;
		}
		if (roadAt6) {
			temp1 = true;
		}
		roadAt1 = temp1;
		roadAt2 = temp2;
		roadAt3 = temp3;
		roadAt4 = temp4;
		roadAt5 = temp5;
		roadAt6 = temp6;
	}


	private void RotateConnectedRoads(){
		bool e1_e2 = false;//temp proxy for edge1_edge2
		bool e1_e3 = false;
		bool e1_e4 = false;
		bool e1_e5 = false;
		bool e1_e6 = false;

		bool e2_e1 = false;
		bool e2_e3 = false;
		bool e2_e4 = false;
		bool e2_e5 = false;
		bool e2_e6 = false;

		bool e3_e1 = false;
		bool e3_e2 = false;
		bool e3_e4 = false;
		bool e3_e5 = false;
		bool e3_e6 = false;

		bool e4_e1 = false;
		bool e4_e2 = false;
		bool e4_e3 = false;
		bool e4_e5 = false;
		bool e4_e6 = false;

		bool e5_e1 = false;
		bool e5_e2 = false;
		bool e5_e3 = false;
		bool e5_e4 = false;
		bool e5_e6 = false;

		bool e6_e1 = false;
		bool e6_e2 = false;
		bool e6_e3 = false;
		bool e6_e4 = false;
		bool e6_e5 = false;

		if (edge1_edge2) {
			e2_e3 = true;
			e3_e2 = true;
		}
		if (edge1_edge3) {
			e2_e4 = true;
			e4_e2 = true;
		}
		if (edge1_edge4) {
			e2_e5 = true;
			e5_e2 = true;
		}
		if (edge1_edge5) {
			e2_e6 = true;
			e6_e2 = true;
		}
		if (edge1_edge6) {
			e2_e1 = true;
			e1_e2 = true;
		}

		if (edge2_edge1) {
			e3_e2 = true;
			e2_e3 = true;
		}
		if (edge2_edge3) {
			e3_e4 = true;
			e4_e3 = true;
		}
		if (edge2_edge4) {
			e3_e5 = true;
		}
		if (edge2_edge5) {
			e3_e6 = true;
			e6_e3 = true;
		}
		if (edge2_edge6) {
			e3_e1 = true;
			e1_e3 = true;
		}

		if (edge3_edge1) {
			e4_e2 = true;
			e2_e4 = true;
		}
		if (edge3_edge2) {
			e4_e3 = true;
			e3_e4 = true;
		}
		if (edge3_edge4) {
			e4_e5 = true;
			e5_e4 = true;
		}
		if (edge3_edge5) {
			e4_e6 = true;
			e6_e4 = true;
		}
		if (edge3_edge6) {
			e4_e1 = true;
			e1_e4 = true;
		}

		if (edge4_edge1) {
			e5_e2 = true;
			e2_e5 = true;
		}
		if (edge4_edge2) {
			e5_e3 = true;
			e3_e5 = true;
		}
		if (edge4_edge3) {
			e5_e4 = true;
			e4_e5 = true;
		}
		if (edge4_edge5) {
			e5_e6 = true;
			e6_e5 = true;
		}
		if (edge4_edge6) {
			e5_e1 = true;
			e1_e5 = true;
		}

		if (edge5_edge1) {
			e6_e2 = true;
			e2_e6 = true;
		}
		if (edge5_edge2) {
			e6_e3 = true;
			e3_e6 = true;
		}
		if (edge5_edge3) {
			e6_e4 = true;
			e4_e6 = true;
		}
		if (edge5_edge4) {
			e6_e5 = true;
			e5_e6 = true;
		}
		if (edge5_edge6) {
			e6_e1 = true;
			e1_e6 = true;
		}
		
		if (edge6_edge1) {
			e1_e2 = true;
			e2_e1 = true;
		}
		if (edge6_edge2) {
			e1_e3 = true;
			e3_e1 = true;
		}
		if (edge6_edge3) {
			e1_e4 = true;
			e4_e1 = true;
		}
		if (edge6_edge4) {
			e1_e5 = true;
			e5_e1 = true;
		}
		if (edge6_edge5) {
			e1_e6 = true;
			e6_e1 = true;
		}

		edge1_edge2 = e1_e2;
		edge1_edge3 = e1_e3;
		edge1_edge4 = e1_e4;
		edge1_edge5 = e1_e5;
		edge1_edge6 = e1_e6;

		edge2_edge1 = e2_e1;
		edge2_edge3 = e2_e3;
		edge2_edge4 = e2_e4;
		edge2_edge5 = e2_e5;
		edge2_edge6 = e2_e6;

		edge3_edge1 = e3_e1;
		edge3_edge2 = e3_e2;
		edge3_edge4 = e3_e4;
		edge3_edge5 = e3_e5;
		edge3_edge6 = e3_e6;

		edge4_edge1 = e4_e1;
		edge4_edge2 = e4_e2;
		edge4_edge3 = e4_e3;
		edge4_edge5 = e4_e5;
		edge4_edge6 = e4_e6;

		edge5_edge1 = e5_e1;
		edge5_edge2 = e5_e2;
		edge5_edge3 = e5_e3;
		edge5_edge4 = e5_e4;
		edge5_edge6 = e5_e6;

		edge6_edge1 = e6_e1;
		edge6_edge2 = e6_e2;
		edge6_edge3 = e6_e3;
		edge6_edge4 = e6_e4;
		edge6_edge5 = e6_e5;
	}

	public void SelectUITile(){
		if (!selected) {
			if (gameObject.transform.parent.parent.gameObject.name != "Canvas") {
				Debug.LogError ("Did not locate correct parent that has UI_Map.cs!");
			}

			cb.normalColor = non_selectedColor;

			for (int i = gameObject.transform.parent.childCount; i > 0; i--) {
				gameObject.transform.parent.GetChild (i - 1).GetComponent<Button> ().colors = cb;
				gameObject.transform.parent.GetChild (i - 1).GetComponent<Tile_UI> ().selected = false;
			}
			
			cb.normalColor = selectedColor;
			gameObject.GetComponent<Button> ().colors = cb;

			selected = true;
			gameManager.GetComponent<GameManager> ().uiHexSelected = true;
		}
	}
}
