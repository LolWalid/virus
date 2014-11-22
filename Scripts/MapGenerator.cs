using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	
	enum TileType {
		Floor,
		Wall,
		Hole
	};
	
	int[,] tileMap = new int[22,22] { 
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 1, 0, 1, 2, 2, 2, 1, 1, 1, 0},
		{0, 1, 1, 1, 2, 2, 2, 2, 2, 2, 0, 1, 1, 0, 1, 1, 1, 1, 2, 1, 1, 0},
		{0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 2, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 2, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
		{0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0},
		{0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 2, 0, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 2, 2, 0, 0, 1, 1, 1, 1, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 0, 2, 2, 2, 2, 0, 1, 0, 0, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
		{0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 0},
		{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
	};

	[SerializeField]
	GameObject floorSprite;

	[SerializeField]
	GameObject wallSprite;

	[SerializeField]
	GameObject holeSprite;

	void buildMap () {
		for (int i=0; i<22; i++) {
			for (int j=0; j<22; j++) {
				if (tileMap[i,j] == 0) {
					GameObject.Instantiate(wallSprite, transform.position + new Vector3 (i, j, 1), transform.rotation);
				}

				if (tileMap[i,j] == 1) {
					GameObject.Instantiate(floorSprite, transform.position + new Vector3 (i, j, 1), transform.rotation);
				}

				if (tileMap[i,j] == 2) {
					GameObject.Instantiate(holeSprite, transform.position + new Vector3 (i, j, 1), transform.rotation);
				}
			}
		}
	}

	void Start () {
		buildMap ();
	}

	void Update () {

	}

}
