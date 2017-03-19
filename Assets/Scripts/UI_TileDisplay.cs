using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TileDisplay : MonoBehaviour {

	/* The positions for the tiles:
	 * X value left side: -480
	 * X value right side: -320
	 * Y value top: 190
	 * Y value lowered by: -140
	 * 
	 * 
	 * 
	 * */

	const float xLeft = -480f;
	const float xRight = -320f;
	const float yTop = 190;
	const float yMinus = -140f;

	public List<GameObject> mapTiles;
	public List<GameObject> displayedUITiles;
	static GameObject gameManager;
	public int numberOfTiles;
	//GameManager gameManager;//TODO: Choose tiles based on game mode.

	void Awake(){
		gameManager = GameObject.FindGameObjectWithTag ("GameController");
		mapTiles = gameManager.GetComponent<GameManager> ().GetUITiles ();
		numberOfTiles = mapTiles.Count;

		for(int i = 0; i < numberOfTiles; i++) {
			float xValue;
			if (i % 2 == 0) {
				xValue = xLeft;
			} else {
				xValue = xRight;
			}
			GameObject tile = Instantiate (mapTiles [i], gameObject.transform.FindChild("UI_Tiles").transform );
			tile.name = mapTiles [i].name;

			//new Vector3(xValue, yTop - (i * yMinus), transform.position.z), Quaternion.identity, 
			tile.transform.localPosition = new Vector3(xValue, yTop + ((i / 2) * yMinus), transform.position.z);
			displayedUITiles.Add (tile);
			//Debug.Log ("Created tile UI " + tile.name);
		}
	}
		
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<GameObject> GetDisplayedUITiles(){
		return displayedUITiles;
	}
}
