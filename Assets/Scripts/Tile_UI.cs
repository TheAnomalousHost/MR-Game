using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile_UI : MonoBehaviour {

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
			if (Input.GetMouseButtonUp (1)) {
				gameObject.GetComponent<RectTransform> ().Rotate (0, 0, -60f);//rotate
				rotatedPosition++;
				if (rotatedPosition > 6) {
					rotatedPosition = 1;
				}

				//update where the roads are at
				if (roadAt1 && roadAt2 && roadAt3 && roadAt4 && roadAt5 && roadAt6) {//if roads are on all sides, no need to update this for the sake of placement
					//do nothing
				} else {//update road-edge positions
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

			} else if (Input.GetMouseButtonUp (0)) {
				//Debug.Log ("Will place tile.");
			}
		}
	}


	public int GetRotation(){
		return rotatedPosition;
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
