using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    private bool mGameOver;
    private bool mRestart;

    public GUIText restartText;
    public GUIText gameOverText;
    public GUIText wolvesKilledText;

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        mGameOver = true;
        restartText.text = "Press 'R' to restart.";
        mRestart = true;
    }

    public void WolfKilled()
    {
        //++mWolvesKilled;
    }

	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
