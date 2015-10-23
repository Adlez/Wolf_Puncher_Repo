using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Objectpooler : MonoBehaviour 
{
	[System.Serializable]
	public class ObjectPoolEntry
	{
		[SerializeField]
		public GameObject mPrefab;

		[SerializeField]
		public int mCount = 3;
	}

	public ObjectPoolEntry[] mEntries;

	public List<GameObject>[] mPool;

	protected GameObject containerObject_;

	public static ObjectPoolEntry Instance { get { return Instance; } }
	private static Objectpooler instance_ = null;

	public static Objectpooler current;
	public GameObject pooledObject;
	public int pooledAmount;
	public bool willGrow = true; //cannot remove so when new ones are added they'll never leave

	List<GameObject> pooledObjects;

	void Awake()
	{
//		current = this;
		if(instance_ != null && instance_ != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance_ = this;
		}

		DontDestroyOnLoad(this.gameObject);
		InitializePool();
	}

	void InitializePool()
	{
		containerObject_ = new GameObject("ObjectPooler");
		containerObject_.transform.parent = transform;

		mPool = new List<GameObject>[mEntries.Length];

		for(int i = 0; i < mPool.Length; ++i)
		{
			ObjectPoolEntry prefab = mEntries[i];
			mPool[i] = new List<GameObject>();
			for(int j = 0; j < prefab.mCount; ++j)
			{
				GameObject newObj = (GameObject)GameObject.Instantiate(prefab.mPrefab);
				newObj.name = prefab.mPrefab.name;
				PoolObject(newObj);
			}
		}
	}

	public void PoolObject(GameObject obj)
	{
		for(int i = 0; i < mEntries.Length; ++i)
		{
			if(mEntries[i].mPrefab.name == obj.name)
			{
				obj.SetActive(false);
				obj.transform.parent = containerObject_.transform;
				mPool[i].Add(obj);
				return;
			}
		}
	}


	// Use this for initialization
	void Start () 
	{
		pooledObjects = new List<GameObject>();
/*		for(int i = 0; i < pooledAmount; ++i)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}*/
	}

	public GameObject GetObjectForType(string typeName, bool onlyPooled)
	{
		for(int i = 0; i < mEntries.Length; ++i)
		{
			GameObject prefab= mEntries[i].mPrefab;
			if(prefab.name == typeName)
			{
				if(mPool[i].Count > 0)
				{
					GameObject pooledObject = mPool[i][0];
					mPool[i].RemoveAt(0);
					pooledObject.transform.parent = null;
					pooledObject.SetActive(true);
					return pooledObject;

				}
				if(!onlyPooled)
				{
					GameObject newObject = (GameObject)GameObject.Instantiate(mEntries[i].mPrefab);
					newObject.name = mEntries[i].mPrefab.name;
					return newObject;
				}
			}
		}
		return null;
	}

	public GameObject GetPooledObject()
	{
		for(int i = 0; i < pooledObjects.Count; ++i)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		if(willGrow)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add(obj);
			return obj;
		}

		return null;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
