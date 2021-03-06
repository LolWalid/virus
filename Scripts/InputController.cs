﻿using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	private Vector3 Speed;
	public float threshold;
	public int id;
	private bool isInfected;
	private string axisH;
	private string axisV;
	private int coeffH = 1;
	private int coeffV = 1;
	private float inputH;
	private float inputV;
	
	private SpriteRenderer sr;
	public Sprite back;
	public Sprite normal;
	private Player player; 
	
	
	[SerializeField]
	public float scale;
	
	public float GetScale() {
		return scale;
	}
	
	public void setScale(float new_scale) {
		scale = new_scale;
	}
	
	
	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}
	public Direction prevInput = Direction.Left;
	
	private enum Commands
	{
		Normal,
		FullInvert,
		HtoV,
		Buttons,
		Cross,
		BlockLeft,
		BlockRight,
		BlockUp,
		BlockDown,
		BlockPrevious,
		SwitchJoystick,
		Randomize
	};

	private Commands mode;
	
	public Vector3 GetSpeed ()
	{
		return Speed / scale;
	}
	
	//public void SetSpeed
	public void SetSpeed (Vector3 speed)
	{
		Speed = speed * scale;
	}


	public void initAxis(int i) {
		id = i;
		axisH = "Horizontal" + i;
		axisV = "Vertical" + i;
	}
	
	// Use this for initialization
	void Start ()
	{
		player = (Player)this.GetComponent ("Player");
		Speed = new Vector3 (0, 0, 0);
		scale = 3.0f;
		threshold = 0.9f;
		sr = (SpriteRenderer) GetComponent("SpriteRenderer");
		sr.sprite = normal;
	}
	
	// Update is called once per frame
	void Update ()
	{
		isInfected = player.IsInfected;

		if (Speed != new Vector3(0,1,0) * scale)
			sr.sprite = normal;
		
		mode = Commands.FullInvert;

		inputH = Input.GetAxis (axisH);

		inputV = Input.GetAxis(axisV);
		if ( Mathf.Abs( inputH )== 1 )
			inputV = 0;

		coeffH = 1;
		coeffV = 1;

		if (isInfected) {
			switch (mode) {
			case Commands.FullInvert:
				coeffH = -1;
				coeffV = -1;
				break;
			case Commands.BlockLeft:
				if (inputH < 0)
					inputH = 0;
				break;
			case Commands.BlockRight:
				if (inputH > 0)
					inputH = 0;
				break;
			case Commands.BlockUp:
				if (inputV < 0)
					inputV = 0;
				break;
			case Commands.BlockDown:
				if (inputV > 0)
					inputV = 0;
				break;
			case Commands.HtoV:
				float temp = inputH;
				inputH = inputV;
				inputV = temp;
				break;
			case Commands.SwitchJoystick:
				inputH = Input.GetAxis ("HorizontalAlternate" + id);
				inputV = Input.GetAxis ("VerticalAlternate" + id);
				break;
			case Commands.BlockPrevious:
				switch (prevInput) {
				case Direction.Left:
				case Direction.Right:
					if(Mathf.Abs(inputH) == 1)
						coeffH = 0;
					break;
				case Direction.Up:
				case Direction.Down:
					if(Mathf.Abs(inputV) == 1)
						coeffV = 0;
					break;
				default:
					break;
				}
				break;
			default:
				break;
			}
		}
		if (Mathf.Abs (inputH) == 1 || Mathf.Abs (inputV) == 1) {
			if (Mathf.Abs (Input.GetAxis(axisH)) >= Mathf.Abs (Input.GetAxis(axisV))) {
				if (inputH < 0)
					prevInput = Direction.Left;
				if (inputH > 0)
					prevInput = Direction.Right;
				Speed = new Vector3 (coeffH * inputH, 0, 0) * scale * Time.deltaTime;
			} else {
				if (inputV < 0)
					prevInput = Direction.Down;
				
				if (inputV > 0) {
					prevInput = Direction.Up;
					sr.sprite = back;
				}
				Speed = new Vector3 (0, coeffV * inputV, 0) * scale * Time.deltaTime;
			}
		}
		inputH = 0;
		inputV = 0;

		transform.position += Speed;

		if (transform.position.x > 11.0f)
			transform.position = new Vector3(10.0f, transform.position.y,1.0f);

		if (transform.position.x < -10.0f)
			transform.position = new Vector3(-9.0f, transform.position.y,1.0f);

		if (transform.position.y > 11.0f)
			transform.position = new Vector3(transform.position.x, 10.0f,1.0f);
		
		if (transform.position.y < -10.0f)
			transform.position = new Vector3(transform.position.x, -9.0f,1.0f);

	}
}
