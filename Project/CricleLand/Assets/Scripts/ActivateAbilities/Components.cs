using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : ActivateAbility
{
    private void Awake()
    {
        neededPoints = 4;
    }

    public override IEnumerator activate()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        for (int i = 0; i < player.GetComponent<PlayerController>().craftAbilityLevel; ++i)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    ++player.GetComponent<PlayerController>().materials["Cricle"];
                    break;
                case 1:
                    ++player.GetComponent<PlayerController>().materials["Fire"];
                    break;
                case 2:
                    ++player.GetComponent<PlayerController>().materials["Triangle"];
                    break;
            }
            yield return null;
        }
    }
}
