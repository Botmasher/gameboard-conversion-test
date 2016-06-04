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
	public List<int> possiblePlayers = new List<int> ();


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

		// change this space if player can and player does not currently have it
		if (possiblePlayers.Contains(playerIndex) && this.targetColor != newColor) {
			// change color
			targetColor = newColor;
			// notify nearby spaces of impending influence
			this.transform.GetComponentInParent<GameGrid>().AlertNearbySpaces (rowIndex, columnIndex, playerIndex);
			// let caller know of successful change
			return true;
		}
		return false;
	}

}
