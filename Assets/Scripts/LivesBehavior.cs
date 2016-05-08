using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesBehavior : MonoBehaviour {

	private int l;
	private Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		l = GameStateScript.playerLives;
		t.text = "Lives: " + l;
	}
}
