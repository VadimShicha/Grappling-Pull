//A simple script that allows you to create teleportation portals
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : MonoBehaviour
{
    [Tooltip("The location you want to teleport to")]
    public Vector3 teleportPos;

	[Tooltip("The target that you want to teleport")]
	public GameObject target;


	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == Main.playerInstacne.GetComponent<Collider2D>())
		{
			teleport();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject == Main.playerInstacne)
		{
			teleport();
		}
	}

	void teleport()
	{
		target.transform.position = teleportPos;
	}
}
