using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
	[HideInInspector]
	public int zoomMode;
	//0 - Set zoom
	//1 - Add zoom
	//2 - Reset zoom

	public float setZoom = 0;
	public float zoomAmount = 1;

	[Tooltip("Disable the SpriteRenderer on this GameObject in PlayMode")]
	public bool disableSprite = true;

	bool ran = false;
	float defaultZoom;

	void Start()
	{
		defaultZoom = Camera.main.orthographicSize;

		if(disableSprite == true)
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == Main.playerInstacne.GetComponent<Collider2D>())
		{
			zoom();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject == Main.playerInstacne)
		{
			zoom();
		}
	}

	void zoom()
	{
		if(ran == false)
		{
			if(zoomMode == 0)
			{
				Camera.main.orthographicSize = setZoom;
			}
			else if(zoomMode == 1)
			{
				Camera.main.orthographicSize += zoomAmount;
			}
			else if(zoomMode == 2)
			{
				Camera.main.orthographicSize = defaultZoom;
			}

			ran = true;
		}
	}
}
