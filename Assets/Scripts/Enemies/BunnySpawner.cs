﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunnySpawner : MonoBehaviour 
{
    public GameObject m_Spawner;

	public float minSpawnTime;
	public float maxSpawnTime;

    public Vector2 m_SpawnXY;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnObj", minSpawnTime, maxSpawnTime);
        m_SpawnXY = m_Spawner.transform.position;
	}

	void SpawnObj()
	{
		//Vector3 ObjPos = new Vector3(1.95f, -1.23f, 0);
        Vector2 ObjPos =  m_SpawnXY;
		GameObject obj = BunnyPooler.current.GetPooledObject();

		if (obj == null) return;

		obj.transform.position = ObjPos;
		//obj.transform.rotation = transform.rotation;
		//obj.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
