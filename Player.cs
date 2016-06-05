using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// player color
	public Color tokenColor;

	// unique identifier
	private int index;
	public int uniqueId {
		get {
			return index;
		} set {
			index = value;
		}
	}

	// likelihood to influence a gamespace
	public float conversionChance = 0.5f;

	// raycast against gamegrid
	private RaycastHit hit;


	void Update () {
		
		// raycast for mouse clicks on board space
		if (Input.GetMouseButton (0)) {
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit);

			// check that hit a gamespace
			if (hit.collider != null && hit.collider.transform.parent.tag == "Gameboard") {

				// let game controller know to end the turn if you can interact with the space (roll to change its color)
				GameController.endTurn = hit.collider.gameObject.GetComponent<GameSpace>().SetColor(index, tokenColor, conversionChance) ? true : false;

			}
		}

	}


}
