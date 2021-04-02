using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechSphere : Enemy
{
    public GameObject currentBorder;
    GameObject player;
    private void Start()
    {
        if (transform.parent != null)
        {
            materialsToDrop = new List<string> { "Triangle"};

            xp = 300;
            ++GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().allMobsCount;
            player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
            hp = 100;
            GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 20;
        }
    }
    public override void doDestroyStuff(){}
    public override void doUpdateStuff() {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            //I want Sphere to explode on cotact with player, stick on contact with walls and ignore enemies
                case "Player":
                player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
                StartCoroutine(explosion());
                    break;
                case "Border":
                if (collision!=currentBorder)
                {
                    currentBorder = collision.gameObject;
                    StartCoroutine(stick());
                }
                else
                {
                    Physics2D.IgnoreCollision(collision, GetComponent<CircleCollider2D>(), true);
                }
                    break;
                default:
                Physics2D.IgnoreCollision(collision, GetComponent<CircleCollider2D>(), true);
                    break;
        }
    }
    IEnumerator stick()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.3f);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * 20;
        yield return new WaitForSeconds(0.25f);
    }
    IEnumerator explosion()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(player.GetComponent<PlayerController>().takeDamage(40));
        transform.Find("ExplosionParticle").GetComponent<ParticleSystem>().gameObject.SetActive(true);
        transform.Find("ExplosionParticle").GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
    public override void takeDamageEffect()
    {

    }
}
