using UnityEngine;
using System.Collections;

public class MayorController : MonoBehaviour 
{
    public GameObject MayorObject;
    public Transform m_MayorTansform;
    public Transform m_FistSpawn;
    public GameObject m_Fist;
	// Use this for initialization
	void Start () 
    {
        var direction = -1f;
        transform.localScale = new Vector3(direction, 1, 1);

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        var mStats = MayorObject.GetComponent<MayorStats>();

        if (Input.GetButton("Fire1") && Time.time > mStats.m_TimeToNextPunch)
        {
            mStats.m_TimeToNextPunch = Time.time + mStats.m_PunchRate;
            Instantiate(m_Fist, m_FistSpawn.position, m_FistSpawn.rotation);
        }
	}
}
