using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour 
{
    public float scrollSpeed;
	private float backgroundWidth;
 //   public float tileSizeX;

	private Transform backgroundTransform;
	

	private Vector3 newPosition;

    private Vector2 startPosition;

    void Start ()
    {
		newPosition = transform.position;
		backgroundTransform = Camera.main.transform;

		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		backgroundWidth = spriteRenderer.sprite.bounds.size.x;
    }

    void Update ()
    {
//		Debug.Log(newPosition);
        newPosition.x += Time.deltaTime * scrollSpeed;
        transform.position = newPosition;

		if ((transform.position.x + backgroundWidth) < backgroundTransform.position.x)
		{
			Vector3 newPos = transform.position;
			newPos.x += 1.98f * backgroundWidth;
			transform.position = newPos;
			newPosition = newPos;
		}

    }
}

