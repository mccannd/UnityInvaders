using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	private float shotInterval = 0.1f; // time between shots in milliseconds
	private float nextShot = 0.0f;

	public GameObject playerProjectile;
	public GameObject playerBeam;

	public Transform barrel;
	private float heat = 0.0f;
	public static bool overheating = false;
	public Slider heatSlider;

	public ParticleSystem indicator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	// Shoot the weapon if possible
	// determine if the weapon is overheating and update the UI
	void Update () {
		if (overheating)
			heat -= 0.3f * Time.deltaTime;
		else if (heat > 0)
			heat -= 0.4f * Time.deltaTime;
		Mathf.Clamp (heat, 0, 1);

		if (Input.GetKey (KeyCode.Q) && Time.time > nextShot && !overheating) {
			nextShot = Time.time + shotInterval;
			heat += 0.20f; // increase the weapon heat
			GameObject shot = Instantiate (playerProjectile, 
				                  barrel.position,
				                  barrel.rotation) as GameObject;
		} else if (Input.GetKey (KeyCode.W) && Time.time > nextShot && !overheating && heat < 0.1f) {
			nextShot = Time.time + shotInterval;
			heat += 1.2f; // increase the weapon heat to overloading levels
			GameObject shot = Instantiate (playerBeam,
				                  barrel.position + new Vector3 (0, 0, 10),
				                  barrel.rotation) as GameObject;
		}

		if (heat >= 1)
			overheating = true;
		else if (overheating && heat <= 0)
			overheating = false;

		Mathf.Clamp (heat, 0, 1);
		heatSlider.value = heat;
		ParticleSystem.ColorBySpeedModule col = indicator.colorBySpeed;
		col.enabled = true;
		ParticleSystem.MinMaxGradient grad;


		float green = Mathf.Clamp (1.0f - heat, 0, 1);
		float red = Mathf.Clamp (heat, 0, 1);
		float intensity = Mathf.Sqrt (red * red + green * green);
		red /= intensity;
		green /= intensity;

		if (overheating) {
			grad = new ParticleSystem.MinMaxGradient (new Color (1, 0, 0, 1));
		} else {
			grad = new ParticleSystem.MinMaxGradient (new Color (red, green, 0, 1));
		}

		col.color = grad;
			


	}


}
