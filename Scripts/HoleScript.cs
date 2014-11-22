using UnityEngine;
using System.Collections;

public class HoleScript : MonoBehaviour {
	public float scale = 0.5f;
	
	void OnTriggerEnter2D (Collider2D collid) {
		InputController script2 = (InputController) collid.GetComponent("InputController");
		script2.scale = 0;
		GameManager.playersAlive -= 1;
		kill(collid.gameObject);
	}

	void kill(GameObject go ) {
		go.transform.localScale = new Vector3(1f,1f,1f);
		Destroy(go);
	}
}
