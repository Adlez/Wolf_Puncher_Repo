using UnityEngine;
using System.Collections;

public class PuncherScript : MonoBehaviour 
{
	public Transform mFistSpawn;
	public GameObject mFist;

	public float mPunchRate;
	private float mNextPunch;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton("Fire1") && Time.time > mNextPunch)
		{
			mNextPunch = Time.time + mPunchRate;
			Instantiate(mFist, mFistSpawn.position, mFistSpawn.rotation);

		}
	}
}
