using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : Weapon
{
    private Rigidbody2D rb;
    private Vector3 shootDirection;
    public Plasma(Vector2 shootDirection)
    {
        this.shootDirection = shootDirection;
    }
    public override IEnumerator shoot()
    {
        damage = 0;
        rb = GetComponent<Rigidbody2D>();

        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float L = 1f;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = shootDirection.x;
        float y2 = shootDirection.y;
        float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        float x = x1 + (x2 - x1) * L / LL;
        float y = y1 + (y2 - y1) * L / LL;

        rb.AddForce((new Vector3(x, y, 0) - transform.position) * 1000 / 10000);
        yield return new WaitForSeconds(0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Background" && collision.gameObject.tag != "BulletExplosion")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                try
                {
                    collision.transform.gameObject.GetComponent<Enemy>().takeDamageEffect();
                    collision.gameObject.GetComponent<Enemy>().hp -= damage;
                    if (transform.Find("BulletExplosion").transform.parent != null)
                    {
                        transform.Find("BulletExplosion").transform.parent = null;
                    }
                    Destroy(gameObject);
                }
                catch (NullReferenceException)
                {

                }
                finally
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                IEnumerator coroutine = delete();
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator delete()
    {
        yield return new WaitForSeconds(0.7f);
        try
        {
            transform.Find("BulletExplosion").transform.parent = null;
        }
        catch(NullReferenceException e)
        {
            Debug.Log(e);
        }
        Destroy(gameObject);
    }
}
