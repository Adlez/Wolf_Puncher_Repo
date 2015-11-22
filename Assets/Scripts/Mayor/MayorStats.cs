using UnityEngine;
using System.Collections;

public class MayorStats : MonoBehaviour 
{
    public float m_PunchRate;
    public float m_Irritation;
    public float m_Boredom;
    public int m_Sadness;
    public int m_WolvesKilled;

    public float m_TimeToNextPunch;
    public float m_tempMad;
    public float m_tempBored;
    public int m_tempSad;

    //Constants to be relocated elsewhere
    public const int MAXIRRITATION = 10;
    public const int MAXSADNESS = 5;
    public const float MAXBOREDOM = 60.0f;
    
}
