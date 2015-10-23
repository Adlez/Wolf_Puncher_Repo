using UnityEngine;
using System.Collections;

public class BunnyBehaviour : MonoBehaviour
{
	public Vector3 mVelocity;
	public Vector3 mRotation;
	public Vector3 mScaling;

	public float mRotSpeed;

	public float mJumpForce;
	public float mMaxSpeed;

	public Vector2 speed;

	bool onGround_ = true;

	// Use this for initialization
	void Start()
	{

	}

	void OnEnable()
	{

	}
	void OnDisable()
	{
		CancelInvoke();
	}

	public void KillBunny()
	{
		gameObject.SetActive(false);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			onGround_ = true;
		}
		if (collision.collider.tag == "Player")
		{
			GetComponent<AudioSource>().Play();
			Invoke("KillBunny", 0.01f);
		}
	}

	// Update is called once per frame
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = transform.position.x * speed;
		if (gameObject.GetComponent<Rigidbody2D>().position.y < -5)
		{
			Invoke("KillBunny", 0.01f);
		}
	}

}
