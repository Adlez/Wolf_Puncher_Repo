using UnityEngine;
using System.Collections;

public class SpriteAnim : MonoBehaviour
{

	public float mFrameRate;
	public Material mMaterial;
	public int mNumFrames;
	public int mFramesWide;
	public int mFramesHigh;


	private int currFrame_;
	private float frameWidth_;
	private float frameHeight_;

	// Use this for initialization
	void Start()
	{
		currFrame_ = 0;
		Texture texture = mMaterial.mainTexture;

		frameWidth_ = (float)texture.width / (float)mFramesWide;
		frameWidth_ /= texture.width;
		frameHeight_ = (float)texture.height / (float)mFramesHigh;
		frameHeight_ /= texture.height;

		StartCoroutine(Draw());
	}

	// Update is called once per frame
	void Update()
	{

	}

	int xPos = 0;
	public int mScrollSpeed;
	IEnumerator Draw()
	{
		while (true)
		{
			currFrame_++;

			if (currFrame_ >= mNumFrames)
			{
				currFrame_ = 0;
			}

			Debug.Log("Draw: " + currFrame_);

			/*---------------Scrolling code----------
			 * xPos += mScrollSpeed;
			 float offsetX = (float)xPos / mMaterial.mainTexture.width;
			 mMaterial.mainTextureOffset = new Vector2(offsetX, 0.0f);*/

			int col = currFrame_ % mFramesWide;
			int row = currFrame_ / mFramesHigh;

			float offsetX = col * frameWidth_;
			float offsetY = row * frameHeight_;

			mMaterial.mainTextureOffset = new Vector2(offsetX, offsetY);
			mMaterial.SetTextureOffset("_BumpMap", new Vector2(offsetX, offsetY));

			yield return new WaitForSeconds(mFrameRate);
		}
	}
}
