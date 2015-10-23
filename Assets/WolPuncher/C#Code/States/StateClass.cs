using UnityEngine;
using System.Collections;

public abstract class StateClass //: MonoBehaviour 
{
	public StateMachine mStateMachine;

	/*public StateClass(StateMachine stateMachine)
	{
		mStateMachine = stateMachine;
	}*/

	public abstract void OnStateEntered();
	public abstract void OnStateExit();
	public abstract void StateUpdate();
	public abstract void StateGUI();

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
