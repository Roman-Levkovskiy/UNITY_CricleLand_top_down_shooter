using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmIntelligance : MonoBehaviour
{
    //OLD CODE
    //NOT IN USE
    //WATCH SWARM AI TEST PROJECT INSTEAD

    public float x;
    public float y;
    public GameObject zombie;
    public GameObject moveDir;
    public Rigidbody2D rb;
    public List<GameObject> allZombies;
    public float degree;
    public float angle;
    public int max_i, min_i, max_j, min_j;
    public int zombieCount;
    public float speedMultipler;
    public GameObject currentSwarm;
    GameObject player;
    public bool isStartCalled = false;
    public static float minDistance = float.MaxValue;
    public float distance;

    void Start()
    {
        if (!isStartCalled)
        {
            isStartCalled = true;

            player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;

            speedMultipler = Random.Range(1f, 2f);
            zombieCount = 0;
            angle = 0;

            moveDir = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
            allZombies = new List<GameObject>();
            rb = GetComponent<Rigidbody2D>();

            for (int i = 0; i < 3; ++i)
            {
                bool canSpawn = false;
                Vector2 currentZombie = new Vector3();
                while (!canSpawn)
                {
                    canSpawn = true;
                    currentZombie = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f), -2);
                    for (int j = 0; j < allZombies.Count; ++j)
                    {
                        if (Vector2.Distance(currentZombie, allZombies[j].transform.position) < 1.8f)
                        {
                            canSpawn = false;
                        }
                    }
                }
                allZombies.Add(Instantiate(zombie, currentZombie, new Quaternion(), transform));
                allZombies[i].name += (i + 1);
                allZombies[i].transform.parent = gameObject.transform;
                ++zombieCount;
            }

            if (gameObject.name != "SwarmIntelligance")
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().allMobs.Add(gameObject);
            }
        }
    }
    public static GameObject closestSwarm;
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        //calculateMoveDir();
        
        rb.velocity = (player.transform.position - transform.position).normalized * 3f * speedMultipler;

        distance = Vector2.Distance(player.transform.position, transform.position);

        if (minDistance >= Vector2.Distance(player.transform.position, transform.position)|| gameObject == closestSwarm)
        {
            minDistance = Vector2.Distance(player.transform.position, transform.position);
            closestSwarm = gameObject;
        }
        else
        {
            if (Vector2.Distance(player.transform.position, transform.position) < 10)
            {
                float L = 5f;
                float x1 = player.transform.position.x;
                float y1 = player.transform.position.y;
                float x2 = transform.position.x;
                float y2 = transform.position.y;
                float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                float x = x1 + (x2 - x1) * L / LL;
                float y = y1 + (y2 - y1) * L / LL;

                rb.velocity = (new Vector2(x, y)).normalized * 3f * speedMultipler;
            }
        }
        for (int i = allZombies.Count-1; i>=0; --i)
        {
            if(allZombies[i] == null)
            {
                allZombies.RemoveAt(i);
            }
        }
        if(allZombies.Count == 0)
        {
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
