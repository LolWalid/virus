using UnityEngine;
using System.Collections;

public class SafeZoneScript : MonoBehaviour {

	public GameObject safeZonePrefab;
	static private GameObject safeZoneSound;

	void OnTriggerEnter2D(Collider2D player) {
		Player script = (Player) player.GetComponent("Player");
		
		bool wasInfected = script.IsInfected;
		
		script.unInfect();
		
		if (wasInfected) {
			GameManager.infect ();
		}

		if (!safeZoneSound)
			safeZoneSound = (GameObject) Instantiate (safeZonePrefab, new Vector3(0,0,0), transform.rotation);
		
		safeZoneSound.audio.Play();

		BonusManager.thereIsASafeZone = false;
		
		Destroy (gameObject);
	}
	
	// Use this for initialization
	void Start () {
	}
	
	void Update() {
	}
}
