using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	private bool mGameOver;
	private bool mRestart;

	public GameObject BloodExplosion;

	float tempMad;
	int tempSad;
	public static float tempBored = 0.0f;

	public WolfBehaviour mWolfBehaviour;
	public MayorStats mMayorStats;

	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText wolvesKilledText;

	public Transform mFistSpawn;
	public GameObject mFist;

	public float mPunchRate;
	private float mNextPunch;

	public float Irritation;
	int MAXIRRITATION = 10;
	public float Boredom;
	float MAXBOREDOM = 60.0f;
	public int Sadness;
	int MAXSADNESS = 5;

	public Vector3 mVelocity;
	public Vector3 mRotation;
	public Vector3 mScaling;

	public Animator mAnimator;
	public Animator mIritAnimator;
	public Animator mSadAnimator;
	public Animator mBoredAnimator;

	public float mRotSpeed;
	public float mMaxSpeed;
	public static int mWolvesKilled = 0;

	public bool mFacingRight;

	public float mJumpForce;

	public bool OnGround
	{
		get
		{
			return onGround_;
		}
		set
		{
			onGround_ = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		Irritation = 0;
		Boredom = 0.0f;
		Sadness = 0;
		mGameOver = false;
		mRestart = false;

		restartText.text = "";
		gameOverText.text = "";
		wolvesKilledText.text = "0";

	}

	// Update is called once per frame
	void Update()
	{
		tempBored += Time.deltaTime;
		Irritation = mIritAnimator.GetFloat("Irritation");
		Boredom = mBoredAnimator.GetFloat("Boredom");
		Sadness = mSadAnimator.GetInteger("Sadness");

		mBoredAnimator.SetFloat("Boredom", tempBored);

		Debug.Log(Boredom);
		wolvesKilledText.text = "" + mWolvesKilled;
		if (Input.GetButton("Fire1") && Time.time > mNextPunch)
		{
			mNextPunch = Time.time + mPunchRate;
			Instantiate(mFist, mFistSpawn.position, mFistSpawn.rotation);
		}
		//Instantiate (mFist, mFistSpawn.position, mFistSpawn.rotation);

		if (Irritation >= MAXIRRITATION)
		{
			//end game
			GameOver();
		}
		if(Boredom >= MAXBOREDOM)
		{
			//end game
			GameOver();
		}
		if(Sadness >= MAXSADNESS)
		{
			//end game
			GameOver();
		}
		if(mRestart)
		{
			if(Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void FixedUpdate()
	{
		//------------Transform Example-------------------------

		/*transform.position += mVelocity;
		transform.Rotate(mRotation);
		transform.localScale = new Vector3(transform.localScale.x * mScaling.x,
										   transform.localScale.y * mScaling.y,
										   transform.localScale.z * mScaling.z);
		 */

		if(Input.GetAxis("Horizontal") < 0)
		{
			mFacingRight = false;
			var direction = -1f;
			transform.localScale = new Vector3(direction, 1, 1);
		}
			//Add option for idle animation here (Else If Horizontal > 0)
			//then else
		else
		{ 
			mFacingRight = true;
			var direction = 1;
			transform.localScale = new Vector3(direction, 1, 1);
		}
		//--------------Rigidbody2D Example----------------------
        //rigidbody2D.AddForce(Vector2.right * Input.GetAxis("Horizontal") * mMaxSpeed);
		// rigidbody2D.AddForceAtPosition(Vector2.right, new Vector2(transform.position.x, 
		//                                 transform.position.y + 1.0f));

		if (onGround_ && Input.GetAxis("Jump") > 0)
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * mJumpForce);
			mAnimator.SetBool("IsJumping", true);
			onGround_ = false;
		}
		// rigidbody2D.AddTorque(mRotSpeed * Input.GetAxis("Horizontal"));
	}

	bool onGround_ = true;
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			onGround_ = true;
			mAnimator.SetBool("IsJumping", false);
		}
		
		if (collision.collider.tag == "Wolf")
		{
			++mWolvesKilled;

		//	gameObject.particleSystem.enableEmission = true;
			Instantiate(BloodExplosion, transform.position, transform.rotation);
			tempMad += 0.5f;
			mIritAnimator.SetFloat("Irritation", tempMad);
		}
		if (collision.collider.tag == "Bunny")
		{
			Instantiate(BloodExplosion, transform.position, transform.rotation);
			tempSad += 1;
			mSadAnimator.SetInteger("Sadness", tempSad);
		}
	}

	void OnCollisionStay2D(Collider2D other)
	{
		Debug.Log("OnTriggerStay");
	}

	void OnCollisionExit2D(Collider2D other)
	{
		Debug.Log("OnTriggerExit");
	}

	void OnCollisionEnter2D(Collider2D other)
	{
		Debug.Log("OnTriggerEnter");
	}

	public void GameOver()
	{
		gameOverText.text = "Game Over";
		mGameOver = true;
		restartText.text = "Press 'R' to restart.";
		mRestart = true;
	}

	public void WolfKilled()
	{
		++mWolvesKilled;
	}

}


