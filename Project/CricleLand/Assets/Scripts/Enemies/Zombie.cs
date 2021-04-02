using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zombie : Enemy
{
    public Rigidbody2D rb;
    public Quaternion quaternion;
    public bool isResting;
    public bool isRegeneratig;
    public bool bite;
    GameObject lookDir;
    public CapsuleCollider2D head;
    public float rushDistance;
    public Animator animationController;
    public float speedMultipler;

    //MOVEMENT
    public GameObject point;
    public Seeker seeker;


    //HIT SOUND
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip hitSound3;
    public AudioClip hitSound4;
    public AudioClip hitSound5;
    public AudioClip hitSound6;
    void Start()
    {
        //point that doesnt move. Can be used to rotate zombie
        lookDir = transform.Find("LookDir").gameObject;

        //used to calculate mobs on current wave
        ++GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().allMobsCount;

        speedMultipler = Random.Range(1f, 2f);

        xp = 50;

        materialsToDrop = new List<string> {"Cricle","Fire"};

        //distance to begin rush
        rushDistance = 8;
        //collider that "bites" on a hit
        head = GetComponent<CapsuleCollider2D>();


        state = "attack";
        rb = GetComponent<Rigidbody2D>();
        hp = 200;
        isResting = false;
        isRegeneratig = false;
        damage = 25;
        bite = false;
        point = transform.Find("Point").gameObject;
    }
    public override void doUpdateStuff()
    {
        //zombiemoves to a random point, that generates on a distance from zombie between him and player
        point = transform.Find("Point").gameObject;
    }

    void FixedUpdate()
    {
        //used to not blink
        Vector2 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, -1);

        //I can do this one time, but I need to manipulate scripts execution time. I can do this, but I dont want to take my time on this at that project
        target = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;


        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        
        stateSwitcher();
        rotate();
    }

    public override void doDestroyStuff()
    {
        transform.Find("DeathParticles").gameObject.SetActive(true);
        Destroy(transform.Find("DeathParticles").gameObject, 2);
        transform.Find("DeathParticles").transform.parent = null;
        shootSound();
    }
    //simple finite state machine
    public void stateSwitcher()
    {
        switch (state)
        {
            case "rush":

                if (hp < 100)
                {
                    state = "regeneration";
                    regeneration();
                    break;
                }
                else
                {
                    if (Vector2.Distance(transform.position, target.transform.position) >= rushDistance)
                    {
                        state = "attack";
                        attack();
                        break;
                    }
                    else
                    {
                        rush();
                    }
                }
                break;

            case "attack":
                    if (hp < 100)
                    {
                        state = "regeneration";
                        regeneration();
                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, target.transform.position) < rushDistance)
                        {
                            rush();
                            break;
                        }
                    }
                    attack();
                break;

            case "regeneration":
                    if (!isRegeneratig)
                    {
                        if (Vector2.Distance(transform.position, target.transform.position) < rushDistance)
                        {
                            state = "rush";
                            rush();
                            break;
                        }
                        else
                        {
                            state = "attack";
                            attack();
                            break;
                        }
                    }
                    regeneration();
                break;  
        }

    }
    public void rush()
    {
        if (!bite)
        {
            if (Vector2.Distance(transform.position, target.transform.position) >= 8)
            {
                calculateDirPos();
                GetComponent<AIPath>().maxSpeed = 21 * speedMultipler;
            }
            else
            {
                GetComponent<AIPath>().enabled = false;
                rb.velocity = (target.transform.position - transform.position).normalized * 10 * speedMultipler;
            }
        }
    }
    public void attack()
    {
        if (!bite)
        {
            calculateDirPos();

            GetComponent<AIPath>().maxSpeed = 10 * speedMultipler;
        }
    }
    private void calculateDirPos()

    {
        GetComponent<AIDestinationSetter>().enabled = true;
        GetComponent<AIPath>().enabled = true;
        try
        {
            if (Vector2.Distance(transform.position, target.transform.position) < 8)
            {
                GetComponent<AIDestinationSetter>().target = target.transform;
            }
            else
            {
                GetComponent<AIDestinationSetter>().target = point.transform;
            }
        }
        catch (UnassignedReferenceException) { }
    }
    //if hp low, zombie stnds and regenerates
    public void regeneration()
    {
        GetComponent<AIPath>().enabled = true;
        GetComponent<AIPath>().maxSpeed = 0;
        if (!isRegeneratig)
        {
            isRegeneratig = true;
            IEnumerator coroutine = regenerate();
            StartCoroutine(coroutine);
        }

    }
    //after bite
    IEnumerator rest()
    {
        yield return new WaitForSeconds(3f);
        bite = false;
    }
    IEnumerator regenerate()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2.5f);
        while (hp < 150)
        {
            hp += 5;
            yield return new WaitForSeconds(0.1f);
        }
        isRegeneratig = false;
        rb.constraints = RigidbodyConstraints2D.None;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<PlayerController>().isTakingDamage)
            {
                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().takeDamage(20));
                animationController.SetBool("isBiting", true);
                bite = true;
                StartCoroutine(rest());
            }
        }
    }

    public void rotate()
    {
        var relativePos = target.transform.position - lookDir.transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    //particles on damge taking
    public override void takeDamageEffect()
    {
        GameObject blood = Instantiate(transform.Find("BloodParticles").gameObject, transform.Find("BloodParticles").transform.position, transform.Find("BloodParticles").transform.rotation) as GameObject;
        blood.gameObject.SetActive(true);
        blood.GetComponent<ParticleSystem>().Play();
        blood.transform.parent = null;
        Destroy(blood, 2);
        shootSound();
    }

    private void shootSound()
    {
        GameObject obj = Instantiate(new GameObject());
        obj.AddComponent<AudioSource>();

        obj.transform.parent = null;

        switch (Random.Range(1, 7))
        {
            case 1:
                obj.GetComponent<AudioSource>().clip = hitSound1;
                break;
            case 2:
                obj.GetComponent<AudioSource>().clip = hitSound2;
                break;
            case 3:
                obj.GetComponent<AudioSource>().clip = hitSound3;
                break;
            case 4:
                obj.GetComponent<AudioSource>().clip = hitSound4;
                break;
            case 5:
                obj.GetComponent<AudioSource>().clip = hitSound5;
                break;
            case 6:
                obj.GetComponent<AudioSource>().clip = hitSound6;
                break;
        }

        obj.GetComponent<AudioSource>().Play();
        Destroy(obj, 3f);
    }

}
