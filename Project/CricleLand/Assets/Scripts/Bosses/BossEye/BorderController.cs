using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    public LineRenderer line1;
    public LineRenderer line2;
    bool isShootingLaser;
    public GameObject spawnPoint1;
    public GameObject firePoint1;

    public IEnumerator shootLaser()
    {
        isShootingLaser = true;
        line1.gameObject.SetActive(true);
        line2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        isShootingLaser = false;
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
    }

    private void Start()
    {
        line1.gameObject.SetActive(false);
        line2.gameObject.SetActive(false);
    }
}
