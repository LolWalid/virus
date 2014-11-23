using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player : MonoBehaviour {
	public float speed = 0.4f;
	private PlayerIndex player;
	public float timespan = 0.5f;
	public float time_max = 15.0f;
	private float time = 0f;
	private bool notPlayed;
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
		player = (PlayerIndex) id - 1;
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

	IEnumerator explode ()
	{
		GameObject explode = new GameObject ();
		Vector3 position = this.transform.position;
		SpriteRenderer sr;
		for (int i = 0; i < 4; i++) {
			explode = (GameObject) Instantiate (explosion, position, transform.rotation);
			sr = (SpriteRenderer)explosion.GetComponent ("SpriteRenderer");
			sr.sprite = sprites[i];
			yield return new WaitForSeconds (0.15f);
			Destroy (explode, 0.15f);
		}
		Destroy (this.gameObject);
		
	}

	// Update is called once per frame
	void Update () {
		if (isInfected)
		{
			time += Time.deltaTime;
		}
		else 
		{
			time = 0f;
		}
		if (time > time_max)
		{
			virusSound.audio.Play();
		//	StartCoroutine(explode ());
			GameManager.kill (id);
		}
		if (time < 5.0f && notPlayed) {
			notPlayed = true;
			countDown();
		}
	}
}
