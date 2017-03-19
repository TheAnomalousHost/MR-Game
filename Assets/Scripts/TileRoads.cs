using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoads : MonoBehaviour {
	/*
	 * This script is mainly for labeling which edges on the hex have roads leading out of them.
	 * 
	 * Should also use this script to determine which clearings/roads are legally linked back to the Borderland.
	 * 
	*/

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

	public bool sixClearingTile;//if tile has 6 clearings, this must be true;

	public bool edge1_edge2;//true if there is a road on this hex that leads from edge 1 to edge 2, else false
	public bool edge1_edge3;
	public bool edge1_edge4;
	public bool edge1_edge5;
	public bool edge1_edge6;

	public bool edge2_edge3;
	public bool edge2_edge4;
	public bool edge2_edge5;
	public bool edge2_edge6;

	public bool edge3_edge4;
	public bool edge3_edge5;
	public bool edge3_edge6;

	public bool edge4_edge5;
	public bool edge4_edge6;

	public bool edge5_edge6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
