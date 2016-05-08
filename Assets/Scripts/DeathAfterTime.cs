using UnityEngine;
using System.Collections;

public class DeathAfterTime : MonoBehaviour {

	public float lifetime = 1.5f;
	private float birthTime;

	// Use this for initialization
	void Start () {
		birthTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > birthTime + lifetime)
			Destroy (this.gameObject);
	}
}
