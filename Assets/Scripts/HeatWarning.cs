using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeatWarning : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		text.text = "Weapon Heat Level";
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerAttack.overheating) {
			text.text = "Overheating!";
		} else text.text = "Weapon Heat Level";
	}
}
