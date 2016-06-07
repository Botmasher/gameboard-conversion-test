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

	// whether or not currently taken by a player
	public bool isTaken;

	void Start () {
		targetColor = GetComponent<MeshRenderer> ().material.color;
		isTaken = false;
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
	public bool SetColor (int playerIndex, Color newColor, float conversionChance) {

		// change this space if player can and player does not currently have it
		if (possiblePlayers.Contains(playerIndex) && this.targetColor != newColor) {
			if (Random.value <= conversionChance && !isTaken) {
				// change color
				targetColor = newColor;
				// notify nearby spaces of impending influence
				this.transform.GetComponentInParent<GameGrid> ().AlertNearbySpaces (rowIndex, columnIndex, playerIndex);
				isTaken = true;
			} else if (Random.value <= conversionChance && isTaken) {
				// harder to influence
				// - spawn up to 3 side counter blocks, once per successful influence
				// - once you have 3, you take this spot
				// - if there are other player blocks, take them down a notch instead
			}
			return true;	// let caller know this counts as a turn
		}
		return false; 	// this didn't count as a turn - space not choosable for this player
	}

}
