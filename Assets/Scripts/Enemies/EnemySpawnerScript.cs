using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject m_SpawnLocation;
    public GameObject m_ThisSpawner;

    public float minSpawnTime;
    public float maxSpawnTime;

    public Vector2 m_SpawnXY;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnObj", minSpawnTime, maxSpawnTime);
        m_SpawnXY = m_SpawnLocation.transform.position;
    }

    void SpawnObj()
    {
        Vector2 ObjPos = m_SpawnXY;
        GameObject obj = m_ThisSpawner.GetComponent<EnemyPooler>().GetPooledObject();
        //GameObject obj = EnemyPooler.current.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = ObjPos;
        //obj.transform.rotation = transform.rotation;
        //obj.SetActive(true);

    }
}
