using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    GameObject player;
    Vector2 pos;
    private void Start()
    {
        pos = transform.parent.transform.position;
        transform.position = transform.parent.transform.position;
        StartCoroutine(changePos());
    }

    public void Update()
    {
        transform.position = pos;
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        if (transform.parent.name != "Zombie")
        {
            if 
            (
                Vector2.Distance(transform.position, transform.parent.position) < 0.5f && 
                Vector2.Distance(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.transform.position, transform.parent.transform.position) > 4
            )
            {
                findNewPos();
            }
        }
    }

    IEnumerator changePos()
    {
        while (true)
        {
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.transform.position, transform.parent.transform.position) > 4)
            {
                findNewPos();
            }
            yield return new WaitForSeconds(0.75f);
        }
    }
    public void findNewPos()
    {
        if (transform.parent.name != "Zombie")
        {
            float L = 4.1f;
            float x1 = transform.position.x;
            float y1 = transform.position.y;
            float x2 = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.transform.position.x;
            float y2 = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.transform.position.y;
            float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            float x = x1 + (x2 - x1) * L / LL;
            float y = y1 + (y2 - y1) * L / LL;
            pos = new Vector2(x + Random.Range(-1.5f, 1.5f), y + Random.Range(-1.5f, 1.5f));
        }
        else
        {
            transform.position = new Vector2(-100, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == transform.parent.gameObject)
        {
            findNewPos();
        }
    }
}
