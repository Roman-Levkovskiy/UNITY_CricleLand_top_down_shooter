using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPrefab : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(1, 1, 0.1f);
    }
    void FixedUpdate()
    {
        if (transform.parent == null)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().hp -= 500;
        }
    }


}
