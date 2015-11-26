using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour 
{
    public GameObject m_EnemyContollerObj;
	public GameObject m_EnemyControl;
	public GameObject[] mSpawnPos; //List of possible spawn locaations
	public GameObject[] mEnemiesToSpawn; //List of possible enemies to spawn
	//public GameObject[] m_Tracks; //The three tracks an enemy can be anchored to

	public GameObject[] m_Bosses; //List of possible enemies to spawn

    public static bool bossSpawned_ = false;

    private Vector2 startPos_;
    private Vector2 xOffSet_;

	void Start () 
	{
		m_EnemyControl = GameObject.FindGameObjectWithTag("EnemyController"); 
		//mSpawnPos[0] = GameObject.FindGameObjectWithTag("ESRL");
		//mSpawnPos[1] = GameObject.FindGameObjectWithTag("ESRM");
		//mSpawnPos[2] = GameObject.FindGameObjectWithTag("ESRF");
        //m_Tracks = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyBaseClass>().m_TargetPoints;
		if(mSpawnPos.Length == 0)
		{
			Debug.LogError("Spawn Area needs spawn positions.");
		}
        xOffSet_ = new Vector2(11.0f, 0.0f);
	}
	
	public void SpawnEnemyFunc(int type)
	{
        if (m_EnemyContollerObj == null)
        {
            m_EnemyContollerObj = GameObject.FindGameObjectWithTag("EnemyController"); 
        }
        //var Blue = Objectpooler.Instance;//.GetObjectForType(mEnemiesToSpawn[type].name, true);
        //GameObject newEnemy = Objectpooler.Instance.GetObjectForType(mEnemiesToSpawn[type].name, true);//new enemy is created
        GameObject newEnemy = mEnemiesToSpawn[type];
        newEnemy.transform.position = startPos_ + xOffSet_; //mSpawnPos[row].transform.position; //the enemy's position is assigned the position at the selected row

        m_EnemyContollerObj.GetComponent<EnemyControllerScript>().AddBullyToList(newEnemy);
        newEnemy.GetComponent<EnemyScript>().InitEnemy(startPos_ + xOffSet_, newEnemy);

	}
}