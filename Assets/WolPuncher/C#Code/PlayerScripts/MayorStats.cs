using UnityEngine;
using System.Collections;

public class MayorStats : MonoBehaviour 
{
	public Animator mIritAnimator;
	public Animator mSadAnimator;
	public Animator mBoredAnimator;
	// Use this for initialization
	void Start () 
	{
		mIritAnimator.SetFloat("Irritation", 0.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
