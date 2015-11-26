using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class BullyScript : EnemyBaseClass
{
	
    void Start()
    {
        LoadFromXML();		
    }

	#region Creation
	public override void InitEnemy(Vector2 spawnPos, GameObject newBully)
	{
		base.InitEnemy(spawnPos, newBully);

		this.m_EnemyController = GameObject.FindGameObjectWithTag("EnemyController");
        this.LoadFromXML();			//Load enemy's stats from xml file
		this.m_EnemyInMotion = true;	//Make the enemy move when it is spawned
		this.m_EnemyGoingLeft = 1;	//Set the starting direction
		this.m_isIdle = true;		//The enemy begins Idle
		this.m_InitialXY = spawnPos;	//Get the initial position to "anchor" it to	

		this.m_UniqueAttackHolder = GameObject.FindGameObjectWithTag("UATKHolder");
		//this.PepperSpray = this.m_UniqueAttackHolder.GetComponent<UniqueAttackScript>().m_PepperSpray;

		//this.m_CurRow = row;

		this.m_TargetPoints[0] = GameObject.FindGameObjectWithTag("Targetpoint2");

		this.m_TargetPoints[1] = GameObject.FindGameObjectWithTag("Targetpoint1");

		this.m_TargetPoints[2] = GameObject.FindGameObjectWithTag("Targetpoint0");

		//this.changeTrackCountdown = this.m_ChangeTrackTimer;
        m_MaxDist = this.GetComponent<Rigidbody2D>().position.x - 0;//Constants.BULLY_MAX_TRAVEL_DIST; //Set the maximum travel distance

	//	Bully.GetComponent<Rigidbody2D>().transform.position = new Vector3(Bully.transform.position.x, m_TargetPoints[(int)spawnPos.x].transform.position.y, m_TargetPoints[(int)spawnPos.y].transform.position.z);

	}
	#endregion

	#region Attacks
	public override void EnemyAttack2(GameObject enemy)
	{
		//play Bully's Kick Animation
		this.m_EnemyAnimator.SetBool("IsKick", true);
		this.m_AnimationLength = 3;
		float AttackTimer = m_AttackResetTime + m_Attack1RestTime; //assign the particular enemy's Resttime for after a Kick
		this.ResetEnemyAttackTimer(AttackTimer); //Reset the AttackTimer according to the last attack and the enemy's default resttime
	}

	public override void EnemyAttack1(GameObject enemy)
	{
		//play Bully's Kick Animation
		this.m_EnemyAnimator.SetBool("IsPunch", true);
		this.m_AnimationLength = 2;
		float AttackTimer = m_AttackResetTime + m_Attack2RestTime; //assign the particular enemy's Resttime for after a Punch
		this.ResetEnemyAttackTimer(AttackTimer); //Reset the AttackTimer according to the last attack and the enemy's default resttime
	}

	public override void EnemyAttackUnique(GameObject enemy)
	{
		this.m_EnemyAnimator.SetBool("IsUnique", true);
		if (enemy.name == "Wolf")
		{
			m_UniqueAttackHolder.GetComponent<UniqueAttackScript>().PepperUniqueAttack(enemy);
		}
        else if (enemy.name == "Bear")
        {
            m_UniqueAttackHolder.GetComponent<UniqueAttackScript>().FatUniqueAttack(enemy);
        }

		this.m_AnimationLength = 10;
		float AttackTimer = this.m_AttackResetTime + this.m_UniqueRestTime; //assign the particular enemy's Resttime for after a Unique Attack
		this.ResetEnemyAttackTimer(AttackTimer); //Reset the AttackTimer according to the last attack and the enemy's default resttime
	}
	#endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        }
    }

    #region Data Loading
    void LoadFromXML()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("./Assets/XML/BullyStats.xml");

        XmlNode root = xmlDoc.FirstChild;

        foreach(XmlNode node in root.ChildNodes)
        {
			m_DetectionDist = 5;
            //Find the node with a matching name as GameObject
            if(node.Name == this.name)
            {
                m_HP = float.Parse(node.Attributes["HP"].Value); //Load the HP value from the XML file
                m_Attack1Damage = float.Parse(node.Attributes["PunchDamage"].Value); //Load the punch damage value from the XML file
                m_Attack2Damage = float.Parse(node.Attributes["KickDamage"].Value); //Load the kick damage value from the XML file
                m_UniqueDamage = float.Parse(node.Attributes["UniqueDamage"].Value); //Load the unique damage value from the XML file
                m_Attack1RestTime = float.Parse(node.Attributes["PunchRest"].Value); //Load the punch rest time from the XML file
                m_Attack2RestTime = float.Parse(node.Attributes["KickRest"].Value); //Load the kick rest time from the XML file
                m_UniqueRestTime = float.Parse(node.Attributes["UniqueRest"].Value); //Load the unique attack rest time from the XML file
                m_Attack1Odds = int.Parse(node.Attributes["PunchOdds"].Value); //Load the punch odds from the XML file
                m_Attack2Odds = int.Parse(node.Attributes["KickOdds"].Value); //Load the kick odds from the XML file
                m_AttackUniqueOdds = int.Parse(node.Attributes["UniqueOdds"].Value); //Load the unique attack odds from the XML file
                m_AttackResetTime = float.Parse(node.Attributes["AttackReset"].Value); //Load the attack reset time from the XML file
                m_VelocityX = int.Parse(node.Attributes["Velocity"].Value); //Load the velocity from the XML file
				m_AttackDist = int.Parse(node.Attributes["AttackDist"].Value);
				//m_ChangeTrackTimer = int.Parse(node.Attributes["ChangeTrackDelay"].Value);
            }
        }
    }
    #endregion
}
