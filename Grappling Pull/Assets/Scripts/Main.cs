using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject player;

	public GameObject scope;

	public float moveSpeed = 4;

	public float grappleCooldown = 2;
    public float grappleSpeed = 0.3f;
	public float grapplePower = 5;

	bool grappling = false;

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
			if(grappling == false)
			{
				StartCoroutine("Grapple");
				grappling = true;
			}
		}
	}

	IEnumerator Grapple()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		Vector3 moveVel = (Vector3.MoveTowards(player.transform.position, Camera.main.ScreenToWorldPoint(mousePos), grappleSpeed) - player.transform.position) * grapplePower;

		player.GetComponent<Rigidbody2D>().velocity = moveVel;
		scope.transform.position = Camera.main.ScreenToWorldPoint(mousePos);

		yield return new WaitForSeconds(grappleCooldown);
		grappling = false;
	}
}
