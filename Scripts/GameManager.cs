using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public Sprite[] Player1 = new Sprite[2];
	public Sprite[] Player2 = new Sprite[2];
	public Sprite[] Player3 = new Sprite[2];
	public Sprite[] Player4 = new Sprite[2];

	public static int playersAlive = 0;
	bool textStart = true;
	bool textWin = false;
	int winner = 0;

	public GameObject musicSound;
	
	[SerializeField]
	GameObject playerModel;
	
	static public GameObject[] players = new GameObject[4];

	void init () {
		((BonusManager) this.GetComponent("BonusManager")).setInGame(true);

		musicSound.audio.Play();
		playersAlive = 4;
		textStart = false;
		textWin = false;

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
			playerScript.Id = i+1;
			script.initAxis(i+1);
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
	if (players[i-1] != null) {
			playersAlive-=1;
			Player playerScript = (Player) players[i-1].GetComponent("Player");
			bool wasInfected = playerScript.IsInfected;
			Destroy(players[i-1]);

			if (wasInfected) 
				infect();
		}
	}

	void OnGUI () {
		if (textStart) {
			GUI.Label (new Rect (Screen.width /2,Screen.height /2,Screen.width,Screen.height),"Press START !!");
		}
		if (textWin) {
			GUI.Label (new Rect (Screen.width /2,Screen.height /2,Screen.width,Screen.height),"PLAYER "+ winner +" WINS !");
			musicSound.audio.Stop();
		}
	}


	void Start () {
		musicSound.audio.loop = true;
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
		if (Input.GetButtonDown ("Start") && playersAlive <= 1) {
			init();
			infect ();
		}

		if (playersAlive == 1) {
			for (int i=0; i<players.Length; i++) {
				if (players[i] != null) {
					winner = i;
					textWin = true;
				}
			}
		}
	}
}
