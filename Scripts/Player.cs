using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player : MonoBehaviour {
	public float speed = 0.4f;
	public PlayerIndex player;
	public float timespan = 0.5f;
	private float time_max = 3.0f;
	private float time = 0f;
	private bool notPlayed;
	private bool dead = false;
	public Sprite[] sprites = new Sprite[4];
	
	public GameObject virusPrefab;
	static private GameObject virusSound;
	public GameObject explosion;

	IEnumerator Vibrate(PlayerIndex player, float speed, float timespan, float delay)
	{
		yield return new WaitForSeconds(delay);
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
		player = (PlayerIndex) id;
		isInfected = true;
		StartCoroutine(Vibrate (player, speed, timespan,delay));
	}
	public void unInfect() {
		isInfected = false;
	}

	void kill(GameObject go ) {
		go.transform.localScale = new Vector3(1f,1f,1f);
		Destroy(go);
	}

	// Use this for initialization
	void Start () {
		if (!virusSound)
			virusSound = (GameObject) Instantiate (virusPrefab, new Vector3(0,0,0), transform.rotation);
	}


	void countDown() {
	}

//	IEnumerator explode ()
//	{
//		GameObject explode;
//		Vector3 position = this.transform.position;
//		SpriteRenderer sr;
//		explode = (GameObject) Instantiate (explosion, position, transform.rotation);
//		sr = (SpriteRenderer) explosion.GetComponent ("SpriteRenderer");
//
//		for (int i = 0; i < 4; i++) {
//			yield return new WaitForSeconds (0.1f);
//			sr.sprite = sprites[i];
//		}
//
//		Destroy (explode.gameObject);
//		GameManager.kill (id);
//	}

	private IEnumerator explode(){
		GameObject explode;
		Vector3 position = this.transform.position;
		SpriteRenderer sr;

		for (int i=0; i < 4; i++) {
			explode = (GameObject) Instantiate (explosion, position, transform.rotation);
			sr = (SpriteRenderer) explosion.GetComponent ("SpriteRenderer");
			sr.sprite = sprites[i];
			yield return new WaitForSeconds(0.1f);
			Destroy(explode);
		}

		yield return new WaitForSeconds(1);
		GameManager.kill (id);
	}

	
	// Update is called once per frame
	void Update () {
		if (isInfected)
		{	
			transform.localScale = new Vector3(0.8f,0.8f,1f);
			time += Time.deltaTime;
		}
		else 
		{
			transform.localScale = new Vector3(0.5f,0.5f,1f);
			time = 0f;
		}

		if (time > time_max)
		{
			dead = true;
			time = 0;
			virusSound.audio.Play();
			StartCoroutine(explode ());
		}

		if (dead)
			transform.localScale = new Vector3(0.0f,0.0f,0f);

		if ((time_max - time) < 5.0f && notPlayed) {
			notPlayed = true;
			countDown();
		}
	}
}
