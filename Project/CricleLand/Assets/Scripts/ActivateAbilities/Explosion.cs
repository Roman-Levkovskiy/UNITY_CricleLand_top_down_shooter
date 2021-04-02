using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ActivateAbility
{
    private void Awake()
    {
        neededPoints = 10;
    }
    public override IEnumerator activate()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        GameObject plasmaBullet = player.GetComponent<PlayerController>().weaponTypes.transform.Find("Plasma").gameObject;
        for (int i = 0; i < player.GetComponent<PlayerController>().attackAbilityLevel * 2; ++i)
        {
            Vector2 pos = player.transform.position;

            Vector2 spawnVector = new Vector2(pos.x + 2 * Mathf.Cos(Mathf.Deg2Rad * 30*i), pos.y + 2 * Mathf.Sin(Mathf.Deg2Rad * 30 * i));

            GameObject newPlazmaBullet = Instantiate(plasmaBullet, spawnVector, new Quaternion());

            newPlazmaBullet.SetActive(true);

            spawnVector = (new Vector2(pos.x + 5 * Mathf.Cos(Mathf.Deg2Rad * 30 * i), pos.y + 5 * Mathf.Sin(Mathf.Deg2Rad * 30 * i)) - pos).normalized * 30;

            spawnVector = new Vector2(spawnVector.x + (player.GetComponent<PlayerController>().movement.x * 20), spawnVector.y + (player.GetComponent<PlayerController>().movement.y * 20));

            newPlazmaBullet.GetComponent<Rigidbody2D>().velocity = spawnVector;

            yield return new WaitForSeconds(0.1f);
        }
    }
}