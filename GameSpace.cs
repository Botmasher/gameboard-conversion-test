using UnityEngine;
using System.Collections;

public class GameSpace : MonoBehaviour {

	public bool changingColors;
	public Color targetColor;


	void Update () {
		// change colors triggered
		if (changingColors) {
			GetComponent<MeshRenderer> ().material.color = Color.Lerp (GetComponent<MeshRenderer>().material.color, targetColor, Time.deltaTime);
		}
	}
}
