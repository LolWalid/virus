using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	public float scale = 0.05f;
	public GameObject sound;
	static private GameObject realSound;

	void OnTriggerEnter2D (Collider2D collid) {
		if (collid.gameObject.tag == "Player") {
			InputController script2 = (InputController) collid.GetComponent("InputController");
			
			Vector3 v2 = -1 * script2.GetSpeed();
			collid.transform.position  += v2;

			script2.SetSpeed(v2);
			if (!realSound)
				realSound = (GameObject) Instantiate (sound, new Vector3(0,0,0), transform.rotation);
			realSound.audio.Play();
		}
	}
	void start() {

	}
}
