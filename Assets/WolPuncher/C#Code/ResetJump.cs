using UnityEngine;
using System.Collections;

public class ResetJump : MonoBehaviour
{

	GameObject parent;

	// Use this for initialization
	void Start()
	{
		parent = transform.root.gameObject;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground")
		{
			parent.GetComponent<Movement>().OnGround = true;
		}
	}
}
