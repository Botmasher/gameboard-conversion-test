﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// player color
	private Color color;
	public Color tokenColor {
		get {
			return color;
		} set {
			color = value;
		}
	}

	// unique identifier
	private int index;
	public int uniqueId {
		get {
			return index;
		} set {
			index = value;
		}
	}

	// raycast
	private RaycastHit hit;


	void Update () {
		// raycast for mouse clicks on board space
		if (!GameController.turnOver[index] && Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {
				// set the space to current player color
				hit.collider.GetComponent<GameSpace> ().targetColor = tokenColor;
				// send message that player turn is over
				GameController.turnOver[index] = true;
			}
		}

		// more robust check for raycast - send mssg to see if it's my turn!

	}

}
