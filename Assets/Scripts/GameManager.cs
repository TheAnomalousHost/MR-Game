using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public string sceneName;
	public string status;
	public GameObject hexSlot;//prefab of blank white hex slot

	public List<GameObject> realmMap;//TODO: make private

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
				realmMap.Add (a);
			}
		}

		if (realmMap.Capacity == 0) {
			Debug.LogError ("No hex slots found and put into realmMap in GameManager.  Closing Application.");
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
		realmMap.Add (hex);
	}

	public List<GameObject> GetRealmMap(){
		return realmMap;
	}

	public GameObject GetHexSlot(){
		return hexSlot;
	}
}
