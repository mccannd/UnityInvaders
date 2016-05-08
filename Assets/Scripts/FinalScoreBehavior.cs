using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreBehavior : MonoBehaviour {

	Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		t.text = "Your final score was: " + ScoreBehavior.score;
		if (ScoreBehavior.score > HiScoreBehavior.Score) {
			HiScoreBehavior.Score = ScoreBehavior.score;
			t.text += ", \n a new High Score!";
		}
	}
}
