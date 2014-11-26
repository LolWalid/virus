using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {
	
	public uint[,] world;

	float delaySpeed;
	float delaySpeedMax = 2.0f;
	
	float delaySafeZone;
	float delaySafeZoneMax = 10f;

	public static bool thereIsASafeZone;
	public GameObject speedBonus;
	public GameObject safeZone;
	public GameObject catBonus;

	public void setWorld(uint[,] value) {
		world = value;
	}

	[SerializeField]
	private bool inGame;
	
	public bool GetInGame() {
		return inGame;
	}
	
	public void setInGame(bool value) {
		inGame = value;
	}

	enum bonusType {
		Speed,
		Cat,
		Shovel,
		Portal,
		Switch,
		SafeZone
	}

	void generateSafeZone() {
		if (!thereIsASafeZone) {
			GameObject.Instantiate (safeZone, transform.position + new Vector3 (8, 12, 1), transform.rotation);
			thereIsASafeZone = true;
		}
	}

	void generateBonus() {
		int i = Random.Range (1, 21);
		int j = Random.Range (1, 21);
		if (world[i,j] != 0 && world[i,j] != 2 && i != 8 && j != 12) {
			if (Random.Range (0,43) == 0) {
				GameObject.Instantiate(catBonus, transform.position + new Vector3(i,j,1), transform.rotation);
			}
			else {
				GameObject.Instantiate(speedBonus, transform.position + new Vector3(i, j, 1), transform.rotation);
			}
		}
	}

	// Use this for initialization
	void Start () {

		delaySpeed = 0;
		delaySafeZone = 0;
		thereIsASafeZone = false;
		inGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (delaySpeed > delaySpeedMax && inGame) {
			generateBonus();
			delaySpeed = 0.0f;
		}

		if (delaySafeZone > delaySafeZoneMax && inGame) {
			generateSafeZone();
			delaySafeZone = 0.0f;
		}
		delaySafeZone += Time.deltaTime;
		delaySpeed += Time.deltaTime;
	}
}
