using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int currentAmmo;
    public int maxAmmo;
    public float damage;
    public abstract IEnumerator shoot();
}
