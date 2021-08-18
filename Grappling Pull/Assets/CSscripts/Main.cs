using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
	[Header("References")]
	public GameObject player;
    public static GameObject playerInstacne;

	public GameObject scope;

	public Sprite emptyHeart;
	public Sprite fullHeart;

	public Sprite emptyBattery;
	public Sprite fullBattery;

	public Slider hitSlider;
	public Image hitBattery;

	public Transform groundChecker;

	public LayerMask groundMask;

	public Image[] hearts;

	[Header("Options")]
	public int maxHealth = 3;
	public static float health = 3;

	public float moveSpeed = 6;
	public float groundRadius = 0.28f;

	[Header("Grappler Options")]
	public float grappleCooldown = 0.8f;
    public float grappleSpeed = 5;
	public float grappleMaxPowerX = 12;
	public float grappleMaxPowerY = 30;

	public float grapplePowerX = 5;
	public float grapplePowerY = 9;

	bool grappling = false;
	bool grounded = false;
	bool dead = false;

	void Start()
	{
		if(VarManager.respawned == true)
		{
			player.transform.position = VarManager.checkpointPos;
			VarManager.respawned = false;
		}

		playerInstacne = gameObject;
	}

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

		//update health
		if(dead == false)
		{
			for(int i = 0; i < hearts.Length; i++)
			{
				if(health >= i + 1)
				{
					hearts[i].sprite = fullHeart;
				}
				else
				{
					hearts[i].sprite = emptyHeart;
				}
			}
		}

		if(health <= 0 && dead == false)
		{
			died();
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			VarManager.checkpointPos = new Vector3(4, 60, 0);
			print(VarManager.checkpointPos);
		}
	}

	IEnumerator Grapple()
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = Camera.main.nearClipPlane;
		Vector3 moveVel = Vector3.MoveTowards(player.transform.position, Camera.main.ScreenToWorldPoint(mousePos), grappleSpeed) - player.transform.position;
		moveVel.x *= grapplePowerX;
		moveVel.y *= grapplePowerY;

		//make sure moveVel isn't grader than grappleMaxPower
		if(moveVel.x > grappleMaxPowerX)
			moveVel.x = grappleMaxPowerX;
		if(moveVel.y > grappleMaxPowerY)
			moveVel.y = grappleMaxPowerY;

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

	void died()
	{
		print("You died!");

		health = maxHealth;
		SceneManager.LoadScene(SceneManager.GetSceneByName("SampleScene").name);

		VarManager.respawned = true;
	}

	void setCheckpoint(Vector3 pos)
	{
		VarManager.checkpointPos = pos;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Spikes"))
		{
			died();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(groundChecker.transform.position, groundRadius);
	}
}
