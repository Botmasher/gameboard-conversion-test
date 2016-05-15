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
	public static bool endTurn = false;


	void Start () {

		// create players within player list based on how many colors were chosen
		for (int i = 0; i < colors.Count; i++) {
			// add and name player
			players.Add (new GameObject (System.String.Format ("Player {0}", i)));
			// set player's script behavior
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().uniqueId = i;
			players[i].GetComponent<Player>().tokenColor = colors[i];
			players[i].GetComponent<Player>().turnOver = true;
		}

		// start the initial player's turn
		players[0].GetComponent<Player>().turnOver = false;

	}


	void Update () {

		// turn over triggered by individual players
		if (endTurn) {
			NextPlayer ();
			endTurn = false;
		}
	}


	/**
	 * 	Cycle to the next player
	 */
	void NextPlayer () {

		// end the last player's turn
		players [currentPlayer].GetComponent<Player>().turnOver = true;

		// figure out who is the next player
		try { currentPlayer++; }
		catch { currentPlayer = 0; }

		// reset from last player to initial player
		if (currentPlayer >= players.Count) {
			currentPlayer = 0;
		}

		StartCoroutine (PauseEndTurn (2f));

		// start the next player's turn
		players [currentPlayer].GetComponent<Player>().turnOver = false;

	}


	/**
	 *	Thread for turn over actions
	 */
	IEnumerator PauseEndTurn (float seconds) {

		// wait
		yield return new WaitForSeconds (seconds);

		// update UI

//		// start accessing next turn logic
//		// reset turn over toggles for new turn
//		for (int i = 0; i < turnsOver.Count; i++) {
//			turnsOver [i] = false;
//		}

		yield return null;
	}

}
