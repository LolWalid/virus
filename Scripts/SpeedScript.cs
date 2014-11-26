using UnityEngine;
using System.Collections;

public class SpeedScript : MonoBehaviour {
	float scale;
	public GameObject speedPrefab;
	static private GameObject speedSound;
	
	public Sprite[] sprites = new Sprite[4];
	private SpriteRenderer sr;
	
	private int spriteNum = 3;
	
	private float timeElapsed = 0.0f;
	public float animCoolDown = 0.1f;
	
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
		if (scale < 10.0f) {
			script.setScale(scale + 1.0f);
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
		if (timeElapsed > animCoolDown ) {
			timeElapsed = 0f;
			changeSprite();
		} 
		timeElapsed += Time.deltaTime;
	}
}
