using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public GameObject player;
    public GameObject pickupController;
    public string effect;
    public bool isDestroying;
    public void Start()
    {
        pickupController = GameObject.FindGameObjectWithTag("PickupController");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        if (collision.gameObject == player)
        {
            if (!isDestroying)
            {
                StartCoroutine(pickupController.GetComponent<PickupController>().executePickup("Pickup", effect, transform.position));
                Destroy(gameObject, 0.01f);
            }
        }
    }
    private void OnDestroy()
    {
        --GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().picupsCount;
        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().allPickups.Remove(gameObject);
    }
}
