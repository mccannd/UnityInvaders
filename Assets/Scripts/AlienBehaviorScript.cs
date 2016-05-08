using UnityEngine;
using System.Collections;

public class AlienBehaviorScript : MonoBehaviour {

	public static float Velocity = 0.75f;
	public static float rowHeight = 1.0f;

	public int points = 0; // the points awarded for destroying this alien

	private static bool descending = false; //tells all aliens to move down the screen
	private float prevRow; // the Z location of the previous row
	private static bool movingLeft = false; // tells all aliens to move left or right

	public GameObject gameState; // the global object determining the game state
	public GameObject enemyProjectile; // the shot fired by this alien
	public GameObject explosion; // on death explosion

	private Rigidbody rb;

	private float nextShot;

	// Use this for initialization
	void Start () {
		nextShot = Time.time + (float) Random.Range (3, 16);
		prevRow = transform.position.z;
		rb = GetComponent<Rigidbody> ();
		GameStateScript.numAliens += 1;
	}
	
	// Update is called once per frame
	void Update () {
		Shoot ();
		if (!descending)
			prevRow = transform.position.z;
	}

	// do movement by physics / rigidbody
	void FixedUpdate() {
		// move the alien down to the next row if it is descending, otherwise horizontally
		if (!descending) {
			if (movingLeft)
				rb.velocity = Velocity * Vector3.left;
			else
				rb.velocity = Velocity  * Vector3.right;

		} else {
			rb.velocity = Velocity * Vector3.back;
			if (transform.position.z < prevRow - rowHeight) {
				descending = false;
			}
		}

	}
		

	void OnTriggerEnter (Collider other) {
		// left wall: descend and go right
		if (other.gameObject.name == "Left_Wall") {
			descending = true;
			movingLeft = false;
		}
		// right wall: descend and go left
		else if (other.gameObject.name == "Right_Wall") {
			descending = true;
			movingLeft = true;
		} else if (other.gameObject.tag == "PlayerProjectile") {
			Die ();
		}

	}

	void Shoot() {
		// enemies should be able to shoot at random times
		if (Time.time > nextShot) {
			GameObject shot = Instantiate (enemyProjectile, 
				transform.position + transform.forward, 
				transform.rotation) as GameObject;
			nextShot = Time.time + Random.Range (5, 14);
		}
	}

	void OnCollisionEnter (Collision col) {
		// player avatar: destroy the player and end the game
		if (col.gameObject.tag == "Player") {Die();}
		else if (col.gameObject.tag == "Wall") {
			Physics.IgnoreCollision (col.collider, GetComponent<Collider> ());
		}
	}

	// called when the ship is hit by a player's shot
	void Die() {
		// update the game score
		// destroy the object
		Velocity *= 1.035f;
		GameObject exp = Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		ScoreBehavior.score += points;
		Destroy(this.gameObject);
		GameStateScript.numAliens -= 1;

	}
}
