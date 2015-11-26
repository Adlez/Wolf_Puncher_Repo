using UnityEngine;
using System.Collections;

public class Fist : MonoBehaviour 
{
    public GameObject m_Mayor;
    public GameObject m_GameController;
	public WolfBehaviour mWolfBehaviour;
	//public Movement mMovement;
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
        var mayorStats = m_Mayor.GetComponent<MayorStats>();
		if (collision.collider.tag == "Wolf")
		{
            m_GameController.GetComponent<GameController>().WolfKilled();

			mBoredAnimator.SetFloat("Boredom", 0.0f);

            mayorStats.m_WolvesKilled += 1;
            mayorStats.m_tempBored = 0.0f;

			mWolfBehaviour.GetComponent<WolfBehaviour>().KillWolf(0);
			Instantiate(BloodExplosion, transform.position, transform.rotation);
			//Destroy(collider.gameObject);
		}
	}
}
