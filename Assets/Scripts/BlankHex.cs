using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankHex : MonoBehaviour {

	/*
	 * Colors:
	 * black 0,0,0
	 * blue 0,0,1
	 * clear 0,0,0,0
	 * cyan 0,1,1
	 * gray 0.5,0.5,0.5
	 * green 0,1,0
	 * magenta 1,0,1
	 * red 1,0,0
	 * white 1,1,1
	 * yellow 1,0.92,0.016
	 * 
	 * 
	 * 
	 * Hex edge IDs:
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

	//Ray ray;
	//RaycastHit hit;

	bool selected;//hex position is selected for placement
	public bool placed;//hex position filled, can no longer be selected

	GameObject gameManager;
	public static int numberOfHexes = 1;

	const float shiftX = 3.75f;
	const float shiftYSmall = 2.16f;
	const float shiftYBig = 4.32f;

	public GameObject adjHex1;//neighbor1;
	public GameObject adjHex2;//neighbor2;
	public GameObject adjHex3;//neighbor3;
	public GameObject adjHex4;//neighbor4;
	public GameObject adjHex5;//neighbor5;
	public GameObject adjHex6;//neighbor6;


	public struct HexID{
		int posA, posB;

		public HexID(int a, int b){
			posA = a;
			posB = b;
		}

		public int GetPosA(){
			return posA;
		}

		public int GetPosB(){
			return posB;
		}
	}

	HexID hexID;
	public string ID;

	void Start () {
		//Debug.Log ("Hex slot created!");

		gameManager = GameObject.FindGameObjectWithTag ("GameController");
		
		selected = false;
		placed = false;
		//hexID = "Slot";

		//start of game hex slot
		//there should only be 1 blank hex at start of realm creation, unless using some variant like SuperRealm (much further down the line)
		if (gameObject.name == "Hex 0") {
			hexID = new HexID (0, 0);
		}

		ID = hexID.GetPosA ().ToString () + ", " + hexID.GetPosB ().ToString ();
	}

	//player is preparing to select hex position for placement
	void OnMouseEnter(){
		if (!placed) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (0, 1, 0);
			selected = true;
		}
	}

	//player decides not to place hex here, for now
	void OnMouseExit(){
		if (!placed) {
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1);
			selected = false;
		}
	}
		

	public GameObject GetNeighbor1(){
		return adjHex1;
	}

	public void SetNeighbor1(GameObject n){
		adjHex1 = n;
	}

	public GameObject GetNeighbor2(){
		return adjHex2;
	}

	public void SetNeighbor2(GameObject n){
		adjHex2 = n;
	}

	public GameObject GetNeighbor3(){
		return adjHex3;
	}

	public void SetNeighbor3(GameObject n){
		adjHex3 = n;
	}

	public GameObject GetNeighbor4(){
		return adjHex4;
	}

	public void SetNeighbor4(GameObject n){
		adjHex4 = n;
	}

	public GameObject GetNeighbor5(){
		return adjHex5;
	}

	public void SetNeighbor5(GameObject n){
		adjHex5 = n;
	}

	public GameObject GetNeighbor6(){
		return adjHex6;
	}

	public void SetNeighbor6(GameObject n){
		adjHex6 = n;
	}

	public bool HexIsPlaced(){
		return placed;
	}

	//player will place hex tile in this selected spot, then automatically create new hex slots around it, if necessary
	void OnMouseUp(){
		if (selected && !placed) {

			//There should only be 1 blank hex at the start of map creation.  From there that will create 6 new hexes.

			//if placed next to < 2 adjacent tiles and there is more than just the starting hex slots, it's illegal
			if (!IsTilePlacementLegal () && numberOfHexes > 7) {
				gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 0.92f, 0.016f);
				return;
			}

			gameObject.GetComponent<SpriteRenderer> ().color = new Color (1, 0, 0);//TODO: replace with actual hex
			placed = true;//aka filled, probably not necessary to have this if using the hexID string

			//tracks which new hex slots were placed as a result of the new hex tile (this gameobject)
			GameObject newAdjHex1 = null;
			GameObject newAdjHex2 = null;
			GameObject newAdjHex3 = null;
			GameObject newAdjHex4 = null;
			GameObject newAdjHex5 = null;
			GameObject newAdjHex6 = null;

			Vector3 newHexSlotPosition = gameObject.transform.position;//pre-position for new hex slot to be placed; will be altered with if-statements below

			//after tile is placed in slot, create 0-3 new hex slots adjacent to it, and have them all connect to this hex tile
			if (adjHex1 == null) {//checking for hex slots that were adjecent to this hex tile during the time it was a hex slot
				newHexSlotPosition.y -= shiftYBig;

				//state that this hex tile is adjacent to new hex slot which will be connected by the '1' side (south hex) of this hex tile
				adjHex1 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);//added in blank prefab hex slot
				adjHex1.transform.position = newHexSlotPosition;
				adjHex1.GetComponent<BlankHex> ().adjHex4 = gameObject;
				adjHex1.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA (), hexID.GetPosB () + 1);
				adjHex1.GetComponent<BlankHex> ().ID = hexID.GetPosA ().ToString () + ", " + (hexID.GetPosB () + 1).ToString ();

				adjHex1.name = "Hex " + numberOfHexes;
				numberOfHexes++;

				newAdjHex1 = adjHex1;

				newHexSlotPosition.y = gameObject.transform.position.y;//reset position
			}
			if (adjHex2 == null) {
				newHexSlotPosition.x -= shiftX;
				newHexSlotPosition.y -= shiftYSmall;

				adjHex2 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);
				adjHex2.transform.position = newHexSlotPosition;
				adjHex2.GetComponent<BlankHex> ().adjHex5 = gameObject;
				adjHex2.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA () - 1, hexID.GetPosB () + 1);
				adjHex2.GetComponent<BlankHex> ().ID = (hexID.GetPosA () - 1).ToString () + ", " + (hexID.GetPosB () + 1).ToString ();

				adjHex2.name = "Hex " + numberOfHexes;
				numberOfHexes++;

				newAdjHex2 = adjHex2;

				newHexSlotPosition.x = gameObject.transform.position.x;
				newHexSlotPosition.y = gameObject.transform.position.y;
			}
			if (adjHex3 == null) {
				newHexSlotPosition.x -= shiftX;
				newHexSlotPosition.y += shiftYSmall;

				adjHex3 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);
				adjHex3.transform.position = newHexSlotPosition;
				adjHex3.GetComponent<BlankHex> ().adjHex6 = gameObject;
				adjHex3.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA () - 1, hexID.GetPosB ());
				adjHex3.GetComponent<BlankHex> ().ID = (hexID.GetPosA () - 1).ToString () + ", " + (hexID.GetPosB ()).ToString ();

				adjHex3.name = "Hex " + numberOfHexes;				
				numberOfHexes++;

				newAdjHex3 = adjHex3;

				newHexSlotPosition.x = gameObject.transform.position.x;
				newHexSlotPosition.y = gameObject.transform.position.y;
			}
			if (adjHex4 == null) {
				newHexSlotPosition.y += shiftYBig;

				adjHex4 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);
				adjHex4.transform.position = newHexSlotPosition;
				adjHex4.GetComponent<BlankHex> ().adjHex1 = gameObject;
				adjHex4.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA (), hexID.GetPosB () - 1);
				adjHex4.GetComponent<BlankHex> ().ID = (hexID.GetPosA ()).ToString () + ", " + (hexID.GetPosB () - 1).ToString ();

				adjHex4.name = "Hex " + numberOfHexes;
				numberOfHexes++;

				newAdjHex4 = adjHex4;

				newHexSlotPosition.y = gameObject.transform.position.y;
			}
			if (adjHex5 == null) {
				newHexSlotPosition.x += shiftX;
				newHexSlotPosition.y += shiftYSmall;

				adjHex5 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);
				adjHex5.transform.position = newHexSlotPosition;
				adjHex5.GetComponent<BlankHex> ().adjHex2 = gameObject;
				adjHex5.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA () + 1, hexID.GetPosB () - 1);
				adjHex5.GetComponent<BlankHex> ().ID = (hexID.GetPosA () + 1).ToString () + ", " + (hexID.GetPosB () - 1).ToString ();

				adjHex5.name = "Hex " + numberOfHexes;
				numberOfHexes++;

				newAdjHex5 = adjHex5;

				newHexSlotPosition.x = gameObject.transform.position.x;
				newHexSlotPosition.y = gameObject.transform.position.y;
			}
			if (adjHex6 == null) {
				newHexSlotPosition.x += shiftX;
				newHexSlotPosition.y -= shiftYSmall;

				adjHex6 = Instantiate (gameManager.GetComponent<GameManager> ().GetHexSlot (), GameObject.FindGameObjectWithTag ("Tiles").transform);
				adjHex6.transform.position = newHexSlotPosition;
				adjHex6.GetComponent<BlankHex> ().adjHex3 = gameObject;
				adjHex6.GetComponent<BlankHex> ().hexID = new HexID (hexID.GetPosA () + 1, hexID.GetPosB ());
				adjHex6.GetComponent<BlankHex> ().ID = (hexID.GetPosA () + 1).ToString () + ", " + (hexID.GetPosB ()).ToString ();

				adjHex6.name = "Hex " + numberOfHexes;
				numberOfHexes++;

				newAdjHex6 = adjHex6;

				newHexSlotPosition.x = gameObject.transform.position.x;
				newHexSlotPosition.y = gameObject.transform.position.y;
			}

			//after new hex slots are placed and linked to hex tile, connect new hex slots to the other adjacent hexes that are also adjacent to this hex tile
			if (newAdjHex1 != null) {
				adjHex1.GetComponent<BlankHex> ().adjHex3 = adjHex2;
				adjHex2.GetComponent<BlankHex> ().adjHex6 = adjHex1;

				adjHex1.GetComponent<BlankHex> ().adjHex5 = adjHex6;
				adjHex6.GetComponent<BlankHex> ().adjHex2 = adjHex1;

				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex1);
			}
			if (newAdjHex2 != null) {
				adjHex2.GetComponent<BlankHex> ().adjHex4 = adjHex3;
				adjHex3.GetComponent<BlankHex> ().adjHex1 = adjHex2;

				adjHex2.GetComponent<BlankHex> ().adjHex6 = adjHex1;
				adjHex1.GetComponent<BlankHex> ().adjHex3 = adjHex2;

				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex2);
			}
			if (newAdjHex3 != null) {
				adjHex3.GetComponent<BlankHex> ().adjHex5 = adjHex4;
				adjHex4.GetComponent<BlankHex> ().adjHex2 = adjHex3;

				adjHex3.GetComponent<BlankHex> ().adjHex1 = adjHex2;
				adjHex2.GetComponent<BlankHex> ().adjHex4 = adjHex3;


				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex3);
			}
			if (newAdjHex4 != null) {
				adjHex4.GetComponent<BlankHex> ().adjHex6 = adjHex5;
				adjHex5.GetComponent<BlankHex> ().adjHex3 = adjHex4;

				adjHex4.GetComponent<BlankHex> ().adjHex2 = adjHex3;
				adjHex3.GetComponent<BlankHex> ().adjHex5 = adjHex4;

				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex4);
			}
			if (newAdjHex5 != null) {
				adjHex5.GetComponent<BlankHex> ().adjHex1 = adjHex6;
				adjHex6.GetComponent<BlankHex> ().adjHex4 = adjHex5;

				adjHex5.GetComponent<BlankHex> ().adjHex3 = adjHex4;
				adjHex4.GetComponent<BlankHex> ().adjHex6 = adjHex5;

				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex5);
			}
			if (newAdjHex6 != null) {
				adjHex6.GetComponent<BlankHex> ().adjHex2 = adjHex1;
				adjHex1.GetComponent<BlankHex> ().adjHex5 = adjHex6;

				adjHex6.GetComponent<BlankHex> ().adjHex4 = adjHex5;
				adjHex5.GetComponent<BlankHex> ().adjHex1 = adjHex6;

				gameManager.GetComponent<GameManager> ().AddToRealmMap (adjHex6);
			}

			//only thing left to check is if 2 adjacent hex slots are not connected, and connect them, using hexID for reference.
			//probably best to use GameManager to store all hexes, and reference their ID's
			
			//check neighbor hex slots for potential unlinked adjacent hex slots
			if(!GetNeighbor1().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor1 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor1());
			}
			if(!GetNeighbor2().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor2 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor2());
			}
			if(!GetNeighbor3().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor3 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor3());
			}
			if(!GetNeighbor4().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor4 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor4());
			}
			if(!GetNeighbor5().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor5 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor5());
			}
			if(!GetNeighbor6().GetComponent<BlankHex>().HexIsPlaced()){
				//Debug.Log ("Checking neighbor6 for non-linked linkable neighbors.");
				CheckForAdjacentSlots (GetNeighbor6());
			}
		}
	}


	//should not be called unless there are at least 5 (?) hex tiles in play
	void CheckForAdjacentSlots(GameObject hexSlotA){
	if (hexSlotA.GetComponent<BlankHex> ().GetNeighbor1() == null || hexSlotA.GetComponent<BlankHex> ().GetNeighbor2() == null || hexSlotA.GetComponent<BlankHex> ().GetNeighbor3() == null ||
		hexSlotA.GetComponent<BlankHex> ().GetNeighbor4() == null || hexSlotA.GetComponent<BlankHex> ().GetNeighbor5() == null || hexSlotA.GetComponent<BlankHex> ().GetNeighbor6() == null) {
			List<GameObject> slotMap = gameManager.GetComponent<GameManager> ().GetRealmMap ();
			foreach (GameObject a in slotMap) {
			if (!a.GetComponent<BlankHex> ().HexIsPlaced ()) {
					if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA()) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB() + 1) &&
					a.GetComponent<BlankHex>().GetNeighbor4() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex1 = a;
						a.GetComponent<BlankHex> ().adjHex4 = hexSlotA;
					}
				if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA() - 1) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB() + 1) &&
					a.GetComponent<BlankHex>().GetNeighbor5() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex2 = a;
						a.GetComponent<BlankHex> ().adjHex5 = hexSlotA;
					}
				if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA() - 1) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB()) &&
					a.GetComponent<BlankHex>().GetNeighbor6() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex3 = a;
						a.GetComponent<BlankHex> ().adjHex6 = hexSlotA;
					}
					if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA()) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB() -1) &&
					a.GetComponent<BlankHex>().GetNeighbor1() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex4 = a;
						a.GetComponent<BlankHex> ().adjHex1 = hexSlotA;
					}
				if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA() + 1) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB() - 1) &&
					a.GetComponent<BlankHex>().GetNeighbor2() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex5 = a;
						a.GetComponent<BlankHex> ().adjHex2 = hexSlotA;
					}
				if ((a.GetComponent<BlankHex> ().hexID.GetPosA () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosA() + 1) &&
					(a.GetComponent<BlankHex> ().hexID.GetPosB () == hexSlotA.GetComponent<BlankHex>().hexID.GetPosB()) &&
					a.GetComponent<BlankHex>().GetNeighbor3() == null) {
						hexSlotA.GetComponent<BlankHex> ().adjHex6 = a;
						a.GetComponent<BlankHex> ().adjHex3 = hexSlotA;
					}
				}
			}
		}
	}


	//check if new hex tile placement is legal
	bool IsTilePlacementLegal(){
		int tileNeighbors = 0;
		if (GetNeighbor1 () != null && GetNeighbor1 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}
		if (GetNeighbor2 () != null && GetNeighbor2 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}
		if (GetNeighbor3 () != null && GetNeighbor3 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}
		if (GetNeighbor4 () != null && GetNeighbor4 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}
		if (GetNeighbor5 () != null && GetNeighbor5 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}
		if (GetNeighbor6 () != null && GetNeighbor6 ().GetComponent<BlankHex> ().HexIsPlaced ()) { 
			tileNeighbors++;
		}

		if (tileNeighbors >= 2) {
			return true;
		} else {
			return false;
		}

	}
}