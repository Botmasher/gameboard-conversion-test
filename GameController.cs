using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// list of players
	[SerializeField]public List<Player> players = new List <Player>();

	// track the current player
	private int currentPlayer;

	// raycast
	private RaycastHit hit;

	// game control flow
	private bool turnOver = false;

	// player attributes
	[System.Serializable]
	public class Player {
		public Color color;
		private int number;
		public int Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}
		public Player (Color playerColor) {
			color = playerColor;
		}
	}


	void Start () {
		// determine initial player
		currentPlayer = 0;
		// assign index to each player in list
		for (int i = 0; i < players.Count; i++) {
			players[i].Number = i+1;
		}
	}

	void Update () {
		// raycast for mouse clicks on board space
		if (!turnOver && Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {
				// set the space to current player color
				hit.collider.GetComponent<GameSpace> ().targetColor = players [currentPlayer].color;
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
