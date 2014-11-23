using UnityEngine;
using System.Collections;

public class SpeedScript : MonoBehaviour {
	float scale;
	public GameObject speedPrefab;
	static private GameObject speedSound;

	void OnTriggerEnter2D(Collider2D player) {
		InputController script = (InputController) player.GetComponent("InputController");

		scale = script.GetScale();
		print (scale);
		if (scale < 0.10f) {
			script.setScale(scale + 0.015f);
		}
		if (!speedSound)
			speedSound = (GameObject) Instantiate (speedPrefab, new Vector3(0,0,0), transform.rotation);

		speedSound.audio.Play();

		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	void Update() {

	}
}
