using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HiScoreBehavior : MonoBehaviour {

	public static int Score = 0;
	private Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		t.text = "High Score: " + Score;
	}
}
