using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float hp = 100;
    public float damage;
    public float xp;
    public GameObject target;
    public string state;
    public bool isFreeze;
    public float timer;
    public bool isCounting;
    public Vector2 freezeStartPos;

    public List<string> materialsToDrop;
    public GameObject allMaterials;
    void Update()
    {
        if(hp<=0){
            Destroy(gameObject);
        }
        if (isFreeze)
        {
            transform.position = freezeStartPos;
        }
        if (timer > 0 && isCounting)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            isCounting = false;
            isFreeze = false;
        }
        target = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        doUpdateStuff();
    }
    //I want child objects to have their own updates on top of parents
    public abstract void doUpdateStuff();
    //called on freeze grenade
    public void freeze()
    {
        freezeStartPos = transform.position;
        isFreeze = true;
        isCounting = true;
        timer = 4;
    }
    private void OnDestroy()
    {
            --GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameController>().allMobsCount;
            target.GetComponent<PlayerController>().killedAnEnemy(xp);
            for (int i = 0; i < materialsToDrop.Count; ++i)
            {
                if (UnityEngine.Random.Range(0, 10) < 1)
                {
                    var loot = Instantiate(allMaterials.transform.Find(materialsToDrop[i]).gameObject, transform.position, new Quaternion());
                    loot.transform.localScale *= 6;
                }
            }
            doDestroyStuff();
    }

    public abstract void doDestroyStuff();

    public abstract void takeDamageEffect();
    //TODO: make hp loss not direct but by through the takeDamage method
    public void takeDamage(float damage)
    {
        hp -= damage;
    }
}
