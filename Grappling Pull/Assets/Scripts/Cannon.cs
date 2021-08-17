using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bullet;

    public GameObject target;

    public float fireRate = 3;
    public float fireSpeed = 0.3f;

    bool firing = false;

    [HideInInspector]
    public int targetMode = 0;
    //0 - both
    //1 - horizontal only
    //2 - verticle only

    List<GameObject> bullets = new List<GameObject>();

	void FixedUpdate()
	{
        int bulletsLength = bullets.Count;

        for(int i = 0; i < bulletsLength; i++)
		{
            try
			{
                Vector3 moveVel = Vector3.zero;

                //x and y
                if(targetMode == 0)
				{
                    moveVel = (Vector3.MoveTowards(bullets[i].transform.position, target.transform.position, fireSpeed) - bullets[i].transform.position);
				}
                //x only
                else if(targetMode == 1)
				{
                    Vector3 targetPos = target.transform.position;
                    targetPos.y = bullets[i].transform.position.y;

                    moveVel = (Vector3.MoveTowards(bullets[i].transform.position, targetPos, fireSpeed) - bullets[i].transform.position);
                }
                //y only
                else if(targetMode == 2)
                {
                    Vector3 targetPos = target.transform.position;
                    targetPos.x = bullets[i].transform.position.x;

                    moveVel = (Vector3.MoveTowards(bullets[i].transform.position, targetPos, fireSpeed) - bullets[i].transform.position);
                }


                bullets[i].GetComponent<Rigidbody2D>().velocity = moveVel;
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

	IEnumerator FireShot()
	{
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.name = bullet.name + "Clone";
        bulletClone.transform.position = gameObject.transform.position;
        bullets.Add(bulletClone);

        yield return new WaitForSeconds(fireRate);
        firing = false;
    }

}
