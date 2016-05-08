using UnityEngine;
using System.Collections;

public class PlayerLaserScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = 20 * transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != "Player") {
			Destroy(this.gameObject);
		}

		//Debug.Log ("Collision: " + col.gameObject.tag);


	}



	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Wall") {
			Destroy (other.gameObject);

		}
		Destroy (this.gameObject);
	}
}
