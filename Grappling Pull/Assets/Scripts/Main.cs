using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject player;

	public GameObject scope;

	public Sprite emptyBattery;
	public Sprite fullBattery;

	public Slider hitSlider;
	public Image hitBattery;

	public Transform groundChecker;

	public LayerMask groundMask;

	public float moveSpeed = 4;
	public float groundRadius = 0.3f;

	public float grappleCooldown = 2;
    public float grappleSpeed = 0.3f;
	public float grapplePowerX = 5;
	public float grapplePowerY = 5;

	bool grappling = false;
	bool grounded = false;

	void Update()
	{
		float xAxisInput = Input.GetAxisRaw("Horizontal");

		grounded = Physics2D.OverlapCircle(groundChecker.transform.position, groundRadius, groundMask);

		if(xAxisInput != 0)
		{
			Vector3 playerVel = player.GetComponent<Rigidbody2D>().velocity;

			player.GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * xAxisInput, playerVel.y, playerVel.z);

			if(xAxisInput > 0)
			{
				player.GetComponent<SpriteRenderer>().flipX = false;
			}
			else
			{
				player.GetComponent<SpriteRenderer>().flipX = true;
			}
		}

		if(Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(grappling == false && grounded == true)
			{
				StartCoroutine("Grapple");
				grappling = true;
			}
		}

		//scope GameObject
		{
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = Camera.main.nearClipPlane;

			scope.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
		}

		if(hitSlider.value >= hitSlider.maxValue)
		{
			hitBattery.sprite = fullBattery;
		}
		else
		{
			hitSlider.value += (hitSlider.maxValue / (grappleCooldown / Time.deltaTime));
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(groundChecker.transform.position, groundRadius);
	}


	IEnumerator Grapple()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		Vector3 moveVel = (Vector3.MoveTowards(player.transform.position, Camera.main.ScreenToWorldPoint(mousePos), grappleSpeed) - player.transform.position);
		moveVel.x *= grapplePowerX;
		moveVel.y *= grapplePowerY;

		player.GetComponent<Rigidbody2D>().velocity = moveVel;

		if(moveVel.x > 0)
		{
			player.GetComponent<SpriteRenderer>().flipX = false;
		}
		else
		{
			player.GetComponent<SpriteRenderer>().flipX = true;
		}

		hitSlider.value = hitSlider.minValue;
		hitBattery.sprite = emptyBattery;


		yield return new WaitForSeconds(grappleCooldown);
		grappling = false;
	}
}
