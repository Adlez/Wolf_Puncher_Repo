using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour 
{
	private StateClass currState_;
	public short hiClass;
	public StateClass mStartState;
	//inspector view only works with some types like ints, floats, gameObjects, etc

	// Use this for initialization
	void Start () 
	{
		if(mStartState == null)
		{
			Debug.LogError("Each state machine must have a start state set via the inspector view");
		}
		currState_ = mStartState;
	}
	


	// Update is called once per frame
	void Update () 
	{
		currState_.StateUpdate(); // this could be in FixedUpdate, also we might
									//want FixedUpdate specific function for our state
	}

	void OnGUI()
	{
		currState_.StateGUI();
	}

	public void ChangeState(StateClass newState)
	{
		currState_.OnStateExit();
		currState_ = newState;
		currState_.OnStateEntered();
	}
}
