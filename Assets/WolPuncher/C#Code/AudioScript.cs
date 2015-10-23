using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour
{
	void Start()
	{
		GetComponent<AudioSource>().Play();
	}
	// Update is called once per frame
	void Update () 
	{

	}
}
