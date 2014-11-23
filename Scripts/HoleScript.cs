using UnityEngine;
using System.Collections;

public class HoleScript : MonoBehaviour {
	public float scale = 0.5f;

	public GameObject holePrefab;
	static private GameObject holeSound;
	
	void OnTriggerEnter2D (Collider2D collid) {
		InputController inputs = (InputController) collid.GetComponent("InputController");
		inputs.scale = 0;
		GameManager.kill(inputs.id);

		if (!holeSound)
			holeSound = (GameObject) Instantiate (holePrefab, new Vector3(0,0,0), transform.rotation);
		
		holeSound.audio.Play();
	}
	
}
