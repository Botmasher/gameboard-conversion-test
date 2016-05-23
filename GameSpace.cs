using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSpace : MonoBehaviour {

	// the color to change to
	public Color targetColor;

	// ids to check row and column location from other scripts
	public int rowIndex;
	public int columnIndex;

	// list of players that can change this square
	private List<int> possiblePlayers = new List<int> ();


	void Start () {
		targetColor = GetComponent<MeshRenderer> ().material.color;
	}

	void Update () {
		// lerp towards new color when target color gets changed
		if (GetComponent<MeshRenderer> ().material.color != targetColor) {
			GetComponent<MeshRenderer> ().material.color = Color.Lerp (GetComponent<MeshRenderer> ().material.color, targetColor, 2f * Time.deltaTime);
		}
	}


	/**
	 * 	Update the space with the player's color if player is allowed to
	 */
	public bool SetColor (int playerIndex, Color newColor) {
		if (possiblePlayers.Contains(playerIndex)) {
			targetColor = newColor;
			return true;
		}
		return false;
	}

	/**
	 * 	Warn adjacent spaces that they can change to player's color
	 */
	void AlertNearbySpaces () {
	}

}
