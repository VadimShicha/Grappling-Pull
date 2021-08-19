using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet
{
    public GameObject bullet;
    public bool hit;

    public Bullet(GameObject _bullet)
	{
        bullet = _bullet;
        hit = false;
	}
}

public class Cannon : MonoBehaviour
{
    public GameObject bullet;

    public Collider2D target;

    [Tooltip("The delay of this cannon")]
    public float delay = 0;

    public float damage = 1;

    [Tooltip("The delay between every shot")]
    public float fireRate = 3;
    [Tooltip("The speed of the shot")]
    public float fireSpeed = 0.3f;

    public bool destroyBullets = true;
    public bool roundBulletPositions = true;
    public bool removeTarget = true;
    public string targetName = "Target";

    [HideInInspector]
    public int targetMode = 0;
    //0 - both
    //1 - horizontal only
    //2 - verticle only

    List<Bullet> bullets = new List<Bullet>();
    bool firing = false;

	void Start()
	{
        //remove the target when the game starts
        if(removeTarget == true)
		{
    		gameObject.transform.Find(targetName).gameObject.SetActive(false);
		}
	}

	void FixedUpdate()
	{
        StartCoroutine("Tick");
    }

    IEnumerator Tick()
	{
        yield return new WaitForSeconds(delay);

        try
        {
            int bulletsLength = bullets.Count;

            for(int i = 0; i < bulletsLength; i++)
            {
                //check if the a bullet is colliding with the player
                if(bullets[i].bullet.GetComponent<Collider2D>().IsTouching(Main.playerInstacne.GetComponent<Collider2D>()))
				{
                    if(bullets[i].hit == false)
					{
                        Main.health -= damage;
                        bullets[i].hit = true;
					}
				}


                if(roundBulletPositions == true)
                {
                    if(Vector3Int.RoundToInt(bullets[i].bullet.transform.position) == Vector3Int.RoundToInt(target.transform.position))
                    {
                        Destroy(bullets[i].bullet);
                        bullets.RemoveAt(i);
                        continue;
                    }
                }
                else if(roundBulletPositions == false)
                {
                    if (bullets[i].bullet.transform.position == target.transform.position)
                    {
                        Destroy(bullets[i].bullet);
                        bullets.RemoveAt(i);
                        continue;
                    }
                }


                try
                {
                    Vector3 moveVel = Vector3.zero;

                    moveVel = Vector3.MoveTowards(bullets[i].bullet.transform.position, target.transform.position, fireSpeed) - bullets[i].bullet.transform.position;

                    bullets[i].bullet.GetComponent<Rigidbody2D>().velocity = moveVel;
                }
                catch(System.NullReferenceException)
                {
                    Debug.LogError("You need to have a Rigidbody2D on your bullet");
                }
            }


            if(firing == false)
            {
                StartCoroutine("FireShot");
                firing = true;
            }

        }
        catch(System.ArgumentOutOfRangeException)
        {
            //handle exception
        }
	}

	IEnumerator FireShot()
	{
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.name = bullet.name + "Clone";
        bulletClone.transform.position = gameObject.transform.position;

        Bullet _bullet = new Bullet(bulletClone);

        bullets.Add(_bullet);

        yield return new WaitForSeconds(fireRate);
        firing = false;
    }
}
