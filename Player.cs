using UnityEngine;
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

	// raycast against gameboard
	private RaycastHit hit;


	void Update () {
		
		// raycast for mouse clicks on board space
		if (Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {
				// try to set the space to current player color
				if (hit.collider.gameObject.GetComponent<GameSpace>().possibleChangers.Contains(this.index)) {
					// activate this space
					hit.collider.GetComponent<GameSpace> ().targetColor = tokenColor;

					// get the adjacent spaces to recognize themselves as possible player targets
					// iterate through them, checking GameSpace scripts for col and row values?

					// send message that player turn is over
					GameController.endTurn = true;
				}
			}
		}

	}

}
