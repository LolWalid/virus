using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {
	public float scale = 0.1f;

	void OnTriggerEnter2D (Collider2D collid) {
		InputController script2 = (InputController) collid.GetComponent("InputController");
		
		Vector3 v2 = -1 * script2.GetSpeed();
		collid.transform.position  += v2 * scale;
		script2.SetSpeed(v2);

	}
}
