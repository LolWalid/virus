﻿using UnityEngine;
using System.Collections;

public class Physics : MonoBehaviour {

	private float coolDown;

	void OnTriggerEnter2D (Collider2D collid) {

		InputController script = (InputController) this.GetComponent("InputController");
		Player player = (Player) this.GetComponent("Player");

		if (collid.gameObject.tag == "Player") {
			InputController script2 = (InputController) collid.GetComponent("InputController");
			Player player2 = (Player) collid.GetComponent("Player");

			Vector3 v = -0.8f *  script.GetSpeed() ;
			this.transform.position  += v;
			script.SetSpeed(v);

			Vector3 v2 = -0.8f * script2.GetSpeed();
			collid.transform.position  += v2;
			script2.SetSpeed(v2);

			if ((player.IsInfected || player2.IsInfected) && coolDown < 0) {

				coolDown = 1f;
				if (player.IsInfected){
					player2.infect(coolDown);
					player.unInfect();
				}
				else {
					player.infect(coolDown);
					player2.unInfect();
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		coolDown -= Time.deltaTime ;
	}
}
