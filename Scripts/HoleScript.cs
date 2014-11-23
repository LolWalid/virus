using UnityEngine;
using System.Collections;

public class HoleScript : MonoBehaviour {
	public float scale = 0.5f;
	
	void OnTriggerEnter2D (Collider2D collid) {
		InputController inputs = (InputController) collid.GetComponent("InputController");
		inputs.scale = 0;
		GameManager.kill(inputs.id);
	}
	
}
