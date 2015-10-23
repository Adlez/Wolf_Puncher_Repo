using UnityEngine;
using System.Collections;

public class Default : StateClass//(StateMachine)
{
	public float m_GameTimer;
	new GameObject testObj;

	public Paused mPausedState; //state we want to transition to.

	public override void OnStateEntered()
	{
		m_GameTimer = 0.0f;
	}

	public override void OnStateExit()
	{
		Debug.Log("Exiting default GameState. Default state ran for " + m_GameTimer+" seconds");	
	}
	public override void StateUpdate()
	{
		m_GameTimer += Time.deltaTime;
		Debug.Log("GameTimer: " + m_GameTimer);

		//condition to switch to PAUSE state
		if(Input.GetAxis("Pause") > 0.0f) //if greater than 0
		{
			mStateMachine.ChangeState(mPausedState);
		}

		if(Input.GetKeyDown(KeyCode.F1))
		{
			//testObj = (GameObject)Objectpooler.Instance.GetObjectForType("Bunny", true);
			//testObj.transform.position = transform.position;
		}

		if(Input.GetKeyDown(KeyCode.F3))
		{
			//testObj = (GameObject)Objectpooler.Instance.GetObjectForType("WolfPlusBlood", true);
			//testObj.transform.position = transform.position;
		}


	}
	public override void StateGUI()
	{
		GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 50.0f), m_GameTimer.ToString());
	}
}
