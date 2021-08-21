using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public int checkpointNumber = 0;
	public ParticleSystem checkpointParticle;
	public float particleLength = 2;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == Main.playerInstacne.GetComponent<Collider2D>())
		{
			setCheckpoint();
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject == Main.playerInstacne)
		{
			setCheckpoint();
		}
	}

	void setCheckpoint()
	{
		if(checkpointNumber > VarManager.checkpointNumber)
		{
			VarManager.checkpointPos = gameObject.transform.position;
			VarManager.checkpointNumber = checkpointNumber;

			Main.saveGame();
			Main.health = Main.playerInstacne.GetComponent<Main>().maxHealth;

			checkpointParticle.Play();
			StartCoroutine("Particle");
		}
	}

	IEnumerator Particle()
	{
		yield return new WaitForSeconds(particleLength);
		checkpointParticle.Stop();
	}
}
