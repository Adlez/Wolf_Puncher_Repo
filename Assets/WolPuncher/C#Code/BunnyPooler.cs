using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunnyPooler : MonoBehaviour 
{
	public static BunnyPooler current;
	public GameObject bunnyPool;
	public int pooledAmount;
	public bool willGrow = true; //cannot remove so when new ones are added they'll never leave

	List<GameObject> pooledBunnies;

	void Awake()
	{
		current = this;
	}


	// Use this for initialization
	void Start () 
	{
		pooledBunnies = new List<GameObject>();
		for(int i = 0; i < pooledAmount; ++i)
		{
			GameObject bunny = (GameObject)Instantiate(bunnyPool);
			bunny.SetActive(false);
			pooledBunnies.Add(bunny);
		}
	}

	public GameObject GetPooledObject()
	{
		for(int i = 0; i < pooledBunnies.Count; ++i)
		{
			if(!pooledBunnies[i].activeInHierarchy)
			{
				return pooledBunnies[i];
			}
		}
		if(willGrow)
		{
			GameObject bunny = (GameObject)Instantiate(bunnyPool);
			pooledBunnies.Add(bunny);
			return bunny;
		}

		return null;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
