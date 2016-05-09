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

	// raycast
	private RaycastHit hit;

	// game control flow
	private bool turnOver = false;


	void Start () {

		// tag initial player
		currentPlayer = 0;

		// create players within player list based on how many colors were chosen
		for (int i = 0; i < colors.Count; i++) {
			// add and name player
			players.Add (new GameObject (System.String.Format ("Player {0}", i)));
			// set player's script behavior
			players[i].AddComponent<Player>();
			players[i].GetComponent<Player>().tokenColor = colors[i];
		}

	}


	void Update () {
		
		// raycast for mouse clicks on board space
		if (!turnOver && Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {
				// set the space to current player color
				hit.collider.GetComponent<GameSpace> ().targetColor = players [currentPlayer].GetComponent<Player>().tokenColor;
				// give the next player a turn
				StartCoroutine ("EndTurn");
			}
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

		// skip any turn instructions
		turnOver = true;

		// wait
		yield return new WaitForSeconds (2f);

		// update players
		NextPlayer();

		// update UI

		// start accessing next turn logic
		turnOver = false;
		yield return null;
	}

}
