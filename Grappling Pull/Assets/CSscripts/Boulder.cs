//This script adds starting force to a boulder
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public Vector3 startingForce = Vector3.one;

	void Start()
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = startingForce;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == Main.playerInstacne.GetComponent<Collider2D>())
		{
			Main.playerInstacne.GetComponent<Main>().died();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject == Main.playerInstacne)
		{
			Main.playerInstacne.GetComponent<Main>().died();
		}
	}
}
