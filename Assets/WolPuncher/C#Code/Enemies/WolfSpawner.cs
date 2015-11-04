using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfSpawner : MonoBehaviour 
{
	public float minSpawnTime = 1.5f;
	public float maxSpawnTime = 10f;

	public int WolvesOnScreen;
	int MaxWolves = 1;
	int gameClock = 0;

	// Use this for initialization
	void Start () 
	{
		
		
	}

	void SpawnObj()
	{
		Vector3 ObjPos = new Vector3(-11.87537f, 1.408689f, 0.3471181f);
		GameObject obj = Objectpooler.current.GetPooledObject();

		if (obj == null) return;

		obj.transform.position = ObjPos;
		obj.transform.rotation = transform.rotation;
		obj.SetActive(true);
		++WolvesOnScreen;
	}
	
	// Update is called once per frame
	void Update () 
	{
	//	gameClock = (int)Time.deltaTime;
		gameClock = 1;
		if (WolvesOnScreen < MaxWolves)
		{
			for (int i = 0; i < MaxWolves; ++i)
			{
				Debug.Log(WolvesOnScreen);
			
				InvokeRepeating("SpawnObj", minSpawnTime, maxSpawnTime);
			}
			
		}
		

		if (gameClock > 2600)
		{
			MaxWolves = 7;
		}
		else if (gameClock > 2150)
		{
			MaxWolves = 6;
		}
		else if (gameClock > 1700)
		{
			MaxWolves = 5;
		}
		else if (gameClock > 1350)
		{
			MaxWolves = 4;
		}
		else if (gameClock > 900)
		{
			MaxWolves = 3;
		}
		else if (gameClock > 450)
		{
			MaxWolves = 2;
		}
		else
		{
			MaxWolves = 1;
		}
		
	}
}
