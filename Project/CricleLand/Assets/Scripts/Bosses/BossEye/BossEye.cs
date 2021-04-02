using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEye : Enemy
{
    GameObject spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4;
    public GameObject blast;
    private bool firstFaseIsRunning, secondFaseIsRunning;
    void Start()
    {
        ++GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().allMobsCount;

        hp = 30000;

        spawnPoint1 = transform.Find("LaserRotatingBorder").transform.Find("SpawnPoint1").gameObject;
        spawnPoint2 = transform.Find("LaserRotatingBorder").transform.Find("SpawnPoint2").gameObject;
        spawnPoint3 = transform.Find("LaserRotatingBorder").transform.Find("SpawnPoint3").gameObject;
        spawnPoint4 = transform.Find("LaserRotatingBorder").transform.Find("SpawnPoint4").gameObject;

        secondFaseIsRunning = false;
        firstFaseIsRunning = true;

        StartCoroutine(firstFase());
    }


    IEnumerator firstFase()
    {
        while(firstFaseIsRunning)
        {
            Invoke("firstFase" + Random.Range(0, 3), 0);
            yield return new WaitForSeconds(8);
            if(hp>15000)
            {
                firstFaseIsRunning = false;
                secondFaseIsRunning = true;
            }
        }

        transform.Find("Eye").GetComponent<EyeClipping>().setSecondFase();
    }

    public IEnumerator secondFase()
    {
        
        while (secondFaseIsRunning)
        {
            Invoke("firstFase" + Random.Range(0, 4), 0);
            yield return new WaitForSeconds(6);
        }
    }

    public void firstFase0()
    {
        GameController.firstLevelCoordinates.Add(spawnPoint1.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint2.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint3.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint4.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint1.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint2.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint3.transform.position);
        GameController.firstLevelCoordinates.Add(spawnPoint4.transform.position);

        for (int i = 0; i<8; ++i)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().createZombie();
        }
    }
    public void firstFase1()
    {
        StartCoroutine(transform.Find("LaserRotatingBorder").GetComponent<BorderController>().shootLaser());
    }
    public void firstFase2()
    {
        for (int i = 0; i < 2; ++i)
        {
            GameController.firstLevelCoordinates.Add(spawnPoint1.transform.position);
            GameController.firstLevelCoordinates.Add(spawnPoint2.transform.position);
        }
        for (int i = 0; i < 4; ++i)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().createMechSphere();
        }
    }
    public void firstFase3()
    {
        StartCoroutine(spawnBlast());
    }
    IEnumerator spawnBlast()
    {
        for (int i = 0; i < 12; ++i)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(blast, target.transform.position, new Quaternion());
        }
    }
    public override void takeDamageEffect(){}
    public override void doUpdateStuff()
    {
        if (hp < 15000)
        {
            secondFaseIsRunning = true;
            firstFaseIsRunning = false;
        }
    }
    public override void doDestroyStuff(){}
}
