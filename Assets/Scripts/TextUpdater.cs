using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour {

	Text textBox;

	void Start () {
		textBox = GetComponent<Text> ();
	}

	public void UpdateText (string message) {
		textBox.text = message;
	}
}
