using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivateAbility : MonoBehaviour
{
    public int neededPoints;
    protected GameObject player;
    public abstract IEnumerator activate();
}
