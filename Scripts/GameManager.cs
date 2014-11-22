using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public Sprite[] Player1 = new Sprite[2];
	public Sprite[] Player2 = new Sprite[2];
	public Sprite[] Player3 = new Sprite[2];
	public Sprite[] Player4 = new Sprite[2];
	
	[SerializeField]
	GameObject playerModel;
	
	GameObject[] players = new GameObject[4];

	
	// Use this for initialization

	void init () {
		InputController script;
		Player playerScript;

		for (int i=0; i < 4; i++) {
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

	void infect(int i) {
		Player playerScript = (Player) players[i].GetComponent("Player");
		playerScript.infect(i+1);

	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			init();
			infect (0);
		}
	}
}
