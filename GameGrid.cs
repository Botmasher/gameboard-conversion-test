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

	// store all instantiated spaces in a matrix
	private List<List<GameObject>> grid;


	void Start () {
		
		// empty grid for storing spawned game spaces
		grid = new List<List<GameObject>> ();
		// current locations for placing spawn objects within grid
		float xLoc = xOrigin;
		float yLoc = yOrigin;

		// build out the grid row by row bottom to top
		for (int i = 0; i < rowCount; i++) {
			// reset column origin each row
			xLoc = xOrigin;
			// create new nested list - will hold row of actual gameobjects
			List<GameObject> rowList = new List<GameObject>();
			for (int j = 0; j < columnCount; j++) {
				// spawn one game space at this x,y and parent to grid
				GameObject space = Instantiate (moveSpace, new Vector3 (xLoc, yLoc, 0f), Quaternion.identity) as GameObject;
				space.transform.parent = this.transform;
				// store column and row as within game space as identifier
				space.GetComponent<GameSpace> ().rowIndex = i;
				space.GetComponent<GameSpace> ().columnIndex = j;
				// add game space to row list
				rowList.Add (space);
				// move to new grid locations
				xLoc++;
			}
			// add the filled nested list to the main grid list
			grid.Add (rowList);
			yLoc ++;
		}


		// choose possible starting spaces for players 1 and 2 (top and bottom rows)
		for (int i=0; i < grid[0].Count; i++) {
			// bottom row
			grid [0][i].GetComponent<GameSpace>().possiblePlayers.Add (0);
			// top row
			grid [grid.Count-1][i].GetComponent<GameSpace>().possiblePlayers.Add (1);
		}

	}


	/**
	 * 	Warn adjacent spaces that they can change to player's color.
	 * 	Called within a GameSpace script e.g. when that space converted to a new color.
	 * 
	 *  Motivation: other space can now check itself independently on player click
	 * 
	 *  row 		: the grid row location of the alerting space
	 * 	col 		: the grid column location of the alerting space
	 *  playerIndex : player's ID - decides direction to notify
	 */
	public void AlertNearbySpaces (int row, int col, int playerIndex) {

		// figure out which row, col the next space is in grid matrix

		if (row < grid.Count - 1 && playerIndex == 0) {

			GameSpace nextSpace = grid [row + 1] [col].GetComponent<GameSpace> ();

			// tell next above space to expect first player
			if (!nextSpace.possiblePlayers.Contains (playerIndex)) {
				nextSpace.possiblePlayers.Add (playerIndex);
			}
		
		} else if (row > 0 && playerIndex == 1) {

			GameSpace nextSpace = grid [row - 1] [col].GetComponent<GameSpace> ();

			// tell next space below this to expect second player
			if (!nextSpace.possiblePlayers.Contains (playerIndex)) {
				nextSpace.possiblePlayers.Add (playerIndex);
			}
		
		}

	}

}
