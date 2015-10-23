using UnityEngine;
using System.Collections;

public class Paused : StateClass
{
	public Default mDefaultState;
	private bool isUnPausable_;
	

	public Paused(StateMachine stateMachine) //: base (StateMachine)
	{

	}

	public override void OnStateEntered()
	{
		
	}

	public override void OnStateExit()
	{
		Debug.Log("Exiting Paused GameState.");
	}
	public override void StateUpdate()
	{
		Debug.Log("GamePaused");

		if(isUnPausable_ && Input.GetAxis("Pause") > 0.0f)
		{
			mStateMachine.ChangeState(mDefaultState);
		}
	}
	public override void StateGUI()
	{
        GUI.Label(new Rect(10.0f, 10.0f, 200.0f, 50.0f), mDefaultState.m_GameTimer.ToString());
	}
}
