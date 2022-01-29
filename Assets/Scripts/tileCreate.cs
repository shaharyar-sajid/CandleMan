using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileCreate : MonoBehaviour
{
    // Start is called before the first frame update
	public Transform tileObj, obstacleObj, horiObstacleObj, rampObj, pickUpObj;
	private Vector3 nextTileSpawn, nextObstacleSpawn, nextHoriObstacleSpawn, nextRampSpawn, nextPickUpSpawn;
	int randX, randObs, randRamp, randPickUp;
	
    void Start()
    {
        nextTileSpawn.z = 24;
		StartCoroutine(spawnTile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	IEnumerator spawnTile()
	{
		yield return new WaitForSeconds(1);
		randX = Random.Range(-1, 2);
		randObs = Random.Range(0, 3);
		randRamp = Random.Range(0, 4);
		randPickUp = Random.Range(0,3);
		
		Instantiate(tileObj, nextTileSpawn,  tileObj.rotation);
		if(randObs == 0)
		{
			nextObstacleSpawn = nextTileSpawn;
			nextObstacleSpawn.y += 0.7f;
			nextObstacleSpawn.x = randX;
			Instantiate(obstacleObj, nextObstacleSpawn, obstacleObj.rotation);
		}
		else if(randObs == 1)
		{
			nextHoriObstacleSpawn = nextTileSpawn;
			nextHoriObstacleSpawn.y += 1.02f;
			nextHoriObstacleSpawn.x = randX;
			Instantiate(horiObstacleObj, nextHoriObstacleSpawn, horiObstacleObj.rotation);
		}
		else if(randObs > 1 && randPickUp <= 1) //Pick up instantiation: increases height
		{
			nextPickUpSpawn = nextTileSpawn;
			nextPickUpSpawn.y += 1.10f; //Opted for sequential instead of for-loop. I think this will perform better
			nextPickUpSpawn.x = -1;		//but wasn't bothered enough to test it :)
			Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
			nextPickUpSpawn.x = 0;
			Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
			nextPickUpSpawn.x = 1;
			Instantiate(pickUpObj, nextPickUpSpawn, pickUpObj.rotation);
		}
		nextTileSpawn.z += 2;
		Instantiate(tileObj, nextTileSpawn,  tileObj.rotation);
		nextTileSpawn.z += 2;
		//ramps
		if(randRamp == 1)
		{
			nextRampSpawn = nextTileSpawn;
			nextRampSpawn.y += 1.24f;
			Instantiate(rampObj, nextRampSpawn, rampObj.rotation);
			nextTileSpawn.z += 3.5433f;
			nextTileSpawn.y += 2.35f;
		}
		StartCoroutine(spawnTile());
	}
}
