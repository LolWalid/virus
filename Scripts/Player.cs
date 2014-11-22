using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player : MonoBehaviour {
	public float speed = 0.4f;
	private PlayerIndex player;
	public float timespan = 0.5f;

	IEnumerator Vibrate(PlayerIndex player, float speed, float timespan, float delay)
	{
		yield return new WaitForSeconds(delay);
		Debug.Log ("vibrate!");
		GamePad.SetVibration(player, speed, speed);
		yield return new WaitForSeconds(timespan);
		GamePad.SetVibration(player, 0, 0);
	}

	[SerializeField]
	private bool isInfected;
	
	public bool IsInfected {
		get {
			return this.isInfected;
		}
		set {
			this.isInfected = value;
		}
	}

	[SerializeField]
	private int id;
	
	public int  Id {
		get {
			return this.id;
		}
		set {
			this.id = value;
		}
	}


	public void infect(float delay) {
		player = (PlayerIndex) id - 1;
		isInfected = true;
		StartCoroutine(Vibrate (player, speed, timespan,delay));
	}
	public void unInfect() {
		isInfected = false;
	}
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isInfected)
			transform.localScale = new Vector3(0.8f,0.8f,1.0f);
		else 
			transform.localScale = new Vector3(0.5f,0.5f,1.0f);
	}
}
