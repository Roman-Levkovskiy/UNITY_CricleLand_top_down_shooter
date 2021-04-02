using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public Vector3 shootDirection;
    float defDistanceRay = 100;
    public GameObject laserFirePoint;
    public GameObject player;
    public LineRenderer lineRenderer;
    Transform m_transform;
    Rigidbody2D rb;

    private float damageMultipler;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        damageMultipler = GameObject.Find("GameController").GetComponent<GameController>().player.GetComponent<PlayerController>().damagePerkMultipler;
    }

    public override IEnumerator shoot()
    {
        damage = 75;
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

        rb.AddForce((new Vector3(x, y, 0) - transform.position)/4f);
        yield return new WaitForSeconds(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Background" && collision.gameObject.tag != "BulletExplosion")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                    collision.gameObject.GetComponent<Enemy>().takeDamage(damage * damageMultipler);
                    collision.transform.gameObject.GetComponent<Enemy>().takeDamageEffect();
                    Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
