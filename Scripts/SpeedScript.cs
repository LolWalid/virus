using UnityEngine;
using System.Collections;

public class SpeedScript : MonoBehaviour {
	float scale;

	void OnTriggerEnter2D(Collider2D player) {
		InputController script = (InputController) player.GetComponent("InputController");

		scale = script.GetScale();
		if (scale < 0.15f) {
			script.setScale(scale + 0.02f);
		}

		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
	}
	
	void Update() {

	}
}
