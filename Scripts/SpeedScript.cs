using UnityEngine;
using System.Collections;

public class SpeedScript : MonoBehaviour {
	float scale;
	public GameObject speedPrefab;
	static private GameObject speedSound;
	
	public Sprite[] sprites = new Sprite[4];
	private SpriteRenderer sr;
	
	private int spriteNum = 3;
	
	private float nextAnim = 0.0f;
	public float elapsed = 0.1f;
	
	void changeSprite(){
		sr = (SpriteRenderer)GetComponent ("SpriteRenderer");
		sr.sprite = sprites [spriteNum];
		spriteNum--;
		if (spriteNum < 0)
			spriteNum = 3;
	}
	
	void OnTriggerEnter2D(Collider2D player) {
		InputController script = (InputController) player.GetComponent("InputController");
		
		scale = script.GetScale();
		if (scale < 0.15f) {
			script.setScale(scale + 0.02f);
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
		if (Time.time > nextAnim ) {
			nextAnim += elapsed; 
			changeSprite();
		} 
	}
}
