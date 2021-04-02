using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    public GameObject consumables;
    public List<string> consumablesNames;
    private void Start()
    {
        consumablesNames = new List<string> { "Grenade", "Medkit", "Grenade2" };
        currentConsumable = "Grenade";
    }
    public string currentConsumable;
    public void nextConsumable()
    {
        int current = consumablesNames.IndexOf(currentConsumable);
        currentConsumable =  current + 1 == consumablesNames.Count ? consumablesNames[0] : consumablesNames[current + 1];
    }

    public void prevConsumable()
    {
        int current = consumablesNames.IndexOf(currentConsumable);
        currentConsumable = current == 0 ? consumablesNames[consumablesNames.Count-1] : consumablesNames[current - 1];
    }

    Vector2 throwDirection;

    public void useConsumable(Vector2 throwDirection)
    {
        this.throwDirection = throwDirection;
        Invoke("use" + currentConsumable, 0);
    }

    public void useGrenade()
    {
        if (GetComponent<PlayerController>().grenadeCount > 0)
        {
            GameObject grenade = Instantiate(consumables.transform.Find("Grenade").gameObject, transform.Find("FirePoint").transform.position, new Quaternion());
            --(GetComponent<PlayerController>().grenadeCount);
            grenade.GetComponent<Granade2>().direction = throwDirection;
        }
    }

    public void useGrenade2()
    {
        if (GetComponent<PlayerController>().grenade2Count > 0)
        {
            GameObject grenade = Instantiate(consumables.transform.Find("Grenade2").gameObject, transform.Find("FirePoint").transform.position, new Quaternion());
            --GetComponent<PlayerController>().grenade2Count;
            grenade.GetComponent<Granade2>().direction = throwDirection;
        }
    }

    public void useMedkit()
    {
        if (GetComponent<PlayerController>().medkitCount> 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player.GetComponent<PlayerController>().hp += 25;
            --GetComponent<PlayerController>().medkitCount;
        }
    }
}
