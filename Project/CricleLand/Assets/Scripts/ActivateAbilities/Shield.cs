using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ActivateAbility
{
    private void Awake()
    {
        neededPoints = 15;
    }
    public override IEnumerator activate()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        player.GetComponent<PlayerController>().shieldPoints = 2 + player.GetComponent<PlayerController>().healAbilityLevel;
        yield return null;
    }
}
