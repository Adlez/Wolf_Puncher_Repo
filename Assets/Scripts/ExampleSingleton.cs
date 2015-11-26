using UnityEngine;
using System.Collections;

public class ExampleSingleton : MonoBehaviour 
{
	private static ExampleSingleton instance = null;

	public static ExampleSingleton Instance
	{
		get
		{
			return instance;
		}
	}

	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject); //covers my ass in case of human error. This really shouldn't ever be reached
		//make sure this gameobject is not cleaned up after this scene
	}

	
}
