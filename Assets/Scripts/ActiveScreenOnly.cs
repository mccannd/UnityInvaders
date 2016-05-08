using UnityEngine;
using System.Collections;

public class ActiveScreenOnly : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.SetActive (GameStateScript.active);
	}
}
