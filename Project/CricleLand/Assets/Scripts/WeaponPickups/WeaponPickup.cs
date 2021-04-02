using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponPickup : MonoBehaviour
{
    public bool isPrefab;
    public GameObject player;
    public GameObject pickupController;
    public string weapon;
    public bool isDestroying;
    public void Start()
    {
        pickupController = GameObject.FindGameObjectWithTag("PickupController");
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        if (player.GetComponent<PlayerController>().currentWeapon.Equals(weapon))
        {
            if (!isDestroying)
            {
                StartCoroutine(destroying(0));
            }
        }
    }
    public IEnumerator destroying(float time)
    {
        isDestroying = true;
        yield return new WaitForSeconds(time);
        --GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().weaponPicupsCount;
        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().allPickups.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isDestroying)
            {
                StartCoroutine(pickupController.GetComponent<PickupController>().executePickup("Weapon", weapon, transform.position));
                StartCoroutine(destroying(0.01f));
            }
        }
    }
}
