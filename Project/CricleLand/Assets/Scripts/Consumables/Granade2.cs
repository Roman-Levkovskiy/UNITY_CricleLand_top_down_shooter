using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade2 : MonoBehaviour
{
    Rigidbody2D rb;
    bool isExploding = false;
    public Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(throwing(direction));
    }

    IEnumerator throwing(Vector2 throwPos)
    {
        Vector3 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float L = 1f;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = shootDirection.x;
        float y2 = shootDirection.y;
        float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        float x = x1 + (x2 - x1) * L / LL;
        float y = y1 + (y2 - y1) * L / LL;

        rb.AddForce((new Vector3(x, y, 0) - transform.position) * 1000);
        StartCoroutine(explode());
        while (true)
        {
            rb.velocity /= 1.4f;
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator explode()
    {
        if (transform.parent == null)
        {
            yield return new WaitForSeconds(3f);
            isExploding = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
            transform.localScale = new Vector3(1, 1, 0.1f);
            yield return new WaitForSeconds(0.75f);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isExploding && transform.parent == null)
        {
            collision.gameObject.GetComponent<Enemy>().freeze();
        }
    }

    void FixedUpdate()
    {
        if (isExploding && transform.parent == null)
        {
            transform.localScale *= 1.08f;
        }
    }
}
