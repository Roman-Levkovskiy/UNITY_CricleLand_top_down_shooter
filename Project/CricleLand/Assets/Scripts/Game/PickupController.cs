using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public List<GameObject> allPickups;

    public GameObject player;
    
    public GameObject blastPrefab;

    public bool isSpeedUp = false;

    public float dashSpeed;
    public float dashTime = 0.05f;
    public float startDashTime = 0.05f;
    public bool isDashing;
    public bool isDashAllowed;

    public int picupsCount;
    public bool pickupCreation;
    public int weaponPicupsCount;
    public bool weaponPickupCreation;
    public GameObject WeaponPickups;
    public GameObject pickups;

    void Start()
    {
        allPickups = new List<GameObject>();
    }
    //called when player hits pickups collider
    public IEnumerator executePickup(string type, string value, Vector2 pos)
    {
        if (type == "Weapon")
        {
            player.GetComponent<PlayerController>().animator.SetBool("isWearing" + player.GetComponent<PlayerController>().currentWeapon, false);
            switch (value)
            {
                case "Pistol":
                    player.GetComponent<PlayerController>().currentWeapon = value;
                    player.GetComponent<PlayerController>().maxAmmo = 8;
                    break;
                case "Plasma":
                    player.GetComponent<PlayerController>().currentWeapon = value;
                    player.GetComponent<PlayerController>().maxAmmo = 4;
                    break;
                case "Rifle":
                    player.GetComponent<PlayerController>().currentWeapon = value;
                    player.GetComponent<PlayerController>().maxAmmo = 20;
                    break;
            }
            player.GetComponent<PlayerController>().animator.SetBool("isWearing" + value, true);
            player.GetComponent<PlayerController>().currentAmmo = player.GetComponent<PlayerController>().maxAmmo;
        }
        if (type == "Pickup")
        {
            switch (value)
            {
                case "Speed":
                    if (!isSpeedUp)
                    {
                        isSpeedUp = true;
                        StartCoroutine(speed());
                        isSpeedUp = false;
                    }
                    break;

                case "Dash":
                    isDashAllowed = true;
                    yield return new WaitForSeconds(10f);
                    isDashAllowed = false;
                    break;

                case "Blast":
                    GameObject blast = Instantiate(blastPrefab, pos, new Quaternion());
                    Destroy(blast, 1.5f);
                    break;
            }
            gameObject.transform.position = new Vector3(1,1,1);
        }
    }

    IEnumerator speed()
    {
        player.GetComponent<PlayerController>().speed += 3;
        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerController>().speed -= 3;
    }

    public IEnumerator destroying(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }


    private void FixedUpdate()
    {
        pickupCheck();
        weaponPickupCheck();
    }

    //creating weapon changing pickup
    void weaponPickupCheck()
    {
        if (weaponPicupsCount < 10 && !weaponPickupCreation)
        {
            StartCoroutine(createWeaponPickup());
        }
    }
    
    IEnumerator createWeaponPickup()
    {
        weaponPickupCreation = true;
        yield return new WaitForSeconds(0.5f);
        int randomPickup = Random.Range(0, 3);
        switch (randomPickup)
        {
            case 0:
                allPickups.Add(Instantiate(WeaponPickups.gameObject.transform.Find("Pistol").gameObject, new Vector2(Random.Range(-45f, 45f), Random.Range(-25f, 25f)), new Quaternion()));
                break;

            case 1:
                allPickups.Add(Instantiate(WeaponPickups.gameObject.transform.Find("Plasma").gameObject, new Vector2(Random.Range(-45f, 45f), Random.Range(-25f, 25f)), new Quaternion()));
                break;

            case 2:
                allPickups.Add(Instantiate(WeaponPickups.gameObject.transform.Find("Rifle").gameObject, new Vector2(Random.Range(-45f, 45f), Random.Range(-25f, 25f)), new Quaternion()));
                break;
        }
        ++weaponPicupsCount;
        weaponPickupCreation = false;
    }
    //creating bonus pickup
    void pickupCheck()
    {
        if (picupsCount < 10 && !pickupCreation)
        {
            StartCoroutine(createPickup());
        }
    }

    IEnumerator createPickup()
    {
        pickupCreation = true;
        yield return new WaitForSeconds(0.5f);
        int randomPickup = Random.Range(0, 2);
        switch (randomPickup)
        {
            case 0:
                allPickups.Add(Instantiate(pickups.gameObject.transform.Find("Speed").gameObject, new Vector2(Random.Range(-45f, 45f), Random.Range(-25f, 25f)), new Quaternion()));
                break;

            case 1:
                allPickups.Add(Instantiate(pickups.gameObject.transform.Find("Blast").gameObject, new Vector2(Random.Range(-45f, 45f), Random.Range(-25f, 25f)), new Quaternion()));
                break;
        }
        ++picupsCount;
        pickupCreation = false;
    }
}
