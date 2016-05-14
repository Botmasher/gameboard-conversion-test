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
	public static List<bool> turnsOver = new List <bool> ();
	public static bool endTurn = false;


	void Start () {

		// tag initial player (NextPlayer will add +1 to find zeroth player)
		currentPlayer = -1;

		// create players within player list based on how many colors were chosen
		for (int i = 0; i < colors.Count; i++) {
			// add and name player
			players.Add (new GameObject (System.String.Format ("Player {0}", i)));
			// set player's script behavior
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().uniqueId = i;
			players[i].GetComponent<Player>().tokenColor = colors[i];
			// add player's turnover toggle to list of turnover toggles
			turnsOver.Add (players[i].GetComponent<Player>().turnOver);
		}

		// redo turnOver list to check whose turn it is currently
		NextPlayer();

	}


	void Update () {
		
		// check toggles to confirm all players have finished
		if (!turnsOver.Contains(false)) {
			StartCoroutine ("EndRound");
		}

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
		// set the last player's turn state
		turnsOver [currentPlayer] = true;

		// figure out who is the next player
		currentPlayer ++;
		if (currentPlayer >= players.Count) {
			currentPlayer = 0;
		}

		// set the next player's turn state
		turnsOver [currentPlayer] = false;
	}


	/**
	 *	Thread for round over actions
	 */
	IEnumerator EndRound () {

		// wait
		yield return new WaitForSeconds (2f);

		// update players
		NextPlayer();

		// update UI

//		// start accessing next turn logic
//		// reset turn over toggles for new turn
//		for (int i = 0; i < turnsOver.Count; i++) {
//			turnsOver [i] = false;
//		}

		yield return null;
	}

}
