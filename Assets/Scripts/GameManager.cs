using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public string sceneName;
	public string status;
	public GameObject hexSlot;//prefab of blank white hex slot
	public bool uiHexSelected {get; set;}

	public List<GameObject> hexMap = new List<GameObject>();//list of ALL hexes, whether they are tiles or just slots
	public List<GameObject> gameTiles = new List<GameObject>();//list of tile gameobject to replace (or add onto) blank hexes
	public List<GameObject> uiTiles = new List<GameObject>();



	void Awake() {
		DontDestroyOnLoad(transform.gameObject);

		sceneName = SceneManager.GetActiveScene ().name;
	}

	void Start(){
		Debug.Log ("Loaded scene: " + sceneName);
		if (sceneName == "Splash") {
			Invoke ("LoadStartMenu", 2f);
		}

		if (sceneName == "Map") {
			status = "Building Map";

			GameObject[] hexSlots = GameObject.FindGameObjectsWithTag ("Tile");
			foreach(GameObject a in hexSlots){//add starting hex slots to realmMap list
				hexMap.Add (a);
			}
		}

		if (hexMap.Capacity == 0) {
			Debug.LogError ("No hex slots found and put into hexMap in GameManager.  Closing Application.");
			Application.Quit ();
		}
		if (gameTiles.Capacity == 0) {
			Debug.LogError ("No hex slots found and put into gameTiles in GameManager.  Closing Application.");
			Application.Quit ();
		}
	}

	public void LoadStartMenu(){
		LoadLevel ("Start Menu");
	}

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		sceneName = name;
		SceneManager.LoadScene (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested.");
		Application.Quit ();
	}

	public void AddToRealmMap(GameObject hex){
		hexMap.Add (hex);
	}

	public List<GameObject> GetRealmMap(){
		return hexMap;
	}

	public List<GameObject> GetGameTiles(){
		return gameTiles;
	}

	public List<GameObject> GetUITiles(){
		return uiTiles;
	}

	public GameObject GetHexSlot(){
		return hexSlot;
	}

	public void HexToTile(GameObject hex){
		//TODO: make code that converts image of selected hex to sprite of tile, and add in clearings (ie sphere colliders to mark their positions)
	}
}
