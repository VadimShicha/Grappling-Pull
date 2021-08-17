using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject player;
	public GameObject grapplingHook;

	public GameObject scope;

	public LayerMask grapplingMask;



	public float moveSpeed = 4;
    public float grappleSpeed = 0.3f;

	public float grappleLength = 5;


	void Update()
	{
		float xAxisInput = Input.GetAxisRaw("Horizontal");

		if(xAxisInput != 0)
		{
			Vector3 playerVel = player.GetComponent<Rigidbody2D>().velocity;

			player.GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * xAxisInput, playerVel.y, playerVel.z);
		}

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			grapple();
		}

		
	}

	void grapple()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, worldPos, grappleLength, grapplingMask);

		if(hit)
		{
			print(hit.transform.gameObject.name);
	

			print(hit.transform.position);
			player.GetComponent<Rigidbody2D>().velocity = hit.transform.position * grappleSpeed;
		}

		

		scope.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
	}
}
