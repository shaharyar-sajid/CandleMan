using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileCreatev2 : MonoBehaviour
{
	// Start is called before the first frame update
	public Transform tileObj, obstacleObj, horiObstacleObj, rampObj, pickUpObj, finishLineObj, cakeObj, obstacle2Obj;
	public GameObject progressbar;
	private Vector3 nextTileSpawn, nextObstacleSpawn, nextHoriObstacleSpawn, nextRampSpawn, nextPickUpSpawn;
	int randX, randObs, randRamp, randPickUp, randSecond1, randSecond2, randSecond3;
	public static bool obstacles;
	public static bool iterate;
	int level;
	void Start()
    {
		level = PlayerPrefs.GetInt("level", 1);
		nextTileSpawn.y = tileObj.position.y;
		obstacles = true;
		iterate = true;
		intiateTiles();
		StartCoroutine(spawnTile());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void intiateTiles()
    {
		nextTileSpawn.z = 2;
		for (int i = 0; i < 3; i++)
        {
			randX = Random.Range(-1, 2);
			randObs = Random.Range(0, 2);

			Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
			nextTileSpawn.z += 2;
			Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
			nextTileSpawn.z += 2;
		}
	}

	IEnumerator spawnTile()
	{
		Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
		if (obstacles)
		{
			randX = Random.Range(-1, 2);
			randObs = Random.Range(0, 4);
			randPickUp = Random.Range(0, 2);
			randRamp = Random.Range(0, 4);
			randSecond1 = Random.Range(0, 2);
			randSecond2 = Random.Range(0, 2);
			randSecond3 = Random.Range(0, 2);

			if (randObs == 0)
			{
				nextObstacleSpawn = nextTileSpawn;
				nextObstacleSpawn.y += 0.55f;
				nextObstacleSpawn.x = randX;
				Instantiate(obstacleObj, nextObstacleSpawn, obstacleObj.rotation);
				
				if(randSecond1 == 1)
				{
					var choices = new List<int>() {-1, 0, 1};
					choices.Remove(randX);
					var secondObst = choices[Random.Range(0, choices.Count)];
					
					nextObstacleSpawn = nextTileSpawn;
					nextObstacleSpawn.y += 0.55f;
					nextObstacleSpawn.x = secondObst;
					Instantiate(obstacleObj, nextObstacleSpawn, obstacleObj.rotation);
				}
			}
			else if (randObs == 1)
			{
				nextHoriObstacleSpawn = nextTileSpawn;
				nextHoriObstacleSpawn.y += 1.02f;
				nextHoriObstacleSpawn.x = randX;
				Instantiate(horiObstacleObj, nextHoriObstacleSpawn, horiObstacleObj.rotation);
				
				if(randSecond2 == 1)
				{
					var choices = new List<int>() {-1, 0, 1};
					choices.Remove(randX);
					var secondObst = choices[Random.Range(0, choices.Count)];
					
					nextHoriObstacleSpawn = nextTileSpawn;
					nextHoriObstacleSpawn.y += 1.02f;
					nextHoriObstacleSpawn.x = secondObst;
					Instantiate(horiObstacleObj, nextHoriObstacleSpawn, horiObstacleObj.rotation);
				}
			}
			else if (randObs == 2 && level > 1)
            {
				nextObstacleSpawn = nextTileSpawn;
				nextObstacleSpawn.y += 1.02f;
				nextObstacleSpawn.x = randX;
				Instantiate(obstacle2Obj, nextObstacleSpawn, obstacle2Obj.rotation);

				if (randSecond3 == 1)
				{
					var choices = new List<int>() { -1, 0, 1 };
					choices.Remove(randX);
					var secondObst = choices[Random.Range(0, choices.Count)];

					nextObstacleSpawn = nextTileSpawn;
					nextObstacleSpawn.y += 1.02f;
					nextObstacleSpawn.x = secondObst;
					Instantiate(obstacle2Obj, nextObstacleSpawn, obstacle2Obj.rotation);
				}
			}
			else if (randObs > 1 && randPickUp == 1) //Pick up instantiation: increases height
			{
				nextPickUpSpawn = nextTileSpawn;
				nextPickUpSpawn.y += 1.10f; //Opted for sequential instead of for-loop. I think this will perform better
				nextPickUpSpawn.x = -1;     //but wasn't bothered enough to test it :)
				Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
				nextPickUpSpawn.x = 0;
				Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
				nextPickUpSpawn.x = 1;
				Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
			}
			nextTileSpawn.z += 2;
			Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
			nextTileSpawn.z += 2;
			//ramps
			if (randRamp == 1)
			{
				nextRampSpawn = nextTileSpawn;
				nextRampSpawn.y += 1.24f;
				Instantiate(rampObj, nextRampSpawn, rampObj.rotation);
				nextTileSpawn.z += 3.5433f;
				nextTileSpawn.y += 2.35f;
			}
		}
		else
        {
			nextTileSpawn.z += 2;
			Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
			nextTileSpawn.z += 2;
			Instantiate(tileObj, nextTileSpawn, tileObj.rotation);
			nextTileSpawn.z += 2;
		}
		
		if (iterate)
		{
			yield return new WaitForSeconds(1);
			StartCoroutine(spawnTile());
		}
		else
        {
			Instantiate(finishLineObj, new Vector3(-0.05f, nextTileSpawn.y+1.6f, nextTileSpawn.z - 10.0f), finishLineObj.rotation);
			Instantiate(cakeObj, new Vector3(0.2f, nextTileSpawn.y - 0.1f, nextTileSpawn.z), cakeObj.rotation);
		}
	}
}
