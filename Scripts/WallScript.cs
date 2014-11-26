using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	public float scale = 0.05f;
	public GameObject sound;
	static private GameObject realSound;

	void OnTriggerEnter2D (Collider2D collid) {
		if (collid.gameObject.tag == "Player") {
			InputController script2 = (InputController) collid.GetComponent("InputController");
			
			Vector3 v2 = -0.4f * script2.GetSpeed();
			Vector3 v3 = -2.0f * script2.GetSpeed();
			collid.transform.position -= script2.GetSpeed() / Time.deltaTime;

			script2.SetSpeed(v2);

			if (!realSound)
				realSound = (GameObject) Instantiate (sound, new Vector3(0,0,0), transform.rotation);
			realSound.audio.Play();
		}
	}
	void start() {

	}
}
