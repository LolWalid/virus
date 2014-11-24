using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class GameManager : MonoBehaviour {
	
	public Sprite[] Player1 = new Sprite[2];
	public Sprite[] Player2 = new Sprite[2];
	public Sprite[] Player3 = new Sprite[2];
	public Sprite[] Player4 = new Sprite[2];

	public Sprite[] countdownSprites = new Sprite[5];
	private SpriteRenderer sr;

	[SerializeField]
	GameObject countdownModel;

	GameObject[] numbers = new GameObject[3];

	public static int playersAlive = 0;
	public GameObject prefabStart;
	private GameObject textStart;

	bool textWin = false;
	int winner = 0;

	public GameObject musicSound;
	public GameObject menuSound;
	public GameObject startSound;

	[SerializeField]
	GameObject playerModel;
	
	static public GameObject[] players = new GameObject[4];


	private IEnumerator countdown(){
		startSound.audio.Play();
		for (int i=0; i < 3; i++) {
			numbers [i] = (GameObject)Instantiate (countdownModel, new Vector3 (0, 0, 0), transform.rotation);
			sr = (SpriteRenderer) numbers[i].GetComponent("SpriteRenderer");
			sr.sprite = countdownSprites[i];
			yield return new WaitForSeconds(1);
			Destroy(numbers[i]);
		}
	}

	private IEnumerator countdownFinal(){
		startSound.audio.Play();
		for (int i=0; i < 5; i++) {
			numbers [i] = (GameObject)Instantiate (countdownModel, new Vector3 (0, 0, 0), transform.rotation);
			sr = (SpriteRenderer) numbers[i].GetComponent("SpriteRenderer");
			sr.sprite = countdownSprites[i];
			yield return new WaitForSeconds(1);
			Destroy(numbers[i]);
		}
	}

	IEnumerator firstInfect(){
		yield return new WaitForSeconds (2);
		infect();
	}

	IEnumerator init () {


		yield return new WaitForSeconds(3);

		((BonusManager) this.GetComponent("BonusManager")).setInGame(true);

		musicSound.audio.Play();

		InputController script;
		Player playerScript;

		for (int i=0; i < 4; i++) {
			if (players[i] != null)
				Destroy(players[i]);

			players[i] = (GameObject) Instantiate (playerModel, new Vector3(i-1,i+1,0), transform.rotation);
			
			script = (InputController) players[i].GetComponent("InputController");
			playerScript = (Player) players[i].GetComponent("Player");
			if (i == 0) {
				script.back = Player1[0];
				script.normal = Player1[1];
			}
			if (i == 1) {
				script.back = Player2[0];
				script.normal = Player2[1];
			}
			if (i == 2) {
				script.back = Player3[0];
				script.normal = Player3[1];
			}
			if (i == 3) {
				script.back = Player4[0];
				script.normal = Player4[1];
			}
			playerScript.Id = i;
			script.initAxis(i);
		}
	}

	public static void infect() {
		int i;
		do {
			i = Random.Range (0,4);
		} while (players[i] == null);

		Player playerScript = (Player) players[i].GetComponent("Player");
		playerScript.infect(1.0f);
	}

	public static void kill(int i) {
	if (players[i] != null) {
			playersAlive-=1;
			Player playerScript = (Player) players[i].GetComponent("Player");
			bool wasInfected = playerScript.IsInfected;
			Destroy(players[i]);

			if (wasInfected && playersAlive > 1) 
				infectAfterKill(i);
		}
	}

	static void infectAfterKill(int id) {
		int i;
		do {
			i = Random.Range (0,4);
		} while (id == i || players[i] == null);

		//print ("Le joueur " + id + " est mort infecté.");
		//print ("Le joueur " + i + " a été infecté.");
		Player playerScript = (Player) players[i].GetComponent("Player");
		playerScript.infect(1.0f);
	}

	void OnGUI () {
		Rect rectangle = new Rect (Screen.width / 2 - 150, Screen.height / 2 - 50, Screen.width, Screen.height);
		if (textWin) {
			GUIStyle font = new GUIStyle();
			font.fontSize = 40;
			font.normal.textColor = Color.white;
			GUI.Label (rectangle,"PLAYER "+ winner +" WINS !", font);
			musicSound.audio.Stop();
		}
	}


	void Start () {
		musicSound.audio.loop = true;
		textStart = (GameObject) Instantiate (prefabStart, new Vector3(0,0,1), transform.rotation);
	}

	bool check() {
		for (int i=0;i<players.Length;i++) {
			if (players[i] != null) {
				Player playerScript = (Player) players[i].GetComponent("Player");
				if (playerScript.IsInfected) 
					return false;
			}
		}
		return true;
	}	


	// Update is called once per frame
	void Update () {
		if (playersAlive == 0 && !menuSound.audio.isPlaying)
			menuSound.audio.Play();

		if (Input.GetButtonDown ("Start") && playersAlive <= 1) {
			menuSound.audio.Stop ();
			playersAlive = 4;
			textWin = false;
			Destroy(textStart.gameObject);
			StartCoroutine (countdown());
			StartCoroutine (init());
			StartCoroutine (firstInfect ());
		}

		if (playersAlive == 1 && !textStart) {
			textStart = (GameObject) Instantiate (prefabStart, new Vector3(0,0,1), transform.rotation);
			playersAlive = 0;
			for (int i=0; i<players.Length; i++) {
				if (players[i] != null) {
					winner = i + 1;
					textWin = true;
				}
			}
		}
	}
}
