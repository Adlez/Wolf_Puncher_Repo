﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfBehaviour : MonoBehaviour 
{
	public Vector3 mVelocity;
	public Vector3 mRotation;
	public Vector3 mScaling;

	public GameObject BloodExplosion;
    public List<GameObject> m_Wolves;

	public float mRotSpeed;

	public float mJumpForce;
	public float mMaxSpeed;

///	Animator mBoredAnim;

	bool onGround_ = true;

	// Use this for initialization
	void Start () 
	{
		
	}

	void OnEnable()
	{

	}
	void OnDisable()
	{
		CancelInvoke();
	}

	public void KillWolf(int index)
	{
		//gameObject.SetActive(false);
        //GameObject.Destroy(transform.root.gameObject);
        //Destroy(this.gameObject);
        //DestroyObject(gameObject);
        //Destroy(m_Wolves[index]);
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		GetComponent<ParticleSystem>().enableEmission = false;
		if (collision.collider.tag == "Ground")
		{
			onGround_ = true;
		}
		if (collision.collider.tag == "Player")
		{
            //KillWolf();
			//Invoke("KillWolf", 0.01f);
			Instantiate(BloodExplosion, transform.position, transform.rotation);
		}
		if (collision.collider.tag == "Fist")
		{
			//Invoke("KillWolf", 0.1f);
            //KillWolf();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		GetComponent<Rigidbody2D>().AddForce(-(Vector2.right * mMaxSpeed));
		if(gameObject.GetComponent<Rigidbody2D>().position.y < -5)
		{
			Invoke("KillWolf", 0.1f);
		}
	}

}
