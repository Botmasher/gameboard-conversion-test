using UnityEngine;
using System.Collections;

public class GameSpace : MonoBehaviour {

	// toggle color lerp from other scripts
	public bool changingColors;

	// the color to change to
	public Color targetColor;

	// ids to check row and column location from other scripts
	public int rowIndex;
	public int columnIndex;


	void Update () {
		// change colors triggered
		if (changingColors) {
			GetComponent<MeshRenderer> ().material.color = Color.Lerp (GetComponent<MeshRenderer>().material.color, targetColor, Time.deltaTime);
		}
	}
}
