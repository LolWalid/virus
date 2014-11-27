using UnityEngine;
using System.Collections;
using System; // Included in order to get the unix timestamp (number of seconds since 1970-01-01)..

public class MapGenerator : MonoBehaviour {


	// Parameters you can play with.
	private const uint worldsSize = 22;
	private const uint numWallBlocksMin = 20, numWallBlocksMax = 25;
	private const uint numHolesMin = 3, numHolesMax = 8;
	private const uint sizeWallBlockMin = 1, sizeWallBlockMax = 5;
	private const uint sizeHoleMin = 1, sizeHoleMax = 3;
	
	// Tiles definition, and the actual world.
	private const uint wall = 0,grass = 1, hole = 2 ;
	private uint[,] world;


	public uint[,] getWorld() {
		return world;
	}

	void GenerateWorld()
	{
		// Allocate the world.
		world = new uint[worldsSize, worldsSize];
		
		// Feed the random generator with a seed.
		UnityEngine.Random.seed = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds; // Unix timestamp.
		
		// Number of holes, number of grouped walls.
		uint numWallBlocks = (uint) UnityEngine.Random.Range(numWallBlocksMin, numWallBlocksMax + 1), numHoles = (uint) UnityEngine.Random.Range(numHolesMin, numHolesMax + 1);
		
		// Surround the world by walls.
		for (uint i = 1; i < worldsSize - 1; i++)
		{
			world[i, 0] = wall;
			world[i, worldsSize - 1] = wall;
			world[0, i] = wall;
			world[worldsSize - 1, i] = wall;
		}
		
		// Fill the world with grass.
		for (uint i = 1; i < worldsSize - 1; i++)
			for (uint j = 1; j < worldsSize - 1; j++)
				world[i, j] = grass;
		
		// Generate a group of walls.
		for (uint k = 0; k < numWallBlocks; k++)
		{
			// Make up lines with walls.
			uint blockWidth = 1;
			uint blockHeight = 1;
			if (UnityEngine.Random.Range (0, 2) == 0)
				blockWidth = (uint) UnityEngine.Random.Range (sizeWallBlockMin, sizeWallBlockMax + 1);
			else
				blockHeight = (uint) UnityEngine.Random.Range (sizeWallBlockMin, sizeWallBlockMax + 1);
			uint blockX = (uint) UnityEngine.Random.Range (1, worldsSize - blockWidth);
			uint blockY = (uint) UnityEngine.Random.Range (1, worldsSize - blockHeight);
			
			// Fill.
			for (uint i = blockX; i < blockX + blockWidth; i++)
				for (uint j = blockY; j < blockY + blockHeight; j++)
					world[i, j] = wall;
		}
		
		// Generate a hole.
		for (uint k = 0; k < numHoles; k++)
		{
			// Choose the size.
			uint holeWidth = (uint) UnityEngine.Random.Range (sizeHoleMin, sizeHoleMax + 1);
			uint holeHeight = (uint) UnityEngine.Random.Range (sizeHoleMin, sizeHoleMax + 1);
			uint holeX = (uint) UnityEngine.Random.Range (1, worldsSize - holeWidth);
			uint holeY = (uint) UnityEngine.Random.Range (1, worldsSize - holeHeight);
			
			// Fill.
			for (uint i = holeX; i < holeX + holeWidth; i++)
				for (uint j = holeY; j < holeY + holeHeight; j++)
					world[i, j] = hole;
		}

		for (uint i = 8; i < 14; i++)
			for (uint j = 10; j < 16; j++)
				world[i,j] = grass;
	}

	public Sprite [] sprites;

	[SerializeField]
	GameObject floorSprite;

	[SerializeField]
	GameObject wallSprite;

	[SerializeField]
	GameObject holeSprite;

	void buildMap () {
		GameObject item;
		for (int i=0; i<22; i++) {
			for (int j=0; j<22; j++) {
				if (world[i,j] == 0) {
					GameObject.Instantiate(wallSprite, transform.position + new Vector3 (i, j, 1), transform.rotation);
				}

				if (world[i,j] == 1) {
					item = GameObject.Instantiate(floorSprite, transform.position + new Vector3 (i, j, 1), transform.rotation) as GameObject;
					SpriteRenderer sr = (SpriteRenderer) item.GetComponent("SpriteRenderer");
					sr.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
				}

				if (world[i,j] == 2) {
					GameObject.Instantiate(holeSprite, transform.position + new Vector3 (i, j, 1), transform.rotation);
				}
			}
		}
	}

	void Start () {
		GenerateWorld();
		buildMap ();
		((BonusManager) this.GetComponent("BonusManager")).setWorld(world);
	}

	void Update () {

	}

}
