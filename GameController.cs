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
	public static List<bool> turnOver = new List <bool> ();


	void Start () {

		// tag initial player
		currentPlayer = 0;

		// create players within player list based on how many colors were chosen
		for (int i = 0; i < colors.Count; i++) {
			// add and name player
			players.Add (new GameObject (System.String.Format ("Player {0}", i)));
			// set player's script behavior
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().uniqueId = i;
			players[i].GetComponent<Player>().tokenColor = colors[i];
			// add player's turnover toggle to list of turnover toggles
			turnOver.Add (false);
		}

	}


	void Update () {
		
		// check toggles to confirm all players have finished
		if (!turnOver.Contains(false)) {
			// end this turn
			StartCoroutine ("EndTurn");
		}

	}


	/**
	 * 	Cycle to the next player
	 */
	void NextPlayer () {
		currentPlayer ++;
		if (currentPlayer >= players.Count) {
			currentPlayer = 0;
		}
	}


	/**
	 *	Thread for round over actions
	 */
	IEnumerator EndTurn () {

		// wait
		yield return new WaitForSeconds (2f);

		// update players
		NextPlayer();

		// update UI

		// start accessing next turn logic
		// reset turn over toggles for new turn
		for (int i = 0; i < turnOver.Count; i++) {
			turnOver [i] = false;
		}

		yield return null;
	}

}
