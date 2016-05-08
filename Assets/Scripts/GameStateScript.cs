using UnityEngine;
using System.Collections;

public class GameStateScript : MonoBehaviour {

	public GameObject[] enemies = new GameObject[3];
	public GameObject wallblock;
	public GameObject player;

	public static int numAliens = 0;
	public static int playerLives = 3;
	public float frontRowZ = 4.0f; // vertical location of front row

	// Game state booleans (kinda vestigial)
	public static bool active = false;
	public static bool startScreen = true;
	public static bool gameOverScreen = false;

	// Game UI elements
	public GameObject startScreenUI; // initial
	public GameObject activeScreenUI; // gameplay
	public GameObject lossScreenUI; // game over

	private static bool paused = false;

	/*public static bool spawnReady = false;
	private static float nextSpawn = 0;*/

	// Use this for initialization (deprecated)
	void Awake () {
		//SpawnWave ();
		//active = true;
		startScreenUI.SetActive(true);
		activeScreenUI.SetActive (false);
		lossScreenUI.SetActive (false);
		player.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		/*if (spawnReady && Time.time > nextSpawn) {
			// keep the speed from being too quick so waves are manageable
			AlienBehaviorScript.Velocity *= 0.4f;
			SpawnWave ();
			spawnReady = false;
		} else*/ if (active && numAliens <= 0) {
			//spawnReady = true;
			SpawnWave();
			//nextSpawn = Time.time + 1;
		} 

		// end the game if it is ongoing and the player has died
		if (playerLives <= 0 && !gameOverScreen) {
			GameOver ();
		}

		if (Input.GetKey (KeyCode.P)) {
			if (!paused) {
				paused = true;
				Time.timeScale = 0;
			} else {
				paused = false;
				Time.timeScale = 1;
			}
		}
	}
		
	public void BeginGame() {
		// let update begin spawning and keeping track of player 
		active = true;
		// hide the start screen GUI
		startScreen = false;
		startScreenUI.SetActive(false);
		activeScreenUI.SetActive (true);
		lossScreenUI.SetActive (false);
		// activate player character
		player.SetActive (true);
		Transform t = player.transform;
		t.position = new Vector3 (0, t.position.y, t.position.z);
		// make walls for the player to hide behind
		SpawnWalls ();


	}

	public void CompleteReset () {
		// reset alien speed
		AlienBehaviorScript.Velocity = 0.75f;
		playerLives = 3;
		numAliens = 0;

		// clear everything
		DestroyAllObjects ();

		ScoreBehavior.score = 0;

		gameOverScreen = false;
		startScreen = false;
		active = true;
		// reset walls
		SpawnWalls ();

		player.SetActive (true);
		Transform t = player.transform;
		t.position = new Vector3 (0, t.position.y, t.position.z);

		startScreenUI.SetActive(false);
		activeScreenUI.SetActive (true);
		lossScreenUI.SetActive (false);
	}

	public void DestroyAllObjects () {
		// find and destroy all aliens, walls, etc
		string[] tags = {"Alien", "Wall", "WallPrefab"};
		for (int i = 0; i < tags.Length; i++) {
			GameObject[] objs = GameObject.FindGameObjectsWithTag(tags[i]);
			for (int j = 0; j < objs.Length; j++) {
				Destroy (objs [j]);
			}
		}

		/*GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
		for (int i = 0; i < aliens.Length; i++) {
			Destroy (aliens [i]);
		}
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
		for (int i = 0; i < walls.Length; i++) {
			Destroy (walls [i]);
		}

		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
		for (int i = 0; i < walls.Length; i++) {
			Destroy (walls [i]);
		}*/
	}

	public void GameOver() {
		DestroyAllObjects ();
		active = false;
		gameOverScreen = true;

		player.SetActive (false);
		startScreenUI.SetActive(false);
		activeScreenUI.SetActive (false);
		lossScreenUI.SetActive (true);
	}

	public void SpawnWalls() {
		for (int x = -6; x <= 6; x += 4) {
			Vector3 pos = new Vector3 (x, 0, -7.29f);
			GameObject wall = Instantiate (wallblock, pos, new Quaternion ()) as GameObject; 
		}
	}

	// creates a fresh wave of enemies
	public void SpawnWave () {
		
		// spawn the back row

		Quaternion rot = Quaternion.Euler (0, 180, 0);
		for (int i = 0; i < 11; i++) {
			Vector3 pos = new Vector3 (-6.0f + 1.2f * i, 0, frontRowZ + 4);

			GameObject back = Instantiate (enemies [2], pos, rot) as GameObject;
		}
		// spawn the middle rows
		for (int i = 0; i < 11; i++) {
			Vector3 pos = new Vector3 (-6.0f + 1.2f * i, 0, frontRowZ + 3);

			GameObject back = Instantiate (enemies [1], pos, rot) as GameObject;
		}
		for (int i = 0; i < 11; i++) {
			Vector3 pos = new Vector3 (-6.0f + 1.2f * i, 0, frontRowZ + 2);

			GameObject back = Instantiate (enemies [1], pos, rot) as GameObject;
		}
		// spawn the front rows
		for (int i = 0; i < 11; i++) {
			Vector3 pos = new Vector3 (-6.0f + 1.2f * i, 0, frontRowZ + 1);

			GameObject back = Instantiate (enemies [0], pos, rot) as GameObject;
		}

		for (int i = 0; i < 11; i++) {
			Vector3 pos = new Vector3 (-6.0f + 1.2f * i, 0, frontRowZ);

			GameObject back = Instantiate (enemies [0], pos, rot) as GameObject;
		}
	}

	// shuts down the entire game
	public void CloseGame() {
		Application.Quit ();
	}
}
