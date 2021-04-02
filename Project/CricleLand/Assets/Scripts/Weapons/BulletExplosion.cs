using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    private bool released = false;
    private List<GameObject> wasHit;
    private float damageMultipler;
    void Start()
    {
        wasHit = new List<GameObject>();
        damageMultipler = GameObject.Find("GameController").GetComponent<GameController>().player.GetComponent<PlayerController>().damagePerkMultipler;
    }
    void FixedUpdate()
    {
        if (transform.parent == null)
        {
            if(!released)
            {
                released = true;
                transform.localScale = new Vector3(1, 1, 0.1f);
            }
            Destroy(gameObject, 0.15f);
            transform.localScale *= 1.15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"&&!wasHit.Contains(collision.gameObject))
        {
            wasHit.Add(collision.gameObject);
            collision.gameObject.GetComponent<Enemy>().takeDamage(150 * damageMultipler);
        }
    }

}
