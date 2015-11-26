using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPooler : MonoBehaviour 
{
    public static EnemyPooler current;

    public List<GameObject> m_EnemyList;
    //public List<GameObject> m_ObjectPool;

    public GameObject m_EnemyObj;
    //public GameObject m_WolfObj;
    //public GameObject m_BunnyObj;
    
    public int pooledAmount;
    public bool willGrow = true; //cannot remove so when new ones are added they'll never leave

    List<GameObject> pooledEnemies;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        //Debug.Log("HEY");
        //m_EnemyList.Add(m_WolfObj);
        //m_EnemyList.Add(m_BunnyObj);

        //for (int i = 0; i < m_EnemyList.Count; ++i)
        //{
            pooledEnemies = new List<GameObject>();
            for (int j = 0; j < pooledAmount; ++j)
            {
                GameObject enemy = (GameObject)Instantiate(m_EnemyList[j]);
                //enemy.SetActive(false);
                pooledEnemies.Add(enemy);
            }
        //}        
    }

    public GameObject GetPooledObject()
    {
        /*for (int i = 0; i < pooledEnemies.Count; ++i)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }*/
        if (willGrow)
        {
            GameObject enemy = (GameObject)Instantiate(m_EnemyObj);
            pooledEnemies.Add(enemy);
            return enemy;
        }

        return null;
    }
}
