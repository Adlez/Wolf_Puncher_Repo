using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjSpawner : MonoBehaviour 
{
    public GameObject m_ObjectPooler;

    public GameObject m_SpawnLocation;

	public float minSpawnTime;
	public float maxSpawnTime;

    public Vector2 m_SpawnXY;

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("SpawnObj", minSpawnTime, maxSpawnTime);
        m_SpawnXY = m_SpawnLocation.transform.position;
	}

	void SpawnObj()
	{
        Vector2 ObjPos = m_SpawnXY;
		//Vector3 ObjPos = new Vector3(-11.87537f, 1.408689f, 0.3471181f);
        //GameObject obj = m_ObjectPooler.GetComponent<Objectpooler>().GetPooledObject();
		//GameObject obj = Objectpooler.current.GetPooledObject();


		/*if (obj == null) return;

		obj.transform.position = ObjPos;
		obj.transform.rotation = transform.rotation;
		obj.SetActive(true);*/

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
