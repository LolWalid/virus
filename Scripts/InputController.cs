using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	private Vector3 Speed;
	public float threshold;

	private SpriteRenderer sr;
	public Sprite haut;
	public Sprite normal;

	private int id;
	private string axisH;
	private string axisV;

	[SerializeField]
	public float scale;
	
	public float GetScale() {
		return scale;
	}
	
	public void setScale(float new_scale) {
		scale = new_scale;
	}

	public Vector3 GetSpeed() {
		return Speed / scale;
	}

	public void SetSpeed(Vector3 speed) {
		Speed = speed * scale;
	}

	//public void SetSpeed

	// Use this for initialization
	void Start () {
		Player player = (Player) this.GetComponent("Player");
		id = player.Id;
		Speed=new Vector3(0,0,0);
		scale = 0.05f;
		threshold = 0.9f;
		axisH = "Horizontal" + id;
		axisV = "Vertical" + id;
		sr = (SpriteRenderer) GetComponent("SpriteRenderer");
		sr.sprite = normal;
	}

	// Update is called once per frame
	void Update () {
		if (Speed != new Vector3(0,1,0) * scale)
			sr.sprite = normal;

		if (Mathf.Abs(Input.GetAxis(axisH)) == 1 || Mathf.Abs(Input.GetAxis(axisV)) == 1)
		{
			if (Mathf.Abs(Input.GetAxis(axisH)) > Mathf.Abs(Input.GetAxis(axisV)))
				Speed = new Vector3(Input.GetAxis(axisH),0,0)  * scale;
			else {
				if (Input.GetAxis(axisV) > 0)
					sr.sprite = haut;
			Speed = new Vector3(0,Input.GetAxis(axisV),0)  * scale;
			}
		}
		transform.position += Speed;
	}
}
