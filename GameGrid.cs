using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGrid : MonoBehaviour {

	// reference to move space prefab
	public GameObject moveSpace;

	// grid dimensions
	public int rowCount;
	public int columnCount;

	// grid origins 
	public float xOrigin;
	public float yOrigin;

	// store all instantiated spaces
	private List<GameObject> grid;

	/*
	// player sides with entry points for each player
	private int numberOfPlayers;
	public GameObject entrySpace;
	*/


	void Start () {

		// empty list for spawned game spaces
		grid = new List<GameObject> ();
		// current locations for placing spawn objects within grid
		float xLoc = xOrigin;
		float yLoc = yOrigin;

		// build out the grid row by row bottom to top
		for (int i = 0; i < rowCount; i++) {
			// reset column origin each row
			xLoc = xOrigin;
			for (int j = 0; j < columnCount; j++) {
				// spawn one game space at this x,y and parent to grid
				GameObject space = Instantiate (moveSpace, new Vector3 (xLoc, yLoc, 0f), Quaternion.identity) as GameObject;
				space.transform.parent = this.transform;
				// store column and row as within game space as identifier
				space.GetComponent<GameSpace> ().rowIndex = i;
				space.GetComponent<GameSpace> ().columnIndex = j;
				// add game space to list
				grid.Add (space);
				// move to new grid locations
				xLoc++;
			}
			yLoc ++;
		}
	}
	

	void Update () {
	
	}
}
