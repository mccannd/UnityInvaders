using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float Velocity = 5;
	private Rigidbody r;

	public int lives = 3;


	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Vector3 direc = new Vector3(Input.GetAxis("Horizontal"), 0, 0);	
		r.velocity = Velocity * direc;

		float x = Mathf.Clamp (transform.position.x, -9.5f, 9.5f);
		transform.position = new Vector3 (x, transform.position.y, transform.position.z);

	}

	void OnCollisionEnter(Collision other) {
		/*Debug.Log ("KABOOM");
		lives = 0;
		Die ();*/
		GameStateScript.playerLives = 0;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "EnemyProjectile") {
			/*lives--;
			if (lives <= 0)
				Die ();*/
			GameStateScript.playerLives -= 1;
		}
	}

	void Die() {
		// unparent the camera
		Camera c = GetComponentInChildren<Camera>();
		c.transform.parent = null;

		Destroy (this.gameObject);
	}
}
