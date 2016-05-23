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
				// end the turn if you can set the space to this player's color
				GameController.endTurn = hit.collider.gameObject.GetComponent<GameSpace>().SetColor(index, tokenColor) ? true : false;
			}
		}

	}

}
