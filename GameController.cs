using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	// list of colors used to create players
	public List<Color> colors = new List<Color>();
	// list to store created players
	private List<GameObject> players = new List<GameObject>();

	// track the current player
	private int currentPlayer;

	// game control flow
	public static bool endTurn;


	void Start () {

		// create players within player list based on how many colors were chosen
		for (int i = 0; i < colors.Count; i++) {
			// add and name player
			players.Add (new GameObject (System.String.Format ("Player {0}", i)));
			// set player's script behavior
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().uniqueId = i;
			players[i].GetComponent<Player>().tokenColor = colors[i];
			TogglePlayer (i);		// start with all players off
		}

		// the initial player's turn
		currentPlayer = 0;
		endTurn = false;
		TogglePlayer (0);			// turn the first player on

	}


	void Update () {

		// turn over triggered by individual players
		if (endTurn) {
			StartCoroutine (NextPlayer ());
			endTurn = false;
		}
	}


	/**
	 * 	Turn a player's script on or off
	 * 		- control "my turn" options like raycasting
	 * 		- eliminate turn control flow from within player scripts
	 */
	void TogglePlayer (int index) {
		players [index].GetComponent<Player>().enabled = !(players[index].GetComponent<Player>().enabled);
	}


	/**
	 * 	Thread for turn over actions
	 */
	IEnumerator NextPlayer () {

		// end the last player's turn
		TogglePlayer (currentPlayer);

		// index to the next player
		currentPlayer++;

		// reset to initial player if index is the final player
		if (currentPlayer >= players.Count) {
			currentPlayer = 0;
		}

		// wait
		yield return new WaitForSeconds (1f);

		// start the next player's turn
		TogglePlayer (currentPlayer);

	}

}