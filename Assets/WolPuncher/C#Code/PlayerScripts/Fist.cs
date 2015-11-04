using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour 
{
	public WolfBehaviour mWolfBehaviour;
	public Movement mMovement;
	public Animator mBoredAnimator;

	private int mTempKilled;

	public GameObject BloodExplosion;

	public Vector2 speed;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = transform.position.x * speed;
	}
	
	// Update is called once per frame
	void Update()
	{
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Wolf")
		{
			mMovement.GetComponent<Movement>().WolfKilled();
			mBoredAnimator.SetFloat("Boredom", 0.0f);
			Movement.mWolvesKilled += 1;
			Movement.tempBored = 0.0f;
			mWolfBehaviour.GetComponent<WolfBehaviour>().KillWolf();
			Instantiate(BloodExplosion, transform.position, transform.rotation);
			//Destroy(collider.gameObject);
		}
	}
}
