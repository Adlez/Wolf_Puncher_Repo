using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjSpawner : MonoBehaviour 
{
	public float minSpawnTime;
	public float maxSpawnTime;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnObj", minSpawnTime, maxSpawnTime);
	}

/*	void SpawnObj()
	{
		Vector3 ObjPos = new Vector3(-11.87537f, 1.408689f, 0.3471181f);
		GameObject obj = Objectpooler.current.GetPooledObject();

		if (obj == null) return;

		obj.transform.position = ObjPos;
		obj.transform.rotation = transform.rotation;
		obj.SetActive(true);

	}*/
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
