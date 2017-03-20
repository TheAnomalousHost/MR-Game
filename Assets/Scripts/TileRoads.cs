using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoads : MonoBehaviour {

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
	 * 
	*/

	public bool roadAt1;//true if there is a road leading out of edge 1
	public bool roadAt2;
	public bool roadAt3;
	public bool roadAt4;
	public bool roadAt5;
	public bool roadAt6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
