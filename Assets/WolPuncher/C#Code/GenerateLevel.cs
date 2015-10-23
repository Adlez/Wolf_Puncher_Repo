using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour 
{

	public float mBaseTileSize = 20.0f;
	public float mBaseY = -4.0f;

	public GameObject[] mTiles;
	public GameObject mPlayer;
	private GameObject currTile_;
	private int currIndex_;

	public GameObject[] mEnemyPoolA;
	public GameObject[] mEnemyPoolB;

	private bool poolA_ = true;
	//public GameObject[] mSpeedPool;




	// Use this for initialization
	void Start()
	{
		//upon startup, choose a tile to place.
		int index = Random.Range(0, mTiles.Length);
		mTiles[index].transform.position = new Vector3(0.0f, mBaseY, 0.0f);
		currTile_ = mTiles[index];
		currIndex_ = index;
	}

	// Update is called once per frame
	void Update()
	{
		if (mPlayer.transform.position.x >= currTile_.transform.position.x)
		{
			int index = 0;
			int temp = Random.Range(0, 2);
			Debug.Log("Random number: " + temp);
			if (currIndex_ > 0)
			{
				if (temp == 1 && currIndex_ < mTiles.Length - 1)
				{
					//choose higher
					index = Random.Range(currIndex_ + 1, mTiles.Length);
				}
				else
				{
					//choose lower
					index = Random.Range(0, currIndex_);
				}
			}
			//choose a higher number
			else
			{
				index = Random.Range(currIndex_ + 1, mTiles.Length);
			}
			InitTile(index);
		}
	}

	void InitTile(int index)
	{
		poolA_ = !poolA_;
		currIndex_ = index;

		mTiles[index].transform.position = currTile_.transform.position +
										   new Vector3(mBaseTileSize, 0.0f, 0.0f);
		currTile_ = mTiles[index];

		int enemyIndex = 0;
		//go through spawn points, placing an enemy at each one.
		foreach (GameObject spawnPoint in currTile_.GetComponent<Tile>().mSpawnPoints)
		{

			if (poolA_)
			{
				if (mEnemyPoolA.Length == enemyIndex)
				{
					Debug.LogError("Enemy pool A not big enough for tile " + currTile_);
				}
				mEnemyPoolA[enemyIndex].transform.position = spawnPoint.transform.position;
				mEnemyPoolA[enemyIndex].GetComponent<FunctionalObject>().Reset();
			}
			else
			{
				if (mEnemyPoolB.Length == enemyIndex)
				{
					Debug.LogError("Enemy pool B not big enough for tile " + currTile_);
				}
				mEnemyPoolB[enemyIndex].transform.position = spawnPoint.transform.position;
				mEnemyPoolB[enemyIndex].GetComponent<FunctionalObject>().Reset();
			}
			enemyIndex++;
		}

	}
}