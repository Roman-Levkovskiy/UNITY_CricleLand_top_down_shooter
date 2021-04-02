using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static PlayerAfterImagePool Instace{ get; private set; }

    private void Awake()
    {
        Instace = this;
        growPool();
    }

    private void growPool()
    {
        for (int i = 0; i<10; ++i)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            addToPool(instanceToAdd);
        }
    }

    public void addToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject getFromPool()
    {
        if(availableObjects.Count == 0)
        {
            growPool();
        }

        var instane = availableObjects.Dequeue();
        instane.SetActive(true);
        return instane;
    }
}
