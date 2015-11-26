using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBaseClass : MonoBehaviour
{
	#region Enemy Variables
	public List<GameObject> m_Enemies = new List<GameObject>();

	public GameObject m_EnemyController;
	public GameObject m_UniqueAttackHolder;

	public StateMachineScript m_StateMachine;

	public GameObject m_Player;// = GameObject.FindGameObjectWithTag("Player");
	public Vector2 m_PlayerPos;// = new Vector2(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y);

	public Animator m_EnemyAnimator;
	//public Collider2D[] m_Tracks;
	public GameObject[] m_TargetPoints;

	public int m_VelocityX;

	/*public int m_PlayerCurRow;
	public int m_CurRow;
	public bool m_AbleToChangeTrack = false;
	public float m_ChangeTrackTimer;*/

	public int m_EnemyType;

	public float m_AttackTimer;
	public float m_AnimationLength;

	public float m_HP;
	public float m_Attack1Damage;
	public float m_Attack2Damage;
	public float m_UniqueDamage;

	public float m_AttackResetTime;
    public float m_Attack1RestTime;
    public float m_Attack2RestTime;
	public float m_UniqueRestTime;

	public int m_Attack1Odds;
	public int m_Attack2Odds;
	public int m_AttackUniqueOdds;

	public float m_MaxDist;
	public float m_DetectionDist;
	public float m_AttackDist;
	public float m_ReactForce = 0.4f;//set this in constants for different enemies

	public bool m_EnemyInMotion;
	public bool m_isIdle;
	//public bool m_TimerIsCounting; //bool for Timer for changing tracks (primary timer)
	public bool m_IsDead = false;
	public bool m_IsABoss = false;

	/*public bool Row0Occupied = false;
	public bool Row1Occupied = false;
	public bool Row2Occupied = false;*/

	public int m_EnemyGoingLeft = 1; //1 == left, -1 == right

	public Rigidbody2D m_RigidBody;
	public Vector2 m_InitialXY;

	//two variables for changing tracks
	//public float changeTrackCountdown = 2.0f; //primary timer, when this reaches 0 the enemy can change tracks, and the secondary timer start counting down and m_TimerIsCounting is set to false
	//public float secondaryTrackTimer = 2.0f; //secondary timer, when this reaches 0 then the primary timer starts counting down and it's bool is set to true

	#endregion

	#region Enemy Movement
	public virtual void EnemyMove(GameObject enemy)
	{
		if (enemy.GetComponent<EnemyBaseClass>().m_EnemyInMotion)
		{
			enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.GetComponent<EnemyBaseClass>().m_VelocityX, 0);//set the enemy's velocity
		}

	}

	public virtual void EnemyIdle(GameObject enemy, Vector2 enemyPos)
	{

		float differenceThenNow = enemy.GetComponent<EnemyBaseClass>().m_InitialXY.x - enemyPos.x;
		float pointB = m_MaxDist;
		float pointA = enemy.GetComponent<EnemyBaseClass>().m_InitialXY.x + 1;
		//moving right and has passed pointA
		if (enemyPos.x >= pointA && enemy.GetComponent<EnemyBaseClass>().m_EnemyGoingLeft == -1)
		{
			enemyPos.x = pointA;
			enemy.GetComponent<EnemyBaseClass>().TurnAround(enemy);
		}
		//moving left and has passed point B
		if (enemyPos.x <= pointB && enemy.GetComponent<EnemyBaseClass>().m_EnemyGoingLeft == 1)
		{
			enemy.GetComponent<EnemyBaseClass>().TurnAround(enemy);
		}
		DetectPlayer(m_Player.transform.position, enemy);
	}

	public virtual void TurnAround(GameObject enemy)
	{
		enemy.GetComponent<EnemyBaseClass>().m_VelocityX *= -1; //turn the enemy around
		enemy.GetComponent<EnemyBaseClass>().m_EnemyGoingLeft *= -1; //tell the enemy it has turned around

		//Brandon's code to fip the animation,,, flips the x 
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public virtual void EnemyStopMotion(GameObject enemy)
	{
		enemy.GetComponent<EnemyBaseClass>().m_RigidBody.velocity = new Vector2(0, 0); //freeze position
		enemy.GetComponent<EnemyBaseClass>().m_EnemyInMotion = false; //set bool that prevents movement in the update
	}


	public virtual void ChasePlayer(Vector2 playerPos, Vector2 enemyPos, GameObject enemy)
	{
		for (int i = 0; i < Physics2D.AllLayers; ++i )
		{
			string LayerName = LayerMask.LayerToName(i);
			if(LayerName == "ActiveEnemy")
			{
				enemy.layer = i;
			}
		}
		float curEnemyXPOS = enemyPos.x;
		float lineOfSight;

		lineOfSight = enemy.GetComponent<EnemyScript>().m_AttackDist;
		
		if (playerPos.x > curEnemyXPOS)//if the player is on the right
		{
			//if the player is less to the right than the currentposition of the enemy's line of sight
			if (playerPos.x < enemyPos.x + lineOfSight)
			{
				EnemyStopMotion(enemy);
			}
			else
			{
				EnemyMove(enemy);
			}
		}
		else //the player is on the left
		{
			//if the player is less to the left than the enemy's pos - it's line of sight
			if (playerPos.x > enemyPos.x - lineOfSight)
			{
				//EnemyStopMotion(enemy);
			}
			else
			{
				EnemyMove(enemy);
			}
		}

		//If the enemy is to the left of the player and if the enemy is moving to the right
		if (enemyPos.x < playerPos.x && enemy.GetComponent<EnemyBaseClass>().m_VelocityX < 0)
		{
			enemy.GetComponent<EnemyBaseClass>().TurnAround(enemy); //correct movement direction
		}
		//If the enemy is to the right, and moving left
		if (enemyPos.x > playerPos.x && enemy.GetComponent<EnemyBaseClass>().m_VelocityX > 0)
		{
			enemy.GetComponent<EnemyBaseClass>().TurnAround(enemy); //correct movement direction
		}
	}

	public virtual void DetectPlayer(Vector2 playerPos, GameObject enemy)
	{
		Vector2 differenceInDistance = new Vector2(enemy.transform.position.x, enemy.transform.position.y) - playerPos; //get the difference between the two entities
		float forwardDetectionX = enemy.transform.position.x - enemy.GetComponent<EnemyBaseClass>().m_DetectionDist; //x position player has to reach or pass for the enemy to wake up

        if (differenceInDistance.x <= forwardDetectionX || differenceInDistance.x <= -forwardDetectionX)
        {
            enemy.GetComponent<EnemyBaseClass>().m_isIdle = false;//then the enemy is no longer Idle	
        }
	}
	#endregion

	#region Enemy Attacks
	public virtual void EnemyAttack(GameObject enemy)
	{
		int attackSelector = Random.Range(0, 100);
		enemy.GetComponent<EnemyBaseClass>().EnemyStopMotion(enemy);
		//			m_EnemyInMotion = false; //prevent continued motion of the enemy
		if (attackSelector <= m_Attack1Odds) //If attack selector is less than the odds of punching
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyAttack1(enemy); //PAWNCH
		}
		else if (attackSelector <= m_Attack2Odds)//not less than Punch odds, so check if less than kick odds
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyAttack2(enemy); //Kick
		}
		else if (attackSelector >= m_Attack2Odds)//must be greater than kick odds by now so Unique Attack is called
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyAttackUnique(enemy); //
		}
		
	}

	public virtual void ResetEnemyAttackTimer(float enemyAttackTimer)
	{
		this.GetComponent<EnemyBaseClass>().m_AttackTimer = enemyAttackTimer; //reassign the attack timer to the enemy's default
		//		this.m_EnemyInMotion = true; //tell the enemy it can move again
	}

	public virtual void EnemyAttack1(GameObject enemy) //This function is overwritten in the BullyScript
	{
		//enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.SetBool("IsAtk1", true);
		//play animation
		//set delay for the attack countdown timer to resume only when the animation is done
	}

	public virtual void EnemyAttack2(GameObject enemy)//This function is overwritten in the BullyScript
	{
		//enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.SetBool("IsAtk2", true);
		//play animation
		//set delay for the attack countdown timer to resume only when the animation is done	
	}

	public virtual void EnemyAttackUnique(GameObject enemy)//This function is overwritten in the BullyScript
	{
	}
	#endregion

	#region Enemy Combat
	//Straightforward
	public virtual void EnemyTakeDamage(int damageDealt)
	{
		//this.GetComponent<BullyScript>().m_EnemyAnimator.SetBool("IsHit", true);
		this.GetComponent<BullyScript>().m_HP -= damageDealt;
		if (m_HP <= 0)
		{
			KillEnemy(this.GetComponent<BullyScript>().gameObject);
		}
	}

	public virtual void KillEnemy(GameObject enemy)
	{
		//enemy.GetComponent<BullyScript>().m_EnemyAnimator.SetBool("IsDead", true);//play enemy death animation
		GameObject.Destroy(transform.root.gameObject);
	}
	#endregion

	#region Creation
	public virtual void InitEnemy(Vector2 spawnPos, GameObject newBully)
	{
		//newBully.GetComponent<EnemyBaseClass>().changeTrackCountdown = m_ChangeTrackTimer;

		newBully.GetComponent<EnemyBaseClass>().m_Player = GameObject.FindGameObjectWithTag("Player");
		newBully.GetComponent<EnemyBaseClass>().m_PlayerPos = new Vector2(m_Player.GetComponent<Rigidbody2D>().position.x, m_Player.GetComponent<Rigidbody2D>().position.y);

		newBully.GetComponent<EnemyBaseClass>().m_InitialXY = spawnPos;
		newBully.GetComponent<EnemyBaseClass>().m_isIdle = true;
		newBully.GetComponent<EnemyBaseClass>().m_EnemyAnimator = newBully.GetComponent<Animator>();

		//		
	}
	#endregion
	void Start()
	{
		m_EnemyController = GameObject.FindGameObjectWithTag("EnemyController");
	}

	public virtual void GetPlayerInfo(GameObject thisEnemy)
	{
		m_PlayerPos = m_Player.GetComponent<Rigidbody2D>().transform.position;
	}

	public virtual void EnemyUpdate(GameObject enemy)
	{
		//Debug.Log(enemy.name);
		
		Vector2 enemyPos = new Vector2(enemy.GetComponent<Rigidbody2D>().position.x, enemy.GetComponent<Rigidbody2D>().position.y);
		//enemy.GetComponent<EnemyScript>().m_UniqueAttackHolder.GetComponent<UniqueAttackScript>().UpdateUATKs(enemy); //Update Enemy Projectiles on screen

		m_Player = GameObject.FindGameObjectWithTag("Player");

		m_PlayerPos = new Vector2(m_Player.GetComponent<Rigidbody2D>().position.x, m_Player.GetComponent<Rigidbody2D>().position.y);

		//Detect Player Track
		GetPlayerInfo(enemy);

			
		//enemy.GetComponent<BullyScript>().GetComponent<Rigidbody2D>().transform.position = new Vector2(enemy.GetComponent<BullyScript>().GetComponent<Rigidbody2D>().transform.position.x, enemy.GetComponent<BullyScript>().m_TargetPoints[m_CurRow].transform.position.y);

		if (enemy.GetComponent<EnemyBaseClass>().m_EnemyInMotion)
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyMove(enemy);
		}

		if (enemy.GetComponent<EnemyBaseClass>().m_isIdle)
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyIdle(enemy, enemyPos);
		}
		else // enemy is not idle, therefore player is nearby
		{
			enemy.GetComponent<EnemyBaseClass>().ChasePlayer(m_PlayerPos, enemyPos, enemy);

			//Animation
			if (enemy.GetComponent<EnemyBaseClass>().m_AnimationLength > 0) //if animating, subtract Delta.Time
			{
				enemy.GetComponent<EnemyBaseClass>().m_AnimationLength -= Time.deltaTime;
			}
			if (enemy.GetComponent<EnemyBaseClass>().m_AttackTimer > 0 && enemy.GetComponent<EnemyBaseClass>().m_AnimationLength <= 0) //if the enemy isn't cooled down, and is not animating
			{
				enemy.GetComponent<EnemyBaseClass>().m_AttackTimer -= Time.deltaTime;
			}
			if (enemy.GetComponent<EnemyBaseClass>().m_AttackTimer <= 0 && enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.GetBool("IsPunch") == false && enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.GetBool("IsKick") == false && enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.GetBool("IsUnique") == false) //If the enemy is cooled down, and is not animating
			{
				enemy.GetComponent<EnemyBaseClass>().m_AttackTimer = 0;
				EnemyAttack(enemy);
			}

			if (enemy.GetComponent<EnemyBaseClass>().m_AnimationLength <= 0)
			{
				enemy.GetComponent<EnemyBaseClass>().m_AnimationLength = 0;
				//enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.SetBool("IsPunch", false);
				//enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.SetBool("IsKick", false);
				//enemy.GetComponent<EnemyBaseClass>().m_EnemyAnimator.SetBool("IsUnique", false);
				//enemy.GetComponent<EnemyBaseClass>().m_EnemyInMotion = true;
			}
		}
		

	}


	// Update is called once per frame
	void Update()
	{
		m_Enemies = m_EnemyController.GetComponent<EnemyControllerScript>().m_Bullies;
		for (int i = 0; i < m_Enemies.Count; ++i)
		{
			EnemyUpdate(m_Enemies[i]);

			
			if (m_Enemies[i].GetComponent<EnemyBaseClass>().m_HP <= 0)
			{
				string BullyJustKilled = m_Enemies[i].name;
                DestroyNow(m_Enemies[i]);
                //Destroy(m_EnemyController.GetComponent<EnemyControllerScript>().m_Bullies[i]);
				//Destroy(m_Enemies[i]);
				m_Enemies.Remove(m_Enemies[i].gameObject);
				if (BullyJustKilled == "KingBully")
				{
					//m_EnemyController.GetComponent<SpawnEnemies>().SpawnBoss(1, "QueenBully");
				}
			}
		}
		
	}

    void DestroyNow(GameObject obj)
    {
        DestroyObject(obj);
        Debug.Log("Wreck " + obj.name);
    }

	#region Collision
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PlayerProjectile")
		{		
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			this.GetComponent<Rigidbody2D>().isKinematic = true;
			//this.m_HP -= collision.gameObject.GetComponent<Projectile>().m_Damage;
		}
		this.GetComponent<Rigidbody2D>().AddForce(new Vector2(m_ReactForce, 0.0f));//Brandon's Wiggle
	}
	#endregion

}

/*

	public void CheckOccupiedTracks()
	{
		for (int i = 0; i < m_Enemies.Count; ++i)
		{
			if (m_Enemies[i].GetComponent<EnemyBaseClass>().m_CurRow == 0)
			{
				Row0Occupied = true;
			}
			else { Row0Occupied = false; }
			if (m_Enemies[i].GetComponent<EnemyBaseClass>().m_CurRow == 1)
			{
				Row1Occupied = true;
			}
			else { Row1Occupied = false; }
			if (m_Enemies[i].GetComponent<EnemyBaseClass>().m_CurRow == 2)
			{
				Row2Occupied = true;
			}
			else { Row2Occupied = false; }
		}
	}

	public virtual void ChangeTrack(GameObject enemy)//Bully will attempt to change tracks to match the player, but will fail if another enemy is on that track
	{
		CheckOccupiedTracks();

		//Moving UP tracks (DOWN on the screen)
		if (enemy.GetComponent<EnemyBaseClass>().m_CurRow < enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow)//If Bully's track is lower in the index than the player's
		{
			if (enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow++ == 1)//if enemy is on track 0 moving to track 1
			{
				if (!Row1Occupied)//if row one does not have a enemy on it
				{
					enemy.GetComponent<EnemyBaseClass>().m_CurRow++;//enemy changes rows and occupies the new track
					Row1Occupied = true;
					Row0Occupied = false;
				}
			}
			else if (enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow++ == 2)//if enemy is on track 1 moving to 2
			{
				if (!Row2Occupied)//if track does not have a enemy on it
				{
					enemy.GetComponent<EnemyBaseClass>().m_CurRow++;//enemy changes rows and occupies the new track
					Row2Occupied = true;
					Row1Occupied = false;
				}
			}
		}

		if (enemy.GetComponent<EnemyBaseClass>().m_CurRow > enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow)//If Bully's track is higher in the index than the player's
		{
			if (enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow-- == 0)//if enemy is on track 1 moving to track 0
			{
				if (!Row0Occupied)//if row zero does not have a enemy on it
				{
					enemy.GetComponent<EnemyBaseClass>().m_CurRow--;//enemy changes rows and occupies the new track
					Row0Occupied = true;
					Row1Occupied = false;
				}
			}
			else if (enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow-- == 1)//if enemy is on track 2 moving to 1
			{
				if (!Row1Occupied)//if track does not have a enemy on it
				{
					enemy.GetComponent<EnemyBaseClass>().m_CurRow--;//enemy changes rows and occupies the new track
					Row1Occupied = true;
					Row2Occupied = false;
				}
			}
		}

		else //function should not have been called in the first place
		{
			Debug.Log("Why was this even called?");
		}

		float xPos = enemy.GetComponent<EnemyBaseClass>().GetComponent<Rigidbody2D>().transform.position.x;

		float yPos = enemy.GetComponent<BullyScript>().m_TargetPoints[m_CurRow].transform.position.y;

		enemy.GetComponent<EnemyBaseClass>().GetComponent<Rigidbody2D>().transform.position = new Vector2(xPos, yPos);		
	}
*/

/* Inside ChasePlayer:
#region track change counter
		if (enemy.GetComponent<EnemyBaseClass>().changeTrackCountdown <= 0)//If Timer is at 0
		{
			enemy.GetComponent<EnemyBaseClass>().changeTrackCountdown = 0; //set the timer to 0
			enemy.GetComponent<EnemyBaseClass>().m_TimerIsCounting = false;//prevent the timer from continueing to count down
			if (enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow != enemy.GetComponent<EnemyBaseClass>().m_CurRow) //If not on the same track
			{
				enemy.GetComponent<EnemyBaseClass>().ChangeTrack(enemy);//will not change track if Idle
				enemy.GetComponent<EnemyBaseClass>().secondaryTrackTimer = Constants.TRACK_COUNTDOWN_DEFAULT; // The secondary timer is assigned its value
			}
		}
		#endregion
*/
/*Detect Player

		if (enemy.GetComponent<EnemyBaseClass>().m_CurRow == enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow)
		{
			//e - p = differenceInDistance
			//difference in distance == a line between the two p_____e
			//if this "line" is shorter than the enemy's forwardDetection aka "Line Of Sight" (while the player is to the left) -|____e____|+
			//or if the "line" is shorter than (player is inside the line) the enemy's ForwardDetectionX (while the player is to the right)
			if (differenceInDistance.x <= forwardDetectionX || differenceInDistance.x <= -forwardDetectionX)
			{
				enemy.GetComponent<EnemyBaseClass>().m_isIdle = false;//then the enemy is no longer Idle	
			}
		}
		else
		{
			if (differenceInDistance.x <= forwardDetectionX)//if the player is within the detection "range" of a enemy
			{
				enemy.GetComponent<EnemyBaseClass>().m_isIdle = false;//then the enemy is no longer Idle	
			}
		}
*/
/*
public virtual void EnemyAttack(GameObject enemy)
	{
		int attackSelector = Random.Range(0, 100);
		enemy.GetComponent<EnemyBaseClass>().EnemyStopMotion(enemy);
		if (enemy.GetComponent<EnemyBaseClass>().m_CurRow == enemy.GetComponent<EnemyBaseClass>().m_PlayerCurRow)
		{
			//			m_EnemyInMotion = false; //prevent continued motion of the enemy
			if (attackSelector <= m_Attack1Odds) //If attack selector is less than the odds of punching
			{
				enemy.GetComponent<EnemyBaseClass>().EnemyAttackPunch(enemy); //PAWNCH
			}
			else if (attackSelector <= m_Attack2Odds)//not less than Punch odds, so check if less than kick odds
			{
				enemy.GetComponent<EnemyBaseClass>().EnemyAttackKick(enemy); //Kick
			}
			else if (attackSelector >= m_Attack2Odds)//must be greater than kick odds by now so Unique Attack is called
			{
				enemy.GetComponent<EnemyBaseClass>().EnemyAttackUnique(enemy); //
			}
		}
		if (attackSelector >= m_Attack2Odds)//must be greater than kick odds by now so Unique Attack is called
		{
			enemy.GetComponent<EnemyBaseClass>().EnemyAttackUnique(enemy); //
		}
	}
*/
/*In Enemy Update
//Conditions for changing tracks
			if (!enemy.GetComponent<EnemyBaseClass>().m_TimerIsCounting) //if the primary timer is not able to count down (disabled)
			{
				enemy.GetComponent<EnemyBaseClass>().changeTrackCountdown = Constants.TRACK_COUNTDOWN_DEFAULT; //set the primary timer to its default value
				enemy.GetComponent<EnemyBaseClass>().secondaryTrackTimer -= Time.deltaTime; // decrement the secondary timer
			}
			if (secondaryTrackTimer <= 0) //once the secondary timer reaches 0
			{
				enemy.GetComponent<EnemyBaseClass>().m_TimerIsCounting = true; //enable the primary timer
				enemy.GetComponent<EnemyBaseClass>().secondaryTrackTimer = Constants.TRACK_COUNTDOWN_DEFAULT; //set the secondary timer to it's default value
			}
			if (enemy.GetComponent<EnemyBaseClass>().m_TimerIsCounting)
			{
				enemy.GetComponent<EnemyBaseClass>().changeTrackCountdown -= Time.deltaTime;
			}
*/
/* indexer Update
#region Keep Bullies away from the same rows if possible
			if (m_Enemies.Count >= 2)
			{
				if (!m_Enemies[i].GetComponent<EnemyBaseClass>().m_IsABoss)
				{
					for (int j = 1; j < m_Enemies.Count; ++j)
					{
//						Debug.Log(m_Bullies[i].name + " row " + m_Bullies[i].GetComponent<EnemyBaseClass>().m_CurRow + " vs " + m_Bullies[j].name + " row " + m_Bullies[j].GetComponent<EnemyBaseClass>().m_CurRow);
						if (!m_Enemies[j].GetComponent<EnemyBaseClass>().m_IsABoss)
						{
							if (m_Enemies[i].GetComponent<EnemyBaseClass>().m_CurRow == m_Enemies[j].GetComponent<EnemyBaseClass>().m_CurRow)
							{
								if (m_Enemies[j].GetComponent<EnemyBaseClass>().m_CurRow != 2)
								{
									m_Enemies[j].GetComponent<EnemyBaseClass>().m_CurRow++;
								}
								else if (m_Enemies[j].GetComponent<EnemyBaseClass>().m_CurRow != 0)
								{
									m_Enemies[j].GetComponent<EnemyBaseClass>().m_CurRow--;
								}
							}
						}
					}
				}
			}
			#endregion
*/