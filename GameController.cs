using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// list of colors used to create players
	public List<Color> colors = new List<Color>();
	// list to store created players
	private List<Player> players = new List<Player>();

	// track the current player
	private int currentPlayer;

	// raycast
	private RaycastHit hit;

	// game control flow
	private bool turnOver = false;

	// Player and player attributes
	//[System.Serializable]
	public class Player {
		// player properties: color
		private Color color;
		public Color tokenColor {
			get {
				return color;
			} set {
				color = value;
			}
		}
		// player properties: number (id)
		private int number;
		public int Number {
			get {
				return number;
			} set {
				number = value;
			}
		}
		public Player (int playerID, Color playerColor) {
			tokenColor = playerColor;
			Number = playerID;
		}
	}


	void Start () {
		// determine initial player
		currentPlayer = 0;
		// create players based on selected colors and add them to players list
		for (int i = 0; i < colors.Count; i++) {
			players.Add (new Player(i+1, colors[i]));
			/*  ?? does Player need an id - or just use list index ??  */
		}
	}

	void Update () {
		// raycast for mouse clicks on board space
		if (!turnOver && Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {
				// set the space to current player color
				hit.collider.GetComponent<GameSpace> ().targetColor = players [currentPlayer].tokenColor;
				// end the turn
				turnOver = true;
				StartCoroutine ("EndTurn");
			}
		}
	}


	IEnumerator EndTurn () {

		yield return new WaitForSeconds (2f);

		// cycle to next player
		currentPlayer ++;
		if (currentPlayer >= players.Count) {
			currentPlayer = 0;
		}
		// UI display new player and turn instructions

		turnOver = false;

		yield return null;
	}

}
