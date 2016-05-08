using UnityEngine;
using System.Collections;

public class StartScreenOnly : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameStateScript.startScreen) {
				
			this.gameObject.SetActive (false);
		}
	}
}
