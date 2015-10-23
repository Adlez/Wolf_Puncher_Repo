using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject mSpawnObj;

	Vector3 spawnPos = new Vector3();
	Quaternion spawnRot = new Quaternion();

	void SpawnObj()
	{
		Instantiate(mSpawnObj, spawnPos, spawnRot);
	}

	// Use this for initialization
	void Start () 
	{
		SpawnObj();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
