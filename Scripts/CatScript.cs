using UnityEngine;
using System.Collections;

public class CatScript : MonoBehaviour {
	
	public Sprite[] sprites = new Sprite[2];
	
	void OnTriggerEnter2D(Collider2D player) {
		InputController script = (InputController) player.GetComponent("InputController");
		
		script.back = sprites [0];
		script.normal = sprites [1];
		
		Destroy (gameObject);
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void Update() {
		
	}
}
