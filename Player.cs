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

}
