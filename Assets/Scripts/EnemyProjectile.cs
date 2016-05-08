using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = 10 * transform.forward;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Alien")
			Physics.IgnoreCollision (col.collider, GetComponent<Collider> ());
		if (col.gameObject.tag != "Alien") {
			Destroy(this.gameObject);
		}

		//Debug.Log ("Collision: " + col.gameObject.tag);


	}



	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Wall") {
			Destroy (other.gameObject);
			// ensure that this can only destroy a single wall

		}

		if (other.gameObject.tag != "PlayerProjectile") Destroy (this.gameObject);
	}
}
