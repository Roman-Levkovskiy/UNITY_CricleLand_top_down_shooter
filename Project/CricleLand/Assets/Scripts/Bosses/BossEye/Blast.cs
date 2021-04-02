using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    bool isBlasting;
    private void FixedUpdate()
    {
        if (!isBlasting)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.05f, transform.localScale.y - 0.05f, transform.localScale.z);
            if(transform.localScale.x<=0.2f)
            {
                isBlasting = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.15f, transform.localScale.y + 0.15f, transform.localScale.z);
            if(transform.localScale.x>6f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isBlasting)
            {
                StartCoroutine(collision.GetComponent<PlayerController>().takeDamage(40));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (isBlasting)
            {
                StartCoroutine(collision.GetComponent<PlayerController>().takeDamage(25));
            }
        }
    }
}
